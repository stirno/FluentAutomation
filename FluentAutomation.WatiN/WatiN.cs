using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using WatiNCore = global::WatiN.Core;

namespace FluentAutomation
{
    public class WatiN
    {
        [Flags]
        public enum Browsers
        {
            InternetExplorer = 1
        }

        public static Browsers SelectedBrowser;

        public static void Bootstrap()
        {
            Bootstrap(Browsers.InternetExplorer);
        }

        public static void Bootstrap(Browsers browser)
        {
            WatiN.SelectedBrowser = browser;

            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
                container.Register<WatiNCore.IE>().UsingConstructor(() => new WatiNCore.IE());
            };
        }
    }
}
