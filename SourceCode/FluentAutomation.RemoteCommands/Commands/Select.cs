using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using System.Linq.Expressions;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(SelectArguments))]
    public class Select : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (SelectArguments)arguments;

            Guard.ArgumentNotNullForCommand<Select>(args.Selector);

            API.FieldCommands.Select selectCommand = null;

            if (args.SelectMode.HasValue)
            {
                if (args.Value != null)
                {
                    selectCommand = manager.Select(args.Value, args.SelectMode.Value);
                }
                else if (args.Values != null)
                {
                    selectCommand = manager.Select(args.SelectMode.Value, args.Values);
                }
                else if (args.Index != null)
                {
                    selectCommand = manager.Select(args.Index.Value);
                }
                else if (args.Indices != null)
                {
                    selectCommand = manager.Select(args.Indices);
                }
                else if (args.ValueExpression != null)
                {
                    selectCommand = manager.Select(args.ValueExpression, args.SelectMode.Value);
                }
                else
                {
                    throw new InvalidCommandException<Select>();
                }
            }
            else
            {
                if (args.Value != null)
                {
                    selectCommand = manager.Select(args.Value);
                }
                else if (args.Values != null)
                {
                    selectCommand = manager.Select(args.Values);
                }
                else if (args.Index != null)
                {
                    selectCommand = manager.Select(args.Index.Value);
                }
                else if (args.Indices != null)
                {
                    selectCommand = manager.Select(args.Indices);
                }
                else if (args.ValueExpression != null)
                {
                    selectCommand = manager.Select(args.ValueExpression);
                }
                else
                {
                    throw new InvalidCommandException<Select>();
                }
            }

            if (args.MatchConditions.HasValue)
            {
                selectCommand.From(args.Selector, args.MatchConditions.Value);
            }
            else
            {
                selectCommand.From(args.Selector);
            }
        }
    }

    public class SelectArguments : ICommandArguments
    {
        public string Selector { get; set; }
        public string Value { get; set; }
        public string[] Values { get; set; }
        public int? Index { get; set; }
        public int[] Indices { get; set; }

        public Expression<Func<string, bool>> ValueExpression { get; set; }
        public SelectMode? SelectMode { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
