using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;
using FluentAutomation.Wrappers;

using OpenQA.Selenium;

namespace FluentAutomation
{
    public class WbTstr : IWbTstr, IDisposable
    {
        private static readonly object _mutex = string.Empty;
        private static IWbTstr _instance;
        private readonly ConcurrentDictionary<string, object> _capabilities;
        private readonly string _uniqueIdentifier;
        private string _browserStackUsername;
        private string _browserStackPassword;
        private bool _browserStackLocalEnabled;
        private bool _browserStackUseProxy;
        private string _browserStackLocalFolder;
        private bool _browserStackOnlyAutomate;
        private bool _browserStackForceLocal;
        private string _browserStackProxyHost;
        private int? _browserStackProxyPort;
        private string _browserStackProxyUser;
        private string _browserStackProxyPassword;
        private SeleniumWebDriver.Browser _localWebDriver;

        private bool _disposed;

        private WbTstr(Guid guid)
        {
            _uniqueIdentifier = string.Format("{0}", guid);
            _capabilities = new ConcurrentDictionary<string, object>();
            _localWebDriver = SeleniumWebDriver.Browser.Chrome;
        }

        ~WbTstr()
        {
            Dispose(false);
        }

        /*-------------------------------------------------------------------*/

        internal Dictionary<string, object> Capabilities
        {
            get
            {
                return _capabilities.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        internal Uri RemoteDriverUri { get; private set; }

        /*-------------------------------------------------------------------*/

        public static IWbTstr Configure()
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = CreateInstance();
                    }
                }
            }

