using System;

namespace FluentAutomation.Interfaces
{
    public interface INativeActionSyntaxProvider : IDisposable
    {
        // native only
        void Click(Func<FluentAutomation.Interfaces.IElement> element);
        void Click(Func<FluentAutomation.Interfaces.IElement> element, int x, int y);
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(Func<FluentAutomation.Interfaces.IElement> element);
        Func<FluentAutomation.Interfaces.IElement> Find(string selector);
        Func<System.Collections.Generic.IEnumerable<FluentAutomation.Interfaces.IElement>> FindMultiple(string selector);
        void Focus(Func<FluentAutomation.Interfaces.IElement> element);
        void Hover(Func<FluentAutomation.Interfaces.IElement> element);
        void Hover(Func<FluentAutomation.Interfaces.IElement> element, int x, int y);
        void Upload(Func<FluentAutomation.Interfaces.IElement> element, int x, int y, string fileName);
        void Upload(Func<FluentAutomation.Interfaces.IElement> element, string fileName);
        void Upload(string selector, int x, int y, string fileName);
        void Upload(string selector, string fileName);
        void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction);
        void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc);

        // remote commands
        void Click(int x, int y);
        void Click(string selector);
        void Click(string selector, int x, int y);
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
