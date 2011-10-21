using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands
{
    public class RemoteCommand
    {
        public string Name { get; set; }
        public Dictionary<string, string> Arguments { get; set; }
    }
}
