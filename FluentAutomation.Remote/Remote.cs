using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class Remote
    {
        public static void Bootstrap()
        {
            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<IRemoteCommandProvider, RemoteCommandProvider>();
                container.Register<IRemoteExpectProvider, RemoteExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            };
        }
    }
}
