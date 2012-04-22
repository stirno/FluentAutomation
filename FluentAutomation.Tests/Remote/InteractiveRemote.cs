using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class InteractiveRemote : RemoteFluentTest
    {
        private static string testUrl = "http://automation.apphb.com/interactive";

        public void YUIDragDrop()
        {
            I.Open(testUrl);

            // wait for page to render properly
            I.Wait(TimeSpan.FromMilliseconds(500));
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
