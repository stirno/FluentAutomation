using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(TypeArguments))]
    public class Type : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (TypeArguments)arguments;

            Guard.ArgumentNotNullForCommand<Type>(args.Value);

            manager.Type(args.Value);
        }
    }

    public class TypeArguments : IRemoteCommandArguments
    {
        public string Value { get; set; }
    }
}
