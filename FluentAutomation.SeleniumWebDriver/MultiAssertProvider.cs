using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class MultiAssertProvider : IAssertProvider
    {
        private readonly CommandProviderList commandProviders = null;
        private readonly List<KeyValuePair<IAssertProvider, ICommandProvider>> providers = null;

        public MultiAssertProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders; // Easier than recomposing it for EnableExceptions() call, so storing it
            this.providers = commandProviders.Select(x => new KeyValuePair<IAssertProvider, ICommandProvider>(new AssertProvider(x), x)).ToList();
        }

        public void Count(string selector, int count)
        {
            Parallel.ForEach(this.providers, x => x.Key.Count(selector, count));
        }

        public void NotCount(string selector, int count)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotCount(selector, count));
        }

        public void Count(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).Count(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void NotCount(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotCount(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void CssClass(string selector, string className)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssClass(selector, className));
        }
        public void NotCssClass(string selector, string className)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotCssClass(selector, className));
        }

        public void CssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).CssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }
        public void NotCssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotCssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }

        public void Text(string selector, string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, text));
        }
        public void NotText(string selector, string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotText(selector, text));
        }

        public void Text(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void NotText(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void Text(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Text(selector, matchFunc));
        }

        public void NotText(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotText(selector, matchFunc));
        }

        public void Text(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotText(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Value(string selector, string value)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, value));
        }

        public void NotValue(string selector, string value)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotValue(selector, value));
        }

        public void Value(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void NotValue(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void Value(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.Value(selector, matchFunc));
        }

        public void NotValue(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotValue(selector, matchFunc));
        }

        public void Value(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotValue(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                new AssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Url(Uri expectedUrl)
        {
            Parallel.ForEach(this.providers, x => x.Key.Url(expectedUrl));
        }
        public void NotUrl(Uri expectedUrl)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotUrl(expectedUrl));
        }

        public void Url(System.Linq.Expressions.Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(this.providers, x => x.Key.Url(urlExpression));
        }

        public void NotUrl(System.Linq.Expressions.Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotUrl(urlExpression));
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

        public void NotThrows(System.Linq.Expressions.Expression<Action> matchAction)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotThrows(matchAction));
        }

        public void Exists(string selector)
        {
            Parallel.ForEach(this.providers, x => x.Key.Exists(selector));
        }

        public void NotExists(string selector)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotExists(selector));
        }

        public void AlertText(string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.AlertText(text));
        }

        public void AlertNotText(string text)
        {
            Parallel.ForEach(this.providers, x => x.Key.AlertNotText(text));
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.AlertText(matchFunc));
        }

        public void AlertNotText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => x.Key.AlertNotText(matchFunc));
        }

        public void Visible(string selector)
        {
            Parallel.ForEach(this.providers, x => x.Key.Visible(selector));
        }

        public void NotVisible(string selector)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotVisible(selector));
        }

        public void Visible(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => x.Key.Visible(element));
        }

        public void NotVisible(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotVisible(element));
        }

        public void CssProperty(string selector, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssProperty(selector, propertyName, propertyValue));
        }

        public void NotCssProperty(string selector, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotCssProperty(selector, propertyName, propertyValue));
        }

        public void CssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.CssProperty(element, propertyName, propertyValue));
        }

        public void NotCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotCssProperty(element, propertyName, propertyValue));
        }

        public void Attribute(string selector, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.Attribute(selector, attributeName, attributeValue));
        }

        public void NotAttribute(string selector, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotAttribute(selector, attributeName, attributeValue));
        }

        public void Attribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.Attribute(element, attributeName, attributeValue));
        }

        public void NotAttribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => x.Key.NotAttribute(element, attributeName, attributeValue));
        }

        public bool ThrowExceptions { get; set; }

        public IAssertProvider EnableExceptions()
        {
            var provider = new MultiAssertProvider(this.commandProviders);
            provider.ThrowExceptions = true;

            return provider;
        }
    }
}
