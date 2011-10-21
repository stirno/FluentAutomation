using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(HoverArguments))]
    public class Hover : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (HoverArguments)arguments;

            Guard.ArgumentNotNullForCommand<Hover>(args.Selector);

            if (args.Point == null)
            {
                if (args.MatchConditions.HasValue)
                {
                    manager.Hover(args.Selector, args.MatchConditions.Value);
                }
                else
                {
                    manager.Hover(args.Selector);
                }
            }
            else
            {
                manager.Hover(args.Point);
            }
        }
    }

    public class HoverArguments : ICommandArguments
    {
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
        public API.Point Point { get; set; }
    }
}
