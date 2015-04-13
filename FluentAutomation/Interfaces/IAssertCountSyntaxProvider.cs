namespace FluentAutomation
{
    public interface IAssertCountSyntaxProvider
    {
        /// <summary>
        /// Assert that the Count does not match - Reverses assertions in this chain.
        /// </summary>
        IAssertCountSyntaxProvider Not { get; }

        /// <summary>
        /// Elements matching <paramref name="selector"/> to be counted.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IAssertSyntaxProvider Of(string selector);

        /// <summary>
        /// Specified <paramref name="elements"/> to be counted.
        /// </summary>
        /// <param name="elements">IElement collection factory function.</param>
        IAssertSyntaxProvider Of(ElementProxy elements);
    }
}