// <copyright file="ExpectValueHandler.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.ExpectHandlers
{
    public class ExpectValueHandler
    {
        private AutomationProvider _automation = null;
        private ExpectType _expectType = ExpectType.Single;

        private string _value = string.Empty;
        private IEnumerable<string> _values = null;

        private enum ExpectType
        {
            Single = 1,
            Any = 2,
            All = 3
        }

        private ExpectValueHandler(AutomationProvider automation, ExpectType expectType)
        {
            _automation = automation;
            _expectType = expectType;
        }

        public ExpectValueHandler(AutomationProvider automation, string value)
            : this(automation, ExpectType.Single)
        {
            _value = value;
        }

        public ExpectValueHandler(AutomationProvider automation, IEnumerable<string> values)
            : this(automation, ExpectType.Any)
        {
            _values = values;
        }

        public ExpectValueHandler(AutomationProvider automation, IEnumerable<string> values, bool requireAll)
            : this(automation, ExpectType.All)
        {
            _values = values;
        }

        public void In(string fieldSelector)
        {
            var element = _automation.GetElement(fieldSelector);

            if (element != null)
            {
                if (_value == null && element.GetText() != null && element.GetValue() != null)
                {
                    throw new AssertException("Null value assertion failed. Element [{0}] has a value of [{1}].", fieldSelector, element.GetText());
                }

                if (!element.IsSelect() && (_expectType == ExpectType.Any || _expectType == ExpectType.All))
                {
                    throw new AssertException("Value assertion of types Any and All can only be used on SelectList elements.");
                }
                else if (element.IsSelect())
                {
                    var selectElement = _automation.GetSelectElement(fieldSelector);
                    if (_expectType == ExpectType.Single)
                    {
                        if (selectElement.IsMultiple)
                        {
                            throw new AssertException("Single value assertion cannot be used on a SelectList that potentially has multiple values. Use Any or All instead.");
                        }

                        if (!selectElement.GetValue().Equals(_value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to have a selected value of [{1}] but actual selected value is [{2}].", fieldSelector, _value, selectElement.GetValue());
                        }
                    }
                    else
                    {
                        int valuesMatching = 0;
                        string[] selectedValues = selectElement.GetValues();

                        if (selectedValues.Length > 0)
                        {
                            foreach (var selectedValue in selectedValues)
                            {
                                foreach (var value in _values)
                                {
                                    if (selectedValue.Equals(value, StringComparison.InvariantCultureIgnoreCase)) valuesMatching++;
                                }
                            }
                        }

                        if (_expectType == ExpectType.Any)
                        {
                            if (valuesMatching == 0)
                            {
                                throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to have at least one value matching the collection.", fieldSelector);
                            }
                        }
                        else if (_expectType == ExpectType.All)
                        {
                            if (valuesMatching < _values.Count())
                            {
                                throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to include all values in collection.", fieldSelector);
                            }
                        }
                    }
                }
                else if (element.IsText())
                {
                    var textElement = _automation.GetTextElement(fieldSelector);
                    if (!textElement.GetValue().Equals(_value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new AssertException("TextElement value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _value, textElement.GetValue());
                    }
                }
                else
                {
                    if (!element.GetValue().Equals(_value))
                    {
                        throw new AssertException("Value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _value, element.GetValue());
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
    }
}