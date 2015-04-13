using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrBrowserStackBrowser : IWbTstrBrowserStackBrowser
    {
        private readonly IWbTstr _wbTstr;

        public WbTstrBrowserStackBrowser(IWbTstr wbTstr)
        {
            _wbTstr = wbTstr;
        }

        /*-------------------------------------------------------------------*/

        public IWbTstr IsAny()
        {
            _wbTstr.RemoveCapability("browser");
            _wbTstr.RemoveCapability("browser_version");

            return _wbTstr;
        }

        public IWbTstr IsChrome(string version = null)
        {
            _wbTstr.SetCapability("browser", "Chrome");
            SetBrowserVersion(version);

            return _wbTstr;
        }

        public IWbTstr IsInternetExplorer(string version = null)
        {
            _wbTstr.SetCapability("browser", "IE");
            SetBrowserVersion(version);

            return _wbTstr;
        }

        public IWbTstr IsFirefox(string version = null)
        {
            _wbTstr.SetCapability("browser", "Firefox");
            SetBrowserVersion(version);
            
            return _wbTstr;
        }

        private void SetBrowserVersion(string version)
        {
            if (version != null)
            {
                _wbTstr.SetCapability("browser_version", version);
            }
            else
            {
                _wbTstr.RemoveCapability("browser_version");
            }
        }
    }
}
