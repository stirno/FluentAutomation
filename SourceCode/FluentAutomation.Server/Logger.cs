using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FluentAutomation.Server
{
    public class Logger
    {
        public Logger()
        {
        }

        public static void Network(string message)
        {
            NLog.Logger log = NLog.LogManager.GetLogger("network");
            log.Debug(message);
        }
    }
}
