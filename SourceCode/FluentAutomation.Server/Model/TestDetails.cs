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
            this.ServiceModeEnabled = false;
        }

        public TestDetails(Dictionary<IRemoteCommand, IRemoteCommandArguments> remoteCommands, List<BrowserType> browsers) : base()
        {
            this.RemoteCommands = remoteCommands;
            this.Browsers = browsers;
        }

        /// <summary>
        /// Gets or sets the remote commands.
        /// </summary>
        /// <value>
        /// The remote commands.
        /// </value>
        public Dictionary<IRemoteCommand, IRemoteCommandArguments> RemoteCommands { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        /// <value>
        /// The browsers.
        /// </value>
        public List<BrowserType> Browsers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [service mode enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [service mode enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ServiceModeEnabled { get; set; }

        /// <summary>
        /// Gets or sets the step completion pingback URI.
        /// </summary>
        /// <value>
        /// The step completion pingback URI.
        /// </value>
        public Uri StepCompletionPingbackUri { get; set; }

        /// <summary>
        /// Gets or sets the unique test run identifier.
        /// </summary>
        /// <value>
        /// The unique test run identifier.
        /// </value>
        public string UniqueTestRunIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the agent identifier.
        /// </summary>
        /// <value>
        /// The agent identifier.
        /// </value>
        public string AgentIdentifier { get; set; }
    }
}
