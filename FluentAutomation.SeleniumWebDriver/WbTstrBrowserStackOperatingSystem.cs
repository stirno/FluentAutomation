using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrBrowserStackOperatingSystem : IWbTstrBrowserStackOperatingSystem
    {
        private readonly IWbTstr _wbTstr;

        public WbTstrBrowserStackOperatingSystem(IWbTstr wbTstr)
        {
            _wbTstr = wbTstr;
        }

        /*-------------------------------------------------------------------*/

        public IWbTstr IsAny()
        {
            _wbTstr.RemoveCapability("os");
            _wbTstr.RemoveCapability("os_version");

            return _wbTstr;
        }
       
        public IWbTstr IsWindows(string version = null)
        {
            _wbTstr.SetCapability("os", "Windows");
            SetOperatingSystemVersion(version);

            return _wbTstr;
        }

        public IWbTstr IsOSX(string version = null)
        {
            _wbTstr.SetCapability("os", "OS X");
            SetOperatingSystemVersion(version);

            return _wbTstr;
        }

        private void SetOperatingSystemVersion(string version)
        {
            if (version != null)
            {
                _wbTstr.SetCapability("os_version", version);
            }
            else
            {
                _wbTstr.RemoveCapability("os_version");
            }
        }
    }
}