using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectNullArguments))]
    public class ExpectNull : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectNullArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectNull>(args.Selector);
            string nullString = null;

            if (args.MatchConditions.HasValue)
            {
                manager.Expect.Value(nullString).In(args.Selector, args.MatchConditions.Value);
            }
            else
            {
                manager.Expect.Value(nullString).In(args.Selector);
            }
        }
    }

    public class ExpectNullArguments : ICommandArguments
    {
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
