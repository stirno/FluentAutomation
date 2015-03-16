using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstr : IWebTstr
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

        public static IWebTstr Configure()
        {
            return new WbTstr();
        }

        public IWebTstr SetBrowserStackCredentials(string username, string password)
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

        public IWebTstr EnableBrowserStackLocal()
        {
            _browserStackLocalEnabled = true;
            SetCapability("browserstack.local", "true");
            return this;
        }

        public IWebTstr DisableBrowserStackLocal()
        {
            _browserStackLocalEnabled = false;
            SetCapability("browserstack.local", "false");
            return this;
        }

        public IWebTstr EnableBrowserStackDebug()
        {
            SetCapability("browserstack.debug", "true");
            return this;
        }

        public IWebTstr DisableBrowserStackDebug()
        {
            SetCapability("browserstack.debug", "false");
            return this;
        }

        public IWebTstr SetUniqueIdentifier(Guid uniqueIdentifier)
        {
            if (uniqueIdentifier == null) throw new ArgumentNullException("uniqueIdentifier");

            // We might need this later, so make local reference
            _uniqueIdentifier = string.Format("{0}", uniqueIdentifier);
            
            SetCapability("browserstack.localIdentifier", _uniqueIdentifier);
            return this;
        }

        public IWebTstr SetCapability(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("value is null or empty");

            _capabilities.Add(key, value);
            return this;
        }

        public IWebTstr UseWebDriver(SeleniumWebDriver.Browser browser)
        {
            _localWebDriver = browser;
            return this;
        }

        public IWebTstr UseRemoteWebDriver(string remoteWebDriver)
        {
            if (remoteWebDriver == null) throw new ArgumentException("remoteWebDriver");

            _remoteWebDriver = new Uri(remoteWebDriver);
            return this;
        }

        public IWebTstr Bootstrap()
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
