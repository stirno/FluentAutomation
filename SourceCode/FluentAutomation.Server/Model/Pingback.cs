using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Server.Model
{
    public class Pingback
    {
        /// <summary>
        /// Gets or sets the agent identifier.
        /// </summary>
        /// <value>
        /// The agent identifier.
        /// </value>
        public string AgentIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the unique test identifier.
        /// </summary>
        /// <value>
        /// The unique test identifier.
        /// </value>
        public string UniqueTestIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public string ExceptionMessage { get; set; }
    }
}
