using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertCssPropertySyntaxProvider : IAssertCssPropertySyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertCssPropertySyntaxProvider _assertCssPropertySyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertCssPropertySyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertCssPropertySyntaxProvider assertCssPropertySyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertCssPropertySyntaxProvider = assertCssPropertySyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public IAssertCssPropertySyntaxProvider Not
        {
            get
            {
                return new WbTstrAssertCssPropertySyntaxProvider(_assertSyntaxProvider, _assertCssPropertySyntaxProvider.Not, _logger);
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
                _assertCssPropertySyntaxProvider.On(selector);
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
                _assertCssPropertySyntaxProvider.On(elements);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}
