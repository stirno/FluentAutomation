using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class RemoteExpectProvider : IRemoteExpectProvider
    {
        private readonly IRemoteCommandProvider commandProvider = null;
        public RemoteExpectProvider(IRemoteCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public void Count(string selector, int count)
        {
            this.commandProvider.Act(new { Expect = "Count", Selector = selector, Count = count });
        }

        public void CssClass(string selector, string className)
        {
            this.commandProvider.Act(new { Expect = "CssClass", Selector = selector, ClassName = className });
        }

        public void Text(string selector, string text)
        {
            this.commandProvider.Act(new { Expect = "Text", Selector = selector, Text = text });
        }

        public void Value(string selector, string value)
        {
            this.commandProvider.Act(new { Expect = "Value", Selector = selector, Value = value });
        }
    }
}
