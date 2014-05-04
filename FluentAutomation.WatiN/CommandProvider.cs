using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using WatiNCore = global::WatiN.Core;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Exceptions;

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

        private AlertDialogHandler alertHandler = null;
        internal AlertDialogHandler AlertHandler
        {
            get
            {
                if (alertHandler == null)
                {
                    alertHandler = new AlertDialogHandler();
                    lazyBrowser.Value.AddDialogHandler(alertHandler);
                }

                return alertHandler;
            }

            set
            {
                if (value == null)
                    lazyBrowser.Value.RemoveDialogHandler(alertHandler);
            }
        }

        private ConfirmDialogHandler confirmHandler = null;
        internal ConfirmDialogHandler ConfirmHandler
        {
            get
            {
                if (confirmHandler == null)
                {
                    confirmHandler = new ConfirmDialogHandler();
                    lazyBrowser.Value.AddDialogHandler(alertHandler);
                }

                return confirmHandler;
            }

            set
            {
                if (value == null)
                    lazyBrowser.Value.RemoveDialogHandler(confirmHandler);
            }
        }

        private PromptDialogHandler promptHandler = null;
        internal PromptDialogHandler PromptHandler
        {
            get
            {
                if (promptHandler == null)
                {
                    promptHandler = new PromptDialogHandler("");
                    lazyBrowser.Value.AddDialogHandler(alertHandler);
                }

                return promptHandler;
            }

            set
            {
                if (value == null)
                    lazyBrowser.Value.RemoveDialogHandler(promptHandler);
            }
        }

        private WatiNCore.DomContainer ActiveDomContainer { get; set; }

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

                // force init handler
                var hack = this.alertHandler;
                var hack2 = this.confirmHandler;
                this.ActiveDomContainer = bf.DomContainer;

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

        public string Source
        {
            get
            {
                return this.browser.Body.Parent.OuterHtml;
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
                        result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(this, () => new Element(element, selector)));
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
            this.Act(CommandType.Action, () =>
            {
                FluentAutomation.MouseControl.Click(x, y);
            });
        }

        public void Click(ElementProxy element, int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
            });
        }

        public void Click(ElementProxy element)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                el.AutomationElement.Click();
            });
        }

        public void DoubleClick(int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                FluentAutomation.MouseControl.Click(x, y);
                System.Threading.Thread.Sleep(30);
                FluentAutomation.MouseControl.Click(x, y);
            });
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
                System.Threading.Thread.Sleep(30);
                FluentAutomation.MouseControl.Click(el.PosX + x, el.PosY + y);
            });
        }

        public void DoubleClick(ElementProxy element)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                this.ActiveDomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).dblclick(); }}", el.AutomationElement.GetJavascriptElementReference()));
            });
        }

        public void RightClick(int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                FluentAutomation.MouseControl.RightClick(x, y);
            });
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                FluentAutomation.MouseControl.RightClick(el.PosX + x, el.PosY + y);
            });
        }

        public void RightClick(ElementProxy element)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                this.RightClick(el.PosX + el.Width / 2, el.PosY + el.Height / 2);
            });
        }

        public void Hover(int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                FluentAutomation.MouseControl.SetPosition(x, y);
            });
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                FluentAutomation.MouseControl.SetPosition(el.PosX + x, el.PosY + y);
            });
        }

        public void Hover(ElementProxy element)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                FluentAutomation.MouseControl.SetPosition(el.PosX, el.PosY);
            });
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
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsText)
                {
                    var txt = new WatiNCore.TextField(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    txt.TypeText(text);
                }
            });
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsText)
                {
                    var txt = new WatiNCore.TextField(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    txt.Value = text;
                    this.ActiveDomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
                }
            });
        }

        public void AppendText(ElementProxy element, string text)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsText)
                {
                    var txt = new WatiNCore.TextField(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    txt.AppendText(text);
                }
            });
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsText)
                {
                    var txt = new WatiNCore.TextField(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    txt.Value = text;
                    this.ActiveDomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).trigger('keyup'); }}", el.AutomationElement.GetJavascriptElementReference()));
                }
            });
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect)
                {
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    sl.Select(optionText);
                    fireOnChange(el.AutomationElement);
                }
            });
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect)
                {
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    sl.SelectByValue(optionValue);
                    fireOnChange(el.AutomationElement);
                }
            });
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect)
                {
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    sl.Options[optionIndex].Select();
                    fireOnChange(el.AutomationElement);
                }
            });
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect && el.IsMultipleSelect)
                {
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    foreach (var text in optionTextCollection)
                    {
                        sl.Select(text);
                        fireOnChange(el.AutomationElement);
                    }
                }
            });
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect && el.IsMultipleSelect)
                {
                    new WatiNCore.SelectList(this.browser.Frame("").DomContainer, el.AutomationElement.NativeElement);
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    foreach (var val in optionValues)
                    {
                        sl.SelectByValue(val);
                        fireOnChange(el.AutomationElement);
                    }
                }
            });
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = element.Element as Element;
                if (el.IsSelect && el.IsMultipleSelect)
                {
                    var sl = new WatiNCore.SelectList(this.ActiveDomContainer, el.AutomationElement.NativeElement);
                    foreach (var i in optionIndices)
                    {
                        sl.Options[i].Select();
                        fireOnChange(el.AutomationElement);
                    }
                }
            });
        }

        public override void TakeScreenshot(string screenshotName)
        {
            this.Act(CommandType.Action, () =>
            {
                this.browser.CaptureWebPageToFile(screenshotName);
            });
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            throw new NotImplementedException();
        }
        
        public void Press(string keys)
        {
            this.Act(CommandType.Action, () =>
            {
                System.Windows.Forms.SendKeys.SendWait(keys);
            });
        }

        public void Type(string text)
        {
            this.Act(CommandType.Action, () =>
            {
                foreach (var chr in text)
                {
                    System.Windows.Forms.SendKeys.SendWait(chr.ToString());
                }
            });
        }

        public void Dispose()
        {
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Stop();
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Quit();
            this.browser.Close();
            this.browser.Dispose();
        }

        public void SwitchToWindow(string windowName)
        {
            this.Act(CommandType.Action, () =>
            {
                this.ActiveDomContainer = WatiNCore.Browser.AttachTo<WatiNCore.IE>(WatiNCore.Find.ByTitle(windowName)).DomContainer;
            });
        }

        public void SwitchToFrame(string frameNameOrSelector)
        {
            this.Act(CommandType.Action, () =>
            {
                // try to locate frame using argument as a selector, if that fails try finding
                // frame by name
                WatiNCore.Frame frame = null;
                try
                {
                    frame = this.browser.Frame(WatiNCore.Find.BySelector(frameNameOrSelector));
                }
                catch (FrameNotFoundException)
                {
                }

                if (frame == null)
                    frame = this.browser.Frame(WatiNCore.Find.ByName(frameNameOrSelector));

                this.ActiveDomContainer = frame.DomContainer;
            });
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            this.Act(CommandType.Action, () =>
            {
                this.ActiveDomContainer = this.browser.Frame(WatiNCore.Find.BySelector(frameElement.Element.Selector)).DomContainer;
            });
        }

        public void AlertClick(Alert accessor)
        {
            if (accessor.Field == AlertField.OKButton)
            {
                this.AlertHandler.WaitUntilExists((int)this.Settings.WaitTimeout.TotalSeconds);
                this.AlertHandler.OKButton.Click();
            }
            else if (accessor.Field == AlertField.CancelButton)
            {
                this.ConfirmHandler.WaitUntilExists((int)this.Settings.WaitTimeout.TotalSeconds);
                this.ConfirmHandler.CancelButton.Click();
            }
            else
            {
                // attempt to close any dialogs to allow other tests to continue
                try
                {
                    this.AlertHandler.OKButton.Click();
                }
                catch (Exception) { }

                try
                {
                    this.ConfirmHandler.OKButton.Click();
                }
                catch (Exception) { }

                throw new FluentException("FluentAutomation only supports clicking on OK or Cancel in alerts or prompts.");
            }
        }

        public void AlertText(Action<string> matchFunc)
        {
            this.ConfirmHandler.WaitUntilExists((int)this.Settings.WaitTimeout.TotalSeconds);
            matchFunc(this.ConfirmHandler.Message);
        }

        public void AlertEnterText(string text)
        {
            throw new FluentException("Due to inconsistent behavior, handling of prompts that accept text entry is disabled when using WatiN with FluentAutomation.");
        }
        
        public void Visible(ElementProxy element, Action<bool> action)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = (element.Element as Element).AutomationElement;
                var isVisible = false;
                try
                {
                    isVisible = bool.Parse(this.ActiveDomContainer.Eval(string.Format("jQuery({0}).is(':visible');", el.GetJavascriptElementReference())));
                }
                catch (JavaScriptException) { }
                action(isVisible);
            });
        }

        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            this.Act(CommandType.Action, () =>
            {
                var el = (element.Element as Element).AutomationElement;
                object cssPropertyValue = null;
                try
                {
                    cssPropertyValue = bool.Parse(this.ActiveDomContainer.Eval(string.Format("jQuery({0}).css('{1}');", el.GetJavascriptElementReference(), propertyName)));
                }
                catch (JavaScriptException) { }

                action(cssPropertyValue != null, cssPropertyValue.ToString());
            });
        }

        public ICommandProvider WithConfig(FluentSettings settings)
        {
            this.Settings = settings;

            if (settings.WindowMaximized)
            {
                this.browser.ShowWindow(WatiNCore.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
            }
            // If the browser size has changed since the last config change, update it
            else if (settings.WindowWidth.HasValue && settings.WindowHeight.HasValue)
            {
                this.browser.SizeWindow(settings.WindowWidth.Value, settings.WindowHeight.Value);
            }

            return this;
        }

        private void fireOnChange(WatiNCore.Element element)
        {
            this.ActiveDomContainer.Eval(string.Format("if (typeof jQuery != 'undefined') {{ jQuery({0}).change(); }}", element.GetJavascriptElementReference()));
        }
    }
}
