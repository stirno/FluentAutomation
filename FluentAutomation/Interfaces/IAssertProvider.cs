using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Interfaces
{
    public interface IAssertProvider
    {
        bool ThrowExceptions { get; set; }
        IAssertProvider EnableExceptions();

        void Count(string selector, int count);
        void Count(ElementProxy elements, int count);

        void CssClass(string selector, string className);
        void CssClass(ElementProxy element, string className);

        void Text(string selector, string text);
        void Text(ElementProxy element, string text);
        void Text(string selector, Expression<Func<string, bool>> matchFunc);
        void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc);

        void Value(string selector, string value);
        void Value(ElementProxy element, string value);
        void Value(string selector, Expression<Func<string, bool>> matchFunc);
        void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc);

        void Url(Uri expectedUrl);
        void Url(Expression<Func<Uri, bool>> urlExpression);

        void True(Expression<Func<bool>> matchFunc);
        void False(Expression<Func<bool>> matchFunc);
        void Throws(Expression<Action> matchAction);
        void Exists(string selector);

        void AlertText(string text);
        void AlertText(Expression<Func<string, bool>> matchFunc);
    }
}