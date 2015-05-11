using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertAttributeSyntaxProvider : IAssertAttributeSyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertAttributeSyntaxProvider _assertAttributeSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertAttributeSyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertAttributeSyntaxProvider assertAttributeSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertAttributeSyntaxProvider = assertAttributeSyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public IAssertAttributeSyntaxProvider Not
        {
            get
            {
                _logger.LogPartialMessage("this to be false: "); 
                return new WbTstrAssertAttributeSyntaxProvider(_assertSyntaxProvider, _assertAttributeSyntaxProvider.Not, _logger);
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

        public IAssertSyntaxProvider On(string selector)
        {
            // Before
            _logger.LogPartialMessage(" on element with selector: " + selector, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertAttributeSyntaxProvider.On(selector);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider On(ElementProxy elements)
        {
            // Before
            _logger.LogPartialMessage(" on element with selector: " + elements.Element.Selector, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertAttributeSyntaxProvider.On(elements);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}
