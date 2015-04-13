using System;
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
        private readonly Dictionary<string, object> _capabilities;
        private readonly string _uniqueIdentifier;
        private string _browserStackUsername;
        private string _browserStackPassword;
        private bool _browserStackLocalEnabled;
        private SeleniumWebDriver.Browser _localWebDriver;
        private Uri _remoteWebDriver;
        private bool _disposed;

        private WbTstr(Guid guid)
        {
            _uniqueIdentifier = string.Format("{0}", guid);
            _capabilities = new Dictionary<string, object>();
            _localWebDriver = SeleniumWebDriver.Browser.Chrome;
        }

        ~WbTstr()
        {
            Dispose(false);
        }

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
                _capabilities.Remove(key);
            }

            return this;
        }

        public IWbTstr UseWebDriver(SeleniumWebDriver.Browser browser)
        {
            _localWebDriver = browser;
            return this;
        }

        public IWbTstr UseRemoteWebDriver(string remoteWebDriver)
        {
            if (remoteWebDriver == null) throw new ArgumentException("remoteWebDriver");

            _remoteWebDriver = new Uri(remoteWebDriver);
            return this;
        }

        public IWbTstrBrowserStackOperatingSystem PreferedOperatingSystem()
        {
            return new WbTstrBrowserStackOperatingSystem(this);
        }

        public IWbTstrBrowserStackScreenResolution PreferedScreenResolution()
        {
            return new WbTstrBrowserStackScreenResolution(this);
        }

        public IWbTstrBrowserStackBrowser PreferedBrowser()
        {
            return new WbTstrBrowserStackBrowser(this);
        }

        public IWbTstr BootstrapInstance()
        {
            if (FluentSettings.Current.IsDryRun)
            {
                SeleniumWebDriver.DryRunBootstrap();
            }
            else if (_remoteWebDriver != null)
            {
                if (_browserStackLocalEnabled)
                {
                    BrowserStackLocal.Instance.Start(_browserStackPassword, _uniqueIdentifier);
                }

                SeleniumWebDriver.Bootstrap(_remoteWebDriver, _capabilities);
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

        private static IWbTstr CreateInstance()
        {
            WbTstr wbTstr = new WbTstr(Guid.NewGuid());

            bool? enableDebug = ConfigReader.GetSettingAsBoolean("WbTstr:EnableDebug");
            if (enableDebug.HasValue && enableDebug.Value)
            {
                wbTstr.EnableDebug();
            }

            bool? enableDryRun = ConfigReader.GetSettingAsBoolean("WbTstr:EnableDryRun");
            if (enableDryRun.HasValue && enableDryRun.Value)
            {
                wbTstr.EnableDryRun();
            }

            bool? useBrowserStack = ConfigReader.GetSettingAsBoolean("WbTstr:UseBrowserStack");
            if (useBrowserStack.HasValue && useBrowserStack.Value)
            {
                wbTstr.UseBrowserStackAsRemoteDriver();
            }

            bool? enableBrowserStackLocal = ConfigReader.GetSettingAsBoolean("WbTstr:EnableBrowserStackLocal");
            if (enableBrowserStackLocal.HasValue && enableBrowserStackLocal.Value)
            {
                wbTstr.EnableBrowserStackLocal();
            }

            return wbTstr;
        }
    }
}