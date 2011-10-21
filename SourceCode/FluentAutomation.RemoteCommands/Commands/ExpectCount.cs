using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectCountArguments))]
    public class ExpectCount : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectCountArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectCount>(args.Value);
            Guard.ArgumentNotNullForCommand<ExpectCount>(args.Selector);

            if (args.MatchConditions.HasValue)
            {
                manager.Expect.Count(args.Value).Of(args.Selector, args.MatchConditions.Value);
            }
            else
            {
                manager.Expect.Count(args.Value).Of(args.Selector);
            }
        }
    }

    public class ExpectCountArguments : ICommandArguments
    {
        public int Value { get; set; }
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
