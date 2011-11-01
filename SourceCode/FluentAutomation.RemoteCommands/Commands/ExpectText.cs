using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectTextArguments))]
    public class ExpectText : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ExpectTextArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectText>(args.Value, args.ValueExpression);
            Guard.ArgumentNotNullForCommand<ExpectText>(args.Selector);

            API.ExpectCommands.Text textExpect = null;
            if (args.Value != null)
            {
                textExpect = manager.Expect.Text(args.Value);
            }
            else if (args.ValueExpression != null)
            {
                textExpect = manager.Expect.Text(args.ValueExpression);
            }

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

    public class ExpectTextArguments : IRemoteCommandArguments
    {
        public string Value { get; set; }
        public string Selector { get; set; }
        public string[] Selectors { get; set; }
        public MatchConditions? MatchConditions { get; set; }
        public Expression<Func<string, bool>> ValueExpression { get; set; }
    }
}
