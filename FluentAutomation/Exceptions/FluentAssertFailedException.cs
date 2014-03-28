using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Exceptions
{
    public class FluentExpectFailedException : FluentAssertFailedException
    {

        public FluentExpectFailedException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }

        public FluentExpectFailedException(FluentAssertFailedException assertException)
            : base(assertException.Message)
        {
        }

        public FluentExpectFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class FluentAssertFailedException : FluentException
    {
        public FluentAssertFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentAssertFailedException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
        }
    }
}
