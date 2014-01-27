using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using FluentAutomation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAutomation.Tests
{
    public class BingSearchPage : PageObject<BingSearchPage>
    {
        public BingSearchPage(FluentTest test)
            : base(test)
        {
            Url = "http://bing.com/";
            At = () => I.Expect.Exists(SearchInput);
        }

        public BingSearchResultsPage Search(string searchText)
        {
            I.Enter(searchText).In(SearchInput);
            I.Press("{ENTER}");
            return this.Switch<BingSearchResultsPage>();
        }

        private const string SearchInput = "input[title='Enter your search term']";
    }

    public class BingSearchResultsPage : PageObject<BingSearchResultsPage>
    {
        public BingSearchResultsPage(FluentTest test)
            : base(test)
        {
            At = () => I.Expect.Exists(SearchResultsContainer);
        }

        public BingSearchResultsPage FindResultUrl(string url)
        {
            I.Expect.Exists(string.Format(ResultUrlLink, url));
            return this;
        }

        private const string SearchResultsContainer = "#b_results";
        private const string ResultUrlLink = "a[href='{0}']";
    }

    [TestClass]
    public class SampleTest : FluentTest
    {
        public SampleTest()
        {
            FluentAutomation.Settings.DefaultWaitUntilTimeout = TimeSpan.FromMilliseconds(1500);
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [TestMethod]
        public void SearchForFluentAutomation()
        {
            new BingSearchPage(this)
                .Go()
                .Search("FluentAutomation")
                .FindResultUrl("http://fluent.stirno.com/blog/FluentAutomation-scriptcs/");
        }
    }
}
