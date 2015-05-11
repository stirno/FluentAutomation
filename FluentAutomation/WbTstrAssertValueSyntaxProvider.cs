using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertValueSyntaxProvider : IAssertValueSyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertValueSyntaxProvider _assertValueSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertValueSyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertValueSyntaxProvider assertValueSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertValueSyntaxProvider = assertValueSyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public IAssertValueSyntaxProvider Not
        {
            get
            {
                return new WbTstrAssertValueSyntaxProvider(_assertSyntaxProvider, _assertValueSyntaxProvider.Not, _logger);
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

        public IAssertSyntaxProvider In(string selector)
        {
            // Before
            _logger.LogPartialMessage("in element with selector: "+ selector, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertValueSyntaxProvider.In(selector);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider In(ElementProxy element)
        {
            // Before
            _logger.LogPartialMessage("in element with selector: " + element.Element.Selector, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertValueSyntaxProvider.In(element);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider In(Alert accessor)
        {
            // Before
            _logger.LogPartialMessage("in : " + accessor, true); 

            // Execute
            if (!IsInDryRunMode)
            {
                _assertValueSyntaxProvider.In(accessor);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}
