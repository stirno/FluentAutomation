using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(TypeArguments))]
    public class Type : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (TypeArguments)arguments;

            Guard.ArgumentNotNullForCommand<Type>(args.Value);

            manager.Type(args.Value);
        }
    }

    public class TypeArguments : ICommandArguments
    {
        public string Value { get; set; }
    }
}
