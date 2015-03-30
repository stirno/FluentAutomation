using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public interface ISwitchSyntaxProvider
    {
        /// <summary>
        /// Switch to a window by name or URL (can be relative such as /about -- matches on the end of the URL)
        /// </summary>
        /// <param name="windowName"></param>
        IActionSyntaxProvider Window(string windowName);

        /// <summary>
        /// Switch back to the primary window
        /// </summary>
        IActionSyntaxProvider Window();

        /// <summary>
        /// Switch to a frame/iframe via page selector or ID
        /// </summary>
        /// <param name="frameSelector"></param>
        IActionSyntaxProvider Frame(string frameSelector);

        /// <summary>
        /// Switch back to the top-level document
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider Frame();

        /// <summary>
        /// Switch focus to a previously selected frame/iframe
        /// </summary>
        /// <param name="frameElement"></param>
        /// <returns></returns>
        IActionSyntaxProvider Frame(ElementProxy frameElement);
    }
}