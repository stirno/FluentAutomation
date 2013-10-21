using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Wrappers
{
    public class EnhancedRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public EnhancedRemoteWebDriver(ICapabilities desiredCapabilities)
            : base(desiredCapabilities)
        {
        }

        public EnhancedRemoteWebDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }

        public EnhancedRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        public EnhancedRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
        }

        public Screenshot GetScreenshot()
        {
            Response response = this.Execute(DriverCommand.Screenshot, null);
            string responseContent = response.Value.ToString();

            return new Screenshot(responseContent);
        }
    }
}