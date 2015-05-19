using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrTextEntrySyntaxProvider : ITextEntrySyntaxProvider
    {
        private readonly IActionSyntaxProvider _actionSyntaxProvider;
        private readonly ITextEntrySyntaxProvider _textEntrySyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrTextEntrySyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, ITextEntrySyntaxProvider textEntrySyntaxProvider, ILogger logger)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
            _textEntrySyntaxProvider = textEntrySyntaxProvider;
            _logger = logger;
        }

        /*-------------------------------------------------------------------*/

        public bool IsInDryRunMode
        {
            get
            {
                return FluentSettings.Current.IsDryRun;
            }
        }

        /*-------------------------------------------------------------------*/

        public ITextEntrySyntaxProvider WithoutEvents()
        {
            // Before
            _logger.LogMessage("Disable firing of key events.");

            // Execute
            if (!IsInDryRunMode)
            {
                _textEntrySyntaxProvider.WithoutEvents();
            }

            // After
            return this;
        }

        public IActionSyntaxProvider In(string selector)
        {
            return In(_actionSyntaxProvider.Find(selector));
        }

        public IActionSyntaxProvider In(ElementProxy element)
        {
            // Before
            string selector = (element != null && element.Element != null) ? element.Element.Selector ?? "?" : "?";
            _logger.LogMessage("Enter text in element with selector: {0}", selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _textEntrySyntaxProvider.In(element);
            }

            // After
            return _actionSyntaxProvider;
        }

        public IActionSyntaxProvider In(Alert accessor)
        {
            // Before
            _logger.LogMessage("Enter text in alert field.");

            // Execute
            if (!IsInDryRunMode)
            {
                _textEntrySyntaxProvider.In(accessor);
            }

            // After
            return _actionSyntaxProvider;
        }
    }
}
