using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class MultiCommandProvider : ICommandProvider
    {
        private readonly CommandProviderList commandProviders = null;

        public MultiCommandProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders;
        }

        public Uri Url
        {
            get { return this.commandProviders.First().Url; }
        }

        public void Navigate(Uri url)
        {
            Parallel.ForEach(this.commandProviders, x => x.Navigate(url));
        }

        public Func<IElement> Find(string selector)
        {
            throw new NotImplementedException("Find commands don't work with multi-browser testing");
        }

        public Func<IEnumerable<IElement>> FindMultiple(string selector)
        {
            throw new NotImplementedException("Find commands don't work with multi-browser testing");
        }

        public void Click(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Click(x, y));
        }

        public void Click(Func<IElement> element, int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Click(xx.Find(element().Selector), x, y));
        }

        public void Click(Func<IElement> element)
        {
            Parallel.ForEach(this.commandProviders, x => x.Click(x.Find(element().Selector)));
        }

        public void DoubleClick(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.DoubleClick(x, y));
        }

        public void DoubleClick(Func<IElement> element, int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.DoubleClick(xx.Find(element().Selector), x, y));
        }

        public void DoubleClick(Func<IElement> element)
        {
            Parallel.ForEach(this.commandProviders, x => x.DoubleClick(x.Find(element().Selector)));
        }

        public void RightClick(Func<IElement> element)
        {
            Parallel.ForEach(this.commandProviders, x => x.RightClick(x.Find(element().Selector)));
        }

        public void Hover(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Hover(x, y));
        }

        public void Hover(Func<IElement> element, int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Hover(xx.Find(element().Selector), x, y));
        }

        public void Hover(Func<IElement> element)
        {
            Parallel.ForEach(this.commandProviders, x => x.Hover(x.Find(element().Selector)));
        }

        public void Focus(Func<IElement> element)
        {
            Parallel.ForEach(this.commandProviders, x => x.Focus(x.Find(element().Selector)));
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            Parallel.ForEach(this.commandProviders, x => x.DragAndDrop(sourceX, sourceY, destinationX, destinationY));
        }

        public void DragAndDrop(Func<IElement> source, Func<IElement> target)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.DragAndDrop(xx.Find(source().Selector), xx.Find(target().Selector)));
        }

        public void EnterText(Func<IElement> element, string text)
        {
            Parallel.ForEach(this.commandProviders, x => x.EnterText(x.Find(element().Selector), text));
        }

        public void EnterTextWithoutEvents(Func<IElement> element, string text)
        {
            Parallel.ForEach(this.commandProviders, x => x.EnterTextWithoutEvents(x.Find(element().Selector), text));
        }

        public void AppendText(Func<IElement> element, string text)
        {
            Parallel.ForEach(this.commandProviders, x => x.AppendText(x.Find(element().Selector), text));
        }

        public void AppendTextWithoutEvents(Func<IElement> element, string text)
        {
            Parallel.ForEach(this.commandProviders, x => x.AppendTextWithoutEvents(x.Find(element().Selector), text));
        }

        public void SelectText(Func<IElement> element, string optionText)
        {
            Parallel.ForEach(this.commandProviders, x => x.SelectText(x.Find(element().Selector), optionText));
        }

        public void SelectValue(Func<IElement> element, string optionValue)
        {
            Parallel.ForEach(this.commandProviders, x => x.SelectValue(x.Find(element().Selector), optionValue));
        }

        public void SelectIndex(Func<IElement> element, int optionIndex)
        {
            Parallel.ForEach(this.commandProviders, x => x.SelectIndex(x.Find(element().Selector), optionIndex));
        }

        public void MultiSelectText(Func<IElement> element, string[] optionTextCollection)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.MultiSelectText(xx.Find(element().Selector), optionTextCollection));
        }

        public void MultiSelectValue(Func<IElement> element, string[] optionValues)
        {
            Parallel.ForEach(this.commandProviders, x => x.MultiSelectValue(x.Find(element().Selector), optionValues));
        }

        public void MultiSelectIndex(Func<IElement> element, int[] optionIndices)
        {
            Parallel.ForEach(this.commandProviders, x => x.MultiSelectIndex(x.Find(element().Selector), optionIndices));
        }

        public void TakeScreenshot(string screenshotName)
        {
            Parallel.ForEach(this.commandProviders, x => x.TakeScreenshot(screenshotName));
        }

        public void UploadFile(Func<IElement> element, int x, int y, string fileName)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.UploadFile(xx.Find(element().Selector), x, y, fileName));
        }

        public void Wait()
        {
            this.commandProviders.First().Wait();
        }

        public void Wait(int seconds)
        {
            this.commandProviders.First().Wait(seconds);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.commandProviders.First().Wait(timeSpan);
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc)
        {
            Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionFunc));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionFunc, timeout));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction)
        {
            Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionAction));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction, TimeSpan timeout)
        {
            Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionAction, timeout));
        }

        public void Press(string keys)
        {
            throw new NotImplementedException("Win32 based events are not currently functioning in multi-browser tests");
        }

        public void Type(string text)
        {
            throw new NotImplementedException("Win32 based events are not currently functioning in multi-browser tests");
        }

        public void Act(Action action, bool waitableAction = true)
        {
            Parallel.ForEach(this.commandProviders, x => x.Act(action, waitableAction));
        }

        public void Dispose()
        {
            Parallel.ForEach(this.commandProviders, x => x.Dispose());
        }
    }
}
