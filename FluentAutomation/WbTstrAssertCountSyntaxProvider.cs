using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertCountSyntaxProvider : IAssertCountSyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertCountSyntaxProvider _assertCountSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertCountSyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertCountSyntaxProvider assertCountSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertCountSyntaxProvider = assertCountSyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public IAssertCountSyntaxProvider Not
        {
            get
            {
                return new WbTstrAssertCountSyntaxProvider(_assertSyntaxProvider, _assertCountSyntaxProvider.Not, _logger);
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

        public IAssertSyntaxProvider Of(string selector)
        {
            // Before
            _logger.LogPartialMessage(" of element with selector: " + selector, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertCountSyntaxProvider.Of(selector);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider Of(ElementProxy elements)
        {
            /* When element is not found, 'elements.Element' will throw an exception. 
             * We don't want a log message to throw an exception in this case, therefore this additional safety check. 
             */
            string selector = elements.Elements.Count > 1 ? elements.Element.Selector : String.Empty;
            _logger.LogPartialMessage(" of element with selector: {0}", true, selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertCountSyntaxProvider.Of(elements);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}