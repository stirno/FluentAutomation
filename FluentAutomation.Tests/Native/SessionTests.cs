using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Native
{
    [TestClass]
    public class SessionTests : FluentTest
    {
        [TestMethod]
        public void Test1()
        {
            I.Open("http://localhost:1474?test1");
        }

        [TestMethod]
        public void Test2()
        {
            I.Open("http://localhost:1474?test2");
        }

        [TestMethod]
        public void Test3()
        {
            I.Open("http://localhost:1474?test3");
        }

        [TestMethod]
        public void Test4()
        {
            I.Open("http://localhost:1474?test4");
        }

        [TestMethod]
        public void Test5()
        {
            I.Open("http://localhost:1474?test5");
        }

        [TestMethod]
        public void Test6()
        {
            I.Open("http://localhost:1474?test6");
        }

        //[TestInitialize]
        //public void Setup()
        //{
        //    FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.PhantomJs);
        //}

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);
            FluentSession.EnableStickySession();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            FluentSession.DisableStickySession();
        }
    }

    //public class XunitSessionTests : FluentTest
    //{
    //    [Fact]
    //    public void Test1()
    //    {
    //        I.Open("http://localhost:1474?test1");
    //    }

    //    [Fact]
    //    public void Test2()
    //    {
    //        I.Open("http://localhost:1474?test2");
    //    }

    //    [Fact]
    //    public void Test3()
    //    {
    //        I.Open("http://localhost:1474?test3");
    //    }

    //    [Fact]
    //    public void Test4()
    //    {
    //        I.Open("http://localhost:1474?test4");
    //    }

    //    [Fact]
    //    public void Test5()
    //    {
    //        I.Open("http://localhost:1474?test5");
    //    }

    //    [Fact]
    //    public void Test6()
    //    {
    //        I.Open("http://localhost:1474?test6");
    //    }

    //    public static void Setup(TestContext context)
    //    {
    //        FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
    //        FluentSession.EnableStickySession();
    //    }

    //    public static void Cleanup()
    //    {
    //        FluentSession.DisableStickySession();
    //    }
    //}
}
