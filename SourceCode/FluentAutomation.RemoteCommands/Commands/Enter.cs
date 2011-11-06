using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(EnterArguments))]
    public class Enter : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (EnterArguments)arguments;

            Guard.ArgumentNotNullForCommand<Enter>(args.Value);
            Guard.ArgumentNotNullForCommand<Enter>(args.Selector);

            if (args.Quickly)
            {
                if (args.MatchConditions.HasValue)
                {
                    manager.Enter(args.Value).Quickly.In(args.Selector, args.MatchConditions.Value);
                }
                else
                {
                    manager.Enter(args.Value).Quickly.In(args.Selector);
                }
            }
            else
            {
                if (args.MatchConditions.HasValue)
                {
                    manager.Enter(args.Value).In(args.Selector, args.MatchConditions.Value);
                }
                else
                {
                    manager.Enter(args.Value).In(args.Selector);
                }
            }
        }
    }

    public class EnterArguments : IRemoteCommandArguments
    {
        public string Value { get; set; }
        public bool Quickly { get; set; }
        public string Selector { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
