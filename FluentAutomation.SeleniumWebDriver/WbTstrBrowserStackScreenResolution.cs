using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrBrowserStackScreenResolution : IWbTstrBrowserStackScreenResolution
    {
        private readonly IWbTstr _wbTstr;

        public WbTstrBrowserStackScreenResolution(IWbTstr wbTstr)
        {
            _wbTstr = wbTstr;
        }

        /*-------------------------------------------------------------------*/

        public IWbTstr IsAny()
        {
            _wbTstr.RemoveCapability("resolution");

            return _wbTstr;
        }

        public IWbTstr Is1024x768()
        {
            _wbTstr.SetCapability("resolution", "1024x768");

            return _wbTstr;
        }

        public IWbTstr Is1280x800()
        {
            _wbTstr.SetCapability("resolution", "1280x800");

            return _wbTstr;
        }

        public IWbTstr Is1280x1024()
        {
            _wbTstr.SetCapability("resolution", "1280x1024");

            return _wbTstr;
        }

        public IWbTstr Is1366x768()
        {
            _wbTstr.SetCapability("resolution", "1366x768");

            return _wbTstr;
        }

        public IWbTstr Is1440x900()
        {
            _wbTstr.SetCapability("resolution", "1440x900");

            return _wbTstr;
        }

        public IWbTstr Is1680x1050()
        {
            _wbTstr.SetCapability("resolution", "1680x1050");

            return _wbTstr;
        }

        public IWbTstr Is1600x1200()
        {
            _wbTstr.SetCapability("resolution", "1600x1200");

            return _wbTstr;
        }

        public IWbTstr Is1920x1200()
        {
            _wbTstr.SetCapability("resolution", "1920x1200");

            return _wbTstr;
        }

        public IWbTstr Is1920x1080()
        {
            _wbTstr.SetCapability("resolution", "1920x1080");

            return _wbTstr;
        }

        public IWbTstr Is2048x1536()
        {
            _wbTstr.SetCapability("resolution", "2048x1536");

            return _wbTstr;
        }
    }
}
