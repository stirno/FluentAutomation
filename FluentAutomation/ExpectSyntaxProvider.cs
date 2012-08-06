using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class ExpectSyntaxProvider : BaseExpectSyntaxProvider
    {
        public ExpectSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
            : base(commandProvider, expectProvider)
        {
        }

        #region Count
        /// <summary>
        /// Expect a specific count.
        /// </summary>
        /// <param name="count">Number of elements found.</param>
        /// <returns><c>ExpectCountSyntaxProvider</c></returns>
        public ExpectCountSyntaxProvider Count(int count)
        {
            return new ExpectCountSyntaxProvider(this.commandProvider, this.expectProvider, count);
        }

        public class ExpectCountSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly int count = 0;

            public ExpectCountSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, int count) 
                : base(commandProvider, expectProvider)
            {
                this.count = count;
            }

            /// <summary>
            /// Elements matching <paramref name="selector"/> to be counted.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void Of(string selector)
            {
                this.expectProvider.Count(selector, this.count);
            }

            /// <summary>
            /// Specified <paramref name="elements"/> to be counted.
            /// </summary>
            /// <param name="elements">IElement collection factory function.</param>
            public void Of(Func<IEnumerable<IElement>> elements)
            {
                this.expectProvider.Count(elements, this.count);
            }
        }
        #endregion

        #region CSS Class
        /// <summary>
        /// Expect that a matching CSS class is found.
        /// </summary>
        /// <param name="className">CSS class name. Example: .row</param>
        /// <returns><c>ExpectClassSyntaxProvider</c></returns>
        public ExpectClassSyntaxProvider Class(string className)
        {
            return new ExpectClassSyntaxProvider(this.commandProvider, this.expectProvider, className);
        }

        public class ExpectClassSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string className = null;

            public ExpectClassSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string className)
                : base(commandProvider, expectProvider)
            {
                this.className = className;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void On(string selector)
            {
                this.expectProvider.CssClass(selector, this.className);
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have matching CSS class.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public void On(Func<IElement> element)
            {
                this.expectProvider.CssClass(element, this.className);
            }
        }
        #endregion

        #region Text
        /// <summary>
        /// Expect that Text matches specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text that must be exactly matched.</param>
        /// <returns><c>ExpectTextSyntaxProvider</c></returns>
        public ExpectTextSyntaxProvider Text(string text)
        {
            return new ExpectTextSyntaxProvider(this.commandProvider, this.expectProvider, text);
        }

        /// <summary>
        /// Expect that Text provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Text matches. Example: (text) => text.Contains("Hello")</param>
        /// <returns><c>ExpectTextSyntaxProvider</c></returns>
        public ExpectTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc)
        {
            return new ExpectTextSyntaxProvider(this.commandProvider, this.expectProvider, matchFunc);
        }

        public class ExpectTextSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string text = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public ExpectTextSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string text) 
                : base(commandProvider, expectProvider)
            {
                this.text = text;
            }

            public ExpectTextSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider)
            {
                this.matchFunc = matchFunc;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should match Text.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(selector, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(selector, this.matchFunc);
                }
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should match Text.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public void In(Func<IElement> element)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(element, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(element, this.matchFunc);
                }
            }
        }
        #endregion

        #region Value
        /// <summary>
        /// Expect a specific integer <paramref name="value"/>
        /// </summary>
        /// <param name="value">Int32 value expected.</param>
        /// <returns><c>ExpectValueSyntaxProvider</c></returns>
        public ExpectValueSyntaxProvider Value(int value)
        {
            return this.Value(value.ToString());
        }

        /// <summary>
        /// Expect a specific string <paramref name="value"/>.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns><c>ExpectValueSyntaxProvider</c></returns>
        public ExpectValueSyntaxProvider Value(string value)
        {
            return new ExpectValueSyntaxProvider(this.commandProvider, this.expectProvider, value);
        }

        /// <summary>
        /// Expect that value provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Value matches. Example: (value) => value != "Hello" && value != "World"</param>
        /// <returns><c>ExpectValueSyntaxProvider</c></returns>
        public ExpectValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc)
        {
            return new ExpectValueSyntaxProvider(this.commandProvider, this.expectProvider, matchFunc);
        }

        public class ExpectValueSyntaxProvider : BaseExpectSyntaxProvider
        {
            private readonly string value = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public ExpectValueSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, string value)
                : base(commandProvider, expectProvider)
            {
                this.value = value;
            }

            public ExpectValueSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider)
            {
                this.matchFunc = matchFunc;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have a matching Value.
            /// </summary>
            /// <param name="selector"></param>
            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(selector, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(selector, this.matchFunc);
                }
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have a matching Value.
            /// </summary>
            /// <param name="element"></param>
            public void In(Func<IElement> element)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(element, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(element, this.matchFunc);
                }
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Expect the current web browser's URL to match <paramref name="expectedUrl"/>.
        /// </summary>
        /// <param name="expectedUrl">Fully-qualified URL to be matched on.</param>
        public void Url(string expectedUrl)
        {
            Url(new Uri(expectedUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Expect the current web browser's URI to match <paramref name="expectedUri"/>.
        /// </summary>
        /// <param name="expectedUri">Absolute URI to be matched on.</param>
        public void Url(Uri expectedUri)
        {
            this.expectProvider.Url(expectedUri);
        }

        /// <summary>
        /// Expect the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> to return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to be matched on.</param>
        public void Url(Expression<Func<Uri, bool>> uriExpression)
        {
            this.expectProvider.Url(uriExpression);
        }
        #endregion

        #region Boolean / Throws
        /// <summary>
        /// Expect that an arbitrary <paramref name="matchFunc">matching function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc"></param>
        public void True(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.True(matchFunc);
            });
        }

        /// <summary>
        /// Expect that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.
        /// </summary>
        /// <param name="matchFunc"></param>
        public void False(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.False(matchFunc);
            });
        }

        /// <summary>
        /// Expect that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        public void Throws(Expression<Action> matchAction)
        {
            this.commandProvider.Act(() =>
            {
                this.expectProvider.Throws(matchAction);
            });
        }
        #endregion
    }

    public class BaseExpectSyntaxProvider
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IExpectProvider expectProvider = null;

        public BaseExpectSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
        }
    }
}
