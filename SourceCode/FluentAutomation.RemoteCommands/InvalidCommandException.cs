using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands
{
    public class InvalidCommandException<T> : Exception
    {
        public InvalidCommandException() 
            : base(string.Format("Invalid arguments for command: {0}", typeof(T).Name))
        {
        }
    }
}
