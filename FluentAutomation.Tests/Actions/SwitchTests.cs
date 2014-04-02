using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class SwitchTests : BaseTest
    {
        public SwitchTests()
        {
            SwitchPage.Go();
        }

        [Fact]
        public void FrameSwitchTest()
        {
            I.Switch.Frame(SwitchPage.IFrameSelector)
             .Assert.Text("Alerts Testbed").In("h2");

            I.Switch.Frame()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Frame(I.Find(SwitchPage.IFrameSelector))
             .Assert.Text("Alerts Testbed").In("h2");

            Assert.Throws<FluentException>(() => I.Switch.Frame("non existent frame"));
        }

        [Fact]
        public void WindowSwitchTest()
        {
            I.Click(SwitchPage.NewWindowSelector);

            I.Switch.Window("Inputs - FluentAutomation Testbed")
             .Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");

            I.Switch.Window("/Inputs")
             .Assert.Text("Input Controls Testbed").In("h2");

            I.Switch.Window()
             .Assert.Text("Switch Testbed").In("h2");

            Assert.Throws<FluentException>(() => I.Switch.Window("non existent window"));
        }
    }
}
