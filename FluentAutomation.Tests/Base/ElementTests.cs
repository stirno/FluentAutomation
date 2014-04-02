using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class ElementTests : BaseTest
    {
        public ElementTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void DetectTypeTests()
        {
            Assert.True(I.Find(InputsPage.TextControlSelector).Element.IsText);
            Assert.True(I.Find(InputsPage.SelectControlSelector).Element.IsSelect);
            Assert.True(I.Find(InputsPage.MultiSelectControlSelector).Element.IsMultipleSelect);
        }

        [Fact]
        public void AttributeTests()
        {
            Assert.True(I.Find(InputsPage.TextControlSelector).Element.Attributes.Get("id") == InputsPage.TextControlSelector.Substring(1));
        }

        [Fact]
        public void ElementSelectTests()
        {
            var selectElement = I.Find(InputsPage.MultiSelectControlSelector);

            I.Select("Manitoba").From(selectElement);

            Assert.True(selectElement.Element.SelectedOptionTextCollection.Count(x => x == "Manitoba") == 1);
            Assert.True(selectElement.Element.SelectedOptionValues.Count(x => x == "MB") == 1);
        }

        [Fact]
        public void ElementWidthHeightTests()
        {
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.Width > 0);
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.Height > 0);
        }

        [Fact]
        public void ElementPositionTests()
        {
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.PosX > 0);
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.PosY > 0);
        }

        [Fact]
        public void ElementOtherParamsTests()
        {
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.Selector == InputsPage.InputButtonControlSelector);
            Assert.True(I.Find(InputsPage.InputButtonControlSelector).Element.TagName == "input");
        }

        [Fact]
        public void ElementTextValueTests()
        {
            I.Enter("Valid Text").In(InputsPage.TextControlSelector);
            Assert.True(I.Find(InputsPage.TextControlSelector).Element.Text == "Valid Text");
            Assert.True(I.Find(InputsPage.TextControlSelector).Element.Value == "Valid Text");
        }
    }
}
