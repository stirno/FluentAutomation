using System;
using System.Linq.Expressions;

namespace FluentAutomation
{
    public interface INotAssertSyntaxProvider
    {
        /// <summary>
        /// Assert the current web browser's URL not to match <paramref name="expectedUrl"/>.
        /// </summary>
        /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
        IAssertSyntaxProvider Url(string expectedUrl);

        /// <summary>
        /// Assert the current web browser's URI shouldn't match <paramref name="expectedUri"/>.
        /// </summary>
        /// <param name="expectedUri">Absolute URI to use for matching.</param>
        IAssertSyntaxProvider Url(Uri expectedUri);

        /// <summary>
        /// Assert the current web browser's URI provided to the specified <paramref name="uriExpression">URI expression</paramref> will return false;
        /// </summary>
        /// <param name="uriExpression">URI expression to use for matching.</param>
        IAssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return true.
        /// </summary>
        /// <param name="matchFunc"></param>
        IAssertSyntaxProvider True(Expression<Func<bool>> matchFunc);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return false.
        /// </summary>
        /// <param name="matchFunc"></param>
        IAssertSyntaxProvider False(Expression<Func<bool>> matchFunc);

        /// <summary>
        /// Assert that an arbitrary <paramref name="matchAction">action</paramref> does not throw an Exception.
        /// </summary>
        /// <param name="matchAction"></param>
        IAssertSyntaxProvider Throws(Expression<Action> matchAction);

        /// <summary>
        /// Assert the element specified does not exist.
        /// </summary>
        /// <param name="selector">Element selector.</param>
        IAssertSyntaxProvider Exists(string selector);

        /// <summary>
        /// Assert the element specified does not exist.
        /// </summary>
        /// <param name="selector">Element reference.</param>
        IAssertSyntaxProvider Exists(ElementProxy element);

        /// <summary>
        /// Assert that the element matching the selector is not visible and cannot be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        IAssertSyntaxProvider Visible(string selector);

        /// <summary>
        /// Assert that the element is not visible and cannot be interacted with.
        /// </summary>
        /// <param name="selector"></param>
        IAssertSyntaxProvider Visible(ElementProxy element);
    }
}