using FluentAutomation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class ClickTests : BaseTest
    {
        public ClickTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void LeftClick()
        {
            I.Click(InputsPage.ButtonControlSelector)
             .Assert.Text("Button Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.Click(InputsPage.InputButtonControlSelector)
             .Assert.Text("Input Button Clicked").In(InputsPage.ButtonClickedTextSelector);
        }

        [Fact]
        public void RightClick()
        {
            I.RightClick(InputsPage.ButtonControlSelector)
             .Assert.Text("Button Right Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.RightClick(InputsPage.InputButtonControlSelector)
             .Assert.Text("Input Button Right Clicked").In(InputsPage.ButtonClickedTextSelector);
        }

        [Fact]
        public void DoubleClick()
        {
            I.DoubleClick(InputsPage.ButtonControlSelector)
             .Assert.Text("Button Double Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.DoubleClick(InputsPage.InputButtonControlSelector)
             .Assert.Text("Input Button Double Clicked").In(InputsPage.ButtonClickedTextSelector);
        }

        [Fact]
        public void XYClicks()
        {
            var el = I.Find(InputsPage.ButtonControlSelector);
            I.Click(el.Element.PosX + 10, el.Element.PosY + 10)
             .Assert.Text("Button Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.DoubleClick(el.Element.PosX + 10, el.Element.PosY + 10)
             .Assert.Text("Button Double Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.RightClick(el.Element.PosX + 10, el.Element.PosY + 10)
             .Assert.Text("Button Right Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.Click(InputsPage.ButtonControlSelector, 10, 10)
             .Assert.Text("Button Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.DoubleClick(InputsPage.ButtonControlSelector, 10, 10)
             .Assert.Text("Button Double Clicked").In(InputsPage.ButtonClickedTextSelector);

            I.RightClick(InputsPage.ButtonControlSelector, 10, 10)
             .Assert.Text("Button Right Clicked").In(InputsPage.ButtonClickedTextSelector);
        }

        [Fact]
        public void AlertClicks()
        {
            AlertsPage.Go();

            // No alerts present
            Assert.Throws<OpenQA.Selenium.NoAlertPresentException>(() =>
            {
                I.Click(Alert.OK);
            });

            // Alert box:
            // Alerts don't have OK/Cancel but both work, so we test as if Cancel was clicked
            I.Click(AlertsPage.TriggerAlertSelector)
             .Click(Alert.OK)
             .Assert.Text("Clicked Alert Cancel").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerAlertSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Alert Cancel").In(AlertsPage.ResultSelector);

            // Confirmation box:
            I.Click(AlertsPage.TriggerConfirmSelector)
             .Click(Alert.OK)
             .Assert.Text("Clicked Confirm OK").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerConfirmSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Confirm Cancel").In(AlertsPage.ResultSelector);

            // Prompt box:
            I.Click(AlertsPage.TriggerPromptSelector)
             .Enter("1").In(Alert.Input)
             .Assert.Text("Clicked Prompt OK: 1").In(AlertsPage.ResultSelector);

            I.Click(AlertsPage.TriggerPromptSelector)
             .Click(Alert.Cancel)
             .Assert.Text("Clicked Prompt Cancel").In(AlertsPage.ResultSelector);
        }
    }
}
