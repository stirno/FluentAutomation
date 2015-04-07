using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface ISelectSyntaxProvider
    {
        /// <summary>
        /// Select from element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IActionSyntaxProvider From(string selector);

        /// <summary>
        /// Select from specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider From(ElementProxy element);
    }
}