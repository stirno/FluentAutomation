using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class HoverTests : BaseTest
    {
        public HoverTests()
            : base()
        {
            InputsPage.Go();
        }

        /// <summary>
        /// Hover over a simple block element, in this case an H1
        /// </summary>
        [Fact]
        public void HoverBlockElement()
        {
            TextPage.Go();

            I.Assert.Css("color", InputsPage.HoverColor).Not.On(TextPage.TitleSelector);
            I.Hover(TextPage.TitleSelector)
             .Assert.Css("color", InputsPage.HoverColor).On(TextPage.TitleSelector);
        }

        [Fact]
        public void HoverLink()
        {
            TextPage.Go();

            I.Assert.Css("color", InputsPage.HoverColor).Not.On(TextPage.Link1Selector);
            I.Hover(TextPage.Link1Selector)
             .Wait(3)
             .Assert.Css("color", InputsPage.HoverColor).On(TextPage.Link1Selector);
        }

        [Fact]
        public void HoverInput()
        {
            I.Assert.Css("color", InputsPage.HoverColor).Not.On(InputsPage.TextControlSelector);
            I.Hover(InputsPage.TextControlSelector)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.TextControlSelector);

            I.Assert.Css("color", InputsPage.HoverColor).Not.On(InputsPage.TextareaControlSelector);
            I.Hover(InputsPage.TextareaControlSelector)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.TextareaControlSelector);
        }

        [Fact]
        public void HoverInputButton()
        {
            I.Assert.Css("color", InputsPage.HoverColor).Not.On(InputsPage.InputButtonControlSelector);
            I.Hover(InputsPage.InputButtonControlSelector)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.InputButtonControlSelector);
        }

        [Fact]
        public void HoverButton()
        {
            I.Assert.Css("color", InputsPage.HoverColor).Not.On(InputsPage.ButtonControlSelector);
            I.Hover(InputsPage.ButtonControlSelector)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.ButtonControlSelector);
        }

        /// <summary>
        /// Test that Hover actually pulls elements into the viewport
        /// </summary>
        [Fact]
        public void HoverScroll()
        {
            ScrollingPage.Go();

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopRightSelector);
            I.Hover(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Hover(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Hover(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Hover(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopLeftSelector);
        }

        [Fact]
        public void HoverXY()
        {
            var el = I.Find(InputsPage.ButtonControlSelector);
            I.Hover(el.Element.PosX + 10, el.Element.PosY + 10)
             .Wait(3)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.ButtonControlSelector);

            I.Hover(InputsPage.InputButtonControlSelector, 10, 10)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.InputButtonControlSelector);
        }

        /// <summary>
        /// Test that Scroll is equivalent to Hover
        /// </summary>
        [Fact]
        public void Scroll()
        {
            // Identical to the first test in this.HoverXY()
            var el = I.Find(InputsPage.ButtonControlSelector);
            I.Scroll(el.Element.PosX + 10, el.Element.PosY + 10)
             .Assert.Css("color", InputsPage.HoverColor).On(InputsPage.ButtonControlSelector);

            ScrollingPage.Go();

            // Identical to this.HoverScroll()
            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopRightSelector);
            I.Scroll(ScrollingPage.TopRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomRightSelector);
            I.Scroll(ScrollingPage.BottomRightSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomRightSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.BottomLeftSelector);
            I.Scroll(ScrollingPage.BottomLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.BottomLeftSelector);

            I.Assert.Css("color", ScrollingPage.HoverColor).Not.On(ScrollingPage.TopLeftSelector);
            I.Scroll(ScrollingPage.TopLeftSelector)
             .Assert.Css("color", ScrollingPage.HoverColor).On(ScrollingPage.TopLeftSelector);
        }
    }
}
