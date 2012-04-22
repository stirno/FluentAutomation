using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IRemoteExpectProvider
    {
        void Count(string selector, int count);
        void CssClass(string selector, string className);
        void Text(string selector, string text);
        void Value(string selector, string value);
        void Url(string url);
    }
}
