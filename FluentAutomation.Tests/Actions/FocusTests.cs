using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class FocusTests : BaseTest
    {
        public FocusTests()
            : base()
        {
            InputsPage.Go();
        }

        /// <summary>
        /// Attempt to focus a simple block element, in this case an H1
        /// </summary>
        [Fact]
        public void FocusBlockElement()
        {
            TextPage.Go();

            I.Assert.Css("color", InputsPage.FocusColor).Not.On(TextPage.TitleSelector);

            // This shouldn't actually do anything. Focusing a block element makes no sense.
            I.Focus(TextPage.TitleSelector)
             .Assert.Css("color", InputsPage.FocusColor).Not.On(TextPage.TitleSelector);
        }

        [Fact]
        public void FocusLink()
        {
            TextPage.Go();

            I.Assert.Css("color", InputsPage.FocusColor).Not.On(TextPage.Link1Selector);
            I.Focus(TextPage.Link1Selector)
             .Assert.Css("color", InputsPage.FocusColor).On(TextPage.Link1Selector);
        }

        [Fact]
        public void FocusInput()
        {
            I.Assert.Css("color", InputsPage.FocusColor).Not.On(InputsPage.TextControlSelector);
            I.Focus(InputsPage.TextControlSelector)
             .Assert.Css("color", InputsPage.FocusColor).On(InputsPage.TextControlSelector);

            I.Assert.Css("color", InputsPage.FocusColor).Not.On(InputsPage.TextareaControlSelector);
            I.Focus(InputsPage.TextareaControlSelector)
             .Assert.Css("color", InputsPage.FocusColor).On(InputsPage.TextareaControlSelector);
        }

        [Fact]
        public void FocusInputButton()
        {
            I.Assert.Css("color", InputsPage.FocusColor).Not.On(InputsPage.InputButtonControlSelector);
            I.Focus(InputsPage.InputButtonControlSelector)
             .Assert.Css("color", InputsPage.FocusColor).On(InputsPage.InputButtonControlSelector);
        }

        [Fact]
        public void FocusButton()
        {
            I.Assert.Css("color", InputsPage.FocusColor).Not.On(InputsPage.ButtonControlSelector);
            I.Focus(InputsPage.ButtonControlSelector)
             .Assert.Css("color", InputsPage.FocusColor).On(InputsPage.ButtonControlSelector);
        }

        /// <summary>
        /// Test that we can still focus elements outside of the viewport
        /// </summary>
        [Fact]
        public void FocusScroll()
        {
            ScrollingPage.Go();

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.TopRightSelector);
            I.Focus(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Focus(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Focus(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.FocusColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Focus(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.FocusColor).On(ScrollingPage.TopLeftSelector);
        }
    }
}
