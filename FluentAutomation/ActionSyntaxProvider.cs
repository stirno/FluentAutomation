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
        private readonly IExpectProvider expectProvder = null;

        public ActionSyntaxProvider(ICommandProvider commandProvider, IExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvder = expectProvider;
        }

        #region Direct Execution Actions
        public void Open(string url)
        {
            this.Open(new Uri(url, UriKind.Absolute));
        }

        public void Open(Uri url)
        {
            this.commandProvider.Navigate(url);
        }

        public Func<IElement> Find(string selector)
        {
            return this.commandProvider.Find(selector);
        }

        public Func<IEnumerable<IElement>> FindMultiple(string selector)
        {
            return this.commandProvider.FindMultiple(selector);
        }

        public void Click(int x, int y)
        {
            this.commandProvider.Click(x, y);
        }

        public void Click(string selector, int x, int y)
        {
            this.Click(this.Find(selector), x, y);
        }

        public void Click(Func<IElement> element, int x, int y)
        {
            this.commandProvider.Click(element, x, y);
        }

        public void Click(string selector)
        {
            this.Click(this.Find(selector));
        }

        public void Click(Func<IElement> element)
        {
            this.commandProvider.Click(element);
        }

        public void Scroll(int x, int y)
        {
            this.commandProvider.Hover(x, y);
        }

        public void Scroll(string selector)
        {
            this.commandProvider.Hover(this.Find(selector));
        }

        public void Scroll(Func<IElement> element)
        {
            this.commandProvider.Hover(element);
        }

        public void DoubleClick(int x, int y)
        {
            this.commandProvider.DoubleClick(x, y);
        }

        public void DoubleClick(string selector, int x, int y)
        {
            this.DoubleClick(this.Find(selector), x, y);
        }

        public void DoubleClick(Func<IElement> element, int x, int y)
        {
            this.commandProvider.DoubleClick(element, x, y);
        }

        public void DoubleClick(string selector)
        {
            this.DoubleClick(this.Find(selector));
        }

        public void DoubleClick(Func<IElement> element)
        {
            this.commandProvider.DoubleClick(element);
        }

        public void RightClick(string selector)
        {
            this.RightClick(this.Find(selector));
        }

        public void RightClick(Func<IElement> element)
        {
            this.commandProvider.RightClick(element);
        }

        public void Hover(int x, int y)
        {
            this.commandProvider.Hover(x, y);
        }

        public void Hover(string selector, int x, int y)
        {
            this.Hover(this.Find(selector), x, y);
        }

        public void Hover(Func<IElement> element, int x, int y)
        {
            this.commandProvider.Hover(element, x, y);
        }

        public void Hover(string selector)
        {
            this.Hover(this.Find(selector));
        }

        public void Hover(Func<IElement> element)
        {
            this.commandProvider.Hover(element);
        }

        public void Focus(string selector)
        {
            this.Focus(this.Find(selector));
        }

        public void Focus(Func<IElement> element)
        {
            this.commandProvider.Focus(element);
        }

        public void Press(string keys)
        {
            this.commandProvider.Press(keys);
        }

        public void TakeScreenshot(string screenshotName)
        {
            this.commandProvider.TakeScreenshot(screenshotName);
        }

        public void Type(string text)
        {
            this.commandProvider.Type(text);
        }

        public void Wait(int seconds)
        {
            this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.commandProvider.Wait(timeSpan);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.commandProvider.WaitUntil(conditionFunc);
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            this.commandProvider.WaitUntil(conditionAction);
        }
        
        public void WaitUntil(Expression<Func<bool>> conditionFunc, int secondsToWait)
        {
            this.WaitUntil(conditionFunc, TimeSpan.FromSeconds(secondsToWait));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionFunc, timeout);
        }

        public void WaitUntil(Expression<Action> conditionAction, int secondsToWait)
        {
            this.WaitUntil(conditionAction, TimeSpan.FromSeconds(secondsToWait));
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionAction, timeout);
        }

        public void Upload(string selector, string fileName)
        {
            this.Upload(selector, 0, 0, fileName);
        }

        public void Upload(string selector, int x, int y, string fileName)
        {
            this.Upload(this.Find(selector), x, y, fileName);
        }

        public void Upload(Func<IElement> element, string fileName)
        {
            this.Upload(element, 0, 0, fileName);
        }

        public void Upload(Func<IElement> element, int x, int y, string fileName)
        {
            this.commandProvider.UploadFile(element, x, y, fileName);
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

        public DragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY)
        {
            return new DragDropByPositionSyntaxProvider(this, sourceX, sourceY);
        }

        public class DragDropSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly Func<IElement> sourceElement = null;

            public DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, Func<IElement> element)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
            }

            /// <summary>
            /// End Drag/Drop operation at element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void To(string selector)
            {
                this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public void To(Func<IElement> targetElement)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, targetElement);
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
            public void To(int destinationX, int destinationY)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceX, this.sourceY, destinationX, destinationY);
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
            /// [deprecated] Use WithoutEvents() instead. To be removed in the future.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextEntrySyntaxProvider Quickly()
            {
                return this.WithoutEvents();
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void In(string selector)
            {
                this.In(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public void In(Func<IElement> element)
            {
                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.EnterText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.EnterTextWithoutEvents(element, text);
                }
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
            /// [deprecated] Use WithoutEvents() instead. To be removed in the future.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextAppendSyntaxProvider Quickly()
            {
                return this.WithoutEvents();
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public void To(string selector)
            {
                this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public void To(Func<IElement> element)
            {
                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.AppendText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.AppendTextWithoutEvents(element, text);
                }
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
            public void From(string selector)
            {
                this.From(this.syntaxProvider.Find(selector));
            }


            /// <summary>
            /// Select from specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public void From(Func<IElement> element)
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
                    this.expect = new ExpectSyntaxProvider(this.commandProvider, this.expectProvder);
                }

                return this.expect;
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