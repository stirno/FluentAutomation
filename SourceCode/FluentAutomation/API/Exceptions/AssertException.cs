// <copyright file="AssertException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace FluentAutomation.API.Exceptions
{
    /// <summary>
    /// General Assert Exception thrown by Expect Commands
    /// </summary>
    /// <remarks>
    /// Credit to MvcContrib.TestHelper.AssertionException for StackTrace
    /// </remarks>
    public class AssertException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatParams">The format params.</param>
        public AssertException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }

        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        /// <returns>A string that describes the immediate frames of the call stack.</returns>
        ///   
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/>
        ///   </PermissionSet>
        public override string StackTrace
        {
            get
            {
                var stackTraceLines = base.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Where(s => !s.TrimStart(' ').StartsWith("at " + this.GetType().Namespace));
                return string.Join(Environment.NewLine, stackTraceLines);
            }
        }
    }
}
