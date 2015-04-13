using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstrBrowserStackScreenResolution
    {
        IWbTstr IsAny();

        IWbTstr Is1024x768();

        IWbTstr Is1280x800();

        IWbTstr Is1280x1024();

        IWbTstr Is1366x768();

        IWbTstr Is1440x900();

        IWbTstr Is1680x1050();

        IWbTstr Is1600x1200();

        IWbTstr Is1920x1200();

        IWbTstr Is1920x1080();

        IWbTstr Is2048x1536();
    }
}
