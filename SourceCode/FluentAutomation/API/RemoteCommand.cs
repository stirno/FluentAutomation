using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands
{
    public class RemoteCommandDetails
    {
        public string Name { get; set; }
        public Dictionary<string, dynamic> Arguments { get; set; }
    }
}
