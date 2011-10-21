using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectTextArguments))]
    public class ExpectText : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ExpectTextArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectText>(args.Value);
            Guard.ArgumentNotNullForCommand<ExpectText>(args.Selector);

            var textExpect = manager.Expect.Text(args.Value);

            if (args.MatchConditions.HasValue)
            {
                if (args.Selector != null)
                {
                    textExpect.In(args.Selector, args.MatchConditions.Value);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectText>(() => args.Selectors.Length > 0);
                    textExpect.In(args.MatchConditions.Value, args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectText>();
                }
            }
            else
            {
                if (args.Selector != null)
                {
                    textExpect.In(args.Selector);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectText>(() => args.Selectors.Length > 0);
                    textExpect.In(args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectText>();
                }
            }
        }
    }

    public class ExpectTextArguments : ICommandArguments
    {
        public string Value { get; set; }
        public string[] Selectors { get; set; }
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
