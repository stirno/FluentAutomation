using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;

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
            /// Mozilla Firefox
            /// </summary>
            Firefox = 2,

            /// <summary>
            /// Google Chrome
            /// </summary>
            Chrome = 4
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
                        EmbeddedResources.UnpackFromAssembly("IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, OpenQA.Selenium.IE.InternetExplorerDriver>().AsMultiInstance();
                        break;
                    case Browser.Firefox:
                        container.Register<IWebDriver, OpenQA.Selenium.Firefox.FirefoxDriver>().AsMultiInstance();
                        break;
                    case Browser.Chrome:
                        EmbeddedResources.UnpackFromAssembly("chromedriver.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                        container.Register<IWebDriver, OpenQA.Selenium.Chrome.ChromeDriver>().AsMultiInstance();
                        break;
                }
            };
        }
    }
}
