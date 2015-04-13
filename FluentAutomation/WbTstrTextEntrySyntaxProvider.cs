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

        public WbTstrTextEntrySyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, ITextEntrySyntaxProvider textEntrySyntaxProvider, ILogger logger)
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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
