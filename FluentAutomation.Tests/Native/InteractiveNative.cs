using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class InteractiveNative : FluentTest
    {
        private static string testUrl = "http://automation.apphb.com/interactive";

        public void MouseClickOperations()
        {
            I.Open(testUrl);

            // single click should add btn-primary
            I.Click("#button-form button:eq(0)");
            I.Expect.Class("btn-primary").On("#button-form button:eq(0)");

            // clicking again should remove it
            I.Click("#button-form button:eq(0)");
            I.Expect.Throws(() => I.Expect.Class("btn-primary").On("#button-form button:eq(0)"));

            // double click should add btn-primary
            I.DoubleClick("#button-form button:eq(1)");
            I.Expect.Class("btn-primary").On("#button-form button:eq(1)");

            // double clicking again should remove it
            I.DoubleClick("#button-form button:eq(1)");
            I.Expect.Throws(() => I.Expect.Class("btn-primary").On("#button-form button:eq(1)"));

            // right click should add btn-primary
            I.RightClick("#button-form button:eq(2)");
            I.Expect.Class("btn-primary").On("#button-form button:eq(2)");

            // right clicking again should remove it
            I.RightClick("#button-form button:eq(2)");
            I.Expect.Throws(() => I.Expect.Class("btn-primary").On("#button-form button:eq(2)"));
        }

        public void YUIDragDrop()
        {
            I.Open(testUrl);

            // wait for page to render properly
            I.WaitUntil(() => I.Expect.Count(1).Of("#pt1"));
            I.Drag("#pt1").To("#t2");
            I.Drag("#pt2").To("#t1");
            I.Drag("#pb1").To("#b1");
            I.Drag("#pb2").To("#b2");
            I.Drag("#pboth1").To("#b3");
            I.Drag("#pboth2").To("#b4");
            I.Drag("#pt1").To("#pt2");
            I.Drag("#pboth1").To("#pb2");
        }
    }
}
