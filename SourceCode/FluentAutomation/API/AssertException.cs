using System;
using System.Linq;

namespace FluentAutomation.API
{
    public class AssertException : System.Exception
    {
        public AssertException(string message, params string[] formatParams) : base(string.Format(message, formatParams)) { }

        public override string StackTrace
        {
            get
            {
                return JoinArrayWithNewLineCharacters(SplitTheStackTraceByEachNewLine().Where(s => !s.TrimStart(' ').StartsWith("at " + this.GetType().Namespace)).ToArray());
            }
        }

        private string[] SplitTheStackTraceByEachNewLine()
        {
            return base.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string JoinArrayWithNewLineCharacters(string[] stacktracestring)
        {
            return string.Join(Environment.NewLine, stacktracestring);
        }
    }
}
