using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstr
    {
        IWbTstr SetBrowserStackCredentials(string username, string password);

        IWbTstr EnableBrowserStackLocal();

        IWbTstr DisableBrowserStackLocal();

        IWbTstr EnableBrowserStackDebug();

        IWbTstr DisableBrowserStackDebug();

        IWbTstr SetUniqueIdentifier(Guid uniqueIdentifier);

        IWbTstr SetCapability(string key, string value);

        IWbTstr RemoveCapability(string key);

        IWbTstr UseWebDriver(SeleniumWebDriver.Browser browser);

        IWbTstrBrowserStackOperatingSystem PreferedOperatingSystem();

        IWbTstrBrowserStackScreenResolution PreferedScreenResolution();

        IWbTstrBrowserStackBrowser PreferedBrowser();

        IWbTstr UseRemoteWebDriver(string remoteWebUri);

        IWbTstr Bootstrap();
    }
}
