using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IRemoteActionSyntaxProvider : IDisposable
    {
        void Click(int x, int y);
        void Click(string selector);
        void Click(string selector, int x, int y);
        RemoteActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector);
        RemoteActionSyntaxProvider.TextEntrySyntaxProvider Enter(dynamic nonString);
        RemoteActionSyntaxProvider.TextEntrySyntaxProvider Enter(string text);
        void Focus(string selector);
        void Hover(int x, int y);
        void Hover(string selector);
        void Hover(string selector, int x, int y);
        void Open(string url);
        void Open(Uri url);
        void Press(string keys);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, params string[] values);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, string value);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(params int[] indices);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(params string[] values);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(int index);
        RemoteActionSyntaxProvider.SelectSyntaxProvider Select(string value);
        void TakeScreenshot(string screenshotName);
        void Type(string text);
        void Wait(int seconds);
        void Wait(TimeSpan timeSpan);

        void Execute();
        RemoteExpectSyntaxProvider Expect { get; }
    }
}
