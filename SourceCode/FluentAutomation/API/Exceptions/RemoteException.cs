using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FluentAutomation.API.Exceptions
{
    public class RemoteException : AssertException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatParams">The format params.</param>
        public RemoteException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        ///   
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        public RemoteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
