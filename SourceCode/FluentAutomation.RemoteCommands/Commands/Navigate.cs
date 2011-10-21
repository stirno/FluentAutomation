using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(NavigateArguments))]
    public class Navigate : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (NavigateArguments)arguments;

            Guard.ArgumentNotNullForCommand<Navigate>(args.Direction);

            manager.Navigate(args.Direction.Value);
        }
    }

    public class NavigateArguments : ICommandArguments
    {
        public NavigateDirection? Direction { get; set; }
    }
}
