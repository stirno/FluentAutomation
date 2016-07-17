using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class WbTstrCheckSyntaxProvider : ICheckSyntaxProvider
    {
        private readonly IAssertSyntaxProvider _assert;
        private readonly ILogger _logger;

        public WbTstrCheckSyntaxProvider(IAssertSyntaxProvider assert, ILogger logger)
        {
            _assert = assert;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public bool Visible(string element)
        {
            _logger.LogPartialMessage("that element '{0}' is visible.", true, element);
            return Check(() => _assert.Visible(element));                        
        }

        public bool Visible(ElementProxy element)
        {
            _logger.LogPartialMessage("that element '{0}' is visible.", true, element.Element.Selector);
            return Check(() => _assert.Visible(element));
        }

        public bool Exist(string element)
        {
            _logger.LogPartialMessage("that element '{0}' exists.", true, element);
            return Check(() => _assert.Exists(element));
        }

        public bool Exist(ElementProxy element)
        {
            _logger.LogPartialMessage("that element '{0}' exists.", true, element.Element.Selector);
            return Check(() => _assert.Exists(element));
        }

        public bool Text(string text, string element)
        {
            _logger.LogPartialMessage("that text of element '{0}' is '{1}'.", true, element, text);
            return Check(() => _assert.Text(text).In(element));
        }

        public bool Text(string text, ElementProxy element)
        {
            _logger.LogPartialMessage("that text of element '{0}' is '{1}'.", true, element.Element.Selector, text);
            return Check(() => _assert.Text(text).In(element));
        }

        private bool Check(Func<IAssertSyntaxProvider> assertFunction)
        {
            try
            {
                assertFunction();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
