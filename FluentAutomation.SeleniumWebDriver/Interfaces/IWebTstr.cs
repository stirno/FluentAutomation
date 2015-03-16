using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWebTstr
    {
        IWebTstr SetBrowserStackCredentials(string username, string password);

        IWebTstr EnableBrowserStackLocal();

        IWebTstr DisableBrowserStackLocal();

        IWebTstr EnableBrowserStackDebug();

        IWebTstr DisableBrowserStackDebug();

        IWebTstr SetUniqueIdentifier(Guid uniqueIdentifier);

        IWebTstr SetCapability(string key, string value);

        IWebTstr UseWebDriver(SeleniumWebDriver.Browser browser);

        IWebTstr UseRemoteWebDriver(string remoteWebUri);

        IWebTstr Bootstrap();
    }
}
