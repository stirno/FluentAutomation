using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Interfaces;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ExpectAllArguments))]
    public class ExpectAll : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ExpectAllArguments)arguments;

            Guard.ArgumentNotNullForCommand<ExpectAll>(args.Values);

            IValueTextCommand allExpect = null;

            if (args.SelectMode.HasValue)
            {
                allExpect = manager.Expect.All(args.SelectMode.Value, args.Values);
            }
            else
            {
                allExpect = manager.Expect.All(args.Values);
            }

            if (args.MatchConditions.HasValue)
            {
                if (args.Selector != null)
                {
                    allExpect.In(args.Selector, args.MatchConditions.Value);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAll>(() => args.Selectors.Length > 0);
                    allExpect.In(args.MatchConditions.Value, args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectAll>();
                }
            }
            else
            {
                if (args.Selector != null)
                {
                    allExpect.In(args.Selector);
                }
                else if (args.Selectors != null)
                {
                    Guard.ArgumentExpressionTrueForCommand<ExpectAll>(() => args.Selectors.Length > 0);
                    allExpect.In(args.Selectors);
                }
                else
                {
                    throw new InvalidCommandException<ExpectAll>();
                }
            }
        }
    }

    public class ExpectAllArguments : IRemoteCommandArguments
    {
        public string[] Values { get; set; }
        public string Selector { get; set; }
        public string[] Selectors { get; set; }
        public SelectMode? SelectMode { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
