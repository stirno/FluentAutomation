using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(DragArguments))]
    public class Drag : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (DragArguments)arguments;

            Guard.ArgumentNotNullForCommand<Drag>(args.From);
            Guard.ArgumentNotNullForCommand<Drag>(args.To);

            manager.Drag(args.From).To(args.To);
        }
    }

    public class DragArguments : ICommandArguments
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}