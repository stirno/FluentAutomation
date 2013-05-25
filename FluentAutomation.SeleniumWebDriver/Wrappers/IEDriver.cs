using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Wrappers
{
    public class IEDriverWrapper : OpenQA.Selenium.IE.InternetExplorerDriver
    {
        public IEDriverWrapper()
            : base(new OpenQA.Selenium.IE.InternetExplorerOptions()
            {
                IgnoreZoomLevel = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnableNativeEvents = true
            })
        {
        }
    }
}
