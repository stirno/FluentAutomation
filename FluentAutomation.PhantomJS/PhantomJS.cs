using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    /// <summary>
    /// PhantomJS FluentAutomation Provider
    /// </summary>
    public class PhantomJS
    {
        /// <summary>
        /// Bootstrap PhantomJS provider.
        /// </summary>
        public static void Bootstrap()
        {
            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            };
        }
    }
}
