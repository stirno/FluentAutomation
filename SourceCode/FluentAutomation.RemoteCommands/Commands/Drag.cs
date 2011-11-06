using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(DragArguments))]
    public class Drag : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (DragArguments)arguments;

            Guard.ArgumentNotNullForCommand<Drag>(args.From);
            Guard.ArgumentNotNullForCommand<Drag>(args.To);

            manager.Drag(args.From).To(args.To);
        }
    }

    public class DragArguments : IRemoteCommandArguments
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}