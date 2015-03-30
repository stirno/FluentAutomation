using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface ITextAppendSyntaxProvider
    {
        /// <summary>
        /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
        /// that bind to the keyup/keydown/keypress events to function.
        /// </summary>
        /// <returns><c>TextEntrySyntaxProvider</c></returns>
        ITextAppendSyntaxProvider WithoutEvents();

        /// <summary>
        /// Enter text into input or textarea element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IActionSyntaxProvider To(string selector);

        /// <summary>
        /// Enter text into specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider To(ElementProxy element);
    }
}