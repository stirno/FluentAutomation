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
        public AssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider)
            : base(commandProvider, assertProvider)
        {
        }

        /// <summary>
        /// Negative assertions
        /// </summary>
        public NotAssertSyntaxProvider Not
        {
            get
            {
                return new NotAssertSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider);
            }
        }

        public class NotAssertSyntaxProvider : BaseAssertSyntaxProvider
        {
            public NotAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
            }

            /// <summary>
            /// Assert the current web browser's URL not to match <paramref name="expectedUrl"/>.
            /// </summary>
            /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
            public AssertSyntaxProvider Url(string expectedUrl)
            {
                return this.Url(new Uri(expectedUrl, UriKind.Absolute));
            }

            /// <summary>
            /// Assert the current web browser's URI shouldn't match <paramref name="expectedUri"/>.
            /// </summary>
            /// <param name="expectedUri">Absolute URI to use for matching.</param>
            public AssertSyntaxProvider Url(Uri expectedUri)
            {
                this.assertProvider.NotUrl(expectedUri);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> will return false;
            /// </summary>
            /// <param name="uriExpression">URI expression to use for matching.</param>
            public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
            {
                this.assertProvider.NotUrl(uriExpression);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return true.
            /// </summary>
            /// <param name="matchFunc"></param>
            public AssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
            {
                this.assertProvider.False(matchFunc);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return false.
            /// </summary>
            /// <param name="matchFunc"></param>
            public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
            {
                this.assertProvider.True(matchFunc);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert that an arbitrary <paramref name="matchAction">action</paramref> does not throw an Exception.
            /// </summary>
            /// <param name="matchAction"></param>
            public AssertSyntaxProvider Throws(Expression<Action> matchAction)
            {
                this.assertProvider.NotThrows(matchAction);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert the element specified does not exist.
            /// </summary>
            /// <param name="selector">Element selector.</param>
            public AssertSyntaxProvider Exists(string selector)
            {
                this.assertProvider.NotExists(selector);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert that the element matching the selector is not visible and cannot be interacted with.
            /// </summary>
            /// <param name="selector"></param>
            public AssertSyntaxProvider Visible(string selector)
            {
                this.assertProvider.NotVisible(selector);
                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Assert that the element is not visible and cannot be interacted with.
            /// </summary>
            /// <param name="selector"></param>
            public AssertSyntaxProvider Visible(ElementProxy element)
            {
                this.assertProvider.NotVisible(element);
                return this.assertSyntaxProvider;
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
            private readonly bool notMode = false;
            
            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, int count)
                : this(commandProvider, assertProvider, assertSyntaxProvider, count, false)
            {
            }

            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, int count, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.count = count;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that the Count does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertCountSyntaxProvider Not
            {
                get
                {
                    return new AssertCountSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, count, true);
                }
            }

            /// <summary>
            /// Elements matching <paramref name="selector"/> to be counted.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider Of(string selector)
            {
                if (this.notMode)
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
                if (this.notMode)
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
            private readonly bool notMode = false;

            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string className)
                : this(commandProvider, assertProvider, assertSyntaxProvider, className, false)
            {
            }

            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string className, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.className = className;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that CSS Class does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertClassSyntaxProvider Not
            {
                get
                {
                    return new AssertClassSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, className, true);
                }
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider On(string selector)
            {
                if (this.notMode)
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
                if (this.notMode)
                    this.assertProvider.NotCssClass(element, this.className);
                else
                    this.assertProvider.CssClass(element, this.className);

                return this.assertSyntaxProvider;
            }
        }
        #endregion

        #region CSS Property
        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        public AssertCssPropertySyntaxProvider Css(string propertyName)
        {
            return this.Css(propertyName, null);
        }

        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        /// <param name="propertyValue">CSS property value. Example: red</param>
        public AssertCssPropertySyntaxProvider Css(string propertyName, string propertyValue)
        {
            return new AssertCssPropertySyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, propertyName, propertyValue);
        }

        public class AssertCssPropertySyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string propertyName = null;
            private readonly string propertyValue = null;
            private readonly bool notMode = false;

            public AssertCssPropertySyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string propertyName, string propertyValue)
                : this(commandProvider, assertProvider, assertSyntaxProvider, propertyName, propertyValue, false)
            {
            }

            public AssertCssPropertySyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string propertyName, string propertyValue, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.propertyName = propertyName;
                this.propertyValue = propertyValue;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that CSS Class does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertCssPropertySyntaxProvider Not
            {
                get
                {
                    return new AssertCssPropertySyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, propertyName, propertyValue, true);
                }
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider On(string selector)
            {
                if (this.notMode)
                    this.assertProvider.NotCssProperty(selector, this.propertyName, this.propertyValue);
                else
                    this.assertProvider.CssProperty(selector, this.propertyName, this.propertyValue);

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have matching CSS class.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (this.notMode)
                    this.assertProvider.NotCssProperty(element, this.propertyName, this.propertyValue);
                else
                    this.assertProvider.CssProperty(element, this.propertyName, this.propertyValue);

                return this.assertSyntaxProvider;
            }
        }
        #endregion

        #region Attribute
        /// <summary>
        /// Assert that a matching attribute is found.
        /// </summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        public AssertAttributeSyntaxProvider Attribute(string attributeName)
        {
            return this.Attribute(attributeName, null);
        }

        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        /// <param name="attributeValue">Attribute value. Example: image.jpg</param>
        public AssertAttributeSyntaxProvider Attribute(string attributeName, string propertyValue)
        {
            return new AssertAttributeSyntaxProvider(this.commandProvider, this.assertProvider, this.assertSyntaxProvider, attributeName, propertyValue);
        }

        public class AssertAttributeSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string attributeName = null;
            private readonly string attributeValue = null;
            private readonly bool notMode = false;

            public AssertAttributeSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string attributeName, string attributeValue)
                : this(commandProvider, assertProvider, assertSyntaxProvider, attributeName, attributeValue, false)
            {
            }

            public AssertAttributeSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string attributeName, string attributeValue, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.attributeName = attributeName;
                this.attributeValue = attributeValue;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that CSS Class does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertAttributeSyntaxProvider Not
            {
                get
                {
                    return new AssertAttributeSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, attributeName, attributeValue, true);
                }
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have matching CSS class.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider On(string selector)
            {
                if (this.notMode)
                    this.assertProvider.NotAttribute(selector, this.attributeName, this.attributeValue);
                else
                    this.assertProvider.Attribute(selector, this.attributeName, this.attributeValue);

                return this.assertSyntaxProvider;
            }

            /// <summary>
            /// Specified <paramref name="element"/> that should have matching CSS class.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (this.notMode)
                    this.assertProvider.NotAttribute(element, this.attributeName, this.attributeValue);
                else
                    this.assertProvider.Attribute(element, this.attributeName, this.attributeValue);

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
            private readonly bool notMode = false;
            
            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string text)
                : this(commandProvider, assertProvider, assertSyntaxProvider, text, false)
            {

            }

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string text, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.text = text;
                this.notMode = notMode;
            }

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : this(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, false)
            {
            }

            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.matchFunc = matchFunc;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that Text does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertTextSyntaxProvider Not
            {
                get
                {
                    if (this.matchFunc != null)
                        return new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, this.matchFunc, true);
                    else
                        return new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, this.text, true);
                }
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should match Text.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public AssertSyntaxProvider In(string selector)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    if (this.notMode)
                        this.assertProvider.NotText(selector, this.text);
                    else
                        this.assertProvider.Text(selector, this.text);
                }
                else if (this.matchFunc != null)
                {
                    if (this.notMode)
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
                    if (this.notMode)
                        this.assertProvider.NotText(element, this.text);
                    else
                        this.assertProvider.Text(element, this.text);
                }
                else if (this.matchFunc != null)
                {
                    if (this.notMode)
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
                    if (this.notMode)
                        this.assertProvider.AlertNotText(this.text);
                    else
                        this.assertProvider.AlertText(this.text);
                }
                else
                {
                    if (this.notMode)
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
            private readonly bool notMode = false;

            
            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string value)
                : this(commandProvider, assertProvider, assertSyntaxProvider, value, false)
            {
            }

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string value, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.value = value;
                this.notMode = notMode;
            }

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : this(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, false)
            {
            }

            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.matchFunc = matchFunc;
                this.notMode = notMode;
            }

            /// <summary>
            /// Assert that Value does not match - Reverses assertions in this chain.
            /// </summary>
            public AssertValueSyntaxProvider Not
            {
                get
                {
                    if (this.matchFunc != null)
                        return new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, this.matchFunc, true);
                    else
                        return new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, this.value, true);
                }
            }

            /// <summary>
            /// Element matching <paramref name="selector"/> that should have a matching Value.
            /// </summary>
            /// <param name="selector"></param>
            public AssertSyntaxProvider In(string selector)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    if (this.notMode)
                        this.assertProvider.NotValue(selector, this.value);
                    else
                        this.assertProvider.Value(selector, this.value);
                }
                else if (this.matchFunc != null)
                {
                    if (this.notMode)
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
                    if (this.notMode)
                        this.assertProvider.NotValue(element, this.value);
                    else
                        this.assertProvider.Value(element, this.value);
                }
                else if (this.matchFunc != null)
                {
                    if (this.notMode)
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
                    if (this.notMode)
                        this.assertProvider.AlertNotText(this.value.ToString());
                    else
                        this.assertProvider.AlertText(this.value.ToString());
                }
                else
                {
                    if (this.notMode)
                        this.assertProvider.AlertNotText(this.value.ToString());
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
        /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
        public AssertSyntaxProvider Url(string expectedUrl)
        {
            return Url(new Uri(expectedUrl, UriKind.Absolute));
        }

        /// <summary>
        /// Assert the current web browser's URI to match <paramref name="expectedUri"/>.
        /// </summary>
        /// <param name="expectedUri">Absolute URI to use for matching..</param>
        public AssertSyntaxProvider Url(Uri expectedUri)
        {
            this.assertProvider.Url(expectedUri);
            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> will return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to use for matching..</param>
        public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
        {
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
            this.assertProvider.True(matchFunc);
            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.
        /// </summary>
        /// <param name="matchFunc"></param>
        public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
        {
            this.assertProvider.False(matchFunc);
            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        public AssertSyntaxProvider Throws(Expression<Action> matchAction)
        {
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
            this.assertProvider.Exists(selector);
            return this.assertSyntaxProvider;
        }

        /// <summary>
        /// Assert that the element matching the selector is visible and can be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        public AssertSyntaxProvider Visible(string selector)
        {
            this.assertProvider.Visible(selector);
            return this;
        }

        /// <summary>
        /// Assert that the element is visible and can be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        public AssertSyntaxProvider Visible(ElementProxy element)
        {
            this.assertProvider.Visible(element);
            return this;
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
