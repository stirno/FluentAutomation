using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Exceptions
{
    public class FluentException : System.Exception, ISerializable
    {
        public FluentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FluentException(string message, params object[] formatParams)
            : base(string.Format(message, formatParams))
        {
            PreserveStackTrace(this);
        }

        public FluentException(string message, Exception innerException, params object[] formatParams)
            : base(string.Format(message, formatParams), innerException)
        {
            PreserveStackTrace(this);
        }

        public override string StackTrace
        {
            get
            {
                var stackTraceLines = base.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Where(s => !s.TrimStart(' ').StartsWith("at " + this.GetType().Namespace));
                return string.Join(Environment.NewLine, stackTraceLines);
            }
        }

        /// <remarks>
        /// Credit to MvcContrib.TestHelper.AssertionException for PreserveStackTrace
        /// </remarks>
        private static void PreserveStackTrace(Exception e)
        {
            var ctx = new StreamingContext(StreamingContextStates.CrossAppDomain);
            var mgr = new ObjectManager(null, ctx);
            var si = new SerializationInfo(e.GetType(), new FormatterConverter());

            e.GetObjectData(si, ctx);
            mgr.RegisterObject(e, 1, si);
            mgr.DoFixups();
        }
    }
    
    public static class ExceptionExtensions
    {
        public static string ToExpressionString(this LambdaExpression expression)
        {
            StringBuilder sbExpression = new StringBuilder();
            foreach (var exprParam in expression.Parameters)
            {
                sbExpression.Append(exprParam);
                if (expression.Parameters.Last() != exprParam) sbExpression.Append(",");
            }
            sbExpression.Append(" => ");

            var exprBody = expression.Body.ToString();
            exprBody = exprBody.Replace("OrElse", "||").Replace("AndAlso", "&&");

            sbExpression.Append(exprBody);

            return sbExpression.ToString();
        }
    }
}
