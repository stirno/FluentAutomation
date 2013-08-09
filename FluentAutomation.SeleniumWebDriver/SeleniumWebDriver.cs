using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace FluentAutomation
{
    /// <summary>
    /// Selenium WebDriver FluentAutomation Provider
    /// </summary>
    public class SeleniumWebDriver
    {
        /// <summary>
        /// Supported browsers for the FluentAutomation Selenium provider.
        /// </summary>
        public enum Browser
        {
            /// <summary>
            /// Internet Explorer. Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer = 1,

            /// <summary>
            /// Internet Explorer (64-bit). Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer64 = 2,

            /// <summary>
            /// Mozilla Firefox
            /// </summary>
            Firefox = 3,

            /// <summary>
            /// Google Chrome
            /// </summary>
            Chrome = 4,

            /// <summary>
            /// PhantomJS - Headless browser - Support is Experimental
            /// </summary>
            PhantomJs = 5
        }

        /// <summary>
        /// Currently selected <see cref="Browser"/>.
        /// </summary>
        public static Browser SelectedBrowser;

        /// <summary>
        /// Bootstrap Selenium provider and utilize Firefox.
        /// </summary>
        public static void Bootstrap()
        {
            Bootstrap(Browser.Firefox);
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser"></param>
        public static void Bootstrap(Browser browser)
        {
            SeleniumWebDriver.SelectedBrowser = browser;

            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            
                switch (SeleniumWebDriver.SelectedBrowser)
                {
                    case Browser.InternetExplorer:
                        EmbeddedResources.UnpackFromAssembly("IEDriverServer32.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, Wrappers.IEDriverWrapper>().AsMultiInstance();
                        break;
                    case Browser.InternetExplorer64:
                        EmbeddedResources.UnpackFromAssembly("IEDriverServer64.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, Wrappers.IEDriverWrapper>().AsMultiInstance();
                        break;
                    case Browser.Firefox:
                        container.Register<IWebDriver, OpenQA.Selenium.Firefox.FirefoxDriver>().AsMultiInstance();
                        break;
                    case Browser.Chrome:
                        EmbeddedResources.UnpackFromAssembly("chromedriver.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, OpenQA.Selenium.Chrome.ChromeDriver>().AsMultiInstance();
                        break;
                    case Browser.PhantomJs:
                        EmbeddedResources.UnpackFromAssembly("phantomjs.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, OpenQA.Selenium.PhantomJS.PhantomJSDriver>().AsMultiInstance();
                        break;
                }
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targetting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        public static void Bootstrap(Uri driverUri, Browser browser)
        {
            SeleniumWebDriver.SelectedBrowser = browser;

            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = null;

                switch (SeleniumWebDriver.SelectedBrowser)
                {
                    case Browser.InternetExplorer:
                    case Browser.InternetExplorer64:
                        browserCapabilities = DesiredCapabilities.InternetExplorer();
                        break;
                    case Browser.Firefox:
                        browserCapabilities = DesiredCapabilities.Firefox();
                        break;
                    case Browser.Chrome:
                        browserCapabilities = DesiredCapabilities.Chrome();
                        break;
                    case Browser.PhantomJs:
                        browserCapabilities = DesiredCapabilities.PhantomJS();
                        break;
                }

                container.Register<IWebDriver, RemoteWebDriver>(new RemoteWebDriver(driverUri, browserCapabilities));
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities)
        {
            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = null;

                switch (SeleniumWebDriver.SelectedBrowser)
                {
                    case Browser.InternetExplorer:
                    case Browser.InternetExplorer64:
                        browserCapabilities = DesiredCapabilities.InternetExplorer();
                        break;
                    case Browser.Firefox:
                        browserCapabilities = DesiredCapabilities.Firefox();
                        break;
                    case Browser.Chrome:
                        browserCapabilities = DesiredCapabilities.Chrome();
                        break;
                    case Browser.PhantomJs:
                        browserCapabilities = DesiredCapabilities.PhantomJS();
                        break;
                }

                if (browserCapabilities == null)
                {
                    browserCapabilities = new DesiredCapabilities(capabilities);
                }
                else
                {
                    foreach (var cap in capabilities)
                    {
                        browserCapabilities.SetCapability(cap.Key, cap.Value);
                    }
                }

                container.Register<IWebDriver, RemoteWebDriver>(new RemoteWebDriver(driverUri, browserCapabilities));
            };
        }
    }
}