            return _instance;
        }

        public static IWbTstr Bootstrap()
        {
            return Configure().BootstrapInstance();
        }

        public IWbTstr UseBrowserStackAsRemoteDriver()
        {
            UseRemoteWebDriver("http://hub.browserstack.com/wd/hub/");

            // Try to get browserstack username and password from configuration
            if (_browserStackUsername == null && _browserStackPassword == null)
            {
                string username = ConfigReader.GetSetting("BrowserStackUsername");
                string password = ConfigReader.GetSetting("BrowserStackPassword");

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    SetBrowserStackCredentials(username, password);
                }
            }
            return this;
        }

        public IWbTstr SetBrowserStackCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("username is null or empty");
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password is null or empty");

            // We might need this later, so make local reference
            _browserStackUsername = username;
            _browserStackPassword = password;

            SetCapability("browserstack.user", username);
            SetCapability("browserstack.key", password);
            return this;
        }

        public IWbTstr EnableBrowserStackLocal()
        {
            _browserStackLocalEnabled = true;
            SetCapability("browserstack.local", "true");
            SetCapability("browserstack.localIdentifier", _uniqueIdentifier);
            return this;
        }

        public IWbTstr DisableBrowserStackLocal()
        {
            _browserStackLocalEnabled = false;
            SetCapability("browserstack.local", "false");
            RemoveCapability("browserstack.localIdentifier");
            return this;
        }

        public IWbTstr EnableBrowserStackProjectGrouping(string projectName)
        {
            SetCapability("project", projectName);

            return this;
        }

        public IWbTstr DisableBrowserStackProjectGrouping()
        {
            RemoveCapability("project");

            return this;
        }

        public IWbTstr SetBrowserStackBuildIdentifier(string buildName)
        {
            SetCapability("build", buildName);

            return this;
        }

        public IWbTstr EnableDebug()
        {
            FluentSettings.Current.InDebugMode = true;
            SetCapability("browserstack.debug", "true");
            return this;
        }

        public IWbTstr DisableDebug()
        {
            FluentSettings.Current.InDebugMode = false;
            SetCapability("browserstack.debug", "false");
            return this;
        }

        public IWbTstr EnableDryRun()
        {
            FluentSettings.Current.IsDryRun = true;
            return this;
        }

        public IWbTstr DisableDryRun()
        {
            FluentSettings.Current.IsDryRun = false;
            return this;
        }

        public IWbTstr SetCapability(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("value is null or empty");

            _capabilities[key] = value;
            return this;
        }

        public IWbTstr RemoveCapability(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");

            if (_capabilities.ContainsKey(key))
            {
                object removed = null;
                _capabilities.TryRemove(key, out removed);
            }

            return this;
        }

        public IWbTstr UseWebDriver(SeleniumWebDriver.Browser browser)
        {
            DisableBrowserStackLocal();

            RemoteDriverUri = null;
            _localWebDriver = browser;
            return this;
        }

        public IWbTstr UseRemoteWebDriver(string remoteWebDriver)
        {
            if (remoteWebDriver == null) throw new ArgumentException("remoteWebDriver");

            RemoteDriverUri = new Uri(remoteWebDriver);
            return this;
        }


        private static IWbTstr CreateInstance()
        {
            WbTstr wbTstr = new WbTstr(Guid.NewGuid());

            bool? enableDebug = ConfigReader.GetSettingAsBoolean("EnableDebug");
            if (enableDebug.HasValue && enableDebug.Value)
            {
                wbTstr.EnableDebug();
            }

            bool? enableDryRun = ConfigReader.GetSettingAsBoolean("EnableDryRun");
            if (enableDryRun.HasValue && enableDryRun.Value)
            {
                wbTstr.EnableDryRun();
            }

            string useWebDriver = ConfigReader.GetSetting("UseWebDriver");
            if (!String.IsNullOrWhiteSpace(useWebDriver))
            {
                SeleniumWebDriver.Browser browser;
                if (Enum.TryParse(useWebDriver, true, out browser))
                {
                    wbTstr.UseWebDriver(browser);
                }
            }

            string buildKey = ConfigReader.GetSetting("BuildResultKey");
            if (!String.IsNullOrWhiteSpace(buildKey))
            {
                wbTstr.SetBrowserStackBuildIdentifier(buildKey);
            }

            bool? useBrowserStack = ConfigReader.GetSettingAsBoolean("UseBrowserStack");
            if (useBrowserStack.HasValue && useBrowserStack.Value)
            {
                wbTstr.UseBrowserStackAsRemoteDriver();
            }

            bool? enableBrowserStackLocal = ConfigReader.GetSettingAsBoolean("EnableBrowserStackLocal");
            if (enableBrowserStackLocal.HasValue && enableBrowserStackLocal.Value)
            {
                wbTstr.EnableBrowserStackLocal();
            }

            string browserStackProject = ConfigReader.GetSetting("BrowserStackProject");
            if (!string.IsNullOrEmpty(browserStackProject))
            {
                wbTstr.EnableBrowserStackProjectGrouping(browserStackProject);
            }

            return wbTstr;
        }

        public IWbTstr SetBrowserStackLocalFolder(string path)
        {
            _browserStackLocalFolder = path;
            return this;
        }

        public IWbTstr DisableBrowserStackLocalFolder()
        {
            _browserStackLocalFolder = null;
            return this;
        }

        public IWbTstr EnableBrowserStackOnlyAutomate()
        {
            _browserStackOnlyAutomate = true;
            return this;
        }

        public IWbTstr DisableBrowserStackOnlyAutomate()
        {
            _browserStackOnlyAutomate = false;
            return this;
        }

        public IWbTstr EnableBrowserStackForceLocal()
        {
            _browserStackForceLocal = true;
            return this;
        }

        public IWbTstr DisableBrowserStackForceLocal()
        {
            _browserStackForceLocal = false;
            return this;
        }

        public IWbTstr SetBrowserStackProxyHost(string host)
        {
            _browserStackUseProxy = true;
            _browserStackProxyHost = host;
            return this;
        }

        public IWbTstr SetBrowserStackProxyPort(int port)
        {
            _browserStackUseProxy = true;
            _browserStackProxyPort = port;
            return this;
        }

        public IWbTstr SetBrowserStackProxyUser(string user)
        {
            _browserStackUseProxy = true;
            _browserStackProxyUser = user;
            return this;
        }

        public IWbTstr SetBrowserStackProxyPassword(string password)
        {
            _browserStackUseProxy = true;
            _browserStackProxyPassword = password;
            return this;
        }

        public IWbTstr DisableBrowserStackProxy()
        {
            _browserStackUseProxy = false;
            return this;
        }


        public IWbTstrBrowserStackOperatingSystem PreferedBrowserStackOperatingSystem()
        {
            return new WbTstrBrowserStackOperatingSystem(this);
        }

        public IWbTstrBrowserStackScreenResolution PreferedBrowserStackScreenResolution()
        {
            return new WbTstrBrowserStackScreenResolution(this);
        }

        public IWbTstrBrowserStackBrowser PreferedBrowserStackBrowser()
        {
            return new WbTstrBrowserStackBrowser(this);
        }

        public IWbTstr BootstrapInstance()
        {
            if (FluentSettings.Current.IsDryRun)
            {
                SeleniumWebDriver.DryRunBootstrap();
            }
            else if (RemoteDriverUri != null)
            {
                if (_browserStackLocalEnabled)
                {
                    string arguments = BrowserStackLocal.Instance.BuildArguments(_browserStackPassword,
                        _browserStackLocalFolder,
                        _browserStackOnlyAutomate,
                        _browserStackForceLocal,
                        _browserStackUseProxy,
                        _browserStackProxyHost,
                        _browserStackProxyPort,
                        _browserStackProxyUser,
                        _browserStackProxyPassword);

                    BrowserStackLocal.Instance.Start(_uniqueIdentifier, arguments);
                }

                SeleniumWebDriver.Bootstrap(RemoteDriverUri, Capabilities);
            }
            else
            {
                SeleniumWebDriver.Bootstrap(_localWebDriver);
            }

            return this;
        }

        /*-------------------------------------------------------------------*/

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                // Now disposed of any unmanaged objects
                BrowserStackLocal.Instance.Stop(_uniqueIdentifier);

                _disposed = true;
            }
        }

        /*-------------------------------------------------------------------*/
    }
}