using System;
using System.Linq.Expressions;

namespace FluentAutomation
{
    public interface IAssertSyntaxProvider
    {
        /// <summary>
        /// Negative assertions
        /// </summary>
        INotAssertSyntaxProvider Not { get; }

        /// <summary>
        /// Assert a specific count.
        /// </summary>
        /// <param name="count">Number of elements found.</param>
        /// <returns><c>AssertCountSyntaxProvider</c></returns>
        IAssertCountSyntaxProvider Count(int count);

        /// <summary>
        /// Assert that a matching CSS class is found.
        /// </summary>
        /// <param name="className">CSS class name. Example: .row</param>
        /// <returns><c>AssertClassSyntaxProvider</c></returns>
        IAssertClassSyntaxProvider Class(string className);

        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        IAssertCssPropertySyntaxProvider Css(string propertyName);

        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        /// <param name="propertyValue">CSS property value. Example: red</param>
        IAssertCssPropertySyntaxProvider Css(string propertyName, string propertyValue);

        /// <summary>
        /// Assert that a matching attribute is found.
        /// </summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        IAssertAttributeSyntaxProvider Attribute(string attributeName);

        /// <summary>
        /// Assert that a matching CSS property is found.
        /// </summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        /// <param name="attributeValue">Attribute value. Example: image.jpg</param>
        IAssertAttributeSyntaxProvider Attribute(string attributeName, string propertyValue);

        /// <summary>
        /// Assert that Text matches specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text that must be exactly matched.</param>
        /// <returns><c>AssertTextSyntaxProvider</c></returns>
        IAssertTextSyntaxProvider Text(string text);

        /// <summary>
        /// Assert that Text provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Text matches. Example: (text) => text.Contains("Hello")</param>
        /// <returns><c>AssertTextSyntaxProvider</c></returns>
        IAssertTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc);

        /// <summary>
        /// Assert a specific integer <paramref name="value"/>
        /// </summary>
        /// <param name="value">Int32 value expected.</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        IAssertValueSyntaxProvider Value(int value);

        /// <summary>
        /// Assert a specific string <paramref name="value"/>.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        IAssertValueSyntaxProvider Value(string value);

        /// <summary>
        /// Assert that value provided to specified <paramref name="matchFunc">match function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc">Function to evaluate if Value matches. Example: (value) => value != "Hello" && value != "World"</param>
        /// <returns><c>AssertValueSyntaxProvider</c></returns>
        IAssertValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc);

        /// <summary>
        /// Assert the current web browser's URL to match <paramref name="expectedUrl"/>.
        /// </summary>
        /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
        IAssertSyntaxProvider Url(string expectedUrl);

        /// <summary>
        /// Assert the current web browser's URI to match <paramref name="expectedUri"/>.
        /// </summary>
        /// <param name="expectedUri">Absolute URI to use for matching..</param>
        IAssertSyntaxProvider Url(Uri expectedUri);

        /// <summary>
        /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> will return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to use for matching..</param>
        IAssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns true.
        /// </summary>
        /// <param name="matchFunc"></param>
        IAssertSyntaxProvider True(Expression<Func<bool>> matchFunc);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.
        /// </summary>
        /// <param name="matchFunc"></param>
        IAssertSyntaxProvider False(Expression<Func<bool>> matchFunc);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        IAssertSyntaxProvider Throws(Expression<Action> matchAction);

        /// <summary>
        /// Assert the element specified exists.
        /// </summary>
        /// <param name="selector">Element selector.</param>
        IAssertSyntaxProvider Exists(string selector);

        /// <summary>
        /// Assert the element specified exists.
        /// </summary>
        /// <param name="element">Reference to element</param>
        /// <returns></returns>
        IAssertSyntaxProvider Exists(ElementProxy element);

        /// <summary>
        /// Assert that the element matching the selector is visible and can be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        IAssertSyntaxProvider Visible(string selector);

        /// <summary>
        /// Assert that the element is visible and can be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        IAssertSyntaxProvider Visible(ElementProxy element);
    }
}