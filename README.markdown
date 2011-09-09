#Fluent Automation API

##NuGet Packages
All FluentAutomation provider stable versions will be available via NuGet.org. Use the Package Manager to install:

####[FluentAutomation.WatiN](http://nuget.org/List/Packages/FluentAutomation.WatiN)
	PM> Install-Package FluentAutomation.WatiN

or

####[FluentAutomation.SeleniumWebDriver](http://nuget.org/List/Packages/FluentAutomation.SeleniumWebDriver)
	PM> Install-Package FluentAutomation.SeleniumWebDriver

##Latest unstable build available at [builds.stirno.com](http://builds.stirno.com/fluentautomation/)

The goal of this project is to create a simplified API to automate testing of web applications using WatiN or Selenium to drive browser interaction.

### Basic Usage

To use the API, you just need to extend the appropriate FluentTest class. Current options include SeleniumWebDriver.FluentTest and WatiN.FluentTest classes. Any test framework should work.

####MSTest examples:

    [TestClass]
    public class CartEditor : FluentAutomation.WatiN.FluentTest
    {
        [TestMethod]
        public void CartEditor_AddDelete()
        {
            /* Snipped */
        }
    }
	
or

    [TestClass]
    public class CartEditor : FluentAutomation.SeleniumWebDriver.FluentTest
    {
        [TestMethod]
        public void CartEditor_AddDelete()
        {
            /* Snipped */
        }
    }

###Quick examples of using the API

####KnockoutJS.com - Cart Editor Example

Select products, enter values, validate values.

	// specify a browser, this is optional - WatiN targets IE and Selenium defaults to Firefox
	I.Use(BrowserType.Chrome);
	I.Open("http://knockoutjs.com/examples/cartEditor.html");
	I.Select("Motorcycles").From("#cartEditor tr select:eq(0)"); // Select by value/text
	I.Select(2).From("#cartEditor tr select:eq(1)"); // Select by index
	I.Enter(6).In("#cartEditor td.quantity input:eq(0)");
	I.Expect.This("$197.70").In("#cartEditor tr span:eq(1)");
	
	// add second product
	I.Click("#cartEditor button:eq(0)");
	I.Select(1).From("#cartEditor tr select:eq(2)");
	I.Select(4).From("#cartEditor tr select:eq(3)");
	I.Enter(8).In("#cartEditor td.quantity input:eq(1)");
	I.Expect.This("$788.64").In("#cartEditor tr span:eq(3)");
	
	// validate totals
	I.Expect.This("$986.34").In("p.grandTotal span");
	
	// remove first product
	I.Click("#cartEditor a:eq(0)");
	
	// validate new total
	I.Expect.This("$788.64").In("p.grandTotal span");

####Google.com - Simple Search

Search google for Fluent Automation API.. yes we need higher PageRank!

	I.Open("http://www.google.com");
	I.Enter("Fluent Automation API").In("#lst-ib");

####YUI - Drag and Drop Example

Drag the target boxes around on the YUI Example.

	I.Open("http://developer.yahoo.com/yui/examples/dragdrop/dd-groups.html");
	I.Drag("#pt1").To("#t2");
	I.Drag("#pt2").To("#t1");
	I.Drag("#pb1").To("#b1");
	I.Drag("#pb2").To("#b2");
	I.Drag("#pboth1").To("#b3");
	I.Drag("#pboth2").To("#b4");
	I.Drag("#pt1").To("#pt2");
	I.Drag("#pboth1").To("#pb2");