using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class AssertSyntaxProvider : BaseAssertSyntaxProvider
    {
        public AssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider)
            : base(commandProvider, expectProvider)
        {
        }

        #region Count
        /// <summary>
        /// Assert a specific count.
        /// </summary>
        /// <param name="count">Number of elements found.</param>
        /// <returns><c>AssertCountSyntaxProvider</c></returns>
        public AssertCountSyntaxProvider Count(int count)
        {
            return new AssertCountSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, count);
        }

        public class AssertCountSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly int count = 0;

            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, int count)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.count = count;
            }

            /// <summary>
            /// Elements matching <paramref name="selector"/> to be counted.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider Of(string selector)
            {
                this.expectProvider.Count(selector, this.count);
                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="elements"/> to be counted.
            /// </summary>
            /// <param name="elements">IElement collection factory function.</param>
            public AssertSyntaxProvider Of(ElementProxy elements)
            {
                this.expectProvider.Count(elements, this.count);
                return this.expectSyntaxProvider;
            }
        }
        #endregion

        #region CSS Class
        /// <summary>
        /// Assert that a matching CSS class is found.
        /// </summary>
        /// <param name="className">CSS class name. Example: .row</param>
        /// <returns><c>AssertClassSyntaxProvider</c></returns>
        public AssertClassSyntaxProvider Class(string className)
        {
            return new AssertClassSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, className);
        }

        public class AssertClassSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string className = null;

            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, string className)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.className = className;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider On(string selector)
            {
                this.expectProvider.CssClass(selector, this.className);
                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have matching CSS class.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                this.expectProvider.CssClass(element, this.className);
                return this.expectSyntaxProvider;
            }
        }
        #endregion

        #region Text
        /// <summary>
        /// Assert that Text matches specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text that must be exactly matched.</param>
        /// <returns><c>AssertTextSyntaxProvider</c></returns>
        public AssertTextSyntaxProvider Text(string text)
        {
            return new AssertTextSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, text);
        }

        /// <summary>
        /// Assert that Text provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Text matches. Example: (text) => text.Contains("Hello")</param>
        /// <returns><c>AssertTextSyntaxProvider</c></returns>
        public AssertTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc)
        {
            return new AssertTextSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, matchFunc);
        }

        public class AssertTextSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string text = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, string text)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.text = text;
            }

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.matchFunc = matchFunc;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should match Text.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider In(string selector)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(selector, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(selector, this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should match Text.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(element, this.text);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Text(element, this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Look in the active Alert/Prompt for the specified text.
            /// </summary>
            /// <param name="accessor"></param>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (this.matchFunc == null)
                {
                    this.expectProvider.AlertText(this.text);
                }
                else
                {
                    this.expectProvider.AlertText(this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }
        }
        #endregion

        #region Value
        /// <summary>
        /// Assert a specific integer <paramref name="value"/>
        /// </summary>
        /// <param name="value">Int32 value expected.</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        public AssertValueSyntaxProvider Value(int value)
        {
            return this.Value(value.ToString());
        }

        /// <summary>
        /// Assert a specific string <paramref name="value"/>.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        public AssertValueSyntaxProvider Value(string value)
        {
            return new AssertValueSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, value);
        }

        /// <summary>
        /// Assert that value provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Value matches. Example: (value) => value != "Hello" && value != "World"</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        public AssertValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc)
        {
            return new AssertValueSyntaxProvider(this.commandProvider, this.expectProvider, this.expectSyntaxProvider, matchFunc);
        }

        public class AssertValueSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string value = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, string value)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.value = value;
            }

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, expectProvider, expectSyntaxProvider)
            {
                this.matchFunc = matchFunc;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have a matching Value.
            /// </summary>
            /// <param name="selector"></param>
            public AssertSyntaxProvider In(string selector)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(selector, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(selector, this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have a matching Value.
            /// </summary>
            /// <param name="element"></param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(element, this.value);
                }
                else if (this.matchFunc != null)
                {
                    this.expectProvider.Value(element, this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }

            /// <summary>
            /// Look in the active Alert/Prompt for the specified value.
            /// </summary>
            /// <param name="accessor"></param>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (this.matchFunc == null)
                {
                    this.expectProvider.AlertText(this.value.ToString());
                }
                else
                {
                    this.expectProvider.AlertText(this.matchFunc);
                }

                return this.expectSyntaxProvider;
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Assert the current web browser's URL to match <paramref name="expectedUrl"/>.
        /// </summary>
        /// <param name="expectedUrl">Fully-qualified URL to be matched on.</param>
        public AssertSyntaxProvider Url(string expectedUrl)
        {
            return Url(new Uri(expectedUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Assert the current web browser's URI to match <paramref name="expectedUri"/>.
        /// </summary>
        /// <param name="expectedUri">Absolute URI to be matched on.</param>
        public AssertSyntaxProvider Url(Uri expectedUri)
        {
            this.expectProvider.Url(expectedUri);
            return this.expectSyntaxProvider;
        }

        /// <summary>
        /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> to return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to be matched on.</param>
        public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
        {
            this.expectProvider.Url(uriExpression);
            return this.expectSyntaxProvider;
        }
        #endregion

        #region Boolean / Throws
        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc"></param>
        public AssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(CommandType.Expect, () =>
            {
                this.expectProvider.True(matchFunc);
            });

            return this.expectSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.
        /// </summary>
        /// <param name="matchFunc"></param>
        public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
        {
            this.commandProvider.Act(CommandType.Expect, () =>
            {
                this.expectProvider.False(matchFunc);
            });

            return this.expectSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        public AssertSyntaxProvider Throws(Expression<Action> matchAction)
        {
            this.commandProvider.Act(CommandType.Expect, () =>
            {
                this.expectProvider.Throws(matchAction);
            });

            return this.expectSyntaxProvider;
        }
        #endregion

        /// <summary>
        /// Assert the element specified exists.
        /// </summary>
        /// <param name="selector">Element selector.</param>
        public AssertSyntaxProvider Exists(string selector)
        {
            this.expectProvider.Exists(selector);
            return this.expectSyntaxProvider;
        }
    }

    public class BaseAssertSyntaxProvider
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IAssertProvider expectProvider = null;
        internal readonly AssertSyntaxProvider expectSyntaxProvider = null;

        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider)
            : this(commandProvider, expectProvider, null)
        {
        }

        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider expectProvider, AssertSyntaxProvider expectSyntaxProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
            this.expectSyntaxProvider = expectSyntaxProvider == null ? (AssertSyntaxProvider)this : expectSyntaxProvider;
        }
    }
}
