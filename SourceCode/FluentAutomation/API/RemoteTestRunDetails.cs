using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.RemoteCommands;

namespace FluentAutomation.API
{
    /// <summary>
    /// Remote Test Run Details
    /// </summary>
    public class RemoteTestRunDetails
    {
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

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public List<RemoteCommandDetails> Commands { get; set; }
    }
}
