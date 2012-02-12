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
        /// Gets or sets a value indicating whether [show interface].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show interface]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowInterface { get; set; }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public List<RemoteCommandDetails> Commands { get; set; }
    }
}
