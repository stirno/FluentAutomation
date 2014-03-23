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
        private readonly CommandProviderList commandProviders = null;
        private readonly List<KeyValuePair<IExpectProvider, ICommandProvider>> providers = null;

        public MultiExpectProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders; // Easier than recomposing it for EnableExceptions() call, so storing it
            this.providers = commandProviders.Select(x => new KeyValuePair<IExpectProvider, ICommandProvider>(new ExpectProvider(x), x)).ToList();
        }

        public void Count(string selector, int count)
        {
            Parallel.ForEach(this.providers, x => x.Key.Count(selector, count));
        }

        public void Count(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).Count(new ElementProxy(e.Key, e.Value), count);
            });
        }

        public void CssClass(string selector, string className)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssClass(selector, className));
        }

        public void CssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).CssClass(new ElementProxy(e.Key, e.Value), className);
            });
        }

        public void Text(string selector, string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, text));
        }

        public void Text(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).Text(new ElementProxy(e.Key, e.Value), text);
            });
        }

        public void Text(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, matchFunc));
        }

        public void Text(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).Text(new ElementProxy(e.Key, e.Value), matchFunc);
            });
        }

        public void Value(string selector, string value)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, value));
        }

        public void Value(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).Value(new ElementProxy(e.Key, e.Value), value);
            });
        }

        public void Value(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, matchFunc));
        }

        public void Value(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new ExpectProvider(e.Key).Value(new ElementProxy(e.Key, e.Value), matchFunc);
            });
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

        public bool ThrowExceptions { get; set; }

        public IExpectProvider EnableExceptions()
        {
            var provider = new MultiExpectProvider(this.commandProviders);
            provider.ThrowExceptions = true;

            return provider;
        }
    }
}
