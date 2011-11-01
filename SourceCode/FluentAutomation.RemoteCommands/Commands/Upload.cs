using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(UploadArguments))]
    public class Upload : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (UploadArguments)arguments;

            Guard.ArgumentNotNullForCommand<Upload>(args.Selector);
            Guard.ArgumentNotNullForCommand<Upload>(args.FileName);

            if (args.Offset == null)
            {
                manager.Upload(args.FileName, args.Selector);
            }
            else
            {
                manager.Upload(args.FileName, args.Selector, args.Offset);
            }
        }
    }

    public class UploadArguments : IRemoteCommandArguments
    {
        public string FileName { get; set; }
        public string Selector { get; set; }
        public API.Point Offset { get; set; }
        public MatchConditions? MatchConditions { get; set; }
    }
}
