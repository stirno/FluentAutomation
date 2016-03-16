﻿using FluentAutomation.Exceptions;
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
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.Navigate(url)));
        }

        public ElementProxy Find(string selector)
        {
            var result = new ElementProxy();

            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(x, x.Find(selector).Elements.First().Item2))));

            return result;
        }

        public ElementProxy FindMultiple(string selector)
        {
            var result = new ElementProxy();

            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x =>
            {
                foreach (var element in x.FindMultiple(selector).Elements)
                {
                    result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(x, element.Item2));
                }
            }));

            return result;
        }

        public void Click(int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.Click(x, y)));
        }

        public void Click(ElementProxy element, int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.Click(xx.Find(element.Element.Selector), x, y)));
        }

        public void Click(ElementProxy element)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Click(new ElementProxy(e.Item1, e.Item2))));
        }

        public void DoubleClick(int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.DoubleClick(x, y)));
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.DoubleClick(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void DoubleClick(ElementProxy element)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.DoubleClick(new ElementProxy(e.Item1, e.Item2))));
        }
        
        public void ExecuteJavaScript(string script)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.ExecuteJavaScript(script)));
        }

        public void RightClick(int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.RightClick(x, y)));
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.RightClick(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void RightClick(ElementProxy element)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.RightClick(new ElementProxy(e.Item1, e.Item2))));
        }

        public void Hover(int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, xx => xx.Hover(x, y)));
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Hover(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void Hover(ElementProxy element)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Hover(new ElementProxy(e.Item1, e.Item2))));
        }

        public void Focus(ElementProxy element)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Focus(new ElementProxy(e.Item1, e.Item2))));
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.DragAndDrop(sourceX, sourceY, destinationX, destinationY)));
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            this.RepackExceptions(() => Parallel.ForEach(source.Elements, e =>
            {
                e.Item1.DragAndDrop(
                    new ElementProxy(e.Item1, e.Item2), sourceOffsetX, sourceOffsetY,
                    new ElementProxy(e.Item1, target.Elements.First(x => x.Item1 == e.Item1).Item2), targetOffsetX, targetOffsetY
                );
            }));
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            this.RepackExceptions(() => Parallel.ForEach(source.Elements, e =>
            {
                e.Item1.DragAndDrop(
                    new ElementProxy(e.Item1, e.Item2),
                    new ElementProxy(e.Item1, target.Elements.First(x => x.Item1 == e.Item1).Item2)
                );
            }));
        }

        public void EnterText(ElementProxy element, string text)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.EnterText(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.EnterTextWithoutEvents(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public void AppendText(ElementProxy element, string text)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e =>
            {
                e.Item1.AppendText(new ElementProxy(e.Item1, e.Item2), text);
            }));
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.AppendTextWithoutEvents(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectText(new ElementProxy(e.Item1, e.Item2), optionText)));
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectValue(new ElementProxy(e.Item1, e.Item2), optionValue)));
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectIndex(new ElementProxy(e.Item1, e.Item2), optionIndex)));
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectText(new ElementProxy(e.Item1, e.Item2), optionTextCollection)));
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectValue(new ElementProxy(e.Item1, e.Item2), optionValues)));
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectIndex(new ElementProxy(e.Item1, e.Item2), optionIndices)));
        }

        public void TakeScreenshot(string screenshotName)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.TakeScreenshot(screenshotName)));
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            this.RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.UploadFile(new ElementProxy(e.Item1, e.Item2), x, y, fileName)));
        }

        public void Wait()
        {
            this.commandProviders.First().Wait();
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.commandProviders.First().Wait(timeSpan);
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionFunc)));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionFunc, timeout)));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionAction)));
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.WaitUntil(conditionAction, timeout)));
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
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.SwitchToFrame(frameName)));
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.SwitchToFrame(frameElement)));
        }

        public void SwitchToWindow(string windowName)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.SwitchToWindow(windowName)));
        }

        public void AlertClick(Alert accessor)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.AlertClick(accessor)));
        }

        public void AlertText(Action<string> matchFunc)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.AlertText(matchFunc)));
        }

        public void AlertEnterText(string text)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.AlertEnterText(text)));
        }

        public void Visible(ElementProxy element, Action<bool> action)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.Visible(element, action)));
        }

        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.CssPropertyValue(element, propertyName, action)));
        }

        public void Act(CommandType commandType, Action action)
        {
            this.RepackExceptions(() => Parallel.ForEach(this.commandProviders, x => x.Act(commandType, action)));
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

        private void RepackExceptions(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.First();
            }
        }
    }
}
