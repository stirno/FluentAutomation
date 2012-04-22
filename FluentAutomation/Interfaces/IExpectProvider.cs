using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Interfaces
{
    public interface IExpectProvider
    {
        void Count(string selector, int count);
        void Count(Func<IEnumerable<IElement>> elements, int count);

        void CssClass(string selector, string className);
        void CssClass(Func<IElement> element, string className);

        void Text(string selector, string text);
        void Text(Func<IElement> element, string text);
        void Text(string selector, Expression<Func<string, bool>> matchFunc);
        void Text(Func<IElement> element, Expression<Func<string, bool>> matchFunc);

        void Value(string selector, string value);
        void Value(Func<IElement> element, string value);
        void Value(string selector, Expression<Func<string, bool>> matchFunc);
        void Value(Func<IElement> element, Expression<Func<string, bool>> matchFunc);

        void Url(Uri expectedUrl);
        void Url(Expression<Func<Uri, bool>> urlExpression);

        void True(Expression<Func<bool>> matchFunc);
        void False(Expression<Func<bool>> matchFunc);
        void Throws(Expression<Action> matchAction);
    }
}