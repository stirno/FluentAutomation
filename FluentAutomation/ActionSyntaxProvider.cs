using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using FluentAutomation.Exceptions;

namespace FluentAutomation
{
    public class ActionSyntaxProvider : IActionSyntaxProvider, IDisposable
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IAssertProvider assertProvider = null;
        internal FluentSettings settings = null;
  
        public ActionSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, FluentSettings settings)
        {
            this.commandProvider = commandProvider.WithConfig(settings);
            this.assertProvider = assertProvider;
            this.settings = settings;
        }

        internal ActionSyntaxProvider WithConfig(FluentSettings settings)
        {
            this.commandProvider.WithConfig(settings);
            this.settings = settings;
            return this;
        }

        #region Direct Execution Actions
        public IActionSyntaxProvider Open(string url)
        {
            if (url.StartsWith("/"))
                return this.Open(new Uri(url, UriKind.Relative));
            else
                return this.Open(new Uri(url, UriKind.Absolute));
        }

        public IActionSyntaxProvider Open(Uri url)
        {
            this.commandProvider.Navigate(url);
            return this;
        }

        public ElementProxy Find(string selector)
        {
            return this.commandProvider.Find(selector);
        }

        public ElementProxy FindMultiple(string selector)
        {
            return this.commandProvider.FindMultiple(selector);
        }

        public IActionSyntaxProvider Click(int x, int y)
        {
            this.commandProvider.Click(x, y);
            return this;
        }

        public IActionSyntaxProvider Click(string selector, int x, int y)
        {
            return this.Click(this.Find(selector), x, y);
        }

        public IActionSyntaxProvider Click(ElementProxy element, int x, int y)
        {
            this.commandProvider.Click(element, x, y);
            return this;
        }

        public IActionSyntaxProvider Click(string selector)
        {
            return this.Click(this.Find(selector));
        }

        public IActionSyntaxProvider Click(ElementProxy element)
        {
            this.commandProvider.Click(element);
            return this;
        }

        public IActionSyntaxProvider Click(Alert alertAccessor)
        {
            if (alertAccessor.Field != AlertField.OKButton && alertAccessor.Field != AlertField.CancelButton)
            {
                this.Click(Alert.Cancel);
                throw new FluentException("FluentAutomation only supports clicking the OK or Cancel buttons of an alert/prompt.");
            }

            this.commandProvider.AlertClick(alertAccessor);
            return this;
        }

        public IActionSyntaxProvider Scroll(int x, int y)
        {
            this.commandProvider.Hover(x, y);
            return this;
        }

        public IActionSyntaxProvider Scroll(string selector)
        {
            return this.Scroll(this.Find(selector));
        }

        public IActionSyntaxProvider Scroll(ElementProxy element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public IActionSyntaxProvider DoubleClick(int x, int y)
        {
            this.commandProvider.DoubleClick(x, y);
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector, int x, int y)
        {
            return this.DoubleClick(this.Find(selector), x, y);
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element, int x, int y)
        {
            this.commandProvider.DoubleClick(element, x, y);
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector)
        {
            return this.DoubleClick(this.Find(selector));
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element)
        {
            this.commandProvider.DoubleClick(element);
            return this;
        }

        public IActionSyntaxProvider RightClick(int x, int y)
        {
            this.commandProvider.RightClick(x, y);
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector, int x, int y)
        {
            return this.RightClick(this.Find(selector), x, y);
        }

        public IActionSyntaxProvider RightClick(ElementProxy element, int x, int y)
        {
            this.commandProvider.RightClick(element, x, y);
            return this;
        }


        public IActionSyntaxProvider RightClick(string selector)
        {
            return this.RightClick(this.Find(selector));
        }

        public IActionSyntaxProvider RightClick(ElementProxy element)
        {
            this.commandProvider.RightClick(element);
            return this;
        }

        public IActionSyntaxProvider Hover(int x, int y)
        {
            this.commandProvider.Hover(x, y);
            return this;
        }

        public IActionSyntaxProvider Hover(string selector, int x, int y)
        {
            return this.Hover(this.Find(selector), x, y);
        }

        public IActionSyntaxProvider Hover(ElementProxy element, int x, int y)
        {
            this.commandProvider.Hover(element, x, y);
            return this;
        }

        public IActionSyntaxProvider Hover(string selector)
        {
            return this.Hover(this.Find(selector));
        }

        public IActionSyntaxProvider Hover(ElementProxy element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public IActionSyntaxProvider Focus(string selector)
        {
            return this.Focus(this.Find(selector));
        }

        public IActionSyntaxProvider Focus(ElementProxy element)
        {
            this.commandProvider.Focus(element);
            return this;
        }

        public IActionSyntaxProvider Press(string keys)
        {
            this.commandProvider.Press(keys);
            return this;
        }

        public IActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            this.commandProvider.TakeScreenshot(screenshotName);
            return this;
        }

        public IActionSyntaxProvider Type(string text)
        {
            this.commandProvider.Type(text);
            return this;
        }

        public IActionSyntaxProvider Wait()
        {
            this.commandProvider.Wait();
            return this;
        }

        public IActionSyntaxProvider Wait(int seconds)
        {
            return this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public IActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            this.commandProvider.Wait(timeSpan);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.commandProvider.WaitUntil(conditionFunc);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            this.commandProvider.WaitUntil(conditionAction);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int secondsToWait)
        {
            return this.WaitUntil(conditionFunc, TimeSpan.FromSeconds(secondsToWait));
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionFunc, timeout);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int secondsToWait)
        {
            return this.WaitUntil(conditionAction, TimeSpan.FromSeconds(secondsToWait));
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionAction, timeout);
            return this;
        }

        public IActionSyntaxProvider Upload(string selector, string fileName)
        {
            return this.Upload(selector, 0, 0, fileName);
        }

        public IActionSyntaxProvider Upload(string selector, int x, int y, string fileName)
        {
            return this.Upload(this.Find(selector), x, y, fileName);
        }

        public IActionSyntaxProvider Upload(ElementProxy element, string fileName)
        {
            return this.Upload(element, 0, 0, fileName);
        }

        public IActionSyntaxProvider Upload(ElementProxy element, int x, int y, string fileName)
        {
            this.commandProvider.UploadFile(element, x, y, fileName);
            return this;
        }

        private ISwitchSyntaxProvider switchProvider = null;
        public ISwitchSyntaxProvider Switch
        {
            get
            {
                if (switchProvider == null)
                {
                    switchProvider = new SwitchSyntaxProvider(this);
                }

                return switchProvider;
            }
        }

        public class SwitchSyntaxProvider : ISwitchSyntaxProvider
        {
            private readonly ActionSyntaxProvider syntaxProvider = null;

            internal SwitchSyntaxProvider(ActionSyntaxProvider syntaxProvider)
            {
                this.syntaxProvider = syntaxProvider;
            }

            /// <summary>
            /// Switch to a window by name or URL (can be relative such as /about -- matches on the end of the URL)
            /// </summary>
            /// <param name="windowName"></param>
            public IActionSyntaxProvider Window(string windowName)
            {
                this.syntaxProvider.commandProvider.SwitchToWindow(windowName);
                return this.syntaxProvider;
            }

            /// <summary>
            /// Switch back to the primary window
            /// </summary>
            public IActionSyntaxProvider Window()
            {
                return this.Window(string.Empty);
            }

            /// <summary>
            /// Switch to a frame/iframe via page selector or ID
            /// </summary>
            /// <param name="frameSelector"></param>
            public IActionSyntaxProvider Frame(string frameSelector)
            {
                this.syntaxProvider.commandProvider.SwitchToFrame(frameSelector);
                return this.syntaxProvider;
            }

            /// <summary>
            /// Switch back to the top-level document
            /// </summary>
            /// <returns></returns>
            public IActionSyntaxProvider Frame()
            {
                return this.Frame(string.Empty);
            }

            /// <summary>
            /// Switch focus to a previously selected frame/iframe
            /// </summary>
            /// <param name="frameElement"></param>
            /// <returns></returns>
            public IActionSyntaxProvider Frame(ElementProxy frameElement)
            {
                this.syntaxProvider.commandProvider.SwitchToFrame(frameElement);
                return this.syntaxProvider;
            }
        }
    
        #endregion


        //todo  fix it
        #region Drag/Drop
        public IDragDropSyntaxProvider Drag(string selector)
        {
            return this.Drag(this.Find(selector));
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element)
        {
            return new DragDropSyntaxProvider(this, element);
        }

        public IDragDropSyntaxProvider Drag(string selector, int sourceX, int sourceY)
        {
            return this.Drag(this.Find(selector), sourceX, sourceY);
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element, int sourceX, int sourceY)
        {
            return new DragDropSyntaxProvider(this, element, sourceX, sourceY);
        }
        
        public IDragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY)
        {
            return new DragDropByPositionSyntaxProvider(this, sourceX, sourceY);
        }
        
        public class DragDropSyntaxProvider : IDragDropSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly ElementProxy sourceElement = null;
            protected readonly int offsetX = 0;
            protected readonly int offsetY = 0;

            internal DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, ElementProxy element)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
            }

            internal DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, ElementProxy element, int offsetX, int offsetY)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
                this.offsetX = offsetX;
                this.offsetY = offsetY;
            }

            /// <summary>
            /// End Drag/Drop operation at element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy targetElement)
            {
                if (this.offsetX != 0 || this.offsetY != 0)
                {
                    this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, offsetX, offsetY, targetElement, 0, 0);
                }
                else
                {
                    this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, targetElement);
                }
                
                return this.syntaxProvider;
            }

            /// <summary>
            /// End Drag/Drop operation at element specified by <paramref name="selector"/> with offset.
            /// </summary>
            /// <param name="selectr"></param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public IActionSyntaxProvider To(string selector, int targetOffsetX, int targetOffsetY)
            {
                return this.To(this.syntaxProvider.Find(selector), targetOffsetX, targetOffsetY);
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public IActionSyntaxProvider To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, offsetX, offsetY, targetElement, targetOffsetX, targetOffsetY);
                return this.syntaxProvider;
            }
        }

        public class DragDropByPositionSyntaxProvider : IDragDropByPositionSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly int sourceX = 0;
            protected readonly int sourceY = 0;

            public DragDropByPositionSyntaxProvider(ActionSyntaxProvider syntaxProvider, int sourceX, int sourceY)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceX = sourceX;
                this.sourceY = sourceY;
            }
            
            /// <summary>
            /// End Drag/Drop operation at specified coordinates.
            /// </summary>
            /// <param name="destinationX">X coordinate</param>
            /// <param name="destinationY">Y coordinate</param>
            public IActionSyntaxProvider To(int destinationX, int destinationY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceX, this.sourceY, destinationX, destinationY);
                return this.syntaxProvider;
            }

            /// <summary>
            /// End Drag/Drop operation at the element specified by <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector"></param>
            public void To(string selector)
            {
                this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public void To(ElementProxy targetElement)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.syntaxProvider.commandProvider.Find("html"), this.sourceX, this.sourceY, targetElement, 0, 0);
            }

            /// <summary>
            /// End Drag/Drop operation at the element specified by <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector"></param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public void To(string selector, int targetOffsetX, int targetOffsetY)
            {
                this.To(this.syntaxProvider.Find(selector), targetOffsetX, targetOffsetY);
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public void To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.syntaxProvider.commandProvider.Find("html"), this.sourceX, this.sourceY, targetElement, targetOffsetX, targetOffsetY);
            }
        }
        #endregion

        #region <input />, <textarea />
        public ITextAppendSyntaxProvider Append(string text)
        {
            return new TextAppendSyntaxProvider(this, text);
        }

        public ITextAppendSyntaxProvider Append(dynamic nonString)
        {
            return this.Append(nonString.ToString());
        }

        public ITextEntrySyntaxProvider Enter(string text)
        {
            return new TextEntrySyntaxProvider(this, text);
        }

        public ITextEntrySyntaxProvider Enter(dynamic nonString)
        {
            return this.Enter(nonString.ToString());
        }

        public class TextEntrySyntaxProvider : ITextEntrySyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;

            internal TextEntrySyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public ITextEntrySyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider In(string selector)
            {
                return this.In(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider In(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Enter().In() is only supported on text elements (input, textarea, etc).");

                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.EnterText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.EnterTextWithoutEvents(element, text);
                }

                return this.syntaxProvider;
            }

            /// <summary>
            /// Enter text into the active prompt
            /// </summary>
            /// <param name="accessor"></param>
            public IActionSyntaxProvider In(Alert accessor)
            {
                if (accessor.Field != AlertField.Input)
                {
                    this.syntaxProvider.commandProvider.AlertClick(Alert.Cancel);
                    throw new FluentException("FluentAutomation only supports entering text in text input of a prompt.");
                }

                this.syntaxProvider.commandProvider.AlertEnterText(text);
                return this.syntaxProvider;
            }
        }

        public class TextAppendSyntaxProvider : ITextAppendSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;
            protected bool isAppend = false;

            internal TextAppendSyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public ITextAppendSyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Append().To() is only supported on text elements (input, textarea, etc).");

                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.AppendText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.AppendTextWithoutEvents(element, text);
                }

                return this.syntaxProvider;
            }
        }
        #endregion

        #region <select />
        public ISelectSyntaxProvider Select(string value)
        {
            return new SelectSyntaxProvider(this, value, SelectionOption.Text);
        }

        public ISelectSyntaxProvider Select(Option mode, string value)
        {
            return new SelectSyntaxProvider(this, value, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);
        }

        public ISelectSyntaxProvider Select(params string[] values)
        {
            return new SelectSyntaxProvider(this, values, SelectionOption.Text);
        }

        public ISelectSyntaxProvider Select(Option mode, params string[] values)
        {
            return new SelectSyntaxProvider(this, values, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);
        }

        public ISelectSyntaxProvider Select(int index)
        {
            return new SelectSyntaxProvider(this, index, SelectionOption.Index);
        }

        public ISelectSyntaxProvider Select(params int[] indices)
        {
            return new SelectSyntaxProvider(this, indices, SelectionOption.Index);
        }
        
        public class SelectSyntaxProvider : ISelectSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly dynamic value = null;
            internal readonly SelectionOption mode;

            internal SelectSyntaxProvider(ActionSyntaxProvider syntaxProvider, dynamic value, SelectionOption mode)
            {
                this.syntaxProvider = syntaxProvider;
                this.value = value;
                this.mode = mode;
            }

            /// <summary>
            /// Select from element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider From(string selector)
            {
                return this.From(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Select from specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider From(ElementProxy element)
            {
                if (this.mode == SelectionOption.Value)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectValue(element, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectValue(element, this.value);
                    }
                }
                else if (this.mode == SelectionOption.Text)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectText(element, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectText(element, this.value);
                    }
                }
                else if (this.value is int)
                {
                    this.syntaxProvider.commandProvider.SelectIndex(element, this.value);
                }
                else if (this.value is int[])
                {
                    this.syntaxProvider.commandProvider.MultiSelectIndex(element, this.value);
                }

                return this.syntaxProvider;
            }
        }
        #endregion

        #region Assert / Expect
        private AssertSyntaxProvider expect = null;
        public AssertSyntaxProvider Expect
        {
            get
            {
                if (this.expect == null)
                {
                    this.expect = new AssertSyntaxProvider(this.commandProvider, this.settings.ExpectIsAssert ? this.assertProvider.EnableExceptions() : this.assertProvider);
                }

                return this.expect;
            }
        }

        private AssertSyntaxProvider assert = null;
        public AssertSyntaxProvider Assert
        {
            get
            {
                if (this.assert == null)
                {
                    this.assert = new AssertSyntaxProvider(this.commandProvider, this.assertProvider.EnableExceptions());
                }

                return this.assert;
            }
        }
        #endregion

        private bool isDisposed = false;
        public bool IsDisposed()
        {
            return isDisposed;
        }

        public void Dispose()
        {
            this.isDisposed = true;
            this.commandProvider.Dispose();
        }
    }

    /// <summary>
    /// Option mode for <select /> manipulation: Text, Value or Index
    /// </summary>
    internal enum SelectionOption
    {
        Text = 1,
        Value = 2,
        Index = 3
    }

    public enum Option
    {
        Text = 1,
        Value = 2
    }
}