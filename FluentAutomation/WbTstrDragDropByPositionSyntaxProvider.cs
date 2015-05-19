using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrDragDropByPositionSyntaxProvider : IDragDropByPositionSyntaxProvider
    {
        private readonly IActionSyntaxProvider _actionSyntaxProvider;
        private readonly IDragDropByPositionSyntaxProvider _dragDropByPositionSyntax;
        private readonly ILogger _logger;

        internal WbTstrDragDropByPositionSyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, IDragDropByPositionSyntaxProvider dragDropByPositionSyntax, ILogger logger)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
            _dragDropByPositionSyntax = dragDropByPositionSyntax;
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

        public IActionSyntaxProvider To(int destinationX, int destinationY)
        {
            // Before
            _logger.LogPartialMessage(" to position x: " + destinationX + " and y: " + destinationY, true);

            // Execute
            if (!IsInDryRunMode)
            {
                _dragDropByPositionSyntax.To(destinationX, destinationY);
            }

            // After
            return _actionSyntaxProvider;
        }

        public void To(string selector)
        {
            To(_actionSyntaxProvider.Find(selector));
        }

        public void To(ElementProxy targetElement)
        {
            // Before
            _logger.LogPartialMessage(" to element with selector" + targetElement.Element.Selector, true);

            // Execute
            _dragDropByPositionSyntax.To(targetElement);

            // After
            // ...
        }

        public void To(string selector, int targetOffsetX, int targetOffsetY)
        {
            To(_actionSyntaxProvider.Find(selector), targetOffsetX, targetOffsetY);
        }

        public void To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
        {
            // Before
            _logger.LogPartialMessage(" to element with selector" + targetElement.Element.Selector + " with offset x: " + targetOffsetX + " y: " + targetOffsetY, true);

            // Execute
            _dragDropByPositionSyntax.To(targetElement, targetOffsetX, targetOffsetY);

            // After
            // ...
        }
    }
}