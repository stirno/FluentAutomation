using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    public class CommandBase
    {
        protected AutomationProvider Provider { get; set; }
        protected CommandManager CommandManager { get; set; }

        public CommandBase(AutomationProvider provider, CommandManager manager)
        {
            Provider = provider;
            CommandManager = manager;
        }
    }
}
