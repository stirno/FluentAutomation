using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrAssertClassSyntaxProvider : IAssertClassSyntaxProvider
    {
        private readonly WbTstrAssertSyntaxProvider _assertSyntaxProvider;
        private readonly IAssertClassSyntaxProvider _assertClassSyntaxProvider;
        private readonly ILogger _logger;

        internal WbTstrAssertClassSyntaxProvider(WbTstrAssertSyntaxProvider assertSyntaxProvider, IAssertClassSyntaxProvider assertClassSyntaxProvider, ILogger logger)
        {
            _assertSyntaxProvider = assertSyntaxProvider;
            _assertClassSyntaxProvider = assertClassSyntaxProvider;
            _logger = logger;
        }
      
        /*-------------------------------------------------------------------*/

        public IAssertClassSyntaxProvider Not
        {
            get
            {
                return new WbTstrAssertClassSyntaxProvider(_assertSyntaxProvider, _assertClassSyntaxProvider.Not, _logger);
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
                _assertClassSyntaxProvider.On(selector);
            }

            // After
            return _assertSyntaxProvider;
        }

        public IAssertSyntaxProvider On(ElementProxy element)
        {
            // Before
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

            // Execute
            if (!IsInDryRunMode)
            {
                _assertClassSyntaxProvider.On(element);
            }

            // After
            return _assertSyntaxProvider;
        }
    }
}
