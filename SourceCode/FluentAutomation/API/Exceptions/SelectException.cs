// <copyright file="SelectException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Exceptions
{
    public class SelectException : AssertException
    {
        public SelectException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }
    }
}
