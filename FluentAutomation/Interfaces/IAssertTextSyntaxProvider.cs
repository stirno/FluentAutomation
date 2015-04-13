namespace FluentAutomation
{
    public interface IAssertTextSyntaxProvider
    {
        /// <summary>
        /// Assert that Text does not match - Reverses assertions in this chain.
        /// </summary>
        IAssertTextSyntaxProvider Not { get; }

        /// <summary>
        /// Element matching <paramref name="selector"/> that should match Text.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IAssertSyntaxProvider In(string selector);

        /// <summary>
        /// Specified <paramref name="element"/> that should match Text.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        IAssertSyntaxProvider In(ElementProxy element);

        /// <summary>
        /// Look in the active Alert/Prompt for the specified text. If the text does not match the prompt will be cleanly exited to allow clean failure or continuation of the test.
        /// </summary>
        /// <param name="accessor"></param>
        IAssertSyntaxProvider In(Alert accessor);
    }
}