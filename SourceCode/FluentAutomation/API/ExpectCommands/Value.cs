﻿// <copyright file="Value.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API.ExpectCommands
{
    /// <summary>
    /// Value Expect Commands
    /// </summary>
    public class Value : CommandBase, IValueTextCommand
    {
        private MatchConditions _matchConditions = MatchConditions.None;
        private ExpectType _expectType = ExpectType.Single;

        private string _value = string.Empty;
        private IEnumerable<string> _values = null;
        private Func<string, bool> _valueFunc = null;
        private Expression<Func<string, bool>> _valueExpression = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="Value"/> class from being created.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="expectType">Type of the expect.</param>
        private Value(AutomationProvider provider, CommandManager manager, ExpectType expectType) : base(provider, manager)
        {
            _expectType = expectType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="value">The value.</param>
        public Value(AutomationProvider provider, CommandManager manager, string value)
            : this(provider, manager, ExpectType.Single)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="value">The value.</param>
        public Value(AutomationProvider provider, CommandManager manager, Expression<Func<string, bool>> value)
            : this(provider, manager, ExpectType.Single)
        {
            _valueExpression = value;
            _valueFunc = _valueExpression.Compile();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="values">The values.</param>
        public Value(AutomationProvider provider, CommandManager manager, IEnumerable<string> values)
            : this(provider, manager, ExpectType.Any)
        {
            _values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="values">The values.</param>
        /// <param name="requireAll">if set to <c>true</c> [require all].</param>
        public Value(AutomationProvider provider, CommandManager manager, IEnumerable<string> values, bool requireAll)
            : this(provider, manager, ExpectType.All)
        {
            _values = values;
        }

        /// <summary>
        /// Expects that the specified field's value matches.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void In(string fieldSelector)
        {
            In(fieldSelector, MatchConditions.None);
        }

        /// <summary>
        /// Expects that the specified field's value matches.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void In(string fieldSelector, MatchConditions conditions)
        {
            CommandManager.CurrentActionBucket.Add(() =>
            {
                var element = Provider.GetElement(fieldSelector, conditions);
                var elementValue = element.GetValue() ?? string.Empty;

                if (element != null)
                {
                    if (_value == null && element.GetText() != null && elementValue != string.Empty)
                    {
                        Provider.TakeAssertExceptionScreenshot();
                        throw new AssertException("Null value assertion failed. Element [{0}] has a value of [{1}].", fieldSelector, element.GetText().PrettifyErrorValue());
                    }

                    if (!element.IsSelect() && (_expectType == ExpectType.Any || _expectType == ExpectType.All))
                    {
                        Provider.TakeAssertExceptionScreenshot();
                        throw new AssertException("Value assertion of types Any and All can only be used on SelectList elements.");
                    }
                    else if (element.IsSelect())
                    {
                        var selectElement = Provider.GetSelectElement(fieldSelector, conditions);
                        if (_expectType == ExpectType.Single)
                        {
                            if (selectElement.IsMultiple)
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Single value assertion cannot be used on a SelectList that potentially has multiple values. Use Any or All instead.");
                            }

                            if (_valueFunc != null)
                            {
                                if (!_valueFunc(selectElement.GetValue()))
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _valueExpression.ToExpressionString(), selectElement.GetValue().PrettifyErrorValue());
                                }
                            }
                            else
                            {
                                if (!selectElement.GetValue().Equals(_value, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to have a selected value of [{1}] but actual selected value is [{2}].", fieldSelector, _value.PrettifyErrorValue(), selectElement.GetValue().PrettifyErrorValue());
                                }
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
                                        if ((_valueFunc != null && !_valueFunc(selectedValue)) ||
                                            (selectedValue.Equals(value, StringComparison.InvariantCultureIgnoreCase)))
                                        {
                                            valuesMatching++;
                                        }
                                    }
                                }
                            }

                            if (_expectType == ExpectType.Any)
                            {
                                if (valuesMatching == 0)
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to have at least one of the following values: [{1}]", fieldSelector, string.Join(", ", _values));
                                }
                            }
                            else if (_expectType == ExpectType.All)
                            {
                                if (valuesMatching < _values.Count())
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement value assertion failed. Expected element [{0}] to include all of the following values: [{1}]", fieldSelector, string.Join(", ", _values));
                                }
                            }
                        }
                    }
                    else if (element.IsText())
                    {
                        var textElement = Provider.GetTextElement(fieldSelector, conditions);
                        var textElementValue = textElement.GetValue() ?? string.Empty;
                        if (_valueFunc != null)
                        {
                            if (!_valueFunc(textElementValue))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("TextElement value assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _valueExpression.ToExpressionString(), textElementValue.PrettifyErrorValue());
                            }
                        }
                        else
                        {
                            if (!textElementValue.Equals(_value ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("TextElement value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _value.PrettifyErrorValue(), textElementValue.PrettifyErrorValue());
                            }
                        }
                    }
                    else if (element.IsCheckBox())
                    {
                        var cbxElement = Provider.GetCheckBoxElement(fieldSelector, conditions);
						var checkedvalue = cbxElement.Checked;
                        if (_valueFunc != null)
                        {
							if (!_valueFunc(checkedvalue.ToString()))
                            {
                                Provider.TakeAssertExceptionScreenshot();
								throw new AssertException("CheckBoxElement value assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _valueExpression.ToExpressionString(), checkedvalue);
                            }
                        }
                        else
                        {
							if (!checkedvalue.ToString().Equals(_value ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Provider.TakeAssertExceptionScreenshot();
								throw new AssertException("CheckBoxElement value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _value.PrettifyErrorValue(), checkedvalue);
                            }
                        }
                    }
                    else
                    {
                        if (_valueFunc != null)
                        {
                            if (!_valueFunc(elementValue))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Value assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _valueExpression.ToExpressionString(), elementValue.PrettifyErrorValue());
                            }
                        }
                        else
                        {
                            if (!elementValue.Equals(_value ?? string.Empty))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _value.PrettifyErrorValue(), elementValue.PrettifyErrorValue());
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Expects that the specified fields' value matches.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void In(MatchConditions conditions, params string[] fieldSelectors)
        {
            _matchConditions = conditions;
            In(fieldSelectors);
        }

        /// <summary>
        /// Expects that the specified fields' value matches.
        /// </summary>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void In(params string[] fieldSelectors)
        {
            foreach (var fieldSelector in fieldSelectors)
            {
                In(fieldSelector, _matchConditions);
            }
        }
    }
}
