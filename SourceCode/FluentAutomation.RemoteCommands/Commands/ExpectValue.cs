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

            Guard.ArgumentNotNullForCommand<ExpectValue>(args.Value, args.ValueExpression);
            Guard.ArgumentNotNullForCommand<ExpectValue>(args.Selector);

            API.ExpectCommands.Value valueExpect = null;
            if (args.Value != null)
            {
                valueExpect = manager.Expect.Value(args.Value);
            }
            else
            {
                valueExpect = manager.Expect.Value(args.ValueExpression);
            }

            if (args.MatchConditions.HasValue)
            {
                valueExpect.In(args.Selector, args.MatchConditions.Value);
            }
            else
            {
                valueExpect.In(args.Selector);
            }
        }
    }

    public class ExpectValueArguments : ICommandArguments
    {
        public string Value { get; set; }
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
        public Expression<Func<string, bool>> ValueExpression { get; set; }
    }
}
