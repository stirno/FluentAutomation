using System;
using FluentAutomation;
using Xunit;

namespace Tests
{
    public class Selenium : FluentTest
    {
        public Selenium()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Firefox);
        }

        [Fact]
        public void TestClickPosition()
        {
            I.Open("http://www.uploadify.com/demos/");
            I.Click(10, 10);
            I.Wait(1);
        }

        [Fact]
        public void TestUpload()
        {
            I.Open("http://www.uploadify.com/demos/");
            I.Upload(".demo-box:eq(0)", @"C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg");
            I.Expect.Text(x => x.Contains("File Size Error")).In("#file_uploadQueue .percentage");
        }

        [Fact]
        public void TestMethod1()
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
        }

        [Fact]
        public void TestSelect()
        {
            I.Open("http://knockoutjs.com/examples/cartEditor.html");
            I.Select("Motorcycles").From(".liveExample tr select:eq(0)");
            I.Select("1957 Vespa GS150").From(".liveExample tr select:eq(1)");
            I.Enter(6).In(".liveExample td.quantity input");
            I.Expect.Text("$197.70").In(".liveExample tr span:eq(1)");

            //I.WaitUntil(() => I.Expect.Text("$197.71").In(".liveExample tr span:eq(1)"));
            //I.Expect.Throws(() => I.Expect.Text("$197.71").In(".liveExample tr span:eq(1)"));
            //I.TakeScreenshot("test");
        }

        [Fact]
        public void CssClassExpect()
        {
            I.Open("http://knockoutjs.com/examples/controlTypes.html");
            I.Expect.Class(".nogutter").On(".syntaxhighlighter");
        }
    }
}
