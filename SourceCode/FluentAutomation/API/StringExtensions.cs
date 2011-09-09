using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    public static class StringExtensions
    {
        public static string PrettifyErrorValue(this string value)
        {
            if (value == string.Empty)
            {
                return "string.Empty";
            }
            else if (value == null)
            {
                return "NULL";
            }
            else
            {
                return value;
            }
        }
    }
}
