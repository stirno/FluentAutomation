using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstrBrowserStackBrowser
    {
        IWbTstr IsAny();

        IWbTstr IsChrome(string version = null);

        IWbTstr IsInternetExplorer(string version = null);

        IWbTstr IsFirefox(string version = null);
    }
}
