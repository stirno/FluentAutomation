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
            this.lazyBrowser = new Lazy<WatiNCore.IE>(() => {
                var bf = browserFactory();
                ((SHDocVw.WebBrowser)bf.InternetExplorer).FullScreen = true;
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
        
        public Func<IElement> Find(string selector)
        {
            return new Func<IElement>(() =>
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

        public Func<IEnumerable<IElement>> FindMultiple(string selector)
        {
            return new Func<IEnumerable<IElement>>(() =>
            {
                try
                {
                    var automationElements = this.browser.Elements.Filter(WatiNCore.Find.BySelector(selector));
                    if (automationElements.Count == 0) throw new KeyNotFoundException();

                    List<Element> resultSet = new List<Element>();
                    automationElements.ToList().ForEach(x => resultSet.Add(new Element(x, selector)));
                    return resultSet;
                }
                catch (KeyNotFoundException)
                {
                    throw new FluentException("Unable to find element with selector: {0}", selector);
                }
            });
        }

        public void Click(int x, int y)
        {
            FluentAutomation.MouseControl.Click(x, y);
        }

        public void Click(Func<IElement> element, int x, int y)
        {
            var el = element() as Element;
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
        }

        public void Click(Func<IElement> element)
        {
            var el = element() as Element;
            el.AutomationElement.Click();
        }

        public void DoubleClick(int x, int y)
        {
            FluentAutomation.MouseControl.Click(x, y);
            System.Threading.Thread.Sleep(30);
            FluentAutomation.MouseControl.Click(x, y);
        }

        public void DoubleClick(Func<IElement> element, int x, int y)
        {
            var el = element() as Element;
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
            System.Threading.Thread.Sleep(30);
            FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
        }

        public void DoubleClick(Func<IElement> element)
        {
            var el = element() as Element;
            this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).dblclick(); }}", el.AutomationElement.GetJavascriptElementReference()));
        }

        public void RightClick(Func<IElement> element)
        {
            var el = element() as Element;
            this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('contextmenu'); }}", el.AutomationElement.GetJavascriptElementReference()));
        }

        public void Hover(int x, int y)
        {
            FluentAutomation.MouseControl.SetPosition(x, y);
        }

        public void Hover(Func<IElement> element, int x, int y)
        {
            var el = element() as Element;
            FluentAutomation.MouseControl.SetPosition(el.PosX + x, el.PosY + y);
        }

        public void Hover(Func<IElement> element)
        {
            var el = element() as Element;
            FluentAutomation.MouseControl.SetPosition(el.PosX, el.PosY);            
        }

        public void Focus(Func<IElement> element)
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

        public void DragAndDrop(Func<IElement> source, Func<IElement> target)
        {
            var sourceEl = source() as Element;
            var targetEl = target() as Element;

            MouseControl.SetPosition(sourceEl.PosX, sourceEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceEl.PosX, sourceEl.PosY, 0, 0);
            MouseControl.SetPosition(targetEl.PosX, targetEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, targetEl.PosX, targetEl.PosY, 0, 0);
        }

        public void DragAndDrop(Func<IElement> source, int sourceOffsetX, int sourceOffsetY, Func<IElement> target, int targetOffsetX, int targetOffsetY)
        {
            var sourceEl = source() as Element;
            var targetEl = target() as Element;

            var sourceX = sourceEl.PosX + sourceOffsetX;
            var sourceY = sourceEl.PosY + sourceOffsetY;
            var targetX = targetEl.PosX + targetOffsetX;
            var targetY = targetEl.PosY + targetOffsetY;

            MouseControl.SetPosition(sourceX, sourceY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceX, sourceY, 0, 0);
            MouseControl.SetPosition(targetX, targetY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, targetX, targetY, 0, 0);
        }

        public void EnterText(Func<IElement> element, string text)
        {
            var el = element() as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.TypeText(text);
            }
        }

        public void EnterTextWithoutEvents(Func<IElement> element, string text)
        {
            var el = element() as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.Value = text;
                this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
            }
        }

        public void AppendText(Func<IElement> element, string text)
        {
            var el = element() as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.AppendText(text);
            }
        }

        public void AppendTextWithoutEvents(Func<IElement> element, string text)
        {
            var el = element() as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                txt.Value = text;
                this.browser.DomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
            }
        }

        public void SelectText(Func<IElement> element, string optionText)
        {
            var el = element() as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.Select(optionText);
                fireOnChange(el.AutomationElement);
            }
        }

        public void SelectValue(Func<IElement> element, string optionValue)
        {
            var el = element() as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.SelectByValue(optionValue);
                fireOnChange(el.AutomationElement);
            }
        }

        public void SelectIndex(Func<IElement> element, int optionIndex)
        {
            var el = element() as Element;
            if (el.IsSelect)
            {
                var sl = new WatiNCore.SelectList(this.browser.DomContainer, el.AutomationElement.NativeElement);
                sl.Options[optionIndex].Select();
                fireOnChange(el.AutomationElement);
            }
        }

        public void MultiSelectText(Func<IElement> element, string[] optionTextCollection)
        {
            var el = element() as Element;
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

        public void MultiSelectValue(Func<IElement> element, string[] optionValues)
        {
            var el = element() as Element;
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

        public void MultiSelectIndex(Func<IElement> element, int[] optionIndices)
        {
            var el = element() as Element;
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

        public void UploadFile(Func<IElement> element, int x, int y, string fileName)
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
