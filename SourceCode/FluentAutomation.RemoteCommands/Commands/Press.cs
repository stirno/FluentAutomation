using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(PressArguments))]
    public class Press : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (PressArguments)arguments;

            Guard.ArgumentNotNullForCommand<Press>(args.Keys);

            manager.Press(args.Keys);
        }
    }

    public class PressArguments : ICommandArguments
    {
        public string Keys { get; set; }
    }
}
