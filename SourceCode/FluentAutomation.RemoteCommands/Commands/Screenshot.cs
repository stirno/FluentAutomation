using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands.Commands
{
    [CommandArgumentsType(typeof(ScreenshotArguments))]
    public class Screenshot : IRemoteCommand
    {
        public void Execute(API.CommandManager manager, IRemoteCommandArguments arguments)
        {
            var args = (ScreenshotArguments)arguments;

            Guard.ArgumentNotNullForCommand<Screenshot>(args.FileName);

            // TODO: Save this somewhere accessible..
            manager.TakeScreenshot(args.FileName);
        }
    }

    public class ScreenshotArguments : IRemoteCommandArguments
    {
        public string FileName { get; set; }
    }
}
