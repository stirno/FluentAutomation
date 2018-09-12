using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FluentAutomation
{
    public class CommandProvider : BaseCommandProvider, ICommandProvider
    {
        private readonly IFileStoreProvider fileStoreProvider;
        private readonly Lazy<IWebDriver> lazyWebDriver;
        private string mainWindowHandle;

        private IWebDriver webDriver => lazyWebDriver.Value;

        public CommandProvider(Func<IWebDriver> webDriverFactory, IFileStoreProvider fileStoreProvider)
        {
            FluentTest.ProviderInstance = null;

            lazyWebDriver = new Lazy<IWebDriver>(() =>
            {
                IWebDriver webDriver = webDriverFactory();
                if (!FluentTest.IsMultiBrowserTest && FluentTest.ProviderInstance == null)
                    FluentTest.ProviderInstance = webDriver;

                webDriver.Manage().Cookies.DeleteAllCookies();
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                
                // If an alert is open, the world ends if we touch the size property. Ignore this and let it get set by the next command chain
                try
                {
                    if (Settings.WindowMaximized)
                    {
                        // store window size back before maximizing so we can 'undo' this action if necessary
                        Size windowSize = webDriver.Manage().Window.Size;
                        if (!Settings.WindowWidth.HasValue)
                            Settings.WindowWidth = windowSize.Width;

                        if (!Settings.WindowHeight.HasValue)
                            Settings.WindowHeight = windowSize.Height;

                        webDriver.Manage().Window.Maximize();
                    }
                    else if (Settings.WindowHeight.HasValue && Settings.WindowWidth.HasValue)
                    {
                        webDriver.Manage().Window.Size = new Size(Settings.WindowWidth.Value, Settings.WindowHeight.Value);
                    }
                    else
                    {
                        Size windowSize = webDriver.Manage().Window.Size;
                        Settings.WindowHeight = windowSize.Height;
                        Settings.WindowWidth = windowSize.Width;
                    }
                }
                catch (UnhandledAlertException) { }

                mainWindowHandle = webDriver.CurrentWindowHandle;

                return webDriver;
            });

            this.fileStoreProvider = fileStoreProvider;
        }

        public ICommandProvider WithConfig(FluentSettings settings)
        {
            // If an alert is open, the world ends if we touch the size property. Ignore this and let it get set by the next command chain
            try
            {
                if (settings.WindowMaximized)
                {
                    // store window size back before maximizing so we can 'undo' this action if necessary
                    // this code intentionally touches this.Settings before its been replaced with the local
                    // configuration code, so that when the With.___.Then block is completed, the outer settings
                    // object has the correct window size to work with.
                    Size windowSize = webDriver.Manage().Window.Size;
                    if (!Settings.WindowWidth.HasValue)
                        Settings.WindowWidth = windowSize.Width;

                    if (!Settings.WindowHeight.HasValue)
                        Settings.WindowHeight = windowSize.Height;

                    webDriver.Manage().Window.Maximize();
                }
                // If the browser size has changed since the last config change, update it
                else if (settings.WindowWidth.HasValue && settings.WindowHeight.HasValue)
                {
                    webDriver.Manage().Window.Size = new Size(settings.WindowWidth.Value, settings.WindowHeight.Value);
                }
            }
            catch (UnhandledAlertException) { }

            Settings = settings;

            return this;
        }

        public Uri Url => new Uri(webDriver.Url, UriKind.Absolute);

        public string Source => webDriver.PageSource;

        public void Navigate(Uri url)
        {
            Act(CommandType.Action, () =>
            {
                if (!url.IsAbsoluteUri)
                {
                    url = new Uri(new Uri(new Uri(webDriver.Url).GetLeftPart(UriPartial.Authority)), url.ToString());
                }

                webDriver.Navigate().GoToUrl(url);
            });
        }

        public ElementProxy Find(string selector)
        {
            return new ElementProxy(this, () =>
            {
                try
                {
                    return new Element(webDriver.FindElement(Sizzle.Find(selector)), selector);
                }
                catch (NoSuchElementException)
                {
                    throw new FluentElementNotFoundException("Unable to find element with selector [{0}]", selector);
                }
            });
        }

        public ElementProxy FindMultiple(string selector)
        {
            var finalResult = new ElementProxy();

            finalResult.Children.Add(() =>
            {
                var result = new ElementProxy();
                var webElements = webDriver.FindElements(Sizzle.Find(selector));
                if (webElements.Count == 0)
                    throw new FluentElementNotFoundException("Unable to find element with selector [{0}].", selector);

                foreach (IWebElement element in webElements)
                {
                    result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(this, () => new Element(element, selector)));
                }

                return result;
            });

            return finalResult;
        }

        public void Click(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)Find("html").Element).WebElement, x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)element.Element).WebElement, x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .Click(((Element)element.Element).WebElement)
                    .Perform();
            });
        }

        public void DoubleClick(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)Find("html").Element).WebElement, x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)element.Element).WebElement, x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .DoubleClick(((Element)element.Element).WebElement)
                    .Perform();
            });
        }

        public void RightClick(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)Find("html").Element).WebElement, x, y)
                    .ContextClick()
                    .Perform();
            });
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)element.Element).WebElement, x, y)
                    .ContextClick()
                    .Perform();
            });
        }
        
        public void RightClick(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .ContextClick(((Element)element.Element).WebElement)
                    .Perform();
            });
        }

        public void Hover(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)Find("html").Element).WebElement, x, y)
                    .Perform();
            });
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)element.Element).WebElement, x, y)
                    .Perform();
            });
        }

        public void Hover(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)element.Element).WebElement)
                    .Perform();
            });
        }

        public void Focus(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = (Element)element.Element;

                switch (unwrappedElement.WebElement.TagName)
                {
                    case "input":
                    case "select":
                    case "textarea":
                    case "a":
                    case "iframe":
                    case "button":
                        ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].focus();", unwrappedElement.WebElement);
                        break;
                }
            });
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = (Element)Find("html").Element;
                new Actions(webDriver)
                    .MoveToElement(rootElement.WebElement, sourceX, sourceY)
                    .ClickAndHold()
                    .MoveToElement(rootElement.WebElement, destinationX, destinationY)
                    .Release()
                    .Perform();
            });
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .MoveToElement(((Element)source.Element).WebElement, sourceOffsetX, sourceOffsetY)
                    .ClickAndHold()
                    .MoveToElement(((Element)target.Element).WebElement, targetOffsetX, targetOffsetY)
                    .Release()
                    .Perform();
            });
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            Act(CommandType.Action, () =>
            {
                new Actions(webDriver)
                    .DragAndDrop(((Element)source.Element).WebElement, ((Element)target.Element).WebElement)
                    .Perform();
            });
        }

        public void EnterText(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = (Element)element.Element;
                unwrappedElement.WebElement.Clear();
                unwrappedElement.WebElement.SendKeys(text);
            });
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>  
            {
                ((IJavaScriptExecutor)webDriver).ExecuteScript($"if (typeof fluentjQuery != 'undefined') {{ fluentjQuery(\"{((Element)element.Element).Selector.Replace("\"", "")}\").val(\"{text.Replace("\"", "")}\").trigger('change'); }}");
            });
        }

        public void AppendText(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                ((Element)element.Element).WebElement.SendKeys(text);
            });
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                ((IJavaScriptExecutor)webDriver).ExecuteScript(string.Format("if (typeof fluentjQuery != 'undefined') {{ fluentjQuery(\"{0}\").val(fluentjQuery(\"{0}\").val() + \"{1}\").trigger('change'); }}", ((Element)element.Element).Selector.Replace("\"", ""), text.Replace("\"", "")));
            });
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByText(optionText);
            });
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (string optionValue in optionValues)
                {
                    selectElement.SelectByValue(optionValue);
                }
            });
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (int optionIndex in optionIndices)
                {
                    selectElement.SelectByIndex(optionIndex);
                }
            });
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (string optionText in optionTextCollection)
                {
                    selectElement.SelectByText(optionText);
                }
            });
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByValue(optionValue);
            });
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            Act(CommandType.Action, () =>
            {
                var selectElement = new SelectElement(((Element)element.Element).WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByIndex(optionIndex);
            });
        }

        public override void TakeScreenshot(string screenshotName)
        {
            Act(CommandType.Action, () =>
            {
                // get raw screenshot
                string tmpImagePath = Path.Combine(Settings.UserTempDirectory, screenshotName);
                ((ITakesScreenshot)webDriver).GetScreenshot().SaveAsFile(tmpImagePath, ScreenshotImageFormat.Png);

                // save to file store
                fileStoreProvider.SaveScreenshot(Settings, File.ReadAllBytes(tmpImagePath), screenshotName);
                File.Delete(tmpImagePath);
            });
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            Act(CommandType.Action, () =>
            {
                // wait before typing in the field
                Task task = Task.Factory.StartNew(() =>
                {
                    Wait(TimeSpan.FromMilliseconds(1000));
                    Type(fileName);
                });

                if (x == 0 && y == 0)
                {
                    Click(element);
                }
                else
                {
                    Click(element, x, y);
                }

                task.Wait();
                Wait(TimeSpan.FromMilliseconds(1500));
            });
        }

        public void Press(string keys)
        {
            Act(CommandType.Action, () => System.Windows.Forms.SendKeys.SendWait(keys));
        }

        public void Type(string text)
        {
            Act(CommandType.Action, () =>
            {
                foreach (char character in text)
                {
                    System.Windows.Forms.SendKeys.SendWait(character.ToString());
                    Wait(TimeSpan.FromMilliseconds(20));
                }
            });
        }

        public void SwitchToWindow(string windowName)
        {
            Act(CommandType.Action, () =>
            {
                if (windowName == string.Empty)
                {
                    webDriver.SwitchTo().Window(mainWindowHandle);
                    return;
                }

                var matchFound = false;
                foreach (string windowHandle in webDriver.WindowHandles)
                {
                    webDriver.SwitchTo().Window(windowHandle);

                    if (webDriver.Title == windowName || webDriver.Url.EndsWith(windowName))
                    {
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    throw new FluentException("No window with a title or URL matching [{0}] could be found.", windowName);
                }
            });
        }

        public void SwitchToFrame(string frameNameOrSelector)
        {
            Act(CommandType.Action, () =>
            {
                if (frameNameOrSelector == string.Empty)
                {
                    webDriver.SwitchTo().DefaultContent();
                    return;
                }

                // try to locate frame using argument as a selector, if that fails pass it into Frame so it can be
                // evaluated as a name by Selenium
                IWebElement frameBySelector = null;
                try
                {
                    frameBySelector = webDriver.FindElement(Sizzle.Find(frameNameOrSelector));
                }
                catch (NoSuchElementException)
                {
                }

                if (frameBySelector == null)
                    webDriver.SwitchTo().Frame(frameNameOrSelector);
                else
                    webDriver.SwitchTo().Frame(frameBySelector);
            });
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            Act(CommandType.Action, () =>
            {
                webDriver.SwitchTo().Frame(((Element)frameElement.Element).WebElement);
            });
        }

        public static IAlert ActiveAlert;
        private void SetActiveAlert()
        {
            if (ActiveAlert == null)
            {
                Act(CommandType.Action, () =>
                {
                    try
                    {
                        ActiveAlert = webDriver.SwitchTo().Alert();
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException(ex.Message, ex);
                    }
                });
            }
        }

        public void AlertClick(Alert accessor)
        {
            SetActiveAlert();
            if (ActiveAlert == null)
                return;

            try
            {
                Act(CommandType.Action, () =>
                {
                    try
                    {
                        if (accessor == Alert.OK)
                            ActiveAlert.Accept();
                        else
                            ActiveAlert.Dismiss();
                    }
                    catch (NoAlertPresentException ex)
                    {
                        throw new FluentException(ex.Message, ex);
                    }
                });
            }
            finally
            {
                ActiveAlert = null;
            }
        }

        public void AlertText(Action<string> matchFunc)
        {
            SetActiveAlert();
            matchFunc(ActiveAlert.Text);
        }

        public void AlertEnterText(string text)
        {
            SetActiveAlert();
            ActiveAlert.SendKeys(text);

            try
            {
                // just do it - attempting to get behaviors between browsers to match
                ActiveAlert.Accept();
            }
            catch {}
        }

        public void Visible(ElementProxy element, Action<bool> action)
        {
            Act(CommandType.Action, () =>
            {
                bool isVisible = (element.Element as Element).WebElement.Displayed;
                action(isVisible);
            });
        }
        
        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            Act(CommandType.Action, () =>
            {
                object propertyValue = ((IJavaScriptExecutor)webDriver).ExecuteScript($"return fluentjQuery(\"{element.Element.Selector}\").css(\"{propertyName}\")");
                if (propertyValue == null)
                    action(false, string.Empty);
                else
                    action(true, propertyValue.ToString());
            });
        }

        public void Dispose()
        {
            try
            {
                webDriver.Manage().Cookies.DeleteAllCookies();
                webDriver.Quit();
                webDriver.Dispose();
            }
            catch { }
        }
    }
}
