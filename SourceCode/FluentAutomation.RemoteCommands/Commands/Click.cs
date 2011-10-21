using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ClickArguments))]
    public class Click : ICommand
    {
        public void Execute(CommandManager manager, ICommandArguments arguments)
        {
            var args = (ClickArguments)arguments;

            if (args.Selector != null && args.ClickMode != null && args.MatchConditions != null)
            {
                manager.Click(args.Selector, args.ClickMode.Value, args.MatchConditions.Value);
            }
            else if (args.Selector != null && args.ClickMode != null)
            {
                manager.Click(args.Selector, args.ClickMode.Value);
            }
            else if (args.Selector != null && args.Point == null)
            {
                manager.Click(args.Selector);
            }
            else if (args.Selector != null && args.Point != null)
            {
                manager.Click(args.Selector, args.Point);
            }
            else if (args.Point != null)
            {
                manager.Click(args.Point);
            }
            else
            {
                throw new InvalidCommandException<Click>();
            }
        }
    }

    public class ClickArguments : ICommandArguments
    {
        public string Selector { get; set; }
        public API.Point Point { get; set; }
        public ClickMode? ClickMode { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
