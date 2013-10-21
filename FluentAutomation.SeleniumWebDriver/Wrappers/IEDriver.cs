using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentAutomation.Wrappers
{
    public class IEDriverWrapper : OpenQA.Selenium.IE.InternetExplorerDriver
    {
        public IEDriverWrapper()
            : this(Path.GetTempPath())
        {
        }

        public IEDriverWrapper(string ieDriverDirectoryPath)
            : base(ieDriverDirectoryPath, new OpenQA.Selenium.IE.InternetExplorerOptions()
            {
                IgnoreZoomLevel = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnableNativeEvents = true
            })
        {
        }
    }
}
