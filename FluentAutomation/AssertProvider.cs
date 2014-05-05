﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class AssertProvider : IAssertProvider
    {
        private readonly ICommandProvider commandProvider = null;
        private CommandType commandType { get { return this.ThrowExceptions ? CommandType.Assert : CommandType.Expect; } }

        public AssertProvider(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        #region Count
        public void Count(string selector, int count)
        {
            this.commandProvider.Act(commandType, () =>
            {
                try
                {
                    var elements = this.commandProvider.FindMultiple(selector).Elements;
                    if (elements.Count() != count)
                    {
                        this.ReportError("Expected count of elements matching selector [{0}] to be [{1}] but instead it was [{2}]", selector, count, elements.Count());
                    }
                }
                catch (FluentException ex) {
                    if (!ex.Message.StartsWith("Unable to"))
                        throw ex;
                    if (count > 0)
                    {
                        this.ReportError("Expected count of elements matching selector [{0}] to be [{1}] but instead it was [{2}]", selector, count, 0);
                    }
                }
            });
        }

        public void NotCount(string selector, int count)
        {
            this.commandProvider.Act(commandType, () =>
            {
                try
                {
                    var elements = this.commandProvider.FindMultiple(selector).Elements;
                    if (elements.Count() == count)
                    {
                        this.ReportError("Expected count of elements matching selector [{0}] not to be [{1}] but it was.", selector, count);
                    }
                }
                catch (FluentException ex)
                {
                    if (!ex.Message.StartsWith("Unable to"))
                        throw ex;
                    if (count == 0)
                    {
                        this.ReportError("Expected count of elements matching selector [{0}] not to be [{1}] but it was.", selector, count);
                    }
                }
            });
        }

        private int CountElementsInProxy(ElementProxy elements)
        {
            int count = 0;

            foreach (var element in elements.Elements)
            {
                try
                {
                    element.Item2();
                    count++;
                }
                catch (Exception) { }
            }

            return count;
        }

        public void Count(ElementProxy elements, int count)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (this.CountElementsInProxy(elements) != count)
                {
                    this.ReportError("Expected count of elements in collection to be [{0}] but instead it was [{1}].", count, elements.Elements.Count());
                }
            });
        }

        public void NotCount(ElementProxy elements, int count)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (this.CountElementsInProxy(elements) == count)
                {
                    this.ReportError("Expected count of elements in collection not to be [{0}] but it was.", count);
                }
            });
        }
        #endregion

        #region CSS Class
        private class ElementHasClassResult
        {
            public bool HasClass { get; set; }

            public string ActualClass { get; set; }
        }

        private ElementHasClassResult elementHasClass(ElementProxy element, string className)
        {
            var hasClass = false;
            var unwrappedElement = element.Element;
            var elementClassAttributeValue = unwrappedElement.Attributes.Get("class").Trim();

            className = className.Replace(".", "").Trim();

            if (elementClassAttributeValue.Contains(' '))
            {
                elementClassAttributeValue.Split(' ').ToList().ForEach(cssClass =>
                {
                    cssClass = cssClass.Trim();
                    if (!string.IsNullOrEmpty(cssClass) && className.Equals(cssClass))
                    {
                        hasClass = true;
                        return;
                    }
                });
            }
            else
            {
                if (className.Equals(elementClassAttributeValue))
                {
                    hasClass = true;
                }
            }

            return new ElementHasClassResult
            {
                HasClass = hasClass,
                ActualClass = elementClassAttributeValue
            };
        }

        public void NotCssClass(string selector, string className)
        {
            this.NotCssClass(this.commandProvider.Find(selector), className);
        }

        public void NotCssClass(ElementProxy element, string className)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasClass(element, className);
                if (result.HasClass)
                {
                    this.ReportError("Expected element [{0}] not to include CSS class [{1}] but current class attribute is [{2}].", element.Element.Selector, className, result.ActualClass);
                }
            });
        }

        public void CssClass(string selector, string className)
        {
            this.CssClass(this.commandProvider.Find(selector), className);
        }

        public void CssClass(ElementProxy element, string className)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasClass(element, className);
                if (!result.HasClass)
                {
                    this.ReportError("Expected element [{0}] to include CSS class [{1}] but current class attribute is [{2}].", element.Element.Selector, className, result.ActualClass);
                }
            });
        }
        #endregion

        #region CSS Property Value

        private class ElementHasCssPropertyResult
        {
            public bool HasProperty { get; set; }

            public bool PropertyMatches { get; set; }

            public string PropertyValue { get; set; }
        }

        private ElementHasCssPropertyResult elementHasCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            var result = new ElementHasCssPropertyResult();

            this.commandProvider.CssPropertyValue(element, propertyName, (hasProperty, actualPropertyValue) =>
            {
                if (!hasProperty) return;

                result.HasProperty = true;
                result.PropertyValue = actualPropertyValue;

                if (propertyValue != null && IsTextMatch(actualPropertyValue, propertyValue))
                {
                    result.PropertyMatches = true;
                }
            });

            return result;
        }

        public void CssProperty(string selector, string propertyName, string propertyValue)
        {
            this.CssProperty(this.commandProvider.Find(selector), propertyName, propertyValue);
        }

        public void NotCssProperty(string selector, string propertyName, string propertyValue)
        {
            this.NotCssProperty(this.commandProvider.Find(selector), propertyName, propertyValue);
        }

        public void CssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = this.elementHasCssProperty(element, propertyName, propertyValue);
                if (!result.HasProperty)
                {
                    this.ReportError("Expected element [{0}] to have CSS property [{1}] but it did not.", element.Element.Selector, propertyName);
                }
                else if (propertyValue != null && !result.PropertyMatches)
                {
                    this.ReportError("Expected element [{0}]'s CSS property [{1}] to have a value of [{2}] but it was actually [{3}].", element.Element.Selector, propertyName, propertyValue, result.PropertyValue);
                }
            });
        }

        public void NotCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = this.elementHasCssProperty(element, propertyName, propertyValue);
                if (propertyValue == null && result.HasProperty)
                {
                    this.ReportError("Expected element [{0}] not to have CSS property [{1}] but it did.", element.Element.Selector, propertyName);
                }
                else if (result.PropertyMatches)
                {
                    this.ReportError("Expected element [{0}]'s CSS property [{1}] not to have a value of [{2}] but it did.", element.Element.Selector, propertyName, propertyValue);
                }
            });
        }

        #endregion

        #region Attributes

        public void Attribute(string selector, string attributeName, string attributeValue)
        {
            this.Attribute(this.commandProvider.Find(selector), attributeName, attributeValue);
        }

        public void NotAttribute(string selector, string attributeName, string attributeValue)
        {
            this.NotAttribute(this.commandProvider.Find(selector), attributeName, attributeValue);
        }

        public void Attribute(ElementProxy element, string attributeName, string attributeValue)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = element.Element.Attributes.Get(attributeName);
                if (result == null)
                {
                    this.ReportError("Expected element [{0}] to have attribute [{1}] but it did not.", element.Element.Selector, attributeName);
                }
                else if (attributeValue != null && !IsTextMatch(result, attributeValue))
                {
                    this.ReportError("Expected element [{0}]'s attribute [{1}] to have a value of [{2}] but it was actually [{3}].", element.Element.Selector, attributeName, attributeValue, result);
                }
            });
        }

        public void NotAttribute(ElementProxy element, string attributeName, string attributeValue)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = element.Element.Attributes.Get(attributeName);
                if (attributeValue == null && result != null)
                {
                    this.ReportError("Expected element [{0}] not to have attribute [{1}] but it did.", element.Element.Selector, attributeName);
                }
                else if (result != null && IsTextMatch(result, attributeValue))
                {
                    this.ReportError("Expected element [{0}]'s attribute [{1}] not to have a value of [{2}] but it did.", element.Element.Selector, attributeName, attributeValue);
                }
            });
        }

        #endregion

        #region Text
        private class ElementHasTextResult
        {
            public bool HasText { get; set; }

            public string ActualText { get; set; }

            public string ElementType { get; set; }

            public string Selector { get; set; }
        }

        private ElementHasTextResult elementHasText(ElementProxy element, Func<string, bool> textMatcher)
        {
            var hasText = false;
            var unwrappedElement = element.Element;
            var actualText = unwrappedElement.Text;
            if (unwrappedElement.IsSelect)
            {
                foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                {
                    if (textMatcher(optionText))
                    {
                        hasText = true;
                        break;
                    }
                }

                actualText = string.Join(", ", unwrappedElement.SelectedOptionTextCollection.Select(x => x).ToArray());
            }
            else
            {
                if (textMatcher(unwrappedElement.Text))
                {
                    hasText = true;
                }
            }

            var elementType = "DOM Element";
            if (unwrappedElement.IsText)
                elementType = "TextElement";
            else if (unwrappedElement.IsMultipleSelect)
                elementType = "MultipleSelectElement";
            else if (unwrappedElement.IsSelect)
                elementType = "SelectElement";

            return new ElementHasTextResult
            {
                HasText = hasText,
                ActualText = actualText,
                ElementType = elementType,
                Selector = element.Element.Selector
            };
        }

        public void Text(string selector, string text)
        {
            this.Text(this.commandProvider.Find(selector), text);
        }

        public void NotText(string selector, string text)
        {
            this.NotText(this.commandProvider.Find(selector), text);
        }

        public void Text(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.Text(this.commandProvider.Find(selector), matchFunc);
        }

        public void NotText(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.NotText(this.commandProvider.Find(selector), matchFunc);
        }

        public void Text(ElementProxy element, string text)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasText(element, (elText) => IsTextMatch(elText, text));
                if (!result.HasText)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with text of [{1}]. Selected option text values include [{2}]", result.Selector, text, string.Join(",", element.Element.SelectedOptionTextCollection));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option text to match expression [{1}] but it was actually [{2}].", result.Selector, text, result.ActualText);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] text to be [{2}] but it was actually [{3}].", result.ElementType, result.Selector, text, result.ActualText);
                    }
                }
            });
        }

        public void NotText(ElementProxy element, string text)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasText(element, (elText) => IsTextMatch(elText, text));
                if (result.HasText)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have no options with text of [{1}]. Selected option text values include [{2}]", result.Selector, text, string.Join(",", element.Element.SelectedOptionTextCollection));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option text not to be [{1}] but it was.", result.Selector, text);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] text not to be [{2}] but it was.", result.ElementType, result.Selector, text);
                    }
                }
            });
        }

        public void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = elementHasText(element, (elText) => compiledFunc(elText));
                if (!result.HasText)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with text matching expression [{1}]. Selected option text values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionTextCollection));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option text to match expression [{1}] but it was actually [{2}].", result.Selector, matchFunc.ToExpressionString(), result.ActualText);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] text to match expression [{2}] but it was actually [{3}].", result.ElementType, result.Selector, matchFunc.ToExpressionString(), result.ActualText);
                    }
                }
            });
        }

        public void NotText(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = elementHasText(element, (elText) => compiledFunc(elText));
                if (result.HasText)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have no options with text matching expression [{1}]. Selected option text values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionTextCollection));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option text not to match expression [{1}] but it did.", result.Selector, matchFunc.ToExpressionString());
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] text not to match expression [{2}] but it did.", result.ElementType, result.Selector, matchFunc.ToExpressionString());
                    }
                }
            });
        }

        private bool IsTextMatch(string elementText, string text)
        {
            return string.Equals(elementText, text, StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region Value
        private class ElementHasValueResult
        {
            public bool HasValue { get; set; }

            public string ElementType { get; set; }

            public string ActualValue { get; set; }

            public string Selector { get; set; }
        }

        private ElementHasValueResult elementHasValue(ElementProxy element, Func<string, bool> valueMatcher)
        {
            var hasValue = false;
            var unwrappedElement = element.Element;
            if (unwrappedElement.IsMultipleSelect)
            {
                foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                {
                    if (valueMatcher(optionValue))
                    {
                        hasValue = true;
                        break;
                    }
                }
            }
            else
            {
                if (valueMatcher(unwrappedElement.Value))
                {
                    hasValue = true;
                }
            }

            var elementType = "DOM Element";
            if (unwrappedElement.IsText)
                elementType = "TextElement";
            else if (unwrappedElement.IsMultipleSelect)
                elementType = "MultipleSelectElement";
            else if (unwrappedElement.IsSelect)
                elementType = "SelectElement";

            return new ElementHasValueResult
            {
                HasValue = hasValue,
                ElementType = elementType,
                ActualValue = unwrappedElement.Value,
                Selector = element.Element.Selector
            };
        }

        public void Value(string selector, string text)
        {
            this.Value(this.commandProvider.Find(selector), text);
        }

        public void NotValue(string selector, string text)
        {
            this.NotValue(this.commandProvider.Find(selector), text);
        }

        public void Value(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.Value(this.commandProvider.Find(selector), matchFunc);
        }

        public void NotValue(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.NotValue(this.commandProvider.Find(selector), matchFunc);
        }

        public void Value(ElementProxy element, string value)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasValue(element, (elValue) => IsTextMatch(elValue, value));
                if (!result.HasValue)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with value of [{1}]. Selected option values include [{2}]", result.Selector, value, string.Join(",", element.Element.SelectedOptionValues));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option value to match expression [{1}] but it was actually [{2}].", result.Selector, value, result.ActualValue);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] value to be [{2}] but it was actually [{3}].", result.ElementType, result.Selector, value, result.ActualValue);
                    }
                }
            });
        }

        public void NotValue(ElementProxy element, string value)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var result = elementHasValue(element, (elValue) => IsTextMatch(elValue, value));
                if (result.HasValue)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have no options with value of [{1}]. Selected option values include [{2}]", result.Selector, value, string.Join(",", element.Element.SelectedOptionValues));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option value not to be [{1}] but it was.", result.Selector, value);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] value not to be [{2}] but it was.", result.ElementType, result.Selector, value);
                    }
                }
            });
        }

        public void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = elementHasValue(element, (elValue) => compiledFunc(elValue));
                if (!result.HasValue)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with value matching expression [{1}]. Selected option values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionValues));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option value to match expression [{1}] but it was actually [{2}].", result.Selector, matchFunc.ToExpressionString(), result.ActualValue);
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] value to match expression [{2}] but it was actually [{3}].", result.ElementType, result.Selector, matchFunc.ToExpressionString(), result.ActualValue);
                    }
                }
            });
        }

        public void NotValue(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = elementHasValue(element, (elValue) => compiledFunc(elValue));
                if (result.HasValue)
                {
                    if (element.Element.IsMultipleSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected options to have no options with value matching expression [{1}]. Selected option values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionValues));
                    }
                    else if (element.Element.IsSelect)
                    {
                        this.ReportError("Expected SelectElement [{0}] selected option value not to match expression [{1}] but it did.", result.Selector, matchFunc.ToExpressionString());
                    }
                    else
                    {
                        this.ReportError("Expected {0} [{1}] value not to match expression [{2}] but it did.", result.ElementType, result.Selector, matchFunc.ToExpressionString());
                    }
                }
            });
        }
        #endregion

        #region URL
        public void Url(Uri expectedUrl)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (!expectedUrl.ToString().Equals(this.commandProvider.Url.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    this.ReportError("Expected URL to match [{0}] but it was actually [{1}].", expectedUrl.ToString(), this.commandProvider.Url.ToString());
                }
            });
        }

        public void NotUrl(Uri expectedUrl)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (expectedUrl.ToString().Equals(this.commandProvider.Url.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    this.ReportError("Expected URL not to match [{0}] but it was actually [{1}].", expectedUrl.ToString(), this.commandProvider.Url.ToString());
                }
            });
        }

        public void Url(Expression<Func<Uri, bool>> urlExpression)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (urlExpression.Compile()(this.commandProvider.Url) != true)
                {
                    this.ReportError("Expected expression [{0}] to return true. URL was [{1}].", urlExpression.ToExpressionString(), this.commandProvider.Url.ToString());
                }
            });
        }

        public void NotUrl(Expression<Func<Uri, bool>> urlExpression)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (urlExpression.Compile()(this.commandProvider.Url) == true)
                {
                    this.ReportError("Expected expression [{0}] to return false. URL was [{1}].", urlExpression.ToExpressionString(), this.commandProvider.Url.ToString());
                }
            });
        }
        #endregion

        #region Boolean / Throws
        public void True(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                if (!compiledFunc())
                {
                    this.ReportError("Expected expression [{0}] to return true.", matchFunc.ToExpressionString());
                }
            });
        }

        public void False(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                if (compiledFunc())
                {
                    this.ReportError("Expected expression [{0}] to return false.", matchFunc.ToExpressionString());
                }
            });
        }

        private bool throwsException(Expression<Action> matchAction)
        {
            bool threwException = false;
            var compiledAction = matchAction.Compile();

            try
            {
                compiledAction();
            }
            catch (FluentAssertFailedException)
            {
                threwException = true;
            }
            catch (FluentException)
            {
                threwException = true;
            }

            return threwException;
        }

        public void Throws(Expression<Action> matchAction)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (!throwsException(matchAction))
                {
                    this.ReportError("Expected expression [{0}] to throw an exception.", matchAction.ToExpressionString());
                }
            });
        }

        public void NotThrows(Expression<Action> matchAction)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (throwsException(matchAction))
                {
                    this.ReportError("Expected expression [{0}] not to throw an exception.", matchAction.ToExpressionString());
                }
            });
        }
        #endregion

        #region Exists
        private bool elementExists(string selector)
        {
            return this.elementExists(this.commandProvider.Find(selector));
        }

        private bool elementExists(ElementProxy element)
        {
            bool exists = false;
            try
            {
                exists = element.Element != null;
            }
            catch (FluentException) { }

            return exists;
        }

        public void Exists(string selector)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (!elementExists(selector))
                {
                    this.ReportError("Expected element matching selector [{0}] to exist.", selector);
                }
            });
        }

        public void Exists(ElementProxy element)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (!elementExists(element))
                {
                    this.ReportError("Expected element provided to exist.");
                }
            });
        }

        public void NotExists(string selector)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (elementExists(selector))
                {
                    this.ReportError("Expected element matching selector [{0}] not to exist.", selector);
                }
            });
        }
        
        public void NotExists(ElementProxy element)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (elementExists(element))
                {
                    this.ReportError("Expected element provided not to exist.");
                }
            });
        }
        #endregion

        #region Visible
        public void Visible(string selector)
        {
            this.Visible(this.commandProvider.Find(selector));
        }

        public void NotVisible(string selector)
        {
            this.NotVisible(this.commandProvider.Find(selector));
        }

        public void Visible(ElementProxy element)
        {
            this.commandProvider.Act(commandType, () =>
            {
                this.commandProvider.Visible(element, (isVisible) =>
                {
                    if (!isVisible)
                        this.ReportError("Expected element [{0}] to be visible.", element.Element.Selector);
                });
            });
        }

        public void NotVisible(ElementProxy element)
        {
            this.commandProvider.Act(commandType, () =>
            {
                this.commandProvider.Visible(element, (isVisible) =>
                {
                    if (isVisible)
                        this.ReportError("Expected element [{0}] not to be visible.", element.Element.Selector);
                });
            });
        }
        #endregion

        #region Alerts
        public void AlertText(string text)
        {
            this.commandProvider.AlertText((alertText) =>
            {
                if (!IsTextMatch(alertText, text))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    this.commandProvider.AlertClick(Alert.Cancel);
                    this.ReportError("Expected alert text to be [{0}] but it was actually [{1}].", text, alertText);
                }
            });
        }

        public void AlertNotText(string text)
        {
            this.commandProvider.AlertText((alertText) =>
            {
                if (IsTextMatch(alertText, text))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    this.commandProvider.AlertClick(Alert.Cancel);
                    this.ReportError("Expected alert text not to be [{0}] but it was.", text);
                }
            });
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            var compiledFunc = matchFunc.Compile();
            this.commandProvider.AlertText((alertText) =>
            {
                if (!compiledFunc(alertText))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    this.commandProvider.AlertClick(Alert.Cancel);
                    this.ReportError("Expected alert text to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), alertText);
                }
            });
        }
            
        public void AlertNotText(Expression<Func<string, bool>> matchFunc)
        {
            var compiledFunc = matchFunc.Compile();
            this.commandProvider.AlertText((alertText) =>
            {
                if (compiledFunc(alertText))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    this.commandProvider.AlertClick(Alert.Cancel);
                    this.ReportError("Expected alert text not to match expression [{0}] but it did.", matchFunc.ToExpressionString());
                }
            });
        }
        #endregion

        public bool ThrowExceptions { get; set; }

        public IAssertProvider EnableExceptions()
        {
            var provider = new AssertProvider(this.commandProvider);
            provider.ThrowExceptions = true;

            return provider;
        }

        public virtual void ReportError(string message, params object[] formatParams)
        {
            if (this.ThrowExceptions)
            {
                var assertException = new FluentAssertFailedException(message, formatParams);
                this.commandProvider.PendingAssertFailedExceptionNotification = new Tuple<FluentAssertFailedException, WindowState>(assertException, new WindowState
                {
                    Source = this.commandProvider.Source,
                    Url = this.commandProvider.Url
                });

                throw assertException;
            }
            else
            {
                var expectException = new FluentExpectFailedException(message, formatParams);
                this.commandProvider.PendingExpectFailedExceptionNotification = new Tuple<FluentExpectFailedException, WindowState>(expectException, new WindowState
                {
                    Source = this.commandProvider.Source,
                    Url = this.commandProvider.Url
                });
            }
        }
    }
}