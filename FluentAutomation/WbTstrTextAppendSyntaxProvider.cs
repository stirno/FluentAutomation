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

        public WbTstrTextAppendSyntaxProvider(WbTstrActionSyntaxProvider actionSyntaxProvider, ITextAppendSyntaxProvider textAppendSyntaxProvider, ILogger logger)
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
                bool? isInDryRunMode = ConfigReader.GetEnvironmentVariableOrAppSettingAsBoolean("WbTstr:DryRunMode");

                return isInDryRunMode.HasValue && isInDryRunMode.Value;
            }
        }

        /*-------------------------------------------------------------------*/

        public ITextAppendSyntaxProvider WithoutEvents()
        {
            // Before
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
            _logger.LogMessage("blablablabla"); // TODO: Elaborate logging

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
