using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FluentAutomation
{
    public class CommandProvider : BaseCommandProvider, ICommandProvider, IDisposable
    {
        private readonly IFileStoreProvider fileStoreProvider = null;
        private readonly Lazy<IWebDriver> lazyWebDriver = null;
        private IWebDriver webDriver
        {
            get
            {
                return lazyWebDriver.Value;
            }
        }

        public CommandProvider(Func<IWebDriver> webDriverFactory, IFileStoreProvider fileStoreProvider)
        {
            this.lazyWebDriver = new Lazy<IWebDriver>(() =>
            {
                var webDriver = webDriverFactory();
                webDriver.Manage().Cookies.DeleteAllCookies();
                return webDriver;
            });

            this.fileStoreProvider = fileStoreProvider;
        }

        public Uri Url
        {
            get
            {
                return new Uri(this.webDriver.Url, UriKind.Absolute);
            }
        }

        public void Navigate(Uri url)
        {
            this.Act(() => this.webDriver.Navigate().GoToUrl(url));
        }

        public Func<IElement> Find(string selector)
        {
            return new Func<IElement>(() =>
            {
                try
                {
                    var webElement = this.webDriver.FindElement(Sizzle.Find(selector));
                    return new Element(webElement, selector);
                }
                catch (NoSuchElementException)
                {
                    throw new FluentException("Unable to find element with selector [{0}]", selector);
                }
            });
        }

        public Func<IEnumerable<IElement>> FindMultiple(string selector)
        {
            return new Func<IEnumerable<IElement>>(() =>
            {
                try
                {
                    var webElements = this.webDriver.FindElements(Sizzle.Find(selector));
                    List<Element> resultSet = new List<Element>();
                    webElements.ToList().ForEach(x => resultSet.Add(new Element(x, selector)));
                    return resultSet;
                }
                catch (NoSuchElementException)
                {
                    throw new FluentException("Unable to find element with selector [{0}]", selector);
                }
            });
        }

        public void Click(int x, int y)
        {
            this.Act(() =>
            {
                var rootElement = this.Find("html")() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(rootElement.WebElement)
                    .MoveByOffset(x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(Func<IElement> element, int x, int y)
        {
            this.Act(() =>
            {
                var containerElement = element() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(containerElement.WebElement)
                    .MoveByOffset(x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(Func<IElement> element)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;
                unwrappedElement.WebElement.Click();
            });
        }

        public void DoubleClick(int x, int y)
        {
            this.Act(() =>
            {
                var rootElement = this.Find("html")() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(rootElement.WebElement)
                    .MoveByOffset(x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(Func<IElement> element, int x, int y)
        {
            this.Act(() =>
            {
                var containerElement = element() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(containerElement.WebElement)
                    .MoveByOffset(x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(Func<IElement> element)
        {
            this.Act(() =>
            {
                var containerElement = element() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(containerElement.WebElement)
                    .DoubleClick()
                    .Perform();
            });
        }
        
        public void RightClick(Func<IElement> element)
        {
            this.Act(() =>
            {
                var containerElement = element() as Element;
                new Actions(this.webDriver)
                    .ContextClick(containerElement.WebElement)
                    .Perform();
            });
        }

        public void Hover(int x, int y)
        {
            this.Act(() =>
            {
                var rootElement = this.Find("html")() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(rootElement.WebElement)
                    .MoveByOffset(x, y)
                    .Perform();
            });
        }

        public void Hover(Func<IElement> element, int x, int y)
        {
            this.Act(() =>
            {
                var containerElement = element() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(containerElement.WebElement)
                    .MoveByOffset(x, y)
                    .Perform();
            });
        }

        public void Hover(Func<IElement> element)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;
                new Actions(this.webDriver)
                    .MoveToElement(unwrappedElement.WebElement)
                    .Perform();
            });
        }

        public void Focus(Func<IElement> element)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                switch (unwrappedElement.WebElement.TagName)
                {
                    case "input":
                    case "select":
                    case "textarea":
                    case "a":
                    case "iframe":
                    case "button":
                        var executor = (IJavaScriptExecutor)this.webDriver;
                        executor.ExecuteScript("arguments[0].focus();", unwrappedElement.WebElement);
                        break;
                }
            });
        }

        public void DragAndDrop(Func<IElement> source, Func<IElement> target)
        {
            this.Act(() =>
            {
                var unwrappedSource = source() as Element;
                var unwrappedTarget = target() as Element;

                new Actions(this.webDriver)
                    .DragAndDrop(unwrappedSource.WebElement, unwrappedTarget.WebElement)
                    .Perform();
            });
        }

        public void EnterText(Func<IElement> element, string text)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                unwrappedElement.WebElement.Clear();
                unwrappedElement.WebElement.SendKeys(text);
            });
        }

        public void SelectText(Func<IElement> element, string optionText)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByText(optionText);
            });
        }

        public void MultiSelectValue(Func<IElement> element, string[] optionValues)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionValue in optionValues)
                {
                    selectElement.SelectByValue(optionValue);
                }
            });
        }

        public void MultiSelectIndex(Func<IElement> element, int[] optionIndices)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionIndex in optionIndices)
                {
                    selectElement.SelectByIndex(optionIndex);
                }
            });
        }

        public void MultiSelectText(Func<IElement> element, string[] optionTextCollection)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionText in optionTextCollection)
                {
                    selectElement.SelectByText(optionText);
                }
            });
        }

        public void SelectValue(Func<IElement> element, string optionValue)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByValue(optionValue);
            });
        }

        public void SelectIndex(Func<IElement> element, int optionIndex)
        {
            this.Act(() =>
            {
                var unwrappedElement = element() as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByIndex(optionIndex);
            });
        }

        public void TakeScreenshot(string screenshotName)
        {
            this.Act(() =>
            {
                // get raw screenshot
                var screenshotDriver = (ITakesScreenshot)this.webDriver;
                var tmpImagePath = Path.Combine(Settings.UserTempDirectory, screenshotName);
                screenshotDriver.GetScreenshot().SaveAsFile(tmpImagePath, ImageFormat.Png);

                // save to file store
                this.fileStoreProvider.SaveScreenshot(File.ReadAllBytes(tmpImagePath), screenshotName);
                File.Delete(tmpImagePath);
            });
        }

        public void UploadFile(Func<IElement> element, int x, int y, string fileName)
        {
            this.Act(() =>
            {
                // wait before typing in the field
                var task = Task.Factory.StartNew(() =>
                {
                    switch (SeleniumWebDriver.SelectedBrowser)
                    {
                        case SeleniumWebDriver.Browser.Firefox:
                            this.Wait(TimeSpan.FromMilliseconds(1000));
                            break;
                        case SeleniumWebDriver.Browser.Chrome:
                            this.Wait(TimeSpan.FromMilliseconds(1500));
                            break;
                    }

                    this.Type(fileName);
                });

                if (x == 0 && y == 0)
                {
                    this.Click(element);
                }
                else
                {
                    this.Click(element, x, y);
                }

                task.Wait();
                this.Wait(TimeSpan.FromMilliseconds(1500));
            });
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
            this.Act(() => System.Windows.Forms.SendKeys.SendWait(keys));
        }

        public void Type(string text)
        {
            this.Act(() =>
            {
                foreach (var character in text)
                {
                    System.Windows.Forms.SendKeys.SendWait(character.ToString());
                    this.Wait(TimeSpan.FromMilliseconds(20));
                }
            });
        }

        public void Dispose()
        {
            try
            {
                this.webDriver.Quit();
                this.webDriver.Dispose();
            }
            catch (Exception) { }
        }
    }
}
