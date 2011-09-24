using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API
{
    public class SelectException : AssertException
    {
        public SelectException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }
    }
}
