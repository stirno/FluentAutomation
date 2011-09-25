// <copyright file="ExpectManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;
using FluentAutomation.API.Exceptions;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    public class ExpectManager
    {
        private AutomationProvider _automation = null;
        private ExpectCommands.Value _nullHandler = null;

        public ExpectManager(AutomationProvider automation)
        {
            _automation = automation;
        }

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

        public virtual ExpectCommands.Text Text(string value)
        {
            return new ExpectCommands.Text(_automation, value);
        }
        
        public virtual ExpectCommands.Text Text(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Text(_automation, valueExpression);
        }

        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectCommands.Value This(string value)
        {
            return Value(value);
        }

        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectCommands.Value This(Expression<Func<string, bool>> valueExpression)
        {
            return Value(valueExpression);
        }

        public virtual ExpectCommands.Value Value(string value)
        {
            return new ExpectCommands.Value(_automation, value);
        }

        public virtual ExpectCommands.Value Value(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectCommands.Value(_automation, valueExpression);
        }

        public virtual ExpectCommands.Value All(params string[] values)
        {
            return new ExpectCommands.Value(_automation, values, true);
        }

        public virtual ExpectCommands.Value Any(params string[] values)
        {
            return new ExpectCommands.Value(_automation, values);
        }

        public virtual ExpectCommands.CssClass Class(string value)
        {
            return new ExpectCommands.CssClass(_automation, value);
        }

        public virtual ExpectCommands.Count Count(int value)
        {
            return new ExpectCommands.Count(_automation, value);
        }
        
        public virtual void Alert()
        {
            _automation.HandleAlertDialog();
        }

        public virtual void Alert(string alertMessage)
        {
            _automation.HandleAlertDialog(alertMessage);
        }

        public virtual void Url(string pageUrl)
        {
            Url(new Uri(pageUrl, UriKind.Absolute));
        }

        public virtual void Url(Uri pageUri)
        {
            if (!pageUri.ToString().Equals(_automation.GetUri().ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AssertException("URL Assertion failed. Expected URL [{0}] but actual URL is [{1}].", pageUri, _automation.GetUri());
            }
        }

        public virtual void Url(Expression<Func<Uri, bool>> valueExpression)
        {
            var _compiledFunc = valueExpression.Compile();
            if (!_compiledFunc(_automation.GetUri()))
            {
                throw new AssertException("URL Assertion failed. Expected URL to match expression [{0}]. Actual URL is [{1}].", valueExpression.ToExpressionString(), _automation.GetUri());
            }
        }
    }
}
