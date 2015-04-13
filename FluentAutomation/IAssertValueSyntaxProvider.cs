namespace FluentAutomation
{
    public interface IAssertValueSyntaxProvider
    {
        /// <summary>
        /// Assert that Value does not match - Reverses assertions in this chain.
        /// </summary>
        IAssertValueSyntaxProvider Not { get; }

        /// <summary>
        /// Element matching <paramref name="selector"/> that should have a matching Value.
        /// </summary>
        /// <param name="selector"></param>
        IAssertSyntaxProvider In(string selector);

        /// <summary>
        /// Specified <paramref name="element"/> that should have a matching Value.
        /// </summary>
        /// <param name="element"></param>
        IAssertSyntaxProvider In(ElementProxy element);

        /// <summary>
        /// Look in the active Alert/Prompt for the specified value.
        /// </summary>
        /// <param name="accessor"></param>
        IAssertSyntaxProvider In(Alert accessor);
    }
}