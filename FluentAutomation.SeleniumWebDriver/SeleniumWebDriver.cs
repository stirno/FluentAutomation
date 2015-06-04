using System.ComponentModel;
using System.Diagnostics;
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

using Polly;

using TinyIoC;

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

        private static TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

        public static void DryRunBootstrap()
        {
            FluentSettings.Current.ContainerRegistration = SetupContainer;
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
            Bootstrap(browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser"></param>
        public static void Bootstrap(Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                SetupContainer(container);

                var browserDriver = GenerateBrowserSpecificDriver(browser, commandTimeout);
                container.Register<IWebDriver>((c, o) => browserDriver());
            };
        }
        
        public static void Bootstrap(params Browser[] browsers)
        {
            Bootstrap(DefaultCommandTimeout, browsers);
        }

        public static void Bootstrap(TimeSpan commandTimeout, params Browser[] browsers)
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
                browsers.Distinct().ToList().ForEach(x => webDrivers.Add(GenerateBrowserSpecificDriver(x, commandTimeout)));

                var commandProviders = new CommandProviderList(webDrivers.Select(x => new CommandProvider(x, new LocalFileStoreProvider())));
                container.Register<CommandProviderList>(commandProviders);

                container.Register<ICommandProvider, MultiCommandProvider>();
                container.Register<IAssertProvider, MultiAssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
                container.Register<ActionSyntaxProvider>();
                container.Register<FluentSettings>((c, o) => FluentSettings.Current);
                container.Register<ILogger, ConsoleLogger>();
            };
        }
        
        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="browser"></param>
        public static void Bootstrap(Uri driverUri, Browser browser)
        {
            Bootstrap(driverUri, browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="browser"></param>
        /// <param name="commandTimeout"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                SetupContainer(container);

                DesiredCapabilities browserCapabilities = GenerateDesiredCapabilities(browser);
                container.Register<IWebDriver, RemoteWebDriver>(CreateEnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }
        
        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities)
        {
            Bootstrap(driverUri, browser, capabilities, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        /// <param name="commandTimeout"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                SetupContainer(container);

                DesiredCapabilities browserCapabilities = GenerateDesiredCapabilities(browser);
                foreach (var cap in capabilities)
                {
                    browserCapabilities.SetCapability(cap.Key, cap.Value);
                }

                container.Register<IWebDriver, RemoteWebDriver>(CreateEnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }
        
        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities)
        {
            Bootstrap(driverUri, capabilities, DefaultCommandTimeout);
        }

        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                SetupContainer(container);

                DesiredCapabilities browserCapabilities = new DesiredCapabilities(capabilities);
                container.Register<IWebDriver, RemoteWebDriver>(CreateEnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser)
        {
            return GenerateBrowserSpecificDriver(browser, DefaultCommandTimeout);
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser, TimeSpan commandTimeout)
        {
            string driverPath = string.Empty;
            switch (browser)
            {
                case Browser.InternetExplorer:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer32.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout));
                case Browser.InternetExplorer64:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer64.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout));
                case Browser.Firefox:
                    return new Func<IWebDriver>(() => {
                        var firefoxBinary = new OpenQA.Selenium.Firefox.FirefoxBinary();
                        return new OpenQA.Selenium.Firefox.FirefoxDriver(firefoxBinary, new OpenQA.Selenium.Firefox.FirefoxProfile
                        {
                            EnableNativeEvents = false,
                            AcceptUntrustedCertificates = true,
                            AlwaysLoadNoFocusLibrary = true
                        }, commandTimeout);
                    });
                case Browser.Chrome:
                    //Providing an unique name for the chromedriver makes it possible to run multiple instances
                    var uniqueName = string.Format("chromedriver{0}.exe", Guid.NewGuid());
                    driverPath = EmbeddedResources.UnpackFromAssembly("chromedriver.exe", uniqueName , Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    var chromeService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath),
                        uniqueName);
                    chromeService.SuppressInitialDiagnosticInformation = true;
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--log-level=3");

                    return new Func<IWebDriver>(() => new OpenQA.Selenium.Chrome.ChromeDriver(chromeService, chromeOptions, commandTimeout));
                case Browser.PhantomJs:
                    driverPath = EmbeddedResources.UnpackFromAssembly("phantomjs.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                    var phantomOptions = new OpenQA.Selenium.PhantomJS.PhantomJSOptions();
                    return new Func<IWebDriver>(() => new OpenQA.Selenium.PhantomJS.PhantomJSDriver(Path.GetDirectoryName(driverPath), phantomOptions, commandTimeout));
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

        private static void SetupContainer(TinyIoCContainer container)
        {
            container.Register<ActionSyntaxProvider>();
            container.Register<FluentSettings>((c, o) => FluentSettings.Current);
            container.Register<ILogger, ConsoleLogger>();
            container.Register<ICommandProvider, CommandProvider>();
            container.Register<IAssertProvider, AssertProvider>();
            container.Register<IFileStoreProvider, LocalFileStoreProvider>();            
        }

        internal static EnhancedRemoteWebDriver CreateEnhancedRemoteWebDriver(Uri driverUri, DesiredCapabilities browserCapabilities, TimeSpan commandTimeout)
        {
            const int NumberOfRetries = 10;
            try
            {
                var policy = Policy.Handle<Exception>().WaitAndRetry(10, i => TimeSpan.FromSeconds(6));
                return policy.Execute(() => new EnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create a enhanced RemoteWebDriver. Retried {0} times.", NumberOfRetries);           
                throw;
            }
        }
    }
}
