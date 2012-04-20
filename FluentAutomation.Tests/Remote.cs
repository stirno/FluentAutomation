using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class Remote : RemoteFluentTest
    {
        public Remote()
        {
            FluentAutomation.Remote.Bootstrap();
        }

        [Fact]
        public void TestRemote()
        {
            I.Open("http://developer.yahoo.com/yui/examples/dragdrop/dd-groups.html");
            I.Drag("#pt1").To("#t2");
            I.Drag("#pt2").To("#t1");
            I.Drag("#pb1").To("#b1");
            I.Drag("#pb2").To("#b2");
            I.Drag("#pboth1").To("#b3");
            I.Drag("#pboth2").To("#b4");
            I.Drag("#pt1").To("#pt2");
            I.Drag("#pboth1").To("#pb2");
            I.Execute();
        }

        [Fact]
        public void TestPhantom()
        {
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Expect.Text("$197.70").In(".liveExample");
        }

        [Fact]
        public void TestSelect()
        {
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Expect.Text("Live example12").In("h2:eq(0)");

            //I.Select("Motorcycles").From(".liveExample tr select:eq(0)"); // Select by value/text
            //I.Select(2).From(".liveExample tr select:eq(1)"); // Select by index
            //I.Enter(6).In(".liveExample td.quantity input:eq(0)");
            //I.Expect.Text("$197.70").In(".liveExample tr span:eq(1)");

            //// add second product
            //I.Click(".liveExample button:eq(0)");
            //I.Select(1).From(".liveExample tr select:eq(2)");
            //I.Select(4).From(".liveExample tr select:eq(3)");
            //I.Enter(8).In(".liveExample td.quantity input:eq(1)");
            //I.Expect.Text("$788.64").In(".liveExample tr span:eq(3)");

            //// validate totals
            //I.Expect.Text("$986.34").In("p.grandTotal span");

            //// remove first product
            //I.Click(".liveExample a:eq(0)");

            //// validate new total
            //I.Expect.Text("$788.64").In("p.grandTotal span");
        }
    }
}
