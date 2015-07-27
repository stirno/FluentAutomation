using System;
using FluentAutomation.Tests.Pages;
using OpenQA.Selenium;

namespace FluentAutomation.Tests
{
	/// <summary>
	///     Base Test that opens the test to the AUT
	/// </summary>
	public class BaseTest : FluentTest<IWebDriver>
	{
		public CheckboxesPage CheckboxesPage;

		public BaseTest()
		{
			FluentSession.EnableStickySession();
			Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

			// Create Page Objects
			CheckboxesPage = new CheckboxesPage(this);

			// Default tests use chrome and load the site
			SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome); //, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);
			I.Open(SiteUrl);
		}

		public string SiteUrl
		{
			get { return "http://the-internet.herokuapp.com/"; }
		}
	}
}