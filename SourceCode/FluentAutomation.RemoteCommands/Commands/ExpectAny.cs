using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectAnyArguments))]
    public class ExpectAny : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ExpectAnyArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectAny>(args.Values);

            IValueTextCommand anyExpect = null;

            if (args.SelectMode.HasValue)
            {
                anyExpect = manager.Expect.Any(args.SelectMode.Value, args.Values);
            }
            else
            {
                anyExpect = manager.Expect.Any(args.Values);
            }

            if (args.MatchConditions.HasValue)
            {
                if (args.Selector != null)
                {
                    anyExpect.In(args.Selector, args.MatchConditions.Value);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAny>(() => args.Selectors.Length > 0);
                    anyExpect.In(args.MatchConditions.Value, args.Selectors);
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
                    anyExpect.In(args.Selector);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAny>(() => args.Selectors.Length > 0);
                    anyExpect.In(args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectAny>();
                }
            }
        }
    }

    public class ExpectAnyArguments : IRemoteCommandArguments
    {
        public string[] Values { get; set; }
        public string Selector { get; set; }
        public string[] Selectors { get; set; }
        public SelectMode? SelectMode { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
