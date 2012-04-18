using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace FluentAutomation
{
    public class RemoteCommandProvider : IRemoteCommandProvider
    {
        private bool executeImmediate = true;
        private WebSocket webSocket = null;
        private readonly List<JObject> actions = new List<JObject>();

        private bool isExecutingAction = false;
        private Exception exceptionToRethrow = null;

        public RemoteCommandProvider()
        {
            this.webSocket = new WebSocket("ws://127.0.0.1:8000", "basic");
            this.webSocket.Connect();
            this.webSocket.OnMessage += (object sender, string eventdata) =>
            {
                var messageData = JObject.Parse(eventdata);
                if (messageData["ExceptionType"] != null) {
                    var exceptionType = messageData["ExceptionType"].ToString();
                    this.exceptionToRethrow = FluentExceptionFactory.Create(exceptionType, messageData["ErrorMessage"].ToString());
                }

                if (messageData["Response"] != null)
                {
                    var response = messageData["Response"].ToString();

                    if (response == "ActionCompleted")
                    {
                        this.isExecutingAction = false;
                    }
                }
            };
        }

        public void Act(dynamic data)
        {
            if (this.executeImmediate)
            {
                this.isExecutingAction = true;
                this.webSocket.Send(new JArray(JObject.FromObject(data)).ToString());

                while (this.isExecutingAction == true)
                {
                    if (this.exceptionToRethrow != null)
                    {
                        this.isExecutingAction = false;
                        this.CleanupAndThrow(this.exceptionToRethrow);
                    }
                }
            }
            else
            {
                this.actions.Add(JObject.FromObject(data));
            }
        }

        public void Navigate(Uri url)
        {
            this.Act(new { Action = "Navigate", Url = url.ToString() });
        }

        public void Click(int x, int y)
        {
            this.Act(new { Action = "Click", X = x, Y = y });
        }

        public void Click(string selector, int x, int y)
        {
            this.Act(new { Action = "Click", Selector = selector, X = x, Y = y });
        }

        public void Click(string selector)
        {
            this.Act(new { Action = "Click", Selector = selector });
        }

        public void Hover(int x, int y)
        {
            this.Act(new { Action = "Hover", X = x, Y = y });
        }

        public void Hover(string selector, int x, int y)
        {
            this.Act(new { Action = "Hover", Selector = selector, X = x, Y = y });
        }

        public void Hover(string selector)
        {
            this.Act(new { Action = "Hover", Selector = selector });
        }

        public void Focus(string selector)
        {
            this.Act(new { Action = "Focus", Selector = selector });
        }

        public void DragAndDrop(string sourceSelector, string targetSelector)
        {
            this.Act(new { Action = "DragAndDrop", SourceSelector = sourceSelector, TargetSelector = targetSelector });
        }

        public void EnterText(string selector, string text)
        {
            this.Act(new { Action = "EnterText", Selector = selector, Text = text });
        }

        public void SelectText(string selector, string optionText)
        {
            this.Act(new { Action = "SelectText", Selector = selector, Text = optionText });
        }

        public void SelectValue(string selector, string optionValue)
        {
            this.Act(new { Action = "SelectValue", Selector = selector, Value = optionValue });
        }

        public void SelectIndex(string selector, int optionIndex)
        {
            this.Act(new { Action = "SelectIndex", Selector = selector, Index = optionIndex });
        }

        public void MultiSelectText(string selector, string[] optionTextCollection)
        {
            this.Act(new { Action = "SelectText", Selector = selector, Text = optionTextCollection });
        }

        public void MultiSelectValue(string selector, string[] optionValues)
        {
            this.Act(new { Action = "SelectValue", Selector = selector, Value = optionValues });
        }

        public void MultiSelectIndex(string selector, int[] optionIndices)
        {
            this.Act(new { Action = "SelectIndex", Selector = selector, Index = optionIndices });
        }

        public void TakeScreenshot(string screenshotName)
        {
            this.Act(new { Action = "TakeScreenshot", FileName = screenshotName });
        }

        public void Wait()
        {
            this.Act(new { Action = "Wait" });
        }

        public void Wait(int seconds)
        {
            this.Act(new { Action = "Wait", Seconds = seconds });
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.Act(new { Action = "Wait", Milliseconds = timeSpan.TotalMilliseconds });
        }

        public void Press(string keys)
        {
            this.Act(new { Action = "Press", Keys = keys });
        }

        public void Type(string text)
        {
            this.Act(new { Action = "Type", Text = text });
        }

        public void Execute()
        {
            if (this.exceptionToRethrow == null && this.actions.Count > 0)
            {
                JArray contentResult = new JArray();
                this.actions.ForEach(o => contentResult.Add(o));

                this.isExecutingAction = true;
                this.webSocket.Send(contentResult.ToString());

                while (this.isExecutingAction == true)
                {
                    if (this.exceptionToRethrow != null)
                    {
                        this.isExecutingAction = false;
                        this.CleanupAndThrow(this.exceptionToRethrow);
                    }
                }
            }

            this.webSocket.Close();
        }

        public void CleanupAndThrow(Exception ex)
        {
            this.isExecutingAction = false;
            this.Dispose();

            throw ex;
        }

        public void Dispose()
        {
            this.webSocket.Close();
        }
    }
}
