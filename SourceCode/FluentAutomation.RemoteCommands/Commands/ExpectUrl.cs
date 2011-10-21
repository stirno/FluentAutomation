using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectUrlArguments))]
    public class ExpectUrl : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectUrlArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectUrl>(args.URL);

            manager.Expect.Url(args.URL);
        }
    }

    public class ExpectUrlArguments : ICommandArguments
    {
        public string URL { get; set; }
    }
}
