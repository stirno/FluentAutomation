using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands
{
    public class CommandArgumentsTypeAttribute : System.Attribute
    {
        public Type ArgsType { get; set; }

        public CommandArgumentsTypeAttribute(Type type)
        {
            this.ArgsType = type;
        }
    }
}
