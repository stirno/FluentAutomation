using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.Tests.Native
{
    [TestClass]
    public class SelectExpects : FluentTest
    {
        public SelectExpects()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [TestMethod]
        public void ExpectByValue()
        {
            I.Open("http://localhost:3816/Default.aspx")
                .Select(1).From("#Province")
                .Assert
                    .Value("AB").In("#Province")
                    .Value("QC").Not.In("#Province");

            I.Select(11).From("#Province")
                .Assert
                    .Value("AB").Not.In("#Province")
                    .Value("QC").In("#Province");
        }

        [TestMethod]
        public void ExpectByLabel()
        {
            I.Open("http://localhost:3816/Default.aspx")
                .Select(1).From("#Province")
                .Assert
                    .Text("Alberta").In("#Province")
                    .Text("Qu&#233;bec").Not.In("#Province");

            I.Select(11).From("#Province")
                .Assert
                    .Text("Alberta").Not.In("#Province")
                    .Text("Québec").In("#Province");
        }
    }
}
