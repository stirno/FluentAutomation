using System;
using System.Collections.Generic;
using System.Linq;
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

        public CommandProvider(Func<WatiNCore.Browser> browserFactory)
        {
            this.lazyBrowser = new Lazy<WatiNCore.IE>(() => browserFactory() as WatiNCore.IE);
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

                    return new Element(automationElement);
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
                    automationElements.ToList().ForEach(x => resultSet.Add(new Element(x)));
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
            throw new NotImplementedException();
        }

        public void Click(Func<IElement> element, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Click(Func<IElement> element)
        {
            throw new NotImplementedException();
        }

        public void Hover(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Hover(Func<IElement> element, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Hover(Func<IElement> element)
        {
            throw new NotImplementedException();
        }

        public void Focus(Func<IElement> element)
        {
            throw new NotImplementedException();
        }

        public void DragAndDrop(Func<IElement> source, Func<IElement> target)
        {
            throw new NotImplementedException();
        }

        public void EnterText(Func<IElement> element, string text)
        {
            throw new NotImplementedException();
        }

        public void SelectText(Func<IElement> element, string optionText)
        {
            throw new NotImplementedException();
        }

        public void SelectValue(Func<IElement> element, string optionValue)
        {
            throw new NotImplementedException();
        }

        public void SelectIndex(Func<IElement> element, int optionIndex)
        {
            throw new NotImplementedException();
        }

        public void MultiSelectText(Func<IElement> element, string[] optionTextCollection)
        {
            throw new NotImplementedException();
        }

        public void MultiSelectValue(Func<IElement> element, string[] optionValues)
        {
            throw new NotImplementedException();
        }

        public void MultiSelectIndex(Func<IElement> element, int[] optionIndices)
        {
            throw new NotImplementedException();
        }

        public void TakeScreenshot(string screenshotName)
        {
            throw new NotImplementedException();
        }

        public void UploadFile(Func<IElement> element, int x, int y, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Wait()
        {
            throw new NotImplementedException();
        }

        public void Wait(int seconds)
        {
            throw new NotImplementedException();
        }

        public void Wait(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc)
        {
            throw new NotImplementedException();
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction)
        {
            throw new NotImplementedException();
        }

        public void WaitUntil(System.Linq.Expressions.Expression<Action> conditionAction, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void Press(string keys)
        {
            throw new NotImplementedException();
        }

        public void Type(string text)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Stop();
            ((SHDocVw.WebBrowser)this.browser.InternetExplorer).Quit();
            this.browser.Close();
            this.browser.Dispose();
        }
    }
}
