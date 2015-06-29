using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrDragDropSyntaxProvider : IDragDropSyntaxProvider
    {
        private readonly IActionSyntaxProvider _actionSyntaxProvider;
        private readonly IDragDropSyntaxProvider _dragDropSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrDragDropSyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, IDragDropSyntaxProvider dragDropSyntaxProvider, ILogger logger)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
            _dragDropSyntaxProvider = dragDropSyntaxProvider;
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

        public IActionSyntaxProvider To(string selector)
        {
            return To(_actionSyntaxProvider.Find(selector));
        }

        public IActionSyntaxProvider To(ElementProxy targetElement)
        {
            // Before
            _logger.LogPartialMessage(" to: " + targetElement.Element.Selector, true);

            // Execute
            if (!IsInDryRunMode)
            {
                _dragDropSyntaxProvider.To(targetElement);
            }

            // After
            return _actionSyntaxProvider;
        }

        public IActionSyntaxProvider To(string selector, int targetOffsetX, int targetOffsetY)
        {
            return To(_actionSyntaxProvider.Find(selector), targetOffsetX, targetOffsetY);
        }

        public IActionSyntaxProvider To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
        {
            // Before
            _logger.LogPartialMessage(" to: " + targetElement.Element.Selector + " with offset x: " + targetOffsetX + " y: " + targetOffsetY , true);

            // Execute
            if (!IsInDryRunMode)
            {
                _dragDropSyntaxProvider.To(targetElement, targetOffsetX, targetOffsetY);
            }

            // After
            return _actionSyntaxProvider;
        }
    }
}
