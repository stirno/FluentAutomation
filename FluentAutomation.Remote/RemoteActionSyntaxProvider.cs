using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class RemoteActionSyntaxProvider : IRemoteActionSyntaxProvider
    {
        private readonly IRemoteCommandProvider commandProvider = null;
        private readonly IRemoteExpectProvider expectProvder = null;

        public RemoteActionSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvder = expectProvider;
        }

        #region Direct Execution Actions
        public void Click(int x, int y)
        {
            this.commandProvider.Click(x, y);
        }

        public void Click(string selector)
        {
            this.commandProvider.Click(selector);
        }

        public void Click(string selector, int x, int y)
        {
            this.commandProvider.Click(selector, x, y);
        }

        public void DoubleClick(int x, int y)
        {
            this.commandProvider.DoubleClick(x, y);
        }

        public void DoubleClick(string selector)
        {
            this.commandProvider.DoubleClick(selector);
        }

        public void DoubleClick(string selector, int x, int y)
        {
            this.commandProvider.DoubleClick(selector, x, y);
        }

        public void RightClick(string selector)
        {
            this.commandProvider.RightClick(selector);
        }

        public void Focus(string selector)
        {
            this.commandProvider.Focus(selector);
        }

        public void Hover(int x, int y)
        {
            this.commandProvider.Hover(x, y);
        }

        public void Hover(string selector)
        {
            this.commandProvider.Hover(selector);
        }

        public void Hover(string selector, int x, int y)
        {
            this.commandProvider.Hover(selector, x, y);
        }

        public void Open(string url)
        {
            this.commandProvider.Navigate(new Uri(url, UriKind.Absolute));
        }

        public void Open(Uri url)
        {
            this.commandProvider.Navigate(url);
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
            this.commandProvider.Wait(seconds);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.commandProvider.Wait(timeSpan);
        }
        #endregion

        #region Drag/Drop
        public DragDropSyntaxProvider Drag(string selector)
        {
            return new DragDropSyntaxProvider(this, selector);
        }

        public class DragDropSyntaxProvider
        {
            protected readonly RemoteActionSyntaxProvider syntaxProvider = null;
            protected readonly string sourceSelector = null;

            public DragDropSyntaxProvider(RemoteActionSyntaxProvider syntaxProvider, string sourceSelector)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceSelector = sourceSelector;
            }

            public void To(string selector)
            {
                this.syntaxProvider.commandProvider.DragAndDrop(this.sourceSelector, selector);
            }
        }
        #endregion

        #region <input />, <textarea />
        public TextEntrySyntaxProvider Enter(dynamic nonString)
        {
            return new TextEntrySyntaxProvider(this, nonString.ToString());
        }

        public TextEntrySyntaxProvider Enter(string text)
        {
            return new TextEntrySyntaxProvider(this, text);
        }

        public class TextEntrySyntaxProvider
        {
            protected readonly RemoteActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;

            public TextEntrySyntaxProvider(RemoteActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            public void In(string selector)
            {
                this.syntaxProvider.commandProvider.EnterText(selector, this.text);
            }
        }
        #endregion

        #region <select />
        public SelectSyntaxProvider Select(Option mode, params string[] values)
        {
            return new SelectSyntaxProvider(this, values, mode);
        }

        public SelectSyntaxProvider Select(Option mode, string value)
        {
            return new SelectSyntaxProvider(this, value, mode);
        }

        public SelectSyntaxProvider Select(params int[] indices)
        {
            return new SelectSyntaxProvider(this, indices, Option.Index);
        }

        public SelectSyntaxProvider Select(params string[] values)
        {
            return new SelectSyntaxProvider(this, values, Option.Text);
        }

        public SelectSyntaxProvider Select(int index)
        {
            return new SelectSyntaxProvider(this, index, Option.Index);
        }

        public SelectSyntaxProvider Select(string value)
        {
            return new SelectSyntaxProvider(this, value, Option.Text);
        }

        public class SelectSyntaxProvider
        {
            protected readonly RemoteActionSyntaxProvider syntaxProvider = null;
            protected readonly dynamic value = null;
            protected readonly Option mode;

            public SelectSyntaxProvider(RemoteActionSyntaxProvider syntaxProvider, dynamic value, Option mode)
            {
                this.syntaxProvider = syntaxProvider;
                this.value = value;
                this.mode = mode;
            }

            public void From(string selector)
            {
                if (this.mode == Option.Value)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectValue(selector, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectValue(selector, this.value);
                    }
                }
                else if (this.mode == Option.Text)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectText(selector, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectText(selector, this.value);
                    }
                }
                else if (this.value is int)
                {
                    this.syntaxProvider.commandProvider.SelectIndex(selector, this.value);
                }
                else if (this.value is int[])
                {
                    this.syntaxProvider.commandProvider.MultiSelectIndex(selector, this.value);
                }
            }
        }
        #endregion

        private RemoteExpectSyntaxProvider expect = null;
        public RemoteExpectSyntaxProvider Expect
        {
            get
            {
                if (this.expect == null)
                {
                    this.expect = new RemoteExpectSyntaxProvider(this.commandProvider, this.expectProvder);
                }

                return this.expect;
            }
        }

        public void Execute()
        {
            this.commandProvider.Execute();
        }

        public void Dispose()
        {
            this.commandProvider.Dispose();
        }
    }
}
