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

            TinyIoC.TinyIoCContainer.Current.Register<IExpectProvider, ExpectProvider>();
            TinyIoC.TinyIoCContainer.Current.Register<ICommandProvider, CommandProvider>();
            TinyIoC.TinyIoCContainer.Current.Register<IFileStoreProvider, LocalFileStoreProvider>();
            TinyIoC.TinyIoCContainer.Current.Register<WatiNCore.Browser, WatiNCore.IE>().UsingConstructor(() => new WatiNCore.IE());
        }
    }
}
