using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface ITextEntrySyntaxProvider
    {
        /// <summary>
        /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
        /// that bind to the keyup/keydown/keypress events to function.
        /// </summary>
        /// <returns><c>TextEntrySyntaxProvider</c></returns>
        ActionSyntaxProvider.TextEntrySyntaxProvider WithoutEvents();

        /// <summary>
        /// Enter text into input or textarea element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IActionSyntaxProvider In(string selector);

        /// <summary>
        /// Enter text into specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider In(ElementProxy element);

        /// <summary>
        /// Enter text into the active prompt
        /// </summary>
        /// <param name="accessor"></param>
        IActionSyntaxProvider In(Alert accessor);
    }
}