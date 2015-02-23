using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class DragTests : BaseTest
    {
        public DragTests()
            : base()
        {
            DragPage.Go();
        }

        [Fact]
        public void DragAndDropBySelector()
        {
            var peg = I.Find(DragPage.Peg1).Element;
            var hole1 = I.Find(DragPage.Hole1).Element;
            var hole2 = I.Find(DragPage.Hole2).Element;

            // selector to selector
            NotOnTop(peg, hole2);
            I.Drag(DragPage.Peg1).To(DragPage.Hole2);
            OnTop(peg, hole2);

            // selector to selector + offset
            NotOnTop(peg, hole1);
            I.Drag(DragPage.Peg1).To(DragPage.Hole1 /* 10, 10 */);
            OnTop(peg, hole1);

            // selector to position - not yet implemented
        }

        [Fact]
        public void DragAndDropBySelectorOffset()
        {
            var peg = I.Find(DragPage.Peg3).Element;
            var hole3 = I.Find(DragPage.Hole3).Element;
            var hole4 = I.Find(DragPage.Hole4).Element;

            // selector + offset to selector
            NotOnTop(peg, hole4);
            I.Drag(DragPage.Peg3, 10, 10).To(DragPage.Hole4);
            OnTop(peg, hole4);

            // selector + offset to selector + offset
            NotOnTop(peg, hole3);
            I.Drag(DragPage.Peg3, 10, 10).To(DragPage.Hole3, 10, 10);
            OnTop(peg, hole3);

            // selector + offset to position - not yet implemented
        }

        [Fact]
        public void DragAndDropByPosition()
        {
            var peg = I.Find(DragPage.Peg4).Element;
            var hole5 = I.Find(DragPage.Hole5).Element;
            var hole6 = I.Find(DragPage.Hole6).Element;

            // position to position
            NotOnTop(peg, hole6);
            I.Drag(peg.PosX + 10, peg.PosY + 10).To(hole6.PosX + 10, hole6.PosY + 10);
            OnTop(peg, hole6);

            // position to selector
            NotOnTop(peg, hole5);
            I.Drag(peg.PosX + 10, peg.PosY + 10).To(DragPage.Hole5, 10, 10);
            OnTop(peg, hole5);

            // position to selector + offset
            NotOnTop(peg, hole6);
            I.Drag(peg.PosX + 10, peg.PosY + 10).To(DragPage.Hole6, 10, 10);
            OnTop(peg, hole6);
        }

        private void NotOnTop(IElement e1, IElement e2)
        {
            I.Assert.False(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }

        private void OnTop(IElement e1, IElement e2)
        {
            I.Assert.True(() => e1.PosX == e2.PosX && e1.PosY == e2.PosY);
        }
    }
}
