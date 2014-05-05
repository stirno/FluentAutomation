using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FluentAutomation.Exceptions
{
    public class FluentElementNotFoundException : FluentException
    {
        public FluentElementNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentElementNotFoundException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }
    }
}
