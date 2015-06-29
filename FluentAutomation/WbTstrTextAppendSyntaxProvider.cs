using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrTextAppendSyntaxProvider : ITextAppendSyntaxProvider
    {
        private readonly IActionSyntaxProvider _actionSyntaxProvider;
        private readonly ITextAppendSyntaxProvider _textAppendSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrTextAppendSyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, ITextAppendSyntaxProvider textAppendSyntaxProvider, ILogger logger)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
            _textAppendSyntaxProvider = textAppendSyntaxProvider;
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

        public ITextAppendSyntaxProvider WithoutEvents()
        {
            // Before
            _logger.LogMessage("Disable firing of key events.");

            // Execute
            if (!IsInDryRunMode)
            {
                _textAppendSyntaxProvider.WithoutEvents();
            }

            // After
            return this;
        }

        public IActionSyntaxProvider To(string selector)
        {
            return To(_actionSyntaxProvider.Find(selector));
        }

        public IActionSyntaxProvider To(ElementProxy element)
        {
            // Before
            string selector = (element != null && element.Element != null) ? element.Element.Selector ?? "?" : "?";
            _logger.LogMessage("Append text to text in element with selector: {0}", selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _textAppendSyntaxProvider.To(element);
            }

            // After
            return _actionSyntaxProvider;
        }
    }
}
