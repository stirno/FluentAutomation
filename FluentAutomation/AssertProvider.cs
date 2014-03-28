using System;
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
                var elements = this.commandProvider.FindMultiple(selector).Elements;
                if (elements.Count() != count)
                {
                    this.ReportError("Expected count of elements matching selector [{0}] to be [{1}] but instead it was [{2}]", selector, count, elements.Count());
                }
            });
        }

        public void Count(ElementProxy elements, int count)
        {
            this.commandProvider.Act(commandType, () =>
            {
                if (elements.Elements.Count() != count)
                {
                    this.ReportError("Expected count of elements in collection to be [{1}] but instead it was [{2}]", count, elements.Elements.Count());
                }
            });
        }
        #endregion

        #region CSS Class
        public void CssClass(string selector, string className)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = this.commandProvider.Find(selector).Element;
                var elementClassAttributeValue = unwrappedElement.Attributes.Get("class").Trim();
                if (!HasCssClass(className, elementClassAttributeValue))
                {
                    this.ReportError("Expected element [{0}] to include CSS class [{1}] but current class attribute is [{2}].", selector, className, elementClassAttributeValue);
                }
            });
        }

        public void CssClass(ElementProxy element, string className)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = element.Element;
                var elementClassAttributeValue = unwrappedElement.Attributes.Get("class").Trim();
                if (!HasCssClass(className, elementClassAttributeValue))
                {
                    this.ReportError("Expected element to include CSS class [{0}] but current class attribute is [{1}].", className, elementClassAttributeValue);
                }
            });
        }

        private bool HasCssClass(string className, string elementClassAttributeValue)
        {
            className = className.Replace(".", "").Trim();
            var hasClass = false;

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

            return hasClass;
        }
        #endregion

        #region Text
        public void Text(string selector, string text)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = this.commandProvider.Find(selector).Element;
                if (unwrappedElement.IsText)
                {
                    if (!IsTextMatch(unwrappedElement.Text, text))
                    {
                        this.ReportError("Expected TextElement [{0}] text to be [{1}] but it was actually [{2}].", selector, text, unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                        {
                            if (IsTextMatch(optionText, text))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with text of [{1}]. Selected option text values include [{2}]", selector, text, string.Join(",", unwrappedElement.SelectedOptionTextCollection));
                        }
                    }
                    else
                    {
                        if (!IsTextMatch(unwrappedElement.Text, text))
                        {
                            this.ReportError("Expected SelectElement [{0}] selected option text to be [{1}] but it was actually [{2}].", selector, text, unwrappedElement.Text);
                        }
                    }
                }
                else
                {
                    if (!IsTextMatch(unwrappedElement.Text, text))
                    {
                        this.ReportError("Expected DOM Element [{0}] text to be [{1}] but it was actually [{2}].", selector, text, unwrappedElement.Text);
                    }
                }
            });
        }

        public void Text(ElementProxy element, string text)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = element.Element;
                if (unwrappedElement.IsText)
                {
                    if (!IsTextMatch(unwrappedElement.Text, text))
                    {
                        this.ReportError("Expected TextElement text to be [{1}] but it was actually [{2}].", text, unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                        {
                            if (IsTextMatch(optionText, text))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement selected options to have at least one option with text of [{0}]. Selected option text values include [{1}]", text, string.Join(",", unwrappedElement.SelectedOptionTextCollection));
                        }
                    }
                    else
                    {
                        if (!IsTextMatch(unwrappedElement.Text, text))
                        {
                            this.ReportError("Expected SelectElement selected option text to be [{1}] but it was actually [{2}].", text, unwrappedElement.Text);
                        }
                    }
                }
                else
                {
                    if (!IsTextMatch(unwrappedElement.Text, text))
                    {
                        this.ReportError("Expected DOM Element text to be [{1}] but it was actually [{2}].", text, unwrappedElement.Text);
                    }
                }
            });
        }

        public void Text(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var unwrappedElement = this.commandProvider.Find(selector).Element;
                if (unwrappedElement.IsText)
                {
                    if (!compiledFunc(unwrappedElement.Text))
                    {
                        this.ReportError("Expected TextElement [{0}] text to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                        {
                            if (compiledFunc(optionText))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with text matching expression[{1}]. Selected option text values include [{2}]", selector, matchFunc.ToExpressionString(), string.Join(",", unwrappedElement.SelectedOptionTextCollection));
                        }
                    }
                    else
                    {
                        if (!compiledFunc(unwrappedElement.Text))
                        {
                            this.ReportError("Expected SelectElement [{0}] selected option text to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Text);
                        }
                    }
                }
                else
                {
                    if (!compiledFunc(unwrappedElement.Text))
                    {
                        this.ReportError("Expected DOM Element [{0}] text to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Text);
                    }
                }
            });
        }

        public void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var unwrappedElement = element.Element;
                if (unwrappedElement.IsText)
                {
                    if (!compiledFunc(unwrappedElement.Text))
                    {
                        this.ReportError("Expected TextElement text to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                        {
                            if (compiledFunc(optionText))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement selected options to have at least one option with text matching expression [{0}]. Selected option text values include [{1}]", matchFunc.ToExpressionString(), string.Join(",", unwrappedElement.SelectedOptionTextCollection));
                        }
                    }
                    else
                    {
                        if (!compiledFunc(unwrappedElement.Text))
                        {
                            this.ReportError("Expected SelectElement selected option text to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Text);
                        }
                    }
                }
                else
                {
                    if (!compiledFunc(unwrappedElement.Text))
                    {
                        this.ReportError("Expected DOM Element text to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Text);
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
        public void Value(string selector, string value)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = this.commandProvider.Find(selector).Element;
                if (unwrappedElement.IsText)
                {
                    if (!IsTextMatch(unwrappedElement.Value, value))
                    {
                        this.ReportError("Expected TextElement [{0}] selected option value to be [{1}] but it was actually [{2}].", selector, value, unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    var foundMatch = false;
                    foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                    {
                        if (IsTextMatch(optionValue, value))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        if (unwrappedElement.IsMultipleSelect)
                        {
                            this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with value of [{1}]. Selected option text values include [{2}]", selector, value, unwrappedElement.Value);
                        }
                        else
                        {
                            this.ReportError("Expected SelectElement [{0}] selected option value to be [{1}] but it was actually [{2}].", selector, value, unwrappedElement.Value);
                        }
                    }
                }
                else
                {
                    if (!IsTextMatch(unwrappedElement.Value, value))
                    {
                        this.ReportError("Expected element [{0}] value to be [{1}] but it was actually [{2}].", selector, value, unwrappedElement.Value);
                    }
                }
            });
        }

        public void Value(string selector, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var unwrappedElement = this.commandProvider.Find(selector).Element;
                if (unwrappedElement.IsText)
                {
                    if (!compiledFunc(unwrappedElement.Value))
                    {
                        this.ReportError("Expected TextElement [{0}] value to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Value);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                        {
                            if (compiledFunc(optionValue))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement [{0}] selected options to have at least one option with value matching expression [{1}]. Selected option values include [{2}]", selector, matchFunc.ToExpressionString(), string.Join(",", unwrappedElement.SelectedOptionValues));
                        }
                    }
                    else
                    {
                        if (!compiledFunc(unwrappedElement.Text))
                        {
                            this.ReportError("Expected SelectElement [{0}] selected option value to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Value);
                        }
                    }
                }
                else
                {
                    if (!compiledFunc(unwrappedElement.Value))
                    {
                        this.ReportError("Expected element [{0}] value to match expression [{1}] but it was actually [{2}].", selector, matchFunc.ToExpressionString(), unwrappedElement.Value);
                    }
                }
            });
        }

        public void Value(ElementProxy element, string value)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = element.Element;
                if (unwrappedElement.IsText)
                {
                    if (!IsTextMatch(unwrappedElement.Value, value))
                    {
                        this.ReportError("Expected TextElement selected option value to be [{0}] but it was actually [{1}].", value, unwrappedElement.Text);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                        {
                            if (IsTextMatch(optionValue, value))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement selected options to have at least one option with value of [{0}]. Selected option text values include [{1}]", value, string.Join(",", unwrappedElement.SelectedOptionValues));
                        }
                    }
                    else
                    {
                        if (!IsTextMatch(unwrappedElement.Value, value))
                        {
                            this.ReportError("Expected SelectElement selected option value to be [{0}] but it was actually [{1}].", value, unwrappedElement.Text);
                        }
                    }
                }
                else
                {
                    if (!IsTextMatch(unwrappedElement.Value, value))
                    {
                        this.ReportError("Expected element value to be [{0}] but it was actually [{1}].", value, unwrappedElement.Value);
                    }
                }
            });
        }

        public void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var unwrappedElement = element.Element;
                if (unwrappedElement.IsText)
                {
                    if (!compiledFunc(unwrappedElement.Value))
                    {
                        this.ReportError("Expected TextElement value to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Value);
                    }
                }
                else if (unwrappedElement.IsSelect)
                {
                    if (unwrappedElement.IsMultipleSelect)
                    {
                        var foundMatch = false;
                        foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                        {
                            if (compiledFunc(optionValue))
                            {
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            this.ReportError("Expected SelectElement selected options to have at least one option with value matching expression [{0}]. Selected option values include [{1}]", matchFunc.ToExpressionString(), string.Join(",", unwrappedElement.SelectedOptionTextCollection));
                        }
                    }
                    else
                    {
                        if (!compiledFunc(unwrappedElement.Text))
                        {
                            this.ReportError("Expected SelectElement selected option value to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Value);
                        }
                    }
                }
                else
                {
                    if (!compiledFunc(unwrappedElement.Value))
                    {
                        this.ReportError("Expected element value to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), unwrappedElement.Value);
                    }
                }
            });
        }
        #endregion

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

        public void Url(Expression<Func<Uri, bool>> urlExpression)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledExpr = urlExpression.Compile();

                if (compiledExpr(this.commandProvider.Url) != true)
                {
                    this.ReportError("Expected expression [{0}] to return true.", urlExpression.ToExpressionString());
                }
            });
        }

        #region Boolean / Throws
        public void True(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                if (!compiledFunc())
                {
                    this.ReportError("Expected expression [{0}] to return false.", matchFunc.ToExpressionString());
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

        public void Throws(Expression<Action> matchAction)
        {
            this.commandProvider.Act(commandType, () =>
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

                if (!threwException)
                {
                    this.ReportError("Expected expression [{0}] to throw an exception.", matchAction.ToExpressionString());
                }
            });
        }
        #endregion

        public void Exists(string selector)
        {
            this.commandProvider.Act(commandType, () =>
            {
                var unwrappedElement = this.commandProvider.Find(selector).Element as IElement;
                if (unwrappedElement == null)
                {
                    this.ReportError("Expected element matching selector [{0}] to exist.", selector);
                }
            });
        }

        public void AlertText(string text)
        {
            this.commandProvider.Act(CommandType.NoRetry, () =>
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
            });
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            this.commandProvider.Act(CommandType.NoRetry, () =>
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
            });
        }

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