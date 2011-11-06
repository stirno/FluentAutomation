using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(PressArguments))]
    public class Press : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (PressArguments)arguments;

            Guard.ArgumentNotNullForCommand<Press>(args.Keys);

            manager.Press(args.Keys);
        }
    }

    public class PressArguments : IRemoteCommandArguments
    {
        public string Keys { get; set; }
    }
}
