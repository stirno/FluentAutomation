using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using FluentAutomation.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            /// PhantomJS - Experimental - Headless browser
            /// </summary>
            PhantomJs = 5,

            /// <summary>
            /// Safari - Experimental - Only usable with a Remote URI
            /// </summary>
            Safari = 6,

            /// <summary>
            /// iPad - Experimental - Only usable with a Remote URI
            /// </summary>
            iPad = 7,

            /// <summary>
            /// iPhone - Experimental - Only usable with a Remote URI
            /// </summary>
            iPhone = 8,

            /// <summary>
            /// Android - Experimental - Only usable with a Remote URI
            /// </summary>
            Android = 9
        }

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
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                var browserDriver = GenerateBrowserSpecificDriver(browser);
                container.Register<IWebDriver>((c, o) => browserDriver());
            };
        }

        public static void Bootstrap(params Browser[] browsers)
        {
            if (browsers.Length == 1)
            {
                Bootstrap(browsers.First());
                return;
            }

            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                FluentTest.IsMultiBrowserTest = true;

                var webDrivers = new List<Func<IWebDriver>>();
                browsers.Distinct().ToList().ForEach(x => webDrivers.Add(GenerateBrowserSpecificDriver(x)));

                var commandProviders = new CommandProviderList(webDrivers.Select(x => new CommandProvider(x, new LocalFileStoreProvider())));
                container.Register<CommandProviderList>(commandProviders);

                container.Register<ICommandProvider, MultiCommandProvider>();
                container.Register<IAssertProvider, MultiAssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="browser"></param>
        public static void Bootstrap(Uri driverUri, Browser browser)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = GenerateDesiredCapabilities(browser);
                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities));
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = GenerateDesiredCapabilities(browser);
                foreach (var cap in capabilities)
                {
                    browserCapabilities.SetCapability(cap.Key, cap.Value);
                }

                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities));
            };
        }

        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = new DesiredCapabilities(capabilities);
                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities));
            };
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser)
        {
            string driverPath = string.Empty;
            switch (browser)
            {
                case Browser.InternetExplorer:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer32.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath)));
                case Browser.InternetExplorer64:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer64.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath)));
                case Browser.Firefox:
                    return new Func<IWebDriver>(() => {
                        return new OpenQA.Selenium.Firefox.FirefoxDriver(new OpenQA.Selenium.Firefox.FirefoxProfile
                        {
                            EnableNativeEvents = true,
                            AcceptUntrustedCertificates = true
                        });
                    });
                case Browser.Chrome:
                    driverPath = EmbeddedResources.UnpackFromAssembly("chromedriver.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                    var chromeService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath));
                    chromeService.SuppressInitialDiagnosticInformation = true;

                    var options = new ChromeOptions();
                    options.AddArgument("--log-level=3");

                    return new Func<IWebDriver>(() => new OpenQA.Selenium.Chrome.ChromeDriver(chromeService, options));
                case Browser.PhantomJs:
                    driverPath = EmbeddedResources.UnpackFromAssembly("phantomjs.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new OpenQA.Selenium.PhantomJS.PhantomJSDriver(Path.GetDirectoryName(driverPath)));
            }

            throw new NotImplementedException("Selected browser " + browser.ToString() + " is not supported yet.");
        }

        private static DesiredCapabilities GenerateDesiredCapabilities(Browser browser)
        {
            DesiredCapabilities browserCapabilities = null;

            switch (browser)
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
                case Browser.Safari:
                    browserCapabilities = DesiredCapabilities.Safari();
                    break;
                case Browser.iPad:
                    browserCapabilities = DesiredCapabilities.IPad();
                    break;
                case Browser.iPhone:
                    browserCapabilities = DesiredCapabilities.IPhone();
                    break;
                case Browser.Android:
                    browserCapabilities = DesiredCapabilities.Android();
                    break;
                default:
                    throw new FluentException("Selected browser [{0}] not supported. Unable to determine appropriate capabilities.", browser.ToString());
            }

            browserCapabilities.IsJavaScriptEnabled = true;
            return browserCapabilities;
        }
    }
}
