using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class MultiCommandProvider : ICommandProvider
    {
        private readonly CommandProviderList commandProviders = null;

        public Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }
        public Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }

        public MultiCommandProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders;
        }

        public Uri Url
        {
            get { return this.commandProviders.First().Url; }
        }

        public string Source
        {
            get { return this.commandProviders.First().Source; }
        }

        public void Navigate(Uri url)
        {
            Parallel.ForEach(this.commandProviders, x => x.Navigate(url));
        }

        public ElementProxy Find(string selector)
        {
            var result = new ElementProxy();

            Parallel.ForEach(this.commandProviders, x => result.Elements.Add(x, x.Find(selector).Elements.First().Value));

            return result;
        }

        public ElementProxy FindMultiple(string selector)
        {
            var result = new ElementProxy();

            Parallel.ForEach(this.commandProviders, x =>
            {
                foreach (var element in x.FindMultiple(selector).Elements)
                {
                    result.Elements.Add(x, element.Value);
                }
            });

            return result;
        }

        public void Click(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Click(x, y));
        }

        public void Click(ElementProxy element, int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Click(xx.Find(element.Element.Selector), x, y));
        }

        public void Click(ElementProxy element)
        {
            Parallel.ForEach(element.Elements, e => e.Key.Click(new ElementProxy(e.Key, e.Value)));
        }

        public void DoubleClick(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.DoubleClick(x, y));
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            Parallel.ForEach(element.Elements, e => e.Key.DoubleClick(new ElementProxy(e.Key, e.Value), x, y));
        }

        public void DoubleClick(ElementProxy element)
        {
            Parallel.ForEach(element.Elements, e => e.Key.DoubleClick(new ElementProxy(e.Key, e.Value)));
        }

        public void RightClick(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.RightClick(x, y));
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            Parallel.ForEach(element.Elements, e => e.Key.RightClick(new ElementProxy(e.Key, e.Value), x, y));
        }

        public void RightClick(ElementProxy element)
        {
            Parallel.ForEach(element.Elements, e => e.Key.RightClick(new ElementProxy(e.Key, e.Value)));
        }

        public void Hover(int x, int y)
        {
            Parallel.ForEach(this.commandProviders, xx => xx.Hover(x, y));
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            Parallel.ForEach(element.Elements, e => e.Key.Hover(new ElementProxy(e.Key, e.Value), x, y));
        }

        public void Hover(ElementProxy element)
        {
            Parallel.ForEach(element.Elements, e => e.Key.Hover(new ElementProxy(e.Key, e.Value)));
        }

        public void Focus(ElementProxy element)
        {
            Parallel.ForEach(element.Elements, e => e.Key.Focus(new ElementProxy(e.Key, e.Value)));
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            Parallel.ForEach(this.commandProviders, x => x.DragAndDrop(sourceX, sourceY, destinationX, destinationY));
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            Parallel.ForEach(source.Elements, e =>
            {
                e.Key.DragAndDrop(
                    new ElementProxy(e.Key, e.Value), sourceOffsetX, sourceOffsetY,
                    new ElementProxy(e.Key, target.Elements[e.Key]), targetOffsetX, targetOffsetY
                );
            });
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            Parallel.ForEach(source.Elements, e =>
            {
                e.Key.DragAndDrop(
                    new ElementProxy(e.Key, e.Value),
                    new ElementProxy(e.Key, target.Elements[e.Key])
                );
            });
        }

        public void EnterText(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e => e.Key.EnterText(new ElementProxy(e.Key, e.Value), text));
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e => e.Key.EnterTextWithoutEvents(new ElementProxy(e.Key, e.Value), text));
        }

        public void AppendText(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                e.Key.AppendText(new ElementProxy(e.Key, e.Value), text);
            });
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e => e.Key.AppendTextWithoutEvents(new ElementProxy(e.Key, e.Value), text));
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            Parallel.ForEach(element.Elements, e => e.Key.SelectText(new ElementProxy(e.Key, e.Value), optionText));
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            Parallel.ForEach(element.Elements, e => e.Key.SelectValue(new ElementProxy(e.Key, e.Value), optionValue));
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            Parallel.ForEach(element.Elements, e => e.Key.SelectIndex(new ElementProxy(e.Key, e.Value), optionIndex));
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            Parallel.ForEach(element.Elements, e => e.Key.MultiSelectText(new ElementProxy(e.Key, e.Value), optionTextCollection));
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            Parallel.ForEach(element.Elements, e => e.Key.MultiSelectValue(new ElementProxy(e.Key, e.Value), optionValues));
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            Parallel.ForEach(element.Elements, e => e.Key.MultiSelectIndex(new ElementProxy(e.Key, e.Value), optionIndices));
        }

        public void TakeScreenshot(string screenshotName)
        {
            Parallel.ForEach(this.commandProviders, x => x.TakeScreenshot(screenshotName));
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            Parallel.ForEach(element.Elements, e => e.Key.UploadFile(new ElementProxy(e.Key, e.Value), x, y, fileName));
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

        public void SwitchToFrame(string frameName)
        {
            Parallel.ForEach(this.commandProviders, x => x.SwitchToFrame(frameName));
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            Parallel.ForEach(this.commandProviders, x => x.SwitchToFrame(frameElement));
        }

        public void SwitchToWindow(string windowName)
        {
            Parallel.ForEach(this.commandProviders, x => x.SwitchToWindow(windowName));
        }

        public void AlertClick(Alert accessor)
        {
            Parallel.ForEach(this.commandProviders, x => x.AlertClick(accessor));
        }

        public void AlertText(Action<string> matchFunc)
        {
            Parallel.ForEach(this.commandProviders, x => x.AlertText(matchFunc));
        }

        public void AlertEnterText(string text)
        {
            Parallel.ForEach(this.commandProviders, x => x.AlertEnterText(text));
        }

        public void Visible(ElementProxy element, Action<bool> action)
        {
            Parallel.ForEach(this.commandProviders, x => x.Visible(element, action));
        }

        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            Parallel.ForEach(this.commandProviders, x => x.CssPropertyValue(element, propertyName, action));
        }

        public void Act(CommandType commandType, Action action)
        {
            Parallel.ForEach(this.commandProviders, x => x.Act(commandType, action));
        }

        public ICommandProvider WithConfig(FluentSettings settings)
        {
            Parallel.ForEach(this.commandProviders, x => x.WithConfig(settings));
            return this;
        }

        public void Dispose()
        {
            Parallel.ForEach(this.commandProviders, x => x.Dispose());
        }
    }
}
