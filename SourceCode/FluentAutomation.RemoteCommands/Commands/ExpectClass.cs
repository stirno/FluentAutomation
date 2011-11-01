using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectClassArguments))]
    public class ExpectClass : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ExpectClassArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectClass>(args.Value);
            Guard.ArgumentNotNullForCommand<ExpectClass>(args.Selector);

            var classExpect = manager.Expect.Class(args.Value);

            if (args.MatchConditions.HasValue)
            {
                classExpect.On(args.Selector);
            }

            if (args.MatchConditions.HasValue)
            {
                if (args.Selector != null)
                {
                    classExpect.On(args.Selector, args.MatchConditions.Value);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAny>(() => args.Selectors.Length > 0);
                    classExpect.On(args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectAny>();
                }
            }
            else
            {
                if (args.Selector != null)
                {
                    classExpect.On(args.Selector);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAny>(() => args.Selectors.Length > 0);
                    classExpect.On(args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectAny>();
                }
            }
        }
    }

    public class ExpectClassArguments : IRemoteCommandArguments
    {
        public string Value { get; set; }
        public string Selector { get; set; }
        public string[] Selectors { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
