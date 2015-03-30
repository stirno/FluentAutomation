using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface IDragDropSyntaxProvider
    {
        /// <summary>
        /// End Drag/Drop operation at element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        IActionSyntaxProvider To(string selector);

        /// <summary>
        /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
        /// </summary>
        /// <param name="targetElement">IElement factory function.</param>
        IActionSyntaxProvider To(ElementProxy targetElement);

        /// <summary>
        /// End Drag/Drop operation at element specified by <paramref name="selector"/> with offset.
        /// </summary>
        /// <param name="selectr"></param>
        /// <param name="targetOffsetX">X-offset for drop.</param>
        /// <param name="targetOffsetY">Y-offset for drop.</param>
        IActionSyntaxProvider To(string selector, int targetOffsetX, int targetOffsetY);

        /// <summary>
        /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
        /// </summary>
        /// <param name="targetElement">IElement factory function.</param>
        /// <param name="targetOffsetX">X-offset for drop.</param>
        /// <param name="targetOffsetY">Y-offset for drop.</param>
        IActionSyntaxProvider To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY);
    }
}