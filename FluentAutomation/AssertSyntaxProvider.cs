using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.Interfaces;
using FluentAutomation.Exceptions;

namespace FluentAutomation
{
    public class AssertSyntaxProvider : BaseAssertSyntaxProvider
    {
        private bool notMode = false;

        public AssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider)
            : this(commandProvider, assertProvider, false)
        {
        }

        public AssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, bool notMode)
            : base(commandProvider, assertProvider)
        {
            this.notMode = notMode;
        }

        public AssertSyntaxProvider Not
        {
            get
            {
                return new AssertSyntaxProvider(commandProvider, assertProvider, true);
            }
        }

        #region Count
        /// <summary>
        /// Assert a specific count.
        /// </summary>
        /// <param name="count">Number of elements found.</param>
        /// <returns><c>AssertCountSyntaxProvider</c></returns>
        public AssertCountSyntaxProvider Count(int count)
        {
            return new AssertCountSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, count);
        }

        public class AssertCountSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly int count = 0;

            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, int count)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.count = count;
            }

            /// <summary>
            /// Elements matching <paramref name="selector"/> to be counted.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider Of(string selector)
            {
                if (this.assertSyntaxProvider.notMode)
                    this.assertProvider.NotCount(selector, this.count);
                else
                    this.assertProvider.Count(selector, this.count);

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="elements"/> to be counted.
            /// </summary>
            /// <param name="elements">IElement collection factory function.</param>
            public AssertSyntaxProvider Of(ElementProxy elements)
            {
                if (this.assertSyntaxProvider.notMode)
                    this.assertProvider.NotCount(elements, count);
                else
                    this.assertProvider.Count(elements, this.count);

                return this.assertSyntaxProvider;
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
            return new AssertClassSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, className);
        }

        public class AssertClassSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string className = null;

            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string className)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.className = className;
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider On(string selector)
            {
                if (this.assertSyntaxProvider.notMode)
                    this.assertProvider.NotCssClass(selector, this.className);
                else
                    this.assertProvider.CssClass(selector, this.className);

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have matching CSS class.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (this.assertSyntaxProvider.notMode)
                    this.assertProvider.NotCssClass(element, this.className);
                else
                    this.assertProvider.CssClass(element, this.className);

                return this.assertSyntaxProvider;
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
            return new AssertTextSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, text);
        }

        /// <summary>
        /// Assert that Text provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Text matches. Example: (text) => text.Contains("Hello")</param>
        /// <returns><c>AssertTextSyntaxProvider</c></returns>
        public AssertTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc)
        {
            return new AssertTextSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, matchFunc);
        }

        public class AssertTextSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string text = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string text)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.text = text;
            }

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
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
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotText(selector, this.text);
                    else
                        this.assertProvider.Text(selector, this.text);
                }
                else if (this.matchFunc != null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotText(selector, this.matchFunc);
                    else
                        this.assertProvider.Text(selector, this.matchFunc);
                }

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should match Text.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotText(element, this.text);
                    else
                        this.assertProvider.Text(element, this.text);
                }
                else if (this.matchFunc != null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotText(element, this.matchFunc);
                    else
                        this.assertProvider.Text(element, this.matchFunc);
                }

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Look in the active Alert/Prompt for the specified text. If the text does not match the prompt will be cleanly exited to allow clean failure or continuation of the test.
            /// </summary>
            /// <param name="accessor"></param>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (this.matchFunc == null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.AlertNotText(this.text);
                    else
                        this.assertProvider.AlertText(this.text);
                }
                else
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.AlertNotText(this.matchFunc);
                    else
                        this.assertProvider.AlertText(this.matchFunc);
                }

                return this.assertSyntaxProvider;
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
            return new AssertValueSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, value);
        }

        /// <summary>
        /// Assert that value provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Value matches. Example: (value) => value != "Hello" && value != "World"</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        public AssertValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc)
        {
            return new AssertValueSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, matchFunc);
        }

        public class AssertValueSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string value = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string value)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.value = value;
            }

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
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
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotValue(selector, this.value);
                    else
                        this.assertProvider.Value(selector, this.value);
                }
                else if (this.matchFunc != null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotValue(selector, this.matchFunc);
                    else
                        this.assertProvider.Value(selector, this.matchFunc);
                }

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have a matching Value.
            /// </summary>
            /// <param name="element"></param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotValue(element, this.value);
                    else
                        this.assertProvider.Value(element, this.value);
                }
                else if (this.matchFunc != null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.NotValue(element, this.matchFunc);
                    else
                        this.assertProvider.Value(element, this.matchFunc);
                }

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Look in the active Alert/Prompt for the specified value.
            /// </summary>
            /// <param name="accessor"></param>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (accessor.Field != AlertField.Message)
                    throw new FluentException("FluentAutomation only supports checking the message in an alert/prompt/confirmation.");

                if (this.matchFunc == null)
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.AlertNotText(this.value.ToString());
                    else
                        this.assertProvider.AlertText(this.value.ToString());
                }
                else
                {
                    if (this.assertSyntaxProvider.notMode)
                        this.assertProvider.AlertNotText(this.matchFunc);
                    else
                        this.assertProvider.AlertText(this.matchFunc);
                }

                return this.assertSyntaxProvider;
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
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.NotUrl(expectedUri);
            else
                this.assertProvider.Url(expectedUri);

            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> to return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to be matched on.</param>
        public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
        {
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.NotUrl(uriExpression);
            else
                this.assertProvider.Url(uriExpression);

            return this.assertSyntaxProvider;
        }
        #endregion

        #region Boolean / Throws
        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc"></param>
        public AssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
        {
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.False(matchFunc);
            else
                this.assertProvider.True(matchFunc);

            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.
        /// </summary>
        /// <param name="matchFunc"></param>
        public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
        {
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.True(matchFunc);
            else
                this.assertProvider.False(matchFunc);

            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        public AssertSyntaxProvider Throws(Expression<Action> matchAction)
        {
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.NotThrows(matchAction);
            else
                this.assertProvider.Throws(matchAction);

            return this.assertSyntaxProvider;
        }
        #endregion

        /// <summary>
        /// Assert the element specified exists.
        /// </summary>
        /// <param name="selector">Element selector.</param>
        public AssertSyntaxProvider Exists(string selector)
        {
            if (this.assertSyntaxProvider.notMode)
                this.assertProvider.NotExists(selector);
            else
                this.assertProvider.Exists(selector);

            return this.assertSyntaxProvider;
        }
    }

    public class BaseAssertSyntaxProvider
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IAssertProvider assertProvider = null;
        internal readonly AssertSyntaxProvider assertSyntaxProvider = null;

        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider)
            : this(commandProvider, assertProvider, null)
        {
        }

        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider)
        {
            this.commandProvider = commandProvider;
            this.assertProvider = assertProvider;
            this.assertSyntaxProvider = assertSyntaxProvider == null ? (AssertSyntaxProvider)this : assertSyntaxProvider;
        }
    }
}
