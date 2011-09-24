// <copyright file="ExpectManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using FluentAutomation.API.Providers;
using System.Linq.Expressions;

namespace FluentAutomation.API
{
    public partial class ExpectManager
    {
        private AutomationProvider _automation = null;
        private ExpectHandlers.ExpectValueHandler _nullHandler = null;

        public ExpectManager(AutomationProvider automation)
        {
            _automation = automation;
        }

        public ExpectHandlers.ExpectValueHandler Null
        {
            get
            {
                if (_nullHandler == null)
                {
                    string value = null;
                    _nullHandler = new ExpectHandlers.ExpectValueHandler(_automation, value);
                }

                return _nullHandler;
            }
        }

        public virtual ExpectHandlers.ExpectTextHandler Text(string value)
        {
            return new ExpectHandlers.ExpectTextHandler(_automation, value);
        }
        
        public virtual ExpectHandlers.ExpectTextHandler Text(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectHandlers.ExpectTextHandler(_automation, valueExpression);
        }

        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectHandlers.ExpectValueHandler This(string value)
        {
            return Value(value);
        }

        [Obsolete("Use Value() instead of This(). Will be removed eventually.")]
        public virtual ExpectHandlers.ExpectValueHandler This(Expression<Func<string, bool>> valueExpression)
        {
            return Value(valueExpression);
        }

        public virtual ExpectHandlers.ExpectValueHandler Value(string value)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, value);
        }

        public virtual ExpectHandlers.ExpectValueHandler Value(Expression<Func<string, bool>> valueExpression)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, valueExpression);
        }

        public virtual ExpectHandlers.ExpectValueHandler All(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, values, true);
        }

        public virtual ExpectHandlers.ExpectValueHandler Any(params string[] values)
        {
            return new ExpectHandlers.ExpectValueHandler(_automation, values);
        }

        public virtual ExpectHandlers.ExpectCssClassHandler Class(string value)
        {
            return new ExpectHandlers.ExpectCssClassHandler(_automation, value);
        }

        public virtual ExpectHandlers.ExpectCountHandler Count(int value)
        {
            return new ExpectHandlers.ExpectCountHandler(_automation, value);
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
