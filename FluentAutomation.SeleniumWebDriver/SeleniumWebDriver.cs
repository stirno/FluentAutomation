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
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

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
            /// Microsoft Internet Explorer. Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer = 1,

            /// <summary>
            /// Microsoft Internet Explorer (64-bit). Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer64 = 2,

            /// <summary>
            /// Microsoft Edge
            /// </summary>
            Edge = 3,

            /// <summary>
            /// Mozilla Firefox
            /// </summary>
            Firefox = 4,

            /// <summary>
            /// Google Chrome
            /// </summary>
            Chrome = 5,

            /// <summary>
            /// Opera
            /// </summary>
            Opera = 6,

            /// <summary>
            /// Safari
            /// </summary>
            Safari = 7
        }

        private static readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        public static void Bootstrap(Browser browser = Browser.Firefox)
        {
            Bootstrap(browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        public static void Bootstrap(Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                var browserDriver = GenerateBrowserSpecificDriver(browser, commandTimeout);
                container.Register((c, o) => browserDriver());
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browsers"/>.
        /// </summary>
        public static void Bootstrap(params Browser[] browsers)
        {
            Bootstrap(DefaultCommandTimeout, browsers);
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browsers"/>.
        /// </summary>
        public static void Bootstrap(TimeSpan commandTimeout, params Browser[] browsers)
        {
            if (browsers.Length == 1)
            {
                Bootstrap(browsers.First());
                return;
            }

            FluentSettings.Current.ContainerRegistration = container =>
            {
                FluentTest.IsMultiBrowserTest = true;

                var webDrivers = new List<Func<IWebDriver>>();
                browsers.Distinct().ToList().ForEach(x => webDrivers.Add(GenerateBrowserSpecificDriver(x, commandTimeout)));

                container.Register(new CommandProviderList(webDrivers.Select(x => new CommandProvider(x, new LocalFileStoreProvider()))));
                container.Register<ICommandProvider, MultiCommandProvider>();
                container.Register<IAssertProvider, MultiAssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            };
        }
        
        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser.
        /// </summary>
        public static void Bootstrap(Uri driverUri, Browser browser)
        {
            Bootstrap(driverUri, browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser.
        /// </summary>
        public static void Bootstrap(Uri driverUri, Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = container =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                container.Register<IWebDriver, RemoteWebDriver>(new RemoteWebDriver(driverUri, GenerateDriverOptions(browser).ToCapabilities(), commandTimeout));
            };
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities.
        /// </summary>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities)
        {
            Bootstrap(driverUri, browser, capabilities, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities.
        /// </summary>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DriverOptions driverOptions = GenerateDriverOptions(browser);
                foreach (var capability in capabilities)
                {
                    driverOptions.AddAdditionalCapability(capability.Key, capability.Value);
                }

                container.Register<IWebDriver, RemoteWebDriver>(new RemoteWebDriver(driverUri, driverOptions.ToCapabilities(), commandTimeout));
            };
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser, TimeSpan commandTimeout)
        {
            switch (browser)
            {
                case Browser.InternetExplorer:
                {
                    string driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer32.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return () => new IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout);
                }
                case Browser.InternetExplorer64:
                {
                    string driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer64.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return () => new IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout);
                }
                case Browser.Firefox:
                    return () =>
                    {
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.AddAdditionalCapability(FirefoxDriver.ProfileCapabilityName, new FirefoxProfile
                        {
                            EnableNativeEvents = false,
                            AcceptUntrustedCertificates = true,
                            AlwaysLoadNoFocusLibrary = true
                        });

                        return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), firefoxOptions, commandTimeout);
                    };
                case Browser.Chrome:
                {
                    string driverPath = EmbeddedResources.UnpackFromAssembly("chromedriver.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath));
                    chromeService.SuppressInitialDiagnosticInformation = true;

                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--log-level=3");


                    string dir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    chromeOptions.AddArgument($"user-data-dir={dir}");

                    return () =>
                    {
                        var driver = new ChromeDriver(chromeService, chromeOptions, commandTimeout);
                        return driver;
                    };
                }
            }

            throw new NotImplementedException("Selected browser " + browser + " is not supported yet.");
        }

        private static DriverOptions GenerateDriverOptions(Browser browser)
        {
            DriverOptions driverOptions;

            switch (browser)
            {
                case Browser.InternetExplorer:
                case Browser.InternetExplorer64:
                    driverOptions = new InternetExplorerOptions();
                    break;
                case Browser.Edge:
                    driverOptions = new EdgeOptions();
                    break;
                case Browser.Firefox:
                    driverOptions = new FirefoxOptions();
                    break;
                case Browser.Chrome:
                    driverOptions = new ChromeOptions();
                    break;
                case Browser.Opera:
                    driverOptions = new OperaOptions();
                    break;
                case Browser.Safari:
                    driverOptions = new SafariOptions();
                    break;
                default:
                    throw new FluentException("Selected browser [{0}] not supported. Unable to determine appropriate capabilities.", browser.ToString());
            }

            driverOptions.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);
            return driverOptions;
        }
    }
}
