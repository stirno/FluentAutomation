using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Exceptions
{
    public class FluentExpectFailedException : FluentException
    {
        public FluentExpectFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentExpectFailedException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }
    }

    public class FluentAssertFailedException : FluentExpectFailedException
    {
        public FluentAssertFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
