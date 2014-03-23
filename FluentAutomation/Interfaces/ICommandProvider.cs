using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Interfaces
{
    public interface ICommandProvider : IActionProvider, IDisposable
    {
        Uri Url { get; }

        void Navigate(Uri url);
        ElementProxy Find(string selector);
        ElementProxy FindMultiple(string selector);

        void Click(int x, int y);
        void Click(ElementProxy element, int x, int y);
        void Click(ElementProxy element);

        void DoubleClick(int x, int y);
        void DoubleClick(ElementProxy element, int x, int y);
        void DoubleClick(ElementProxy element);

        void RightClick(ElementProxy element);

        void Hover(int x, int y);
        void Hover(ElementProxy element, int x, int y);
        void Hover(ElementProxy element);

        void Focus(ElementProxy element);

        void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY);
        void DragAndDrop(ElementProxy source, ElementProxy target);
        void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY);
        void EnterText(ElementProxy element, string text);
        void EnterTextWithoutEvents(ElementProxy element, string text);
        void AppendText(ElementProxy element, string text);
        void AppendTextWithoutEvents(ElementProxy element, string text);

        void SelectText(ElementProxy element, string optionText);
        void SelectValue(ElementProxy element, string optionValue);
        void SelectIndex(ElementProxy element, int optionIndex);

        void MultiSelectText(ElementProxy element, string[] optionTextCollection);
        void MultiSelectValue(ElementProxy element, string[] optionValues);
        void MultiSelectIndex(ElementProxy element, int[] optionIndices);

        void TakeScreenshot(string screenshotName);
        void UploadFile(ElementProxy element, int x, int y, string fileName);

        void Wait();
        void Wait(int seconds);
        void Wait(TimeSpan timeSpan);
        void WaitUntil(Expression<Func<bool>> conditionFunc);
        void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout);
        void WaitUntil(Expression<Action> conditionAction);
        void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout);

        void Press(string keys);
        void Type(string text);
    }
}
