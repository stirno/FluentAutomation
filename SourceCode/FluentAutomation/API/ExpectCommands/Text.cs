// <copyright file="Text.cs" author="Brandon Stirnaman">
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
    /// Text Expect Commands
    /// </summary>
    public class Text : CommandBase, IValueTextCommand
    {
        private MatchConditions _matchConditions = MatchConditions.None;
        private ExpectType _expectType = ExpectType.Single;

        private string _expectedText = string.Empty;
        private IEnumerable<string> _expectedStrings = null;
        private Func<string, bool> _expectedTextFunc = null;
        private Expression<Func<string, bool>> _expectedTextExpression = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="Text"/> class from being created.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="expectType">Type of the expect.</param>
        private Text(AutomationProvider provider, CommandManager manager, ExpectType expectType) : base(provider, manager)
        {
            _expectType = expectType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="text">The text.</param>
        public Text(AutomationProvider provider, CommandManager manager, string text)
            : this(provider, manager, ExpectType.Single)
        {
            _expectedText = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="expression">The expression.</param>
        public Text(AutomationProvider provider, CommandManager manager, Expression<Func<string, bool>> expression)
            : this(provider, manager, ExpectType.Single)
        {
            _expectedTextExpression = expression;
            _expectedTextFunc = _expectedTextExpression.Compile();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="strings">The strings.</param>
        public Text(AutomationProvider provider, CommandManager manager, IEnumerable<string> strings)
            : this(provider, manager, ExpectType.Any)
        {
            _expectedStrings = strings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="strings">The strings.</param>
        /// <param name="requireAll">if set to <c>true</c> [require all].</param>
        public Text(AutomationProvider provider, CommandManager manager, IEnumerable<string> strings, bool requireAll)
            : this(provider, manager, ExpectType.All)
        {
            _expectedStrings = strings;
        }

        /// <summary>
        /// Expects that the specified field's text matches.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        public void In(string fieldSelector)
        {
            In(fieldSelector, MatchConditions.None);
        }

        /// <summary>
        /// Expects that the specified field's text matches.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="conditions">The conditions.</param>
        public void In(string fieldSelector, MatchConditions conditions)
        {
            CommandManager.CurrentActionBucket.Add(() =>
            {
                var element = Provider.GetElement(fieldSelector, conditions);
                var elementText = element.GetText() ?? string.Empty;

                if (element != null)
                {
                    if (element.IsSelect())
                    {
                        var selectElement = Provider.GetSelectElement(fieldSelector, conditions);
                        if (_expectType == ExpectType.Single)
                        {
                            if (selectElement.IsMultiple)
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Single value assertion cannot be used on a SelectList that potentially has multiple values. Use Any or All instead.");
                            }

                            if (_expectedTextFunc != null)
                            {
                                if (!_expectedTextFunc(selectElement.GetSelectedOptionText()))
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement text assertion failed. Expected element [{0}] to match expression [{1}]. Actual text is [{2}].", fieldSelector, _expectedTextExpression.ToExpressionString(), selectElement.GetSelectedOptionText().PrettifyErrorValue());
                                }
                            }
                            else
                            {
                                if (!selectElement.GetSelectedOptionText().Equals(_expectedText, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Provider.TakeAssertExceptionScreenshot();
                                    throw new AssertException("SelectElement text assertion failed. Expected element [{0}] to have selected text of [{1}] but actual selected text is [{2}].", fieldSelector, _expectedText.PrettifyErrorValue(), selectElement.GetSelectedOptionText().PrettifyErrorValue());
                                }
                            }
                        }
                        else
                        {
                            int textMatching = 0;
                            string[] selectedText = selectElement.GetSelectedOptionsText();

                            if (selectedText.Length > 0)
                            {
                                foreach (string text in _expectedStrings)
                                {
                                    bool isMatch = selectedText.Any(s => s.Equals(text, StringComparison.InvariantCultureIgnoreCase));
                                    if (isMatch) textMatching++;
                                }

                                if (_expectType == ExpectType.Any)
                                {
                                    if (textMatching == 0)
                                    {
                                        Provider.TakeAssertExceptionScreenshot();
                                        throw new AssertException("SelectElement text assertion failed. Expected element [{0}] to have at least one option with text matching the following options: [{1}]", fieldSelector, string.Join(", ", _expectedStrings));
                                    }
                                }
                                else if (_expectType == ExpectType.All)
                                {
                                    if (textMatching != _expectedStrings.Count())
                                    {
                                        Provider.TakeAssertExceptionScreenshot();
                                        throw new AssertException("SelectElement text assertion failed. Expected element [{0}] to include option text matching all the following options: [{1}]", fieldSelector, string.Join(", ", _expectedStrings));
                                    }
                                }
                            }
                        }
                    }
                    else if (element.IsText())
                    {
                        var textElement = Provider.GetTextElement(fieldSelector, conditions);
                        var textElementText = textElement.GetText() ?? string.Empty;

                        if (_expectedTextFunc != null)
                        {
                            if (!_expectedTextFunc(textElementText))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("TextElement text assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _expectedTextExpression.ToExpressionString(), textElementText.PrettifyErrorValue());
                            }
                        }
                        else
                        {
                            if (!textElementText.Equals(_expectedText ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("TextElement text assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _expectedText.PrettifyErrorValue(), textElementText.PrettifyErrorValue());
                            }
                        }
                    }
                    else
                    {
                        if (_expectedTextFunc != null)
                        {
                            if (!_expectedTextFunc(elementText))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Value assertion failed. Expected element [{0}] to match expression [{1}]. Actual value is [{2}].", fieldSelector, _expectedTextExpression.ToExpressionString(), elementText.PrettifyErrorValue());
                            }
                        }
                        else
                        {
                            if (!elementText.Equals(_expectedText ?? string.Empty))
                            {
                                Provider.TakeAssertExceptionScreenshot();
                                throw new AssertException("Value assertion failed. Expected element [{0}] to have a value of [{1}] but actual value is [{2}].", fieldSelector, _expectedText.PrettifyErrorValue(), elementText.PrettifyErrorValue());
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Expects that the specified field's text matches.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="fieldSelectors">The field selectors.</param>
        public void In(MatchConditions conditions, params string[] fieldSelectors)
        {
            _matchConditions = conditions;
            In(fieldSelectors);
        }

        /// <summary>
        /// Expects that the specified fields' text matches.
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
