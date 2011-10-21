using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ScreenshotArguments))]
    public class Screenshot : ICommand
    {
        public void Execute(API.CommandManager manager, ICommandArguments arguments)
        {
            var args = (ScreenshotArguments)arguments;

            Guard.ArgumentNotNullForCommand<Screenshot>(args.FileName);

            // TODO: Save this somewhere accessible..
            manager.TakeScreenshot(args.FileName);
        }
    }

    public class ScreenshotArguments : ICommandArguments
    {
        public string FileName { get; set; }
    }
}
