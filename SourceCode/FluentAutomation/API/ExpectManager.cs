// <copyright file="ExpectManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.API
{
    /// <summary>
    /// I.Expect, the primary method of making assertions
    /// </summary>
    public class ExpectManager
    {
        private ExpectCommands.Value _nullHandler = null;

        protected CommandManager Manager = null;
        protected AutomationProvider Provider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectManager"/> class.
        /// </summary>
        /// <param name="automation">The automation.</param>
        public ExpectManager(AutomationProvider automation, CommandManager manager)
        {
            Provider = automation;
            Manager = manager;
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
                    _nullHandler = new ExpectCommands.Value(Provider, Manager, value);
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
            return new ExpectCommands.Text(Provider, Manager, value);
        }

        /// <summary>
        /// Expect Text to match expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Text Text(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Text(Provider, Manager, valueExpression);
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
            return new ExpectCommands.Value(Provider, Manager, value);
        }

        /// <summary>
        /// Expect Value to match expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value Value(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Value(Provider, Manager, valueExpression);
        }

        /// <summary>
        /// Alls the specified select mode.
        /// </summary>
        /// <param name="selectMode">The select mode.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual IValueTextCommand All(SelectMode selectMode, params string[] values)
        {
            switch (selectMode)
            {
                case SelectMode.Value:
                    return new ExpectCommands.Value(Provider, Manager, values, true);
                case SelectMode.Text:
                    return new ExpectCommands.Text(Provider, Manager, values, true);
                default:
                    throw new Exception("Only Value and Text select modes are supported by this method.");
            }
        }

        /// <summary>
        /// Expect All Values within SelectedValues collection.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value All(params string[] values)
        {
            return new ExpectCommands.Value(Provider, Manager, values, true);
        }

        /// <summary>
        /// Anies the specified select mode.
        /// </summary>
        /// <param name="selectMode">The select mode.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual IValueTextCommand Any(SelectMode selectMode, params string[] values)
        {
            switch (selectMode)
            {
                case SelectMode.Value:
                    return new ExpectCommands.Value(Provider, Manager, values);
                case SelectMode.Text:
                    return new ExpectCommands.Text(Provider, Manager, values);
                default:
                    throw new Exception("Only Value and Text select modes are supported by this method.");
            }
        }

        /// <summary>
        /// Expect Any Values (at least one) within SelectedValues collection.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Value Any(params string[] values)
        {
            return new ExpectCommands.Value(Provider, Manager, values);
        }

        /// <summary>
        /// Expect Class on element.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.CssClass Class(string value)
        {
            return new ExpectCommands.CssClass(Provider, Manager, value);
        }

        /// <summary>
        /// Expect Count of element.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Count Count(int value)
        {
            return new ExpectCommands.Count(Provider, Manager, value);
        }

        /// <summary>
        /// Elements the specified element expression.
        /// </summary>
        /// <param name="elementExpression">The element expression.</param>
        /// <returns></returns>
        public virtual ExpectCommands.Element Element(Expression<Func<IElementDetails, bool>> elementExpression)
        {
            return new ExpectCommands.Element(Provider, Manager, elementExpression);
        }

        /// <summary>
        /// Expect Alert dialog.
        /// </summary>
        public virtual void Alert()
        {
            Provider.HandleAlertDialog();
        }

        /// <summary>
        /// Expect Alert dialog with specified message.
        /// </summary>
        /// <param name="alertMessage">The alert message.</param>
        public virtual void Alert(string alertMessage)
        {
            Provider.HandleAlertDialog(alertMessage);
        }

        /// <summary>
        /// Expect an HTTP Basic Authentication dialog and handle it.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /*public virtual void BasicAuthenticationDialog(string username, string password)
        {
            Provider.Authenticate(username, password);
        }*/

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
            if (!pageUri.ToString().Equals(Provider.GetUri().ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                Provider.TakeAssertExceptionScreenshot();
                throw new AssertException("URL Assertion failed. Expected URL [{0}] but actual URL is [{1}].", pageUri, Provider.GetUri());
            }
        }

        /// <summary>
        /// Expects page URL matches expression.
        /// </summary>
        /// <param name="valueExpression">The value expression.</param>
        public virtual void Url(Expression<Func<Uri, bool>> valueExpression)
        {
            var _compiledFunc = valueExpression.Compile();
            if (!_compiledFunc(Provider.GetUri()))
            {
                Provider.TakeAssertExceptionScreenshot();
                throw new AssertException("URL Assertion failed. Expected URL to match expression [{0}]. Actual URL is [{1}].", valueExpression.ToExpressionString(), Provider.GetUri());
            }
        }
    }
}
