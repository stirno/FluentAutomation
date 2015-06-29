using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstr
    {
        /// <summary>
        /// Use the BrowserStack service as remote driver, requires credentials. 
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr UseBrowserStackAsRemoteDriver();

        /// <summary>
        /// Sets the credentials for the BrowserStack service. 
        /// </summary>
        /// <param name="username">BrowserStack username</param>
        /// <param name="password">BrowserStack password</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr SetBrowserStackCredentials(string username, string password);

        /// <summary>
        /// Enables the automatic startup of BrowserStackLocal (VPN).
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr EnableBrowserStackLocal();

        /// <summary>
        /// Disables the automatic startup of BrowserStackLocal (VPN).
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr DisableBrowserStackLocal();

        /// <summary>
        /// Enables grouping of tests in BrowserStack.
        /// </summary>
        /// <param name="projectName">Name of the project</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr EnableBrowserStackProjectGrouping(string projectName);

        /// <summary>
        /// Disables grouping of tests in BrowserStack.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr DisableBrowserStackProjectGrouping();

        /// <summary>
        /// Marks the tests of a run with a unique build identifier.
        /// </summary>
        /// <param name="buildName">Build identifier</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr SetBrowserStackBuildIdentifier(string buildName);

        /// <summary>
        /// Enable debug, including BrowserStack debug.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr EnableDebug();

        /// <summary>
        /// Disables debug, including BrowserStack debug.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr DisableDebug();

        /// <summary>
        /// Enables dryrun mode, might throw exceptions.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr EnableDryRun();

        /// <summary>
        /// Disables dryrun mode.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr DisableDryRun();

        /// <summary>
        /// Sets a custom capability.
        /// </summary>
        /// <param name="key">Capability key</param>
        /// <param name="value">Capability value</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr SetCapability(string key, string value);

        /// <summary>
        /// Removes a custom capability.
        /// </summary>
        /// <param name="key">Capability key</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr RemoveCapability(string key);

        /// <summary>
        /// Specifies the browser to be used as web driver.
        /// </summary>
        /// <param name="browser">Selenium supported browser</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr UseWebDriver(SeleniumWebDriver.Browser browser);

        /// <summary>
        /// Specifies the prefered operating system (BrowserStack).
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstrBrowserStackOperatingSystem PreferedOperatingSystem();

        /// <summary>
        /// Specifies the prefered screen resolution (BrowserStack). 
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstrBrowserStackScreenResolution PreferedScreenResolution();

        /// <summary>
        /// Specifies the prefered browser (BrowserStack).
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstrBrowserStackBrowser PreferedBrowser();

        /// <summary>
        /// Sets a custom remote webdriver uri
        /// </summary>
        /// <param name="remoteWebUri">Remote driver uri</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr UseRemoteWebDriver(string remoteWebUri);

        /// <summary>
        /// Boostraps current WbTstr instance.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr BootstrapInstance();
    }
}
