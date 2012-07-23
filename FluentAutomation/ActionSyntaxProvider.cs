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

        public class DragDropSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly Func<IElement> sourceElement = null;

            public DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, Func<IElement> element)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
            }

            public void To(string selector)
            {
                this.To(this.syntaxProvider.Find(selector));
            }

            public void To(Func<IElement> targetElement)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceElement, targetElement);
            }
        }
        #endregion

        #region <input />, <textarea />
        public TextEntrySyntaxProvider Enter(string text)
        {
            return new TextEntrySyntaxProvider(this, text);
        }

        public TextEntrySyntaxProvider Enter(dynamic nonString)
        {
            return new TextEntrySyntaxProvider(this, nonString.ToString());
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

            public TextEntrySyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            public void In(string selector)
            {
                this.In(this.syntaxProvider.Find(selector));
            }

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

            public void From(string selector)
            {
                this.From(this.syntaxProvider.Find(selector));
            }

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

        public void Dispose()
        {
            this.commandProvider.Dispose();
        }
    }

    public enum Option
    {
        Text = 1,
        Value = 2,
        Index = 3
    }
}