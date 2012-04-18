using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface IRemoteCommandProvider : IDisposable
    {
        void Navigate(Uri url);

        void Click(int x, int y);
        void Click(string selector, int x, int y);
        void Click(string selector);

        void Hover(int x, int y);
        void Hover(string selector, int x, int y);
        void Hover(string selector);

        void Focus(string selector);

        void DragAndDrop(string sourceSelector, string targetSelector);
        void EnterText(string selector, string text);

        void SelectText(string selector, string optionText);
        void SelectValue(string selector, string optionValue);
        void SelectIndex(string selector, int optionIndex);

        void MultiSelectText(string selector, string[] optionTextCollection);
        void MultiSelectValue(string selector, string[] optionValues);
        void MultiSelectIndex(string selector, int[] optionIndices);

        void TakeScreenshot(string screenshotName);

        void Wait();
        void Wait(int seconds);
        void Wait(TimeSpan timeSpan);

        void Press(string keys);
        void Type(string text);

        void Act(dynamic data);
        void Execute();
    }
}
