// <copyright file="FluentTest.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API;

namespace FluentAutomation.WatiN
{
    public class FluentTest : FluentAutomation.API.FluentTest
    {
        public AutomationProvider Provider = null;

        private CommandManager _actionManager = null;
        public override CommandManager I
        {
            get
            {
                if (_actionManager == null)
                {
                    this.Setup();
                    this.Provider = new AutomationProvider()
                    {
                        ScreenshotOnAssertException = this.ScreenshotOnAssertException,
                        ScreenshotPath = this.ScreenshotPath
                    };
                    _actionManager = new CommandManager(this.Provider);
                }

                return _actionManager;
            }

            set
            {
                _actionManager = value;
            }
        }
    }
}
