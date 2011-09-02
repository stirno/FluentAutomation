using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var element = _browser.Child(Find.BySelector(fieldSelector));
            if (_expectType == ExpectType.Single)
            {
                if (_value == null)
                {
                    if (element.Text != null)
                    {
                        Assert.Fail(string.Format("Null value assertion failed. Element [{0}] has a value of [{1}].", fieldSelector, element.Text));
                    }
                }
                else
                {
                    if (element is SelectList)
                    {
                        var selectElement = (SelectList)element;
                        if (selectElement.SelectedOption != null && 
                            selectElement.SelectedOption.Value != _value && 
                            selectElement.SelectedOption.Text != _value)
                        {
                            Assert.Fail(string.Format("SelectList value assertion failed. Expected selected value of [{0}] but actual selected value is [{1}].", _value, selectElement.SelectedOption.Value));
                        }
                    }
                    else if (element is TextField)
                    {
                        var textFieldElement = (TextField)element;
                        if (textFieldElement.Value != _value && textFieldElement.Text != _value)
                        {
                            Assert.Fail(string.Format("TextField value assertion failed. Expected value of [{0}] but actual value is [{1}].", _value, element.Text));
                        }
                    }
                    else
                    {
                        if (element.Text != _value)
                        {
                            Assert.Fail(string.Format("Value assertion failed. Expected value of {0} but actual value is {1}.", _value, element.Text));
                        }
                    }
                }
            }
            else
            {
                if (!(element is SelectList))
                {
                    Assert.Fail("Value assertion of types Any and All can only be used on SelectList elements.");
                }

                int valuesMatching = 0;
                foreach (var value in _values)
                {
                    if (element.Text == _value)
                    {
                        valuesMatching++;
                    }
                }

                if (_expectType == ExpectType.Any)
                {
                    if (valuesMatching == 0)
                    {
                        Assert.Fail("SelectList value assertion failed. Expected at least one value matching collection.");
                    }
                }
                else if (_expectType == ExpectType.All)
                {
                    if (valuesMatching < _values.Count())
                    {
                        Assert.Fail("SelectList value assertion failed. Expected all values to match collection.");
                    }
                }
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
    }
}
