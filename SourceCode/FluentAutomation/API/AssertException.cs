using System;
using System.Linq;

namespace FluentAutomation.API
{
    public class AssertException : System.Exception
    {
        public AssertException(string message, params string[] formatParams) : base(string.Format(message, formatParams)) { }

        public override string Message
        {
            get
            {
                return base.Message.Replace("FluentAutomation.API.AssertException:", "");
            }
        }

        public override string StackTrace
        {
            get
            {
                string Namespace = this.GetType().Namespace;
                return JoinArrayWithNewLineCharacters(SplitTheStackTraceByEachNewLine().Where(s => !s.TrimStart(' ').StartsWith("at " + Namespace)).ToArray());
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
