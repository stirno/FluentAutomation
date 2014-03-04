using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class ActionSyntaxProvider : INativeActionSyntaxProvider, IDisposable
    {
        private readonly ICommandProvider commandProvider = null;
        private readonly IExpectProvider expectProvider = null;

        public ActionSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
        }

        #region Direct Execution Actions
        public INativeActionSyntaxProvider Open(string url)
        {
            return this.Open(new Uri(url, UriKind.Absolute));
        }

        public INativeActionSyntaxProvider Open(Uri url)
        {
            this.commandProvider.Navigate(url);
            return this;
        }

        public Func<IElement> Find(string selector)
        {
            return this.commandProvider.Find(selector);
        }

        public Func<IEnumerable<IElement>> FindMultiple(string selector)
        {
            return this.commandProvider.FindMultiple(selector);
        }

        public INativeActionSyntaxProvider Click(int x, int y)
        {
            this.commandProvider.Click(x, y);
            return this;
        }

        public INativeActionSyntaxProvider Click(string selector, int x, int y)
        {
            return this.Click(this.Find(selector), x, y);
        }

        public INativeActionSyntaxProvider Click(Func<IElement> element, int x, int y)
        {
            this.commandProvider.Click(element, x, y);
            return this;
        }

        public INativeActionSyntaxProvider Click(string selector)
        {
            return this.Click(this.Find(selector));
        }

        public INativeActionSyntaxProvider Click(Func<IElement> element)
        {
            this.commandProvider.Click(element);
            return this;
        }

        public INativeActionSyntaxProvider Scroll(int x, int y)
        {
            this.commandProvider.Hover(x, y);
            return this;
        }

        public INativeActionSyntaxProvider Scroll(string selector)
        {
            this.commandProvider.Hover(this.Find(selector));
            return this;
        }

        public INativeActionSyntaxProvider Scroll(Func<IElement> element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public INativeActionSyntaxProvider DoubleClick(int x, int y)
        {
            this.commandProvider.DoubleClick(x, y);
            return this;
        }

        public INativeActionSyntaxProvider DoubleClick(string selector, int x, int y)
        {
            return this.DoubleClick(this.Find(selector), x, y);
        }

        public INativeActionSyntaxProvider DoubleClick(Func<IElement> element, int x, int y)
        {
            this.commandProvider.DoubleClick(element, x, y);
            return this;
        }

        public INativeActionSyntaxProvider DoubleClick(string selector)
        {
            return this.DoubleClick(this.Find(selector));
        }

        public INativeActionSyntaxProvider DoubleClick(Func<IElement> element)
        {
            this.commandProvider.DoubleClick(element);
            return this;
        }

        public INativeActionSyntaxProvider RightClick(string selector)
        {
            return this.RightClick(this.Find(selector));
        }

        public INativeActionSyntaxProvider RightClick(Func<IElement> element)
        {
            this.commandProvider.RightClick(element);
            return this;
        }

        public INativeActionSyntaxProvider Hover(int x, int y)
        {
            this.commandProvider.Hover(x, y);
            return this;
        }

        public INativeActionSyntaxProvider Hover(string selector, int x, int y)
        {
            return this.Hover(this.Find(selector), x, y);
        }

        public INativeActionSyntaxProvider Hover(Func<IElement> element, int x, int y)
        {
            this.commandProvider.Hover(element, x, y);
            return this;
        }

        public INativeActionSyntaxProvider Hover(string selector)
        {
            return this.Hover(this.Find(selector));
        }

        public INativeActionSyntaxProvider Hover(Func<IElement> element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public INativeActionSyntaxProvider Focus(string selector)
        {
            return this.Focus(this.Find(selector));
        }

        public INativeActionSyntaxProvider Focus(Func<IElement> element)
        {
            this.commandProvider.Focus(element);
            return this;
        }

        public INativeActionSyntaxProvider Press(string keys)
        {
            this.commandProvider.Press(keys);
            return this;
        }

        public INativeActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            this.commandProvider.TakeScreenshot(screenshotName);
            return this;
        }

        public INativeActionSyntaxProvider Type(string text)
        {
            this.commandProvider.Type(text);
            return this;
        }

        public INativeActionSyntaxProvider Wait(int seconds)
        {
            return this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public INativeActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            this.commandProvider.Wait(timeSpan);
            return this;
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.commandProvider.WaitUntil(conditionFunc);
            return this;
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            this.commandProvider.WaitUntil(conditionAction);
            return this;
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int secondsToWait)
        {
            return this.WaitUntil(conditionFunc, TimeSpan.FromSeconds(secondsToWait));
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionFunc, timeout);
            return this;
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int secondsToWait)
        {
            return this.WaitUntil(conditionAction, TimeSpan.FromSeconds(secondsToWait));
        }

        public INativeActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionAction, timeout);
            return this;
        }

        public INativeActionSyntaxProvider Upload(string selector, string fileName)
        {
            return this.Upload(selector, 0, 0, fileName);
        }

        public INativeActionSyntaxProvider Upload(string selector, int x, int y, string fileName)
        {
            return this.Upload(this.Find(selector), x, y, fileName);
        }

        public INativeActionSyntaxProvider Upload(Func<IElement> element, string fileName)
        {
            return this.Upload(element, 0, 0, fileName);
        }

        public INativeActionSyntaxProvider Upload(Func<IElement> element, int x, int y, string fileName)
        {
            this.commandProvider.UploadFile(element, x, y, fileName);
            return this;
        }
        #endregion

        #region Drag/Drop
        public DragDropSyntaxProvider Drag(string selector)
        {
            return this.Drag(this.Find(selector));
        }

        public DragDropSyntaxProvider Drag(Func<IElement> element)
        {
            return new DragDropSyntaxProvider(this, element);
        }
        
        public DragDropSyntaxProvider Drag(Func<IElement> element, int sourceX, int sourceY)
        {
            return new DragDropSyntaxProvider(this, element, sourceX, sourceY);
        }

        public DragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY)
        {
            return new DragDropByPositionSyntaxProvider(this, sourceX, sourceY);
        }

        public class DragDropSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly Func<IElement> sourceElement = null;
            protected readonly int offsetX = 0;
            protected readonly int offsetY = 0;

            public DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, Func<IElement> element)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
            }

            public DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, Func<IElement> element, int offsetX, int offsetY)
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
            public INativeActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public INativeActionSyntaxProvider To(Func<IElement> targetElement)
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
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public INativeActionSyntaxProvider To(Func<IElement> targetElement, int targetOffsetX, int targetOffsetY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, offsetX, offsetY, targetElement, targetOffsetX, targetOffsetY);
                return this.syntaxProvider;
            }
        }

        public class DragDropByPositionSyntaxProvider
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
            public INativeActionSyntaxProvider To(int destinationX, int destinationY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceX, this.sourceY, destinationX, destinationY);
                return this.syntaxProvider;
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public void To(Func<IElement> targetElement)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.syntaxProvider.commandProvider.Find("html"), this.sourceX, this.sourceY, targetElement, 0, 0);
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public void To(Func<IElement> targetElement, int targetOffsetX, int targetOffsetY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.syntaxProvider.commandProvider.Find("html"), this.sourceX, this.sourceY, targetElement, targetOffsetX, targetOffsetY);
            }
        }
        #endregion

        #region <input />, <textarea />
        public TextAppendSyntaxProvider Append(string text)
        {
            return new TextAppendSyntaxProvider(this, text);
        }

        public TextAppendSyntaxProvider Append(dynamic nonString)
        {
            return this.Append(nonString.ToString());
        }

        public TextEntrySyntaxProvider Enter(string text)
        {
            return new TextEntrySyntaxProvider(this, text);
        }

        public TextEntrySyntaxProvider Enter(dynamic nonString)
        {
            return this.Enter(nonString.ToString());
        }

        public class TextEntrySyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;

            public TextEntrySyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextEntrySyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public INativeActionSyntaxProvider In(string selector)
            {
                return this.In(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public INativeActionSyntaxProvider In(Func<IElement> element)
            {
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
        }

        public class TextAppendSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;
            protected bool isAppend = false;

            public TextAppendSyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextAppendSyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public INativeActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public INativeActionSyntaxProvider To(Func<IElement> element)
            {
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
        public SelectSyntaxProvider Select(string value)
        {
            return new SelectSyntaxProvider(this, value, Option.Text);
        }

        public SelectSyntaxProvider Select(Option mode, string value)
        {
            return new SelectSyntaxProvider(this, value, mode);
        }

        public SelectSyntaxProvider Select(params string[] values)
        {
            return new SelectSyntaxProvider(this, values, Option.Text);
        }

        public SelectSyntaxProvider Select(Option mode, params string[] values)
        {
            return new SelectSyntaxProvider(this, values, mode);
        }

        public SelectSyntaxProvider Select(int index)
        {
            return new SelectSyntaxProvider(this, index, Option.Index);
        }

        public SelectSyntaxProvider Select(params int[] indices)
        {
            return new SelectSyntaxProvider(this, indices, Option.Index);
        }
        
        public class SelectSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly dynamic value = null;
            protected readonly Option mode;

            public SelectSyntaxProvider(ActionSyntaxProvider syntaxProvider, dynamic value, Option mode)
            {
                this.syntaxProvider = syntaxProvider;
                this.value = value;
                this.mode = mode;
            }

            /// <summary>
            /// Select from element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public INativeActionSyntaxProvider From(string selector)
            {
                this.syntaxProvider.commandProvider.ExecWithElement(selector, (x, element) =>
                {
                    if (this.mode == Option.Value)
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
                    else if (this.mode == Option.Text)
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
                });

                return this.syntaxProvider;
            }

            /// <summary>
            /// Select from specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public INativeActionSyntaxProvider From(Func<IElement> element)
            {
                if (this.mode == Option.Value)
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
                else if (this.mode == Option.Text)
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

        private ExpectSyntaxProvider expect = null;
        public ExpectSyntaxProvider Expect
        {
            get
            {
                if (this.expect == null)
                {
                    this.expect = new ExpectSyntaxProvider(this.commandProvider, FluentAutomation.Settings.ExpectIsAssert ? this.expectProvider.EnableExceptions() : this.expectProvider);
                }

                return this.expect;
            }
        }

        private ExpectSyntaxProvider assert = null;
        public ExpectSyntaxProvider Assert
        {
            get
            {
                if (this.assert == null)
                {
                    this.assert = new ExpectSyntaxProvider(this.commandProvider, this.expectProvider.EnableExceptions());
                }

                return this.assert;
            }
        }

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
    public enum Option
    {
        Text = 1,
        Value = 2,
        Index = 3
    }
}