using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;
using FluentAutomation.RemoteCommands;

namespace FluentAutomation.Server.Model
{
    public class TestDetails
    {
        public TestDetails()
        {
            this.RemoteCommands = new Dictionary<IRemoteCommand, IRemoteCommandArguments>();
            this.Browsers = new List<BrowserType>();
        }

        public TestDetails(Dictionary<IRemoteCommand, IRemoteCommandArguments> remoteCommands, List<BrowserType> browsers)
        {
            this.RemoteCommands = remoteCommands;
            this.Browsers = browsers;
        }

        public Dictionary<IRemoteCommand, IRemoteCommandArguments> RemoteCommands { get; set; }

        public List<BrowserType> Browsers { get; set; }
    }
}
