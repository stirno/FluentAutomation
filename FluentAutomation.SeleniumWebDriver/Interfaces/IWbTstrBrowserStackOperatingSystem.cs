using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstrBrowserStackOperatingSystem
    {
        IWbTstr IsAny();

        IWbTstr IsWindows(string version = null);

        IWbTstr IsOSX(string version = null);
    }
}
