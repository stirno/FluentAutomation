using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(OpenArguments))]
    public class Open : IRemoteCommand
    {
        public void Execute(CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (OpenArguments)arguments;
            Guard.ArgumentNotNullForCommand<ExpectValue>(args.Url);

            manager.Open(args.Url);
        }
    }

    public class OpenArguments : IRemoteCommandArguments
    {
        public string Url { get; set; }
    }
}
