namespace FluentAutomation
{
    public interface IAssertClassSyntaxProvider
    {
        /// <summary>
        /// Assert that CSS Class does not match - Reverses assertions in this chain.
        /// </summary>
        IAssertClassSyntaxProvider Not { get; }

        /// <summary>
        /// Element matching <paramref name="selector"/> that should have matching CSS class.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IAssertSyntaxProvider On(string selector);

        /// <summary>
        /// Specified <paramref name="element"/> that should have matching CSS class.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        IAssertSyntaxProvider On(ElementProxy element);
    }
}