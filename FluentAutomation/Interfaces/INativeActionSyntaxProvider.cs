using System;
using System.Linq.Expressions;

namespace FluentAutomation.Interfaces
{
    public interface INativeActionSyntaxProvider : ISyntaxProvider
    {
        // native only
        /// <summary>
        /// Click a specified element.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        void Click(Func<IElement> element);

        /// <summary>
        /// Click a specified coordinate, starting from the position of the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void Click(Func<IElement> element, int x, int y);

        /// <summary>
        /// DoubleClick a specified element.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        void DoubleClick(Func<IElement> element);

        /// <summary>
        /// DoubleClick a specified coordinate, starting from the position of the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void DoubleClick(Func<IElement> element, int x, int y);

        /// <summary>
        /// RightClick a specified element.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        void RightClick(Func<IElement> element);

        /// <summary>
        /// Begin a Drag/Drop operation starting with the specified element.
        /// </summary>
        /// <param name="element"><see cref="IElement"/> factory function.</param>
        /// <returns><c>DragDropSyntaxProvider</c></returns>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(Func<IElement> element);

        /// <summary>
        /// Find an element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle DOM selector.</param>
        /// <returns>IElement factory function for lazy access to elements.</returns>
        Func<IElement> Find(string selector);

        /// <summary>
        /// Find a set of elements matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns>IElement factory function for lazy access to a collection of elements.</returns>
        Func<System.Collections.Generic.IEnumerable<IElement>> FindMultiple(string selector);

        /// <summary>
        /// Sets the focus to a specific element.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        void Focus(Func<IElement> element);

        /// <summary>
        /// Causes the mouse to hover over a specified element.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        void Hover(Func<IElement> element);

        /// <summary>
        /// Causes the mouse to hover over a specific coordinate, starting from the position of the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void Hover(Func<IElement> element, int x, int y);

        /// <summary>
        /// Upload a file via a standard <c><input type='file'></input></c> DOM element. Triggers a click event at the specified coordinate, starting from the position of the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element">IElement factory function for the <c><input type='file'></input></c> DOM element.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="fileName">Path to the local file to be uploaded. Example: <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c></param>
        void Upload(Func<IElement> element, int x, int y, string fileName);

        /// <summary>
        /// Upload a file via a standard <c><input type='file'></input></c> DOM element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="fileName">Path to the local file to be uploaded. Example: <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c></param>
        void Upload(Func<IElement> element, string fileName);

        /// <summary>
        /// Upload a file via a standard <c><input type='file'></input></c> DOM element. Triggers a click event at the specified coordinate, starting from the position of the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Selector for the <c><input type='file'></input></c> DOM element.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="fileName">Path to the local file to be uploaded. Example: <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c></param>
        void Upload(string selector, int x, int y, string fileName);

        /// <summary>
        /// Uploads a file via a standard <c><input type='file'></input></c> DOM element.
        /// </summary>
        /// <param name="selector">Selector for the <c><input type='file'></input></c> DOM element.</param>
        /// <param name="fileName">Path to the local file to be uploaded. Example: <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c></param>
        void Upload(string selector, string fileName);

        /// <summary>
        /// Wait until the provided <paramref name="conditionAction">action</paramref> succeeds. Intended for use with I.Expect.* methods.
        /// </summary>
        /// <param name="conditionAction">Action to be repeated until it succeeds or exceeds the timeout. <see cref="Settings.DefaultWaitUntilTimeout"/> determines the timeout.</param>
        void WaitUntil(Expression<Action> conditionAction);

        /// <summary>
        /// Wait until the provided <paramref name="conditionFunc">function</paramref> returns <c>true</c>.
        /// </summary>
        /// <param name="conditionFunc">Function to be repeated until it returns true or exceeds the timeout. <see cref="Settings.DefaultWaitUntilTimeout"/> determines the timeout.</param>
        void WaitUntil(Expression<Func<bool>> conditionFunc);

        // remote commands
        /// <summary>
        /// Click at the specified coordinates.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate</param>
        void Click(int x, int y);

        /// <summary>
        /// Click the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        void Click(string selector);

        /// <summary>
        /// Click a specified coordinate, starting from the position of the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void Click(string selector, int x, int y);

        /// <summary>
        /// DoubleClick at the specified coordinates.
        /// </summary>
        /// <param name="x">X-coordinate specified.</param>
        /// <param name="y">Y-coordinate specified.</param>
        void DoubleClick(int x, int y);

        /// <summary>
        /// DoubleClick the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        void DoubleClick(string selector);

        /// <summary>
        /// DoubleClick a specified coordinate, starting from the position of the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void DoubleClick(string selector, int x, int y);

        /// <summary>
        /// RightClick the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        void RightClick(string selector);

        /// <summary>
        /// Begin a Drag/Drop operation starting with the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        /// <returns><c>DragDropSyntaxProvider</c></returns>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector);

        /// <summary>
        /// Enter a number or other object value into a valid input or textarea. Syntactical candy to avoid having to call .ToString() on integers in tests.
        /// </summary>
        /// <param name="nonString">Value to enter into input or textarea.</param>
        /// <returns><c>TextEntrySyntaxProvider</c></returns>
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(dynamic nonString);

        /// <summary>
        /// Enter text into a valid input or textarea.
        /// </summary>
        /// <param name="text">Text to enter into input or textarea.</param>
        /// <returns><c>TextEntrySyntaxProvider</c></returns>
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(string text);

        /// <summary>
        /// Sets the focus to element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        void Focus(string selector);

        /// <summary>
        /// Causes the mouse to hover over a specified coordinate.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        void Hover(int x, int y);

        /// <summary>
        /// Causes the mouse to hover over element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector"></param>
        void Hover(string selector);

        /// <summary>
        /// Causes the mouse to hover over a specific coordinate, starting from the position of the element matching <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">Sizzle selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        void Hover(string selector, int x, int y);

        /// <summary>
        /// Open a web browser and navigate to specified URL
        /// </summary>
        /// <param name="url">Fully-qualified URL. Example: <c>http://google.com/</c></param>
        void Open(string url);

        /// <summary>
        /// Open a web browser and navigate to the specified URI
        /// </summary>
        /// <param name="uri">Absolute URI. Example: <c>new Uri("http://www.google.com/");</c></param>
        void Open(Uri uri);

        /// <summary>
        /// Triggers keypress events using WinForms SendKeys.
        /// </summary>
        /// <param name="keys">WinForms SendKeys values. Example: <c>{ENTER}</c></param>
        void Press(string keys);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting options with matching <paramref name="values"/> using the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">Mode of interaction with the <c>&lt;select /></c>; by Text, Value or Index.</param>
        /// <param name="values">Options to be selected.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, params string[] values);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting an option with matching <paramref name="value"/> using the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">Mode of interaction with the <c>&lt;select /></c>; by Text, Value or Index.</param>
        /// <param name="value">Option to be selected.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, string value);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting options at the specified <paramref name="indices"/>.
        /// </summary>
        /// <param name="indices">Options to be selected by Index.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(params int[] indices);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting options with matching <paramref name="text"/>.
        /// </summary>
        /// <param name="values">Options to be selected by Text.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(params string[] text);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting an option at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Option to be selected by Index.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(int index);

        /// <summary>
        /// Manipulates a <c>&lt;select /></c> DOM element by selecting an option with matching <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Option to be selected by Text.</param>
        /// <returns><c>SelectSyntaxProvider</c></returns>
        ActionSyntaxProvider.SelectSyntaxProvider Select(string text);

        /// <summary>
        /// Takes a screenshot of the active web browser window.
        /// </summary>
        /// <param name="screenshotName">Filename to save the screenshot with.</param>
        void TakeScreenshot(string screenshotName);

        /// <summary>
        /// Triggers Win32 events per character in the provided string. Not for use with WinForms SendKeys commands, only simple characters. Useful
        /// for entering text into applications that can gain focus but do not have proper DOM representation for use with <see cref="Enter"/>.
        /// </summary>
        /// <param name="text">String to be sent, one character at a time.</param>
        void Type(string text);

        /// <summary>
        /// Waits a determined period of time.
        /// </summary>
        /// <param name="seconds">Seconds to wait.</param>
        void Wait(int seconds);

        /// <summary>
        /// Waits a determined period of time.
        /// </summary>
        /// <param name="timeSpan">TimeSpan to wait.</param>
        void Wait(TimeSpan timeSpan);

        /// <summary>
        /// Expects - Fluent's assertion functionality.
        /// </summary>
        ExpectSyntaxProvider Expect { get; }
    }
}
