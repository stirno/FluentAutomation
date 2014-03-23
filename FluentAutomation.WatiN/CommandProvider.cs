using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using WatiNCore = global::WatiN.Core;

namespace FluentAutomation
{
    // Dirty hacks on several funcs that require jQuery to properly work due to flaw in WatiN events in IE9.. Replace with native JavaScript ASAP
    public class CommandProvider : BaseCommandProvider, ICommandProvider, IDisposable
    {
        private readonly Lazy<WatiNCore.IE> lazyBrowser = null;
        private WatiNCore.IE browser
        {
            get
            {
                return lazyBrowser.Value;
            }
        }

        public CommandProvider(Func<WatiNCore.IE> browserFactory)
        {
            FluentTest.ProviderInstance = null;

            this.lazyBrowser = new Lazy<WatiNCore.IE>(() => {
                var bf = browserFactory();
                ((SHDocVw.WebBrowser)bf.InternetExplorer).FullScreen = true;

                if (FluentTest.ProviderInstance == null)
                    FluentTest.ProviderInstance = bf;
                else
                    FluentTest.IsMultiBrowserTest = true;

                return bf;
            });
        }

        public Uri Url
        {
            get
            {
                return this.browser.Uri;
            }
        }

        public void Navigate(Uri uri)
        {
            this.browser.GoTo(uri);
        }
        
        public ElementProxy Find(string selector)
        {
            return new ElementProxy(this, () =>
            {
                try
                {
                    var automationElement = this.browser.Element(WatiNCore.Find.BySelector(selector));
                    if (!automationElement.Exists) throw new KeyNotFoundException();

                    return new Element(automationElement, selector);
                }
                catch (KeyNotFoundException)
                {
                    throw new FluentException("Unable to find element with selector: {0}", selector);
                }
            });
        }

        public ElementProxy FindMultiple(string selector)
        {
            var finalResult = new ElementProxy();

            finalResult.Children.Add(new Func<ElementProxy>(() =>
            {
                try
                {
                    var automationElements = this.browser.Elements.Filter(WatiNCore.Find.BySelector(selector));
                    if (automationElements.Count == 0) throw new KeyNotFoundException();

                    var result = new ElementProxy();
                    foreach (var element in automationElements)
                    {
                        result.Elements.Add(this, () => new Element(element, selector));
                    }

                    return result;
                }
                catch (KeyNotFoundException)
                {
                    throw new FluentException("Unable to find element with selector: {0}", selector);
                }
            }));

            return finalResult;
        }

        public void Click(int x, int y)
        {
            FluentAutomation.MouseControl.Click(x, y);
        }

        public void Click(ElementProxy element, int x, int y)
        {
            var el = element.Element as Element;
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
        }

        public void Click(ElementProxy element)
        {
            var el = element.Element as Element;
            el.AutomationElement.Click();
        }

        public void DoubleClick(int x, int y)
        {
            FluentAutomation.MouseControl.Click(x, y);
            System.Threading.Thread.Sleep(30);
            FluentAutomation.MouseControl.Click(x, y);
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            var el = element.Element as Element;
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
            System.Threading.Thread.Sleep(30);
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
        }

        public void DoubleClick(ElementProxy element)
        {
            var el = element.Element as Element;
            this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).dblclick(); }}", el.AutomationElement.GetJavascriptElementReference()));
        }

        public void RightClick(ElementProxy element)
        {
            var el = element.Element as Element;
            this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('contextmenu'); }}", el.AutomationElement.GetJavascriptElementReference()));
        }

        public void Hover(int x, int y)
        {
            FluentAutomation.MouseControl.SetPosition(x, y);
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            var el = element.Element as Element;
            FluentAutomation.MouseControl.SetPosition(el.PosX + x, el.PosY + y);
        }

        public void Hover(ElementProxy element)
        {
            var el = element.Element as Element;
            FluentAutomation.MouseControl.SetPosition(el.PosX, el.PosY);            
        }

        public void Focus(ElementProxy element)
        {
            throw new NotImplementedException();
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            MouseControl.SetPosition(sourceX, sourceY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceX, sourceY, 0, 0);
            MouseControl.SetPosition(destinationX, destinationY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, destinationX, destinationY, 0, 0);
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            var sourceEl = source.Element as Element;
            var targetEl = target.Element as Element;

            MouseControl.SetPosition(sourceEl.PosX, sourceEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceEl.PosX, sourceEl.PosY, 0, 0);
            MouseControl.SetPosition(targetEl.PosX, targetEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, targetEl.PosX, targetEl.PosY, 0, 0);
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            var sourceEl = source.Element as Element;
            var targetEl = target.Element as Element;

            var sourceX = sourceEl.PosX + sourceOffsetX;
            var sourceY = sourceEl.PosY + sourceOffsetY;
            var targetX = targetEl.PosX + targetOffsetX;
            var targetY = targetEl.PosY + targetOffsetY;

            MouseControl.SetPosition(sourceX, sourceY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceX, sourceY, 0, 0);
            MouseControl.SetPosition(targetX, targetY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, targetX, targetY, 0, 0);
        }

        public void EnterText(ElementProxy element, string text)
        {
            var el = element.Element as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.TypeText(text);
            }
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            var el = element.Element as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.Value = text;
                this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
            }
        }

        public void AppendText(ElementProxy element, string text)
        {
            var el = element.Element as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.AppendText(text);
            }
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            var el = element.Element as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.Value = text;
                this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
            }
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            var el = element.Element as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.Select(optionText);
                fireOnChange(el.AutomationElement);
            }
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            var el = element.Element as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.SelectByValue(optionValue);
                fireOnChange(el.AutomationElement);
            }
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            var el = element.Element as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.Options[optionIndex].Select();
                fireOnChange(el.AutomationElement);
            }
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            var el = element.Element as Element;
            if (el.IsSelect && el.IsMultipleSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                foreach (var text in optionTextCollection)
                {
                    sl.Select(text);
                    fireOnChange(el.AutomationElement);
                }
            }
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            var el = element.Element as Element;
            if (el.IsSelect && el.IsMultipleSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                foreach (var val in optionValues)
                {
                    sl.SelectByValue(val);
                    fireOnChange(el.AutomationElement);
                }
            }
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            var el = element.Element as Element;
            if (el.IsSelect && el.IsMultipleSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                foreach (var i in optionIndices)
                {
                    sl.Options[i].Select();
                    fireOnChange(el.AutomationElement);
                }
            }
        }

        public override void TakeScreenshot(string screenshotName)
        {
            this.browser.CaptureWebPageToFile(screenshotName);
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            throw new NotImplementedException();
        }
        
        public void Press(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        public void Type(string text)
        {
            foreach (var chr in text)
            {
                System.Windows.Forms.SendKeys.SendWait(chr.ToString());
            }
        }

        public void Dispose()
        {
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Stop();
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Quit();
            this.browser.Close();
            this.browser.Dispose();
        }

        private void fireOnChange(WatiNCore.Element element)
        {
            this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).change(); }}", element.GetJavascriptElementReference()));
        }
    }
}
