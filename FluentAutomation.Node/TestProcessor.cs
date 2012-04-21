using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FluentAutomation.Node
{
    public class TestProcessor : IDisposable
    {
        private readonly ICommandProvider commandProvider = null;
        private readonly IExpectProvider expectProvider = null;
        private readonly Dictionary<MethodInfo, BindingSignatureAttribute> methodSignatures = null;

        // event action handlers
        private Action<FluentException> errorHandler = null;
        private Action actionCompleteHandler = null;
        private bool isTestFailed = false;

        public TestProcessor(ICommandProvider commandProvider, IExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
            this.methodSignatures = new Dictionary<MethodInfo, BindingSignatureAttribute>();
            this.LoadSignatures();
        }

        internal void LoadSignatures()
        {
            foreach (var method in this.GetType().GetMethods())
            {
                var signature = method.GetCustomAttributes(typeof(BindingSignatureAttribute), false).FirstOrDefault();
                if (signature != null)
                {
                    this.methodSignatures.Add(method, signature as BindingSignatureAttribute);
                }
            }
        }

        public void Execute(string testMessage)
        {
            // bail out, in case of race conditions when debuggers are attached
            // where the test has already failed but the breakpoint has stopped
            // the test from closing the socket.
            if (this.isTestFailed) return;

            JArray testActions = null;
            try
            {
                testActions = JArray.Parse(testMessage);
            }
            catch (Exception ex)
            {
                this.Dispose();
                this.errorHandler(new FluentException("Unable to parse JSON provided by client. See InnerException for details.", ex));
                return;
            }

            foreach (var action in testActions)
            {
                try
                {
                    MethodInfo itemMethod = null;
                    foreach (var signature in methodSignatures)
                    {
                        var isMatch = signature.Value.IsMatch(action);
                        if (isMatch)
                        {
                            itemMethod = signature.Key;
                            break;
                        }
                    }

                    if (itemMethod == null)
                    {
                        var methodKey = action["Action"] != null ? action["Action"] : action["Expect"] != null ? action["Expect"] : "UnknownType";
                        throw new FluentException("Unable to find method to match signature for action [{0}].", methodKey.ToString());
                    }
                    itemMethod.Invoke(this, new[] { action });
                    if (this.actionCompleteHandler != null) this.actionCompleteHandler();
                }
                catch (FluentException exception)
                {
                    this.isTestFailed = true;
                    if (this.errorHandler != null) this.errorHandler(exception);
                    this.Dispose();
                    return;
                }
                catch (TargetInvocationException exception)
                {
                    if (exception.InnerException is FluentException)
                    {
                        this.errorHandler(exception.InnerException as FluentException);
                    }
                    else
                    {
                        this.errorHandler(new FluentException(exception.InnerException.Message));
                    }

                    this.Dispose();
                    return;
                }
            }
        }

        public void OnError(Action<FluentException> errorHandler)
        {
            this.errorHandler = errorHandler;
        }

        public void OnActionComplete(Action actionCompleteHandler)
        {
            this.actionCompleteHandler = actionCompleteHandler;
        }

        public void Dispose()
        {
            this.commandProvider.Dispose();
        }

        #region Remote Bindings
        public static T ToType<T>(JToken action, T type)
        {
            return JsonConvert.DeserializeAnonymousType(action.ToString(), type);
        }

        #region Action Methods

        [BindingSignature(BindingType.Action, "Navigate", "Url")]
        public void Navigate(JToken action)
        {
            var t = ToType(action, new { Url = "" });
            this.commandProvider.Navigate(new Uri(t.Url, UriKind.Absolute));
        }

        #region Click
        [BindingSignature(BindingType.Action, "Click", "Selector")]
        public void ClickSelector(JToken action)
        {
            var t = ToType(action, new { Selector = "" });
            this.commandProvider.Click(this.commandProvider.Find(t.Selector));
        }

        [BindingSignature(BindingType.Action, "Click", "X", "Y")]
        public void ClickCoords(JToken action)
        {
            var t = ToType(action, new { X = 0, Y = 0 });
            this.commandProvider.Click(t.X, t.Y);
        }

        [BindingSignature(BindingType.Action, "Click", "Selector", "X", "Y")]
        public void ClickSelectorCoords(JToken action)
        {
            var t = ToType(action, new { Selector = "", X = 0, Y = 0 });
            this.commandProvider.Click(this.commandProvider.Find(t.Selector), t.X, t.Y);
        }
        #endregion

        #region Hover
        [BindingSignature(BindingType.Action, "Hover", "Selector")]
        public void HoverSelector(JToken action)
        {
            var t = ToType(action, new { Selector = "" });
            this.commandProvider.Hover(this.commandProvider.Find(t.Selector));
        }

        [BindingSignature(BindingType.Action, "Hover", "X", "Y")]
        public void HoverCoords(JToken action)
        {
            var t = ToType(action, new { X = 0, Y = 0 });
            this.commandProvider.Hover(t.X, t.Y);
        }

        [BindingSignature(BindingType.Action, "Hover", "Selector", "X", "Y")]
        public void HoverSelectorCoords(JToken action)
        {
            var t = ToType(action, new { Selector = "", X = 0, Y = 0 });
            this.commandProvider.Hover(this.commandProvider.Find(t.Selector), t.X, t.Y);
        }
        #endregion

        #region Drag/Drop
        [BindingSignature(BindingType.Action, "DragAndDrop", "SourceSelector", "TargetSelector")]
        public void DragAndDrop(JToken action)
        {
            var t = ToType(action, new { SourceSelector = "", TargetSelector = "" });
            this.commandProvider.DragAndDrop(this.commandProvider.Find(t.SourceSelector), this.commandProvider.Find(t.TargetSelector));
        }
        #endregion

        #region <select />
        [BindingSignature(BindingType.Action, "SelectText", "Selector", "Text")]
        public void SelectText(JToken action)
        {
            var t = ToType(action, new { Selector = "", Text = "" });
            this.commandProvider.SelectText(this.commandProvider.Find(t.Selector), t.Text);
        }

        [BindingSignature(BindingType.Action, "SelectValue", "Selector", "Value")]
        public void SelectValue(JToken action)
        {
            var t = ToType(action, new { Selector = "", Value = "" });
            this.commandProvider.SelectValue(this.commandProvider.Find(t.Selector), t.Value);
        }

        [BindingSignature(BindingType.Action, "SelectIndex", "Selector", "Index")]
        public void SelectIndex(JToken action)
        {
            var t = ToType(action, new { Selector = "", Index = 0 });
            this.commandProvider.SelectIndex(this.commandProvider.Find(t.Selector), t.Index);
        }
        #endregion

        #region Wait
        [BindingSignature(BindingType.Action, "TakeScreenshot", "FileName")]
        public void TakeScreenshot(JToken action)
        {
            var t = ToType(action, new { FileName = "" });
            this.commandProvider.TakeScreenshot(t.FileName);
        }

        [BindingSignature(BindingType.Action, "Wait")]
        public void Wait(JToken action)
        {
            this.commandProvider.Wait();
        }

        [BindingSignature(BindingType.Action, "Wait", "Seconds")]
        public void WaitSeconds(JToken action)
        {
            var t = ToType(action, new { Seconds = 0 });
            this.commandProvider.Wait(t.Seconds);
        }

        [BindingSignature(BindingType.Action, "Wait", "Milliseconds")]
        public void WaitMilliseconds(JToken action)
        {
            var t = ToType(action, new { Milliseconds = 0 });
            this.commandProvider.Wait(TimeSpan.FromMilliseconds(t.Milliseconds));
        }
        #endregion

        #region Text Entry
        [BindingSignature(BindingType.Action, "EnterText", "Selector", "Text")]
        public void EnterText(JToken action)
        {
            var t = ToType(action, new { Selector = "", Text = "" });
            this.commandProvider.EnterText(this.commandProvider.Find(t.Selector), t.Text);
        }

        [BindingSignature(BindingType.Action, "Press", "Keys")]
        public void Press(JToken action)
        {
            var t = ToType(action, new { Keys = "" });
            this.commandProvider.Press(t.Keys);
        }

        [BindingSignature(BindingType.Action, "Type", "Text")]
        public void Type(JToken action)
        {
            var t = ToType(action, new { Text = "" });
            this.commandProvider.Type(t.Text);
        }
        #endregion
        #endregion

        #region Expects
        [BindingSignature(BindingType.Expect, "Text", "Selector", "Text")]
        public void ExpectText(JToken expect)
        {
            var t = ToType(expect, new { Selector = "", Text = "" });
            this.expectProvider.Text(t.Selector, t.Text);
        }

        [BindingSignature(BindingType.Expect, "Value", "Selector", "Value")]
        public void ExpectValue(JToken expect)
        {
            var t = ToType(expect, new { Selector = "", Value = "" });
            this.expectProvider.Value(t.Selector, t.Value);
        }

        [BindingSignature(BindingType.Expect, "Count", "Selector", "Count")]
        public void ExpectCount(JToken expect)
        {
            var t = ToType(expect, new { Selector = "", Count = 0 });
            this.expectProvider.Count(t.Selector, t.Count);
        }

        [BindingSignature(BindingType.Expect, "CssClass", "Selector", "CssClass")]
        public void ExpectCssClass(JToken expect)
        {
            var t = ToType(expect, new { Selector = "", CssClass = "" });
            this.expectProvider.CssClass(t.Selector, t.CssClass);
        }
        #endregion

        #endregion
    }
}
