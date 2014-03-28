using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using WatiNCore = global::WatiN.Core;

namespace FluentAutomation
{
    /// <summary>
    /// WatiN FluentAutomation Provider
    /// </summary>
    public class WatiN
    {
        /// <summary>
        /// Supported browsers for the FluentAutomation WatiN provider.
        /// </summary>
        public enum Browser
        {
            /// <summary>
            /// Internet Explorer
            /// </summary>
            InternetExplorer = 1
        }

        /// <summary>
        /// Currently selected <see cref="Browser"/>.
        /// </summary>
        public static Browser SelectedBrowser;

        /// <summary>
        /// Bootstrap WatiN provider and utilize Internet Explorer.
        /// </summary>
        public static void Bootstrap()
        {
            Bootstrap(Browser.InternetExplorer);
        }

        /// <summary>
        /// Bootstrap WatiN provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser"></param>
        public static void Bootstrap(Browser browser)
        {
            WatiN.SelectedBrowser = browser;

            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
                container.Register<WatiNCore.IE>().UsingConstructor(() => new WatiNCore.IE());
            };
        }
    }
}
