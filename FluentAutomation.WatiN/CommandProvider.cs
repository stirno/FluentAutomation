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

        public void DragAndDrop(Func<IElement> source, Func<IElement> target)
        {
            var sourceEl = source() as Element;
            var targetEl = target() as Element;

            MouseControl.SetPosition(sourceEl.PosX, sourceEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonDown, sourceEl.PosX, sourceEl.PosY, 0, 0);
            MouseControl.SetPosition(targetEl.PosX, targetEl.PosY);
            MouseControl.MouseEvent(MouseControl.MouseEvent_LeftButtonUp, targetEl.PosX, targetEl.PosY, 0, 0);
        }

        public void EnterText(Func<IElement> element, string text)
        {
            var el = element() as Element;
            if (el.IsText)
            {
                var txt = new WatiNCore.TextField(this.browser.DomContainer, el.AutomationElement.NativeElement);
                
                foreach (var chr in text)
                {
                    txt.KeyDown(chr);
                    txt.KeyPress(chr);
                    txt.KeyUp(chr);
                    fireOnChange(el.AutomationElement);
                }
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

        public void TakeScreenshot(string screenshotName)
        {
            this.browser.CaptureWebPageToFile(screenshotName);
        }

        public void UploadFile(Func<IElement> element, int x, int y, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Wait(int seconds)
        {
            this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public void Wait()
        {
            this.Wait(Settings.DefaultWaitTimeout);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.Act(() => System.Threading.Thread.Sleep(timeSpan));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.WaitUntil(conditionFunc, Settings.DefaultWaitUntilTimeout);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.Act(() =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool isFuncValid = false;
                var compiledFunc = conditionFunc.Compile();

                while (DateTime.Now < dateTimeTimeout)
                {
                    if (compiledFunc() == true)
                    {
                        isFuncValid = true;
                        break;
                    }

                    System.Threading.Thread.Sleep(Settings.DefaultWaitUntilThreadSleep);
                }

                // If func is still not valid, assume we've hit the timeout.
                if (isFuncValid == false)
                {
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}].", timeout.TotalMilliseconds, conditionFunc.ToExpressionString());
                }
            });
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            this.WaitUntil(conditionAction, Settings.DefaultWaitUntilTimeout);
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.Act(() =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool threwException = false;
                var compiledAction = conditionAction.Compile();

                while (DateTime.Now < dateTimeTimeout)
                {
                    try
                    {
                        threwException = false;
                        compiledAction();
                    }
                    catch (FluentException)
                    {
                        threwException = true;
                    }

                    if (!threwException)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(Settings.DefaultWaitUntilThreadSleep);
                }

                // If an exception was thrown the last loop, assume we hit the timeout
                if (threwException == true)
                {
                    throw new FluentException("Conditional wait passed the timeout [{0}ms]", timeout.TotalMilliseconds, conditionAction.ToExpressionString());
                }
            });
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
