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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
