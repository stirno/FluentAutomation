using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class MultiExpectProvider : IExpectProvider
    {
        private readonly List<KeyValuePair<IExpectProvider, ICommandProvider>> providers = null;

        public MultiExpectProvider(CommandProviderList commandProviders)
        {
            this.providers = commandProviders.Select(x => new KeyValuePair<IExpectProvider, ICommandProvider>(new ExpectProvider(x), x)).ToList();
        }

        public void Count(string selector, int count)
        {
            Parallel.ForEach(this.providers, x => x.Key.Count(selector, count));
        }

        public void Count(Func<IEnumerable<IElement>> elements, int count)
        {
            Parallel.ForEach(this.providers, x => x.Key.Count(x.Value.FindMultiple(elements().First().Selector), count));
        }

        public void CssClass(string selector, string className)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssClass(selector, className));
        }

        public void CssClass(Func<IElement> element, string className)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssClass(x.Value.Find(element().Selector), className));
        }

        public void Text(string selector, string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, text));
        }

        public void Text(Func<IElement> element, string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(x.Value.Find(element().Selector), text));
        }

        public void Text(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, matchFunc));
        }

        public void Text(Func<IElement> element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(x.Value.Find(element().Selector), matchFunc));
        }

        public void Value(string selector, string value)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, value));
        }

        public void Value(Func<IElement> element, string value)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(x.Value.Find(element().Selector), value));
        }

        public void Value(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, matchFunc));
        }

        public void Value(Func<IElement> element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(x.Value.Find(element().Selector), matchFunc));
        }

        public void Url(Uri expectedUrl)
        {
            Parallel.ForEach(this.providers, x => x.Key.Url(expectedUrl));
        }

        public void Url(System.Linq.Expressions.Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(this.providers, x => x.Key.Url(urlExpression));
        }

        public void True(System.Linq.Expressions.Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.True(matchFunc));
        }

        public void False(System.Linq.Expressions.Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.False(matchFunc));
        }

        public void Throws(System.Linq.Expressions.Expression<Action> matchAction)
        {
            Parallel.ForEach(this.providers, x => x.Key.Throws(matchAction));
        }

        public void Exists(string selector)
        {
            Parallel.ForEach(this.providers, x => x.Key.Exists(selector));
        }
    }
}
