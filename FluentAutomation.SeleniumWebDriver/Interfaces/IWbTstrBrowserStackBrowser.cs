using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstrBrowserStackBrowser
    {
        /// <summary>
        /// It doesn't matter which browser is used.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsAny();

        /// <summary>
        /// Chrome is the prefered browser
        /// </summary>
        /// <param name="version">Optional version number (see BrowserStack documentation)</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsChrome(string version = null);

        /// <summary>
        /// Internet Explorerer is the prefered browser.
        /// </summary>
        /// <param name="version">Optional version number (see BrowserStack documentation)</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsInternetExplorer(string version = null);

        /// <summary>
        /// Firefox is the prefered browser
        /// </summary>
        /// <param name="version">Optional version number (see BrowserStack documentation)</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsFirefox(string version = null);
    }
}
