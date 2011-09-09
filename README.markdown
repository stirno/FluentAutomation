#Fluent Automation API

###New binaries available at [builds.stirno.com](http://builds.stirno.com/fluentautomation/).

The goal of this project is to create a simplified API to automate testing of web applications using WatiN or Selenium to drive browser interaction.

###Quick examples of using the API

Apologies for the lack of linebreaks. 

KnockoutJS.com - Cart Editor Example

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

Google.com - Simple Search

	I.Open("http://www.google.com");
	I.Enter("knockoutjs").In("#lst-ib");

YUI - Drag and Drop Example

	I.Open("http://developer.yahoo.com/yui/examples/dragdrop/dd-groups.html");
	I.Drag("#pt1").To("#t2");
	I.Drag("#pt2").To("#t1");
	I.Drag("#pb1").To("#b1");
	I.Drag("#pb2").To("#b2");
	I.Drag("#pboth1").To("#b3");
	I.Drag("#pboth2").To("#b4");
	I.Drag("#pt1").To("#pt2");
	I.Drag("#pboth1").To("#pb2");