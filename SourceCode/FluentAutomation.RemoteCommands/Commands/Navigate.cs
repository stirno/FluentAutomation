using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(NavigateArguments))]
    public class Navigate : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (NavigateArguments)arguments;

            Guard.ArgumentNotNullForCommand<Navigate>(args.Direction);

            manager.Navigate(args.Direction.Value);
        }
    }

    public class NavigateArguments : IRemoteCommandArguments
    {
        public NavigateDirection? Direction { get; set; }
    }
}
