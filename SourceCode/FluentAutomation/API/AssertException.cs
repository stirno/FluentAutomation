// <copyright file="AssertException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace FluentAutomation.API
{
    // Credit to MvcContrib.TestHelper.AssertionException
    public class AssertException : System.Exception
    {
        public AssertException(string message, params object[] formatParams) : base(string.Format(message, formatParams)) { }

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
