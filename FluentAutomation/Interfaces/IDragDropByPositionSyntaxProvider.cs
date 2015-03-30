using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface IDragDropByPositionSyntaxProvider
    {
        /// <summary>
        /// End Drag/Drop operation at specified coordinates.
        /// </summary>
        /// <param name="destinationX">X coordinate</param>
        /// <param name="destinationY">Y coordinate</param>
        IActionSyntaxProvider To(int destinationX, int destinationY);

        /// <summary>
        /// End Drag/Drop operation at the element specified by <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector"></param>
        void To(string selector);

        /// <summary>
        /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
        /// </summary>
        /// <param name="targetElement">IElement factory function.</param>
        void To(ElementProxy targetElement);

        /// <summary>
        /// End Drag/Drop operation at the element specified by <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="targetOffsetX">X-offset for drop.</param>
        /// <param name="targetOffsetY">Y-offset for drop.</param>
        void To(string selector, int targetOffsetX, int targetOffsetY);

        /// <summary>
        /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
        /// </summary>
        /// <param name="targetElement">IElement factory function.</param>
        /// <param name="targetOffsetX">X-offset for drop.</param>
        /// <param name="targetOffsetY">Y-offset for drop.</param>
        void To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY);
    }
}