// <copyright file="SelectException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Exceptions
{
    /// <summary>
    /// Exception thrown when unable to perform the requested select.
    /// </summary>
    public class SelectException : AssertException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatParams">The format params.</param>
        public SelectException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }
    }
}
