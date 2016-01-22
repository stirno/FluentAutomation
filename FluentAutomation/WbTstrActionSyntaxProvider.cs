using System;
using System.Linq.Expressions;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrActionSyntaxProvider : IActionSyntaxProvider, IWithConfig
    {
        private readonly ActionSyntaxProvider _actionSyntaxProvider;
        private readonly ILogger _logger;
        private IAssertSyntaxProvider _expect;
        private IAssertSyntaxProvider _assert;
        private ICheckSyntaxProvider _check;

        public WbTstrActionSyntaxProvider(ActionSyntaxProvider actionSyntaxProvider, ILogger logger)
        {
            _actionSyntaxProvider = actionSyntaxProvider;
            _logger = logger;

            _logger.LogMessage("Created WbTstr-Interface");
        }
      
        /*-------------------------------------------------------------------*/

        public ICheckSyntaxProvider Check
        {
            get
            {
                _logger.LogPartialMessage("I check ");
                return _check ?? (_check = new WbTstrCheckSyntaxProvider(_actionSyntaxProvider.Assert, _logger));
            }
        }

        public IAssertSyntaxProvider Expect
        {
            get
            {
                _logger.LogPartialMessage("I expect "); 
                return _expect ?? (_expect = new WbTstrAssertSyntaxProvider(_actionSyntaxProvider.Expect, _logger));
            }
        }

        public IAssertSyntaxProvider Assert
        {
            get
            {
                _logger.LogPartialMessage("I assert "); 
                return _assert ?? (_assert = new WbTstrAssertSyntaxProvider(_actionSyntaxProvider.Assert, _logger));
            }
        }

        public ISwitchSyntaxProvider Switch
        {
            get
            {
                _logger.LogPartialMessage("I switch "); 
                return _actionSyntaxProvider.Switch;
            }
        }

        public bool IsInDryRunMode
        {
            get
            {
                return FluentSettings.Current.IsDryRun;
            }
        }

        internal ICommandProvider CommandProvider
        {
            get
            {
                return _actionSyntaxProvider.commandProvider;
            }
        }

        /*-------------------------------------------------------------------*/

        public void Dispose()
        {
            _actionSyntaxProvider.Dispose();

            _logger.LogMessage("Disposed WbTstr-Interface");
        }

        public bool IsDisposed()
        {
            return _actionSyntaxProvider.IsDisposed();
        }

        /*-------------------------------------------------------------------*/

        /* CLICK ACTIONS */

        public IActionSyntaxProvider Click(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Click element with selector: " + element.Element.Selector); 
            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(element);
            }
            
            // After
            return this;
        }

        public IActionSyntaxProvider Click(ElementProxy element, int x, int y)
        {
            // Before
            _logger.LogMessage("Click element with selector: " + element.Element.Selector + " postion x: " + x + " position y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(element, x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Click(Alert accessor)
        {
            // Before
            _logger.LogMessage("Click: " + accessor.Field);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(accessor);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Click(int x, int y)
        {
            // Before
            _logger.LogMessage("Click on postion x: " + x + " y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Click(string selector)
        {
            // Before
            _logger.LogMessage("Click element with selector: " + selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Click(string selector, int x, int y)
        {
            // Before
            _logger.LogMessage("Click element with selector: " + selector + " postion x: " + x + " position y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Click(selector, x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Double click element with selector: " + element.Element.Selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.DoubleClick(element);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider DoubleClick(int x, int y)
        {
            // Before
            _logger.LogMessage("Double click on postion x: " + x + " y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.DoubleClick(x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element, int x, int y)
        {
            // Before
            _logger.LogMessage("Double click element with selector: " + element.Element.Selector + " postion x: " + x + " position y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.DoubleClick(element, x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector)
        {
            // Before
            _logger.LogMessage("Double click element with selector: " + selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.DoubleClick(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector, int x, int y)
        {
            // Before
            _logger.LogMessage("Double click element with selector: " + selector + " postion x: " + x + " position y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.DoubleClick(selector, x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider RightClick(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Rightclick element with selector: " + element.Element.Selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.RightClick(element);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider RightClick(int x, int y)
        {
            // Before
            _logger.LogMessage("Right click on postion x: " + x + " y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.RightClick(x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector)
        {
            // Before
            _logger.LogMessage("Rightclick element with selector: " + selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.RightClick(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector, int x, int y)
        {
            // Before
            _logger.LogMessage("Right click element with selector: " + selector + " postion x: " + x + " position y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.RightClick(selector, x, y);
            }

            // After
            return this;
        }

        /* DRAG ACTIONS */

        public IDragDropSyntaxProvider Drag(string selector)
        {
            // Before
            _logger.LogPartialMessage("Drag: " + selector); 

            // Execute
            IDragDropSyntaxProvider dragDropSyntaxProvider = Drag(Find(selector));

            // After
            return dragDropSyntaxProvider;
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element)
        {
            // Before
            _logger.LogPartialMessage("Drag: " + element.Element.Selector); 

            // Execute
            IDragDropSyntaxProvider dragDropSyntaxProvider = _actionSyntaxProvider.Drag(element);

            // After
            return new WbTstrDragDropSyntaxProvider(this, dragDropSyntaxProvider, _logger);
        }

        public IDragDropSyntaxProvider Drag(string selector, int offsetX, int offsetY)
        {
            // Before
            _logger.LogMessage("Drag element with selector: " + selector + " offset x: " + offsetX + " offset y: " + offsetY); 

            // Execute
            IDragDropSyntaxProvider dragDropSyntaxProvider = Drag(Find(selector), offsetX, offsetY);

            // After
            return dragDropSyntaxProvider;
        }

        public IDragDropSyntaxProvider Drag(ElementProxy element, int offsetX, int offsetY)
        {
            // Before
            _logger.LogMessage("Drag element with selector: " + element.Element.Selector + " offset x: " + offsetX + " offset y: " + offsetY); 

            // Execute
            IDragDropSyntaxProvider dragDropSyntaxProvider = _actionSyntaxProvider.Drag(element, offsetX, offsetY);

            // After
            return new WbTstrDragDropSyntaxProvider(this, dragDropSyntaxProvider, _logger);
        }

        public IDragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY)
        {
            // Before
            _logger.LogMessage("Drag postion from position x: " + sourceX + " y: " + sourceY); 

            // Execute
            IDragDropByPositionSyntaxProvider dragDropByPositionSyntaxProvider = _actionSyntaxProvider.Drag(sourceX, sourceY);

            // After
            return new WbTstrDragDropByPositionSyntaxProvider(this, dragDropByPositionSyntaxProvider, _logger);
        }

        /* FIND ACTIONS */

        public ElementProxy Find(string selector)
        {
            // Before
            _logger.LogMessage("Find element with selector: " + selector); 

            // Execute
            ElementProxy elementProxy;
            if (!IsInDryRunMode)
            {
                elementProxy = _actionSyntaxProvider.Find(selector);
            }
            else
            {
                elementProxy = new ElementProxy(CommandProvider, () => new WbTstrElement(selector));
            }

            // After
            return elementProxy;
        }

        public ElementProxy FindMultiple(string selector)
        {
            // Before
            _logger.LogMessage("Find multiple elements with selector: " + selector); 

            // Execute
            ElementProxy elementProxy;
            if (!IsInDryRunMode)
            {
                elementProxy = _actionSyntaxProvider.FindMultiple(selector);
            }
            else
            {
                elementProxy = new ElementProxy(CommandProvider, () => new WbTstrElement(selector));
            }

            // After
            return elementProxy;
        }

        /* FOCUS/HOVER ACTIONS */

        public IActionSyntaxProvider Focus(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Focus on element with selector: " + element.Element.Selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Focus(element);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Focus(string selector)
        {
            // Before
            _logger.LogMessage("Focus on element with selector: " + selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Focus(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Hover(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Hover over element with selector: " + element.Element.Selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Hover(element);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Hover(int x, int y)
        {
            // Before
            _logger.LogMessage("Hover over position x: " + x + "y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Hover(x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Hover(ElementProxy element, int x, int y)
        {
            // Before
            _logger.LogMessage("Hover over element with selector: " + element.Element.Selector + " position x: " + x + "y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Hover(element, x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Hover(string selector)
        {
            // Before
            _logger.LogMessage("Hover over element with selector: " + selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Hover(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Hover(string selector, int x, int y)
        {
            // Before
            _logger.LogMessage("Hover over element with selector: " + selector + " position x: " + x + "y: " + y);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Hover(selector, x, y);
            }

            // After
            return this;
        }

        /* UPLOAD ACTIONS */

        public IActionSyntaxProvider Upload(string selector, string fileName)
        {
            // Before
            _logger.LogMessage("Upload file :" + fileName + " into element with selector: " + selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Upload(selector, fileName);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Upload(ElementProxy element, string fileName)
        {
            // Before
            _logger.LogMessage("Upload file :" + fileName + " into element with selector: " + element.Element.Selector);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Upload(element, fileName);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Upload(ElementProxy element, int x, int y, string fileName)
        {
            // Before
            _logger.LogMessage("Upload file :" + fileName + " into element with selector: " + element.Element.Selector + " position x: " + x + "y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Upload(element, x, y, fileName);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Upload(string selector, int x, int y, string fileName)
        {
            // Before
            _logger.LogMessage("Upload file :" + fileName + " into element with position x: " + x + "y: " + y); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Upload(selector, x, y, fileName);
            }

            // After
            return this;
        }

        /* WAIT ACTIONS */

        public IActionSyntaxProvider Wait()
        {
            // Before
            _logger.LogMessage("Wait for default time"); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Wait();
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Wait(int seconds)
        {
            // Before
            _logger.LogMessage("Wait for " + seconds + " seconds");

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Wait(seconds);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            // Before
            _logger.LogMessage("Wait for " + timeSpan); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Wait(timeSpan);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            // Before
            _logger.LogMessage("Wait until condition is met");

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionAction);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            // Before
            _logger.LogMessage("Wait until condition is met, with timeout " + timeout); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionAction, timeout);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int seconds)
        {
            // Before
            _logger.LogMessage("Wait until condition is met, with timeout " + seconds + " seconds");

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionAction, seconds);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            // Before
            _logger.LogMessage("Wait until condition is met");

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionFunc);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int seconds)
        {
            // Before
            _logger.LogMessage("Wait until condition is met, with timeout " + seconds + " seconds"); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionFunc, seconds);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            // Before
            _logger.LogMessage("Wait until condition is met, with timeout " + timeout); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.WaitUntil(conditionFunc, timeout);
            }

            // After
            return this;
        }

        /* KEY ACTIONS */

        public ITextEntrySyntaxProvider Enter(dynamic nonString)
        {
            // Before
            _logger.LogMessage("Enter text: " + nonString);

            // Execute
            ITextEntrySyntaxProvider textEntrySyntaxProvider = _actionSyntaxProvider.Enter(nonString);

            // After
            return new WbTstrTextEntrySyntaxProvider(this, textEntrySyntaxProvider, _logger);
        }

        public ITextEntrySyntaxProvider Enter(string text)
        {
            // Before
            _logger.LogMessage("Enter text: " + text);

            // Execute
            ITextEntrySyntaxProvider textEntrySyntaxProvider = _actionSyntaxProvider.Enter(text);

            // After
            return new WbTstrTextEntrySyntaxProvider(this, textEntrySyntaxProvider, _logger);
        }

        public ITextAppendSyntaxProvider Append(dynamic nonString)
        {
            // Before
            _logger.LogMessage("Append text: " + nonString ); 

            // Execute
            ITextAppendSyntaxProvider textAppendSyntaxProvider = _actionSyntaxProvider.Append(nonString);

            // After
            return new WbTstrTextAppendSyntaxProvider(this, textAppendSyntaxProvider, _logger);
        }

        public ITextAppendSyntaxProvider Append(string text)
        {
            // Before
            _logger.LogMessage("Append text: " + text); 

            // Execute
            ITextAppendSyntaxProvider textAppendSyntaxProvider = _actionSyntaxProvider.Append(text);

            // After
            return new WbTstrTextAppendSyntaxProvider(this, textAppendSyntaxProvider, _logger);
        }

        public IActionSyntaxProvider Press(string keys)
        {
            // Before
            _logger.LogMessage("Press keys: {0}", keys);

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Press(keys);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Type(string text)
        {
            // Before
            _logger.LogMessage("Type text: " + text); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Type(text);
            }

            // After
            return this;
        }

        /* NAVIGATION ACTIONS */

        public IActionSyntaxProvider Scroll(int x, int y)
        {
            // Before
            _logger.LogMessage("Scroll to postion x: " + x + " y: " + y );

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Scroll(x, y);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Scroll(string selector)
        {
            // Before
            _logger.LogMessage("Scroll to element with selector: " + selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Scroll(selector);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Scroll(ElementProxy element)
        {
            // Before
            _logger.LogMessage("Scroll to element with selector: " + element.Element.Selector); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Scroll(element);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Open(string url)
        {
            // Before
            _logger.LogMessage("Open url: " + url); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Open(url);
            }

            // After
            return this;
        }

        public IActionSyntaxProvider Open(Uri uri)
        {
            // Before
            _logger.LogMessage("Open url: " + uri); 

            // Execute
            if (!IsInDryRunMode)
            {
                _actionSyntaxProvider.Open(uri);
            }

            // After
            return this;
        }

        /* SELECT ACTIONS */

        public ISelectSyntaxProvider Select(Option mode, string value)
        {
            // Before
            _logger.LogMessage("Select value : " + value); 

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(mode, value);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        public ISelectSyntaxProvider Select(Option mode, params string[] values)
        {
            // Before
            _logger.LogMessage("Select values : " + values); 

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(mode, values);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        public ISelectSyntaxProvider Select(params int[] indices)
        {
            // Before
            _logger.LogMessage("Select indices " + indices); 

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(indices);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        public ISelectSyntaxProvider Select(params string[] text)
        {
            // Before
            _logger.LogMessage("Select parameters: " + text);

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(text);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        public ISelectSyntaxProvider Select(int index)
        {
            // Before
            _logger.LogMessage("Select index :" + index);

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(index);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        public ISelectSyntaxProvider Select(string text)
        {
            // Before
            _logger.LogMessage("Select text: " + text);

            // Execute
            ISelectSyntaxProvider selectSyntaxProvider = _actionSyntaxProvider.Select(text);

            // After
            return new WbTstrSelectSyntaxProvider(this, selectSyntaxProvider, _logger);
        }

        /* SCREENSHOT ACTIONS */

        public IActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            // Before
            _logger.LogMessage("Take a screenshot and save as: " + screenshotName); 

            // Execute
            _actionSyntaxProvider.TakeScreenshot(screenshotName);

            // After
            return this;
        }

        /* MISCELLENOUS ACTIONS */

        public IActionSyntaxProvider WithConfig(FluentSettings settings)
        {
           // Execute
            ((ActionSyntaxProvider)_actionSyntaxProvider).WithConfig(settings);

            // After
            return this;
        }
    }
}