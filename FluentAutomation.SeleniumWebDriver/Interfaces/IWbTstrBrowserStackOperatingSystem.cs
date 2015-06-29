using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IWbTstrBrowserStackOperatingSystem
    {
        /// <summary>
        /// It doesn't matter which operating system is used.
        /// </summary>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsAny();

        /// <summary>
        /// Windows is the prefered operating system.
        /// </summary>
        /// <param name="version">Optional version number (see BrowserStack documentation)</param>
        /// <returns>Current WbTstr instance</returns>
        IWbTstr IsWindows(string version = null);

        /// <summary>
        /// OS X is the prefered operating system.
        /// </summary>
        /// <param name="version">Optional version number (see BrowserStack documentation)</param>
        /// <returns></returns>
        IWbTstr IsOSX(string version = null);
    }
}
