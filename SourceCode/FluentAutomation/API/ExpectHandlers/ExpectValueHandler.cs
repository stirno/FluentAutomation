using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core.Constraints;

namespace FluentAutomation.API.ExpectHandlers
{
    public class ExpectValueHandler
    {
        private Browser _browser = null;
        private ExpectType _expectType = ExpectType.Single;

        private string _value = string.Empty;
        private IEnumerable<string> _values = null;

        private enum ExpectType
        {
            Single = 1,
            Any = 2,
            All = 3
        }

        private ExpectValueHandler(Browser browser, ExpectType expectType)
        {
            _browser = browser;
            _expectType = expectType;
        }

        public ExpectValueHandler(Browser browser, string value) : this(browser, ExpectType.Single)
        {
            _value = value;
        }

        public ExpectValueHandler(Browser browser, IEnumerable<string> values) : this(browser, ExpectType.Any)
        {
            _values = values;
        }

        public ExpectValueHandler(Browser browser, IEnumerable<string> values, bool requireAll) : this(browser, ExpectType.All)
        {
            _values = values;
        }

        public void In(string fieldSelector)
        {
            QuerySelectorConstraint constraint = Find.BySelector(fieldSelector);

            var element = _browser.Element(constraint);

            if (_value == null)
            {
                if (element != null && element.Text != null && element.GetAttributeValue("value") != null)
                {
                    Assert.Fail("Null value assertion failed. Element [{0}] has a value of [{1}].", fieldSelector, element.Text);
                }
            }

            if (_expectType == ExpectType.Any || _expectType == ExpectType.All)
            {
                if (element.TagName.ToUpper() != "SELECT")
                {
                    Assert.Fail("Value assertion of types Any and All can only be used on SelectList elements.");
                }
            }

            if (element != null)
            {
                bool handled = false;
                handled = handleSelectList(constraint, element);
                if (!handled) handled = handleTextField(constraint, element);
                if (!handled) handleElement(constraint, element);
            }
        }

        public void In(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                In(fieldSelector);
            }
        }

        private void In(Func<string, bool> fieldSelectorsFunc)
        {
            List<string> fieldSelectors = new List<string>();
            foreach (var child in _browser.Elements)
            {
                bool isMatch = false;
                if (!string.IsNullOrEmpty(child.ClassName)) fieldSelectorsFunc(child.ClassName);
                if (!isMatch && !string.IsNullOrEmpty(child.Id)) isMatch = fieldSelectorsFunc(child.Id);
                if (!isMatch && !string.IsNullOrEmpty(child.Name)) isMatch = fieldSelectorsFunc(child.Name);

                if (isMatch && !string.IsNullOrEmpty(child.IdOrName))
                {
                    fieldSelectors.Add(child.IdOrName);
                }
            }

            In(fieldSelectors.ToArray());
        }

        private bool handleTextField(QuerySelectorConstraint constraint, Element element)
        {
            if (!(
                string.Equals(element.TagName, "INPUT", StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(element.GetAttributeValue("type"), "TEXT", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(element.GetAttributeValue("type"), "PASSWORD", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(element.GetAttributeValue("type"), "HIDDEN", StringComparison.InvariantCultureIgnoreCase)
                ) || !string.Equals(element.TagName, "TEXTAREA", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (!string.Equals(element.GetAttributeValue("value"), _value, StringComparison.InvariantCultureIgnoreCase))
            {
                Assert.Fail("TextField value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", constraint.Selector, _value, element.GetAttributeValue("value"));
            }

            return true;
        }

        private bool handleSelectList(QuerySelectorConstraint constraint, Element element)
        {
            if (!string.Equals(element.TagName, "SELECT", StringComparison.InvariantCultureIgnoreCase)) return false;

            var selectElement = _browser.ElementOfType<SelectList>(constraint);
            if (selectElement == null) return false;

            if (_expectType == ExpectType.Single)
            {
                if (selectElement.Multiple)
                {
                    Assert.Fail("Single value assertion cannot be used on a SelectList that potentially has multiple values. Use Any or All instead.");
                }

                if (selectElement.SelectedOption != null &&
                    !string.Equals(selectElement.SelectedOption.Value, _value, StringComparison.InvariantCultureIgnoreCase) &&
                    !string.Equals(selectElement.SelectedOption.Text, _value, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail("SelectList value assertion failed. Expected element [{0}] to have a selected value of [{1}] but actual selected value is [{2}].", constraint.Selector, _value, selectElement.SelectedOption.Value);
                }
            }
            else
            {
                int valuesMatching = 0;
                if (selectElement.HasSelectedItems)
                {
                    foreach (Option option in selectElement.SelectedOptions)
                    {
                        foreach (var value in _values)
                        {
                            if (string.Equals(option.Value, value, StringComparison.InvariantCultureIgnoreCase)) valuesMatching++;
                        }
                    }
                }

                if (_expectType == ExpectType.Any)
                {
                    if (valuesMatching == 0)
                    {
                        Assert.Fail("SelectList value assertion failed. Expected element [{0}] to have at least one value matching the collection.", constraint.Selector);
                    }
                }
                else if (_expectType == ExpectType.All)
                {
                    if (valuesMatching < _values.Count())
                    {
                        Assert.Fail("SelectList value assertion failed. Expected element [{0}] to include all values in collection.", constraint.Selector);
                    }
                }
            }

            return true;
        }

        private void handleElement(QuerySelectorConstraint constraint, Element element)
        {
            if (!string.Equals(element.Text, _value, StringComparison.InvariantCultureIgnoreCase) &&
                !string.Equals(element.GetAttributeValue("value"), _value, StringComparison.InvariantCultureIgnoreCase))
            {
                Assert.Fail("Value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", constraint.Selector, _value, element.Text ?? element.GetAttributeValue("value"));
            }
        }
    }
}
