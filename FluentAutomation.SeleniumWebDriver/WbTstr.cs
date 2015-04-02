using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstr : IWbTstr
    {
        private readonly Dictionary<string, object> _capabilities;
        private string _browserStackUsername;
        private string _browserStackPassword;
        private bool _browserStackLocalEnabled;
        private SeleniumWebDriver.Browser _localWebDriver;
        private Uri _remoteWebDriver;
        private string _uniqueIdentifier;

        private WbTstr()
        {
            _capabilities = new Dictionary<string, object>();
            _localWebDriver = SeleniumWebDriver.Browser.Chrome;
        }

        /*-------------------------------------------------------------------*/

        public static IWbTstr Configure()
        {
            return new WbTstr();
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
            return this;
        }

        public IWbTstr DisableBrowserStackLocal()
        {
            _browserStackLocalEnabled = false;
            SetCapability("browserstack.local", "false");
            return this;
        }

        public IWbTstr EnableBrowserStackDebug()
        {
            SetCapability("browserstack.debug", "true");
            return this;
        }

        public IWbTstr DisableBrowserStackDebug()
        {
            SetCapability("browserstack.debug", "false");
            return this;
        }

        public IWbTstr SetUniqueIdentifier(Guid uniqueIdentifier)
        {
            if (uniqueIdentifier == null) throw new ArgumentNullException("uniqueIdentifier");

            // We might need this later, so make local reference
            _uniqueIdentifier = string.Format("{0}", uniqueIdentifier);
            
            SetCapability("browserstack.localIdentifier", _uniqueIdentifier);
            return this;
        }

        public IWbTstr SetCapability(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("value is null or empty");

            _capabilities.Add(key, value);
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

        public IWbTstr Bootstrap()
        {
            if (_remoteWebDriver != null)
            {
                if (_browserStackLocalEnabled)
                {
                    SeleniumWebDriver.EnableBrowserStackLocal(_browserStackPassword);                                                   
                }
                SeleniumWebDriver.Bootstrap(_remoteWebDriver, _capabilities);    
            }
            else
            {
                SeleniumWebDriver.Bootstrap(_localWebDriver);
            }

            return this;
        }
    }
}
