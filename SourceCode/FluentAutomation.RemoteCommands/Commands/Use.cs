using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(UseArguments))]
    public class Use : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            // for the JSON-based API, this command is used to send a set of browsers
            // to test in and is not executed directly.
            throw new NotImplementedException();
        }
    }

    public class UseArguments : ICommandArguments
    {
        public List<BrowserType> BrowserType { get; set; }
    }
}
