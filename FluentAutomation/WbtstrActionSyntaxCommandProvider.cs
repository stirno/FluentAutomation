using System;
using System.Linq.Expressions;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbtstrActionSyntaxCommandProvider : IActionSyntaxProvider
    {
        private readonly IActionSyntaxProvider _actionSyntaxProvider;
        private bool _dryrun = false;
        public WbtstrActionSyntaxCommandProvider(ActionSyntaxProvider actionSyntaxProvider)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
        }

        private void LogInfo(string logLine)
        {
            Console.WriteLine(logLine);
        }

        private void ExecuteAction(string logLine, Expression<Action> callExpression)
        {
            var action = callExpression.Compile();
            LogInfo(logLine);
         
            if (!_dryrun)
            {
                action();
            }
        }
       
        public void Dispose()
        {
            _actionSyntaxProvider.Dispose();
        }

        public bool IsDisposed()
        {
            return _actionSyntaxProvider.IsDisposed();
        }

        public IActionSyntaxProvider Click(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(element));
            return this;
        }

        public IActionSyntaxProvider Click(ElementProxy element, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(element, x, y));
            return this;
        }

        public IActionSyntaxProvider Click(Alert accessor)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(accessor));
            return this;
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.DoubleClick(element));
            return this;
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.DoubleClick(element,x,y));
            return this;
        }

        public IActionSyntaxProvider RightClick(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.RightClick(element));
            return this;
        }


        public IDragDropSyntaxProvider Drag(string selector)
        {
            LogInfo("blala");
            if (!_dryrun)
            {
                return _actionSyntaxProvider.Drag(selector);
            }
            else
            {
                IDragDropSyntaxProvider test;
                ExecuteAction("test", () => _actionSyntaxProvider.Drag("abc"));
            }
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element)
        {
            LogInfo("blala");
            if (!_dryrun)
            { 
            return _actionSyntaxProvider.Drag(element);
            }
            else
            {
                return null;    
            }
        }

        public IDragDropSyntaxProvider Drag(string selector, int offsetX, int offsetY)
        {
            LogInfo("blala");
            if (!_dryrun)
            {
                return _actionSyntaxProvider.Drag(selector, offsetX, offsetY);
            }
            else
            {
                return null;
            }
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element, int offsetX, int offsetY)
        {
            LogInfo("blala");
            if (!_dryrun)
            {
                return _actionSyntaxProvider.Drag(element, offsetX, offsetY);
            }
            else
            {
                return null;
            }
        }

        public IDragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY)
        {
            LogInfo("blala");
            if (!_dryrun)
            {
                return _actionSyntaxProvider.Drag(sourceX, sourceY);
            }
            else
            {
                return null;
            }
        }

        public ElementProxy Find(string selector)
        {
            throw new NotImplementedException();
        }

        public ElementProxy FindMultiple(string selector)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider Focus(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Focus(element));
            return this;
        }

        public IActionSyntaxProvider Hover(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Hover(element));
            return this;
        }

        public IActionSyntaxProvider Hover(ElementProxy element, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Hover(element, x, y));
            return this;
        }

        public IActionSyntaxProvider Upload(ElementProxy element, int x, int y, string fileName)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Upload(element, x, y, fileName));
            return this;
        }

        public IActionSyntaxProvider Upload(ElementProxy element, string fileName)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Upload(element, fileName));
            return this;
        }

        public IActionSyntaxProvider Upload(string selector, int x, int y, string fileName)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Upload(selector, x, y, fileName));
            return this;
        }

        public IActionSyntaxProvider Upload(string selector, string fileName)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Upload(selector, fileName));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionAction));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionAction, timeout));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int seconds)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionAction, seconds));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionFunc));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int seconds)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionFunc, seconds));
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.WaitUntil(conditionFunc, timeout));
            return this;
        }

        public IActionSyntaxProvider Click(int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(x, y));
            return this;
        }

        public IActionSyntaxProvider Click(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(selector));
            return this;
        }

        public IActionSyntaxProvider Click(string selector, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Click(selector, x, y));
            return this;
        }

        public IActionSyntaxProvider DoubleClick(int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.DoubleClick(x, y));
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.DoubleClick(selector));
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.DoubleClick(selector,x ,y));
            return this;
        }

        public IActionSyntaxProvider RightClick(int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.RightClick(x, y));
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.RightClick(selector));
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.RightClick(selector, x, y));
            return this;
        }

      
        public ITextEntrySyntaxProvider Enter(dynamic nonString)
        {
            throw new NotImplementedException();
        }

        public ITextEntrySyntaxProvider Enter(string text)
        {
            throw new NotImplementedException();
        }

        public ITextAppendSyntaxProvider Append(dynamic nonString)
        {
            throw new NotImplementedException();
        }

        public ITextAppendSyntaxProvider Append(string text)
        {
            throw new NotImplementedException();
        }


        public IActionSyntaxProvider Focus(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Focus(selector));
            return this;
        }

        public IActionSyntaxProvider Hover(int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Hover(x, y));
            return this;
        }

        public IActionSyntaxProvider Hover(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Hover(selector));
            return this;
        }

        public IActionSyntaxProvider Hover(string selector, int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Hover(selector,x ,y));
            return this;
        }

        public IActionSyntaxProvider Scroll(int x, int y)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Scroll(x, y));
            return this;
        }

        public IActionSyntaxProvider Scroll(string selector)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Scroll(selector));
            return this;
        }

        public IActionSyntaxProvider Scroll(ElementProxy element)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Scroll(element));
            return this;
        }

        public IActionSyntaxProvider Open(string url)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Open(url));
            return this;
        }

        public IActionSyntaxProvider Open(Uri uri)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Open(uri));
            return this;
        }

        public IActionSyntaxProvider Press(string keys)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Press(keys));
            return this;
        }

        public ISelectSyntaxProvider Select(Option mode, params string[] values)
        {
            throw new NotImplementedException();
        }

        public ISelectSyntaxProvider Select(Option mode, string value)
        {
            throw new NotImplementedException();
        }

        public ISelectSyntaxProvider Select(params int[] indices)
        {
            throw new NotImplementedException();
        }

        public ISelectSyntaxProvider Select(params string[] text)
        {
            throw new NotImplementedException();
        }

        public ISelectSyntaxProvider Select(int index)
        {
            throw new NotImplementedException();
        }

        public ISelectSyntaxProvider Select(string text)
        {
            throw new NotImplementedException();
        }


        public IActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.TakeScreenshot(screenshotName));
            return this;
        }

        public IActionSyntaxProvider Type(string text)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Type(text));
            return this;
        }

        public IActionSyntaxProvider Wait()
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Wait());
            return this;
        }

        public IActionSyntaxProvider Wait(int seconds)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Wait(seconds));
            return this;
        }

        public IActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            const string logLine = "blablabla";
            ExecuteAction(logLine, () => _actionSyntaxProvider.Wait(timeSpan));
            return this;
        }


        //TODO
        #region AssertSyntaxProvider

        private AssertSyntaxProvider expect = null;
        public AssertSyntaxProvider Expect
        {
            get
            {
                return _actionSyntaxProvider.Expect;
            }
            
            
        }
        private AssertSyntaxProvider assert = null;
        public AssertSyntaxProvider Assert
        {
            get
            {
                return _actionSyntaxProvider.Assert;
            }


        }

        

        #endregion 
        //TODO
        #region Switch
        
        public ISwitchSyntaxProvider Switch
        {
            get
            {
                return _actionSyntaxProvider.Switch;
            }
        }
        #endregion
    }
}
