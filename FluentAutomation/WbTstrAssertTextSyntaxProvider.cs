using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertTextSyntaxProvider : IAssertTextSyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertTextSyntaxProvider _assertTextSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertTextSyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertTextSyntaxProvider assertTextSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertTextSyntaxProvider = assertTextSyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public IAssertTextSyntaxProvider Not
        {
            get
            {
                return new WbTstrAssertTextSyntaxProvider(_assertSyntaxProvider, _assertTextSyntaxProvider.Not, _logger);
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
            _logger.LogPartialMessage("in element with selector: " + selector, true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertTextSyntaxProvider.In(selector);
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
                _assertTextSyntaxProvider.In(element);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider In(Alert accessor)
        {
            // Before
            _logger.LogPartialMessage("in: " + accessor, true);

            // Execute
            if (!IsInDryRunMode)
            {
                _assertTextSyntaxProvider.In(accessor);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}
