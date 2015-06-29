using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertSyntaxProvider : IAssertSyntaxProvider
    {
        private readonly IAssertSyntaxProvider _assertSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertSyntaxProvider(IAssertSyntaxProvider assertSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _logger = logger;
        }
      
        /*-------------------------------------------------------------------*/

        public INotAssertSyntaxProvider Not
        {
            get
            {
                _logger.LogPartialMessage("this to be false: ");
                return _assertSyntaxProvider.Not;
            }
        }

        public bool IsInDryRunMode
        {
            get
            {
                return FluentSettings.Current.IsDryRun;
            }
        }

        /*-------------------------------------------------------------------*/

        public IAssertCountSyntaxProvider Count(int count)
        {
            // Before
            _logger.LogPartialMessage("count to be " + count + " ", true); 

            // Execute
            IAssertCountSyntaxProvider assertCountSyntaxProvider = _assertSyntaxProvider.Count(count);

            // After
            return new WbTstrAssertCountSyntaxProvider(this, assertCountSyntaxProvider, _logger);
        }

        public IAssertClassSyntaxProvider Class(string className)
        {
            // Before
            _logger.LogPartialMessage("class to be " + className + " ", true); 

            // Execute
            IAssertClassSyntaxProvider assertClassSyntaxProvider = _assertSyntaxProvider.Class(className);

            // After
            return new WbTstrAssertClassSyntaxProvider(this, assertClassSyntaxProvider, _logger);
        }

        public IAssertCssPropertySyntaxProvider Css(string propertyName)
        {
            // Before
            _logger.LogPartialMessage("CSS property to be " + propertyName + " ", true);

            // Execute
            IAssertCssPropertySyntaxProvider assertClassSyntaxProvider = _assertSyntaxProvider.Css(propertyName);

            // After
            return new WbTstrAssertCssPropertySyntaxProvider(this, assertClassSyntaxProvider, _logger);
        }

        public IAssertCssPropertySyntaxProvider Css(string propertyName, string propertyValue)
        {
            // Before
            _logger.LogPartialMessage("CSS property to be " + propertyName + " and value " + propertyValue + " ", true);

            // Execute
            IAssertCssPropertySyntaxProvider assertClassSyntaxProvider = _assertSyntaxProvider.Css(propertyName, propertyValue);

            // After
            return new WbTstrAssertCssPropertySyntaxProvider(this, assertClassSyntaxProvider, _logger);
        }

        public IAssertAttributeSyntaxProvider Attribute(string attributeName)
        {
            // Before
            _logger.LogPartialMessage("attribute to be " + attributeName + " ", true);

            // Execute
            IAssertAttributeSyntaxProvider assertAttributeSyntaxProvider = _assertSyntaxProvider.Attribute(attributeName);

            // After
            return new WbTstrAssertAttributeSyntaxProvider(this, assertAttributeSyntaxProvider, _logger);
        }

        public IAssertAttributeSyntaxProvider Attribute(string attributeName, string propertyValue)
        {
            // Before
            _logger.LogPartialMessage("tttribute to be " + attributeName + " with value" + " ", true);

            // Execute
            IAssertAttributeSyntaxProvider assertAttributeSyntaxProvider = _assertSyntaxProvider.Attribute(attributeName, propertyValue);

            // After
            return new WbTstrAssertAttributeSyntaxProvider(this, assertAttributeSyntaxProvider, _logger);
        }

        public IAssertTextSyntaxProvider Text(string text)
        {
            // Before
            _logger.LogPartialMessage("text to be " + text + " ", true);

            // Execute
            IAssertTextSyntaxProvider assertTextSyntaxProvider = _assertSyntaxProvider.Text(text);

            // After
            return new WbTstrAssertTextSyntaxProvider(this, assertTextSyntaxProvider, _logger);
        }

        public IAssertTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc)
        {
            // Before
            _logger.LogPartialMessage("text to match " + matchFunc + " ", true);

            // Execute
            IAssertTextSyntaxProvider assertTextSyntaxProvider = _assertSyntaxProvider.Text(matchFunc);

            // After
            return new WbTstrAssertTextSyntaxProvider(this, assertTextSyntaxProvider, _logger);
        }

        public IAssertValueSyntaxProvider Value(int value)
        {
            // Before
            _logger.LogPartialMessage("value to equal " + value + " ", true);

            // Execute
            IAssertValueSyntaxProvider assertValueSyntaxProvider = _assertSyntaxProvider.Value(value);

            // After
            return new WbTstrAssertValueSyntaxProvider(this, assertValueSyntaxProvider, _logger);
        }

        public IAssertValueSyntaxProvider Value(string value)
        {
            // Before
            _logger.LogPartialMessage("value to equal" + value + " ", true);

            // Execute
            IAssertValueSyntaxProvider assertValueSyntaxProvider = _assertSyntaxProvider.Value(value);

            // After
            return new WbTstrAssertValueSyntaxProvider(this, assertValueSyntaxProvider, _logger);
        }

        public IAssertValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc)
        {
            // Before
            _logger.LogPartialMessage("value to match" + matchFunc + " ", true);

            // Execute
            IAssertValueSyntaxProvider assertValueSyntaxProvider = _assertSyntaxProvider.Value(matchFunc);

            // After
            return new WbTstrAssertValueSyntaxProvider(this, assertValueSyntaxProvider, _logger);
        }

        public IAssertSyntaxProvider Url(string expectedUrl)
        {
            // Before
            _logger.LogPartialMessage("url to equal" + expectedUrl + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Url(expectedUrl);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Url(Uri expectedUri)
        {
            // Before
            _logger.LogPartialMessage("url to equal" + expectedUri + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Url(expectedUri);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
        {
            // Before
            _logger.LogPartialMessage("url to match" + uriExpression + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Url(uriExpression);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
        {
            // Before
            _logger.LogPartialMessage("to be true" + matchFunc + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.True(matchFunc);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
        {
            // Before
            _logger.LogPartialMessage("to be false" + matchFunc + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.False(matchFunc);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Throws(Expression<Action> matchAction)
        {
            // Before
            _logger.LogPartialMessage("to throw expection " + matchAction + " ", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Throws(matchAction);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Exists(string selector)
        {
            // Before
            _logger.LogPartialMessage("element with selector: " + selector + " exists", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Exists(selector);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Exists(ElementProxy element)
        {
            // Before
            _logger.LogPartialMessage("element with selector: " + element.Element.Selector + " exists", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Exists(element);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Visible(string selector)
        {
            // Before
            _logger.LogPartialMessage("element with selector: " + selector + " to be v _logger.LogPartialMessageisible", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Visible(selector);
            }

            // After
            return this;
        }

        public IAssertSyntaxProvider Visible(ElementProxy element)
        {
            // Before
            _logger.LogPartialMessage("element with selector: " + element.Element.Selector + " to be visible", true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertSyntaxProvider.Visible(element);
            }

            // After
            return this;
        }
    }
}