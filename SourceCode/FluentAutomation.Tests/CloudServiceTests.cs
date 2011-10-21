using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using FluentAutomation.RemoteCommands;
using FluentAutomation.RemoteCommands.Commands;

namespace FluentAutomation.Tests
{
    [TestClass]
    public class CloudServiceTests
    {
        /*
            I.Open("http://www.quackit.com/javascript/javascript_alert_box.cfm");
            I.Click("input[type='button']:eq(0)", ClickMode.NoWait);
            I.TakeScreenshot("Dialog.jpg");
            I.Expect.Alert("Not the message");
         */
        [TestMethod]
        public void TestProcessor()
        {
            RemoteCommandManager processor = new RemoteCommandManager();

            List<RemoteCommand> commands = new List<RemoteCommand>();
            commands.Add(new RemoteCommand()
            {
                Name = "Open",
                Arguments = new Dictionary<string, string>() {
                    { "url", "http://www.quackit.com/javascript/javascript_alert_box.cfm" }
                }
            });

            commands.Add(new RemoteCommand()
            {
                Name = "Click",
                Arguments = new Dictionary<string, string>() {
                    { "Selector",  "input[type='button']:eq(0)" },
                    { "ClickMode", "NoWait" }
                }
            });

            processor.Execute(new FluentAutomation.SeleniumWebDriver.AutomationProvider(), commands);
        }

        [TestMethod]
        public void TestEnter()
        {
            RemoteCommandManager processor = new RemoteCommandManager();

            List<RemoteCommand> commands = new List<RemoteCommand>();
            commands.Add(new RemoteCommand()
            {
                Name = "Open",
                Arguments = new Dictionary<string, string>() {
                    { "url", "http://knockoutjs.com/examples/helloWorld.html" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "Enter",
                Arguments = new Dictionary<string, string>() {
                    { "Value", "Fuck Salt" },
                    { "Selector", "input:eq(0)" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "ExpectValue",
                Arguments = new Dictionary<string, string>() {
                    { "Value", "Fuck Salt" },                    
                    { "Selector", "input:eq(0)" }
                }
            });

            processor.Execute(new FluentAutomation.SeleniumWebDriver.AutomationProvider(), commands);
        }

        [TestMethod]
        //I.Open("http://knockoutjs.com/examples/cartEditor.html");
        //I.Select("Motorcycles").From("#cartEditor tr select:eq(0)"); // Select by value/text
        //I.Select(2).From("#cartEditor tr select:eq(1)"); // Select by index
        //I.Enter(6).In("#cartEditor td.quantity input:eq(0)");
        //I.Expect.This("$197.70").In("#cartEditor tr span:eq(1)");
        public void TestKnockoutJS()
        {
            RemoteCommandManager processor = new RemoteCommandManager();
            List<RemoteCommand> commands = new List<RemoteCommand>();

            commands.Add(new RemoteCommand()
            {
                Name = "Open",
                Arguments = new Dictionary<string, string>()
                {
                    { "URL", "http://knockoutjs.com/examples/cartEditor.html" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "Select",
                Arguments = new Dictionary<string, string>()
                {
                    { "Selector", "#cartEditor tr select:eq(0)" },
                    { "Value", "Motorcycles" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "Select",
                Arguments = new Dictionary<string, string>()
                {
                    { "Selector", "#cartEditor tr select:eq(1)" },
                    { "Index", "2" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "Enter",
                Arguments = new Dictionary<string, string>()
                {
                    { "Selector", "#cartEditor td.quantity input:eq(0)" },
                    { "Value", "6" }
                }
            });
            commands.Add(new RemoteCommand()
            {
                Name = "ExpectText",
                Arguments = new Dictionary<string, string>()
                {
                    { "ValueExpression", "x.Contains(\"197.73\")" },
                    { "Selector", "#cartEditor tr span:eq(1)" }
                }
            });

            processor.Execute(new FluentAutomation.SeleniumWebDriver.AutomationProvider(), commands);
        }

        [TestMethod]
        public void TestConversion()
        {
            var dictionary = new Dictionary<string, string>() {
                { "selector",  "input[type='button']:eq(0)" },
                { "clickmode", "NoWait" },
                { "point", "10,21" }
            };

            var args = RemoteCommandManager.DeserializeArguments(typeof(ClickArguments), dictionary);
        }
    }
}
