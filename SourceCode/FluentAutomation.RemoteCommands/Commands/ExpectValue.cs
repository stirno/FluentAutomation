using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectValueArguments))]
    public class ExpectValue : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectValueArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectValue>(args.Value);
            Guard.ArgumentNotNullForCommand<ExpectValue>(args.Selector);

            if (args.MatchConditions.HasValue)
            {
                manager.Expect.Value(args.Value).In(args.Selector, args.MatchConditions.Value);
            }
            else
            {
                manager.Expect.Value(args.Value).In(args.Selector);
            }
        }
    }

    public class ExpectValueArguments : ICommandArguments
    {
        public string Value { get; set; }
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
