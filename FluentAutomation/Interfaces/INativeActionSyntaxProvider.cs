using System;
using System.Linq.Expressions;

namespace FluentAutomation.Interfaces
{
    public interface INativeActionSyntaxProvider : IDisposable
    {
        // native only
        void Click(Func<IElement> element);
        void Click(Func<IElement> element, int x, int y);
        void DoubleClick(Func<IElement> element);
        void DoubleClick(Func<IElement> element, int x, int y);
        void RightClick(Func<IElement> element);
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(Func<IElement> element);
        Func<IElement> Find(string selector);
        Func<System.Collections.Generic.IEnumerable<IElement>> FindMultiple(string selector);
        void Focus(Func<IElement> element);
        void Hover(Func<IElement> element);
        void Hover(Func<IElement> element, int x, int y);
        void Upload(Func<IElement> element, int x, int y, string fileName);
        void Upload(Func<IElement> element, string fileName);
        void Upload(string selector, int x, int y, string fileName);
        void Upload(string selector, string fileName);
        void WaitUntil(Expression<Action> conditionAction);
        void WaitUntil(Expression<Func<bool>> conditionFunc);

        // remote commands
        void Click(int x, int y);
        void Click(string selector);
        void Click(string selector, int x, int y);
        void DoubleClick(int x, int y);
        void DoubleClick(string selector);
        void DoubleClick(string selector, int x, int y);
        void RightClick(string selector);
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector);
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(dynamic nonString);
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(string text);
        void Focus(string selector);
        void Hover(int x, int y);
        void Hover(string selector);
        void Hover(string selector, int x, int y);
        void Open(string url);
        void Open(Uri url);
        void Press(string keys);
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, params string[] values);
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, string value);
        ActionSyntaxProvider.SelectSyntaxProvider Select(params int[] indices);
        ActionSyntaxProvider.SelectSyntaxProvider Select(params string[] values);
        ActionSyntaxProvider.SelectSyntaxProvider Select(int index);
        ActionSyntaxProvider.SelectSyntaxProvider Select(string value);
        void TakeScreenshot(string screenshotName);
        void Type(string text);
        void Wait(int seconds);
        void Wait(TimeSpan timeSpan);

        ExpectSyntaxProvider Expect { get; }
    }
}
