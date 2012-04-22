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
        Func<IElement> Find(string selector);
        Func<IEnumerable<IElement>> FindMultiple(string selector);

        void Click(int x, int y);
        void Click(Func<IElement> element, int x, int y);
        void Click(Func<IElement> element);

        void DoubleClick(int x, int y);
        void DoubleClick(Func<IElement> element, int x, int y);
        void DoubleClick(Func<IElement> element);

        void RightClick(Func<IElement> element);

        void Hover(int x, int y);
        void Hover(Func<IElement> element, int x, int y);
        void Hover(Func<IElement> element);

        void Focus(Func<IElement> element);

        void DragAndDrop(Func<IElement> source, Func<IElement> target);
        void EnterText(Func<IElement> element, string text);

        void SelectText(Func<IElement> element, string optionText);
        void SelectValue(Func<IElement> element, string optionValue);
        void SelectIndex(Func<IElement> element, int optionIndex);

        void MultiSelectText(Func<IElement> element, string[] optionTextCollection);
        void MultiSelectValue(Func<IElement> element, string[] optionValues);
        void MultiSelectIndex(Func<IElement> element, int[] optionIndices);

        void TakeScreenshot(string screenshotName);
        void UploadFile(Func<IElement> element, int x, int y, string fileName);

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
