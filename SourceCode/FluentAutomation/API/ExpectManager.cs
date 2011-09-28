// <copyright file="ExpectManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    /// <summary>
    /// I.Expect, the primary method of making assertions
    /// </summary>
    public class ExpectManager
    {
        private AutomationProvider _automation = null;
        private ExpectCommands.Value _nullHandler = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectManager"/> class.
        /// </summary>
        /// <param name="automation">The automation.</param>
        public ExpectManager(AutomationProvider automation)
        {
            _automation = automation;
        }

        /// <summary>
        /// Expect Null.
        /// </summary>
        public ExpectCommands.Value Null
        {
            get
            {
                if (_nullHandler == null)
                {
                    string value = null;
                    _nullHandler = new ExpectCommands.Value(_automation, value);
                }

                return _nullHandler;
            }
        }

        /// <summary>
        /// Expect Text to match value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Text Text(string value)
        {
            return new ExpectCommands.Text(_automation, value);
        }

        /// <summary>
        /// Expect Text to match expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Text Text(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Text(_automation, valueExpression);
        }

        /// <summary>
        /// Expect Value to match string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectCommands.Value This(string value)
        {
            return Value(value);
        }

        /// <summary>
        /// Expect Value to match expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns></returns>
        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectCommands.Value This(Expression<Func<string, bool>> valueExpression)
        {
            return Value(valueExpression);
        }

        /// <summary>
        /// Expect Value to match string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value Value(string value)
        {
            return new ExpectCommands.Value(_automation, value);
        }

        /// <summary>
        /// Expect Value to match expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value Value(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Value(_automation, valueExpression);
        }

        /// <summary>
        /// Expect All Values within SelectedValues collection.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value All(params string[] values)
        {
            return new ExpectCommands.Value(_automation, values, true);
        }

        /// <summary>
        /// Expect Any Values (at least one) within SelectedValues collection.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value Any(params string[] values)
        {
            return new ExpectCommands.Value(_automation, values);
        }

        /// <summary>
        /// Expect Class on element.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.CssClass Class(string value)
        {
            return new ExpectCommands.CssClass(_automation, value);
        }

        /// <summary>
        /// Expect Count of element.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Count Count(int value)
        {
            return new ExpectCommands.Count(_automation, value);
        }

        /// <summary>
        /// Expect Alert dialog.
        /// </summary>
        public virtual void Alert()
        {
            _automation.HandleAlertDialog();
        }

        /// <summary>
        /// Expect Alert dialog with specified message.
        /// </summary>
        /// <param name="alertMessage">The alert message.</param>
        public virtual void Alert(string alertMessage)
        {
            _automation.HandleAlertDialog(alertMessage);
        }

        /// <summary>
        /// Expect page URL matches specified URL.
        /// </summary>
        /// <param name="pageUrl">The page URL.</param>
        public virtual void Url(string pageUrl)
        {
            Url(new Uri(pageUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Expect page URI matches specified URI.
        /// </summary>
        /// <param name="pageUri">The page URI.</param>
        public virtual void Url(Uri pageUri)
        {
            if (!pageUri.ToString().Equals(_automation.GetUri().ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                _automation.TakeScreenshot();
                throw new AssertException("URL Assertion failed. Expected URL [{0}] but actual URL is [{1}].", pageUri, _automation.GetUri());
            }
        }

        /// <summary>
        /// Expects page URL matches expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        public virtual void Url(Expression<Func<Uri, bool>> valueExpression)
        {
            var _compiledFunc = valueExpression.Compile();
            if (!_compiledFunc(_automation.GetUri()))
            {
                _automation.TakeScreenshot();
                throw new AssertException("URL Assertion failed. Expected URL to match expression [{0}]. Actual URL is [{1}].", valueExpression.ToExpressionString(), _automation.GetUri());
            }
        }
    }
}
