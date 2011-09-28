// <copyright file="FluentTest.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API;

namespace FluentAutomation.SeleniumWebDriver
{
    public class FluentTest : FluentAutomation.API.FluentTest
    {
        public AutomationProvider Provider = null;

        private ActionManager _actionManager = null;
        public override ActionManager I
        {
            get
            {
                if (_actionManager == null)
                {
                    this.Setup();
                    this.Provider = new AutomationProvider()
                    {
                        ScreenshotPath = this.ScreenshotPath
                    };
                    _actionManager = new ActionManager(this.Provider);
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
