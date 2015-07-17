##Simple Testing

WbTstr.Net makes Automated Functional Testing simple. It's based on FluentAutomation which already brings an human readable way of handling your Selenium Webdriver tests. 

With WbTstr.Net we're focussing an easy integration of Automated Functional Testing within your development process. 

###Some Features
- Support for BrowserStack 
- Easy configure the browser
- Easy installation
- Quickly create new test projects
- Human readable logging of the actions


###HowTo
The easiest way to get started with WbTstr.Net is to install our Visual Studio 2013 Extension from the Visual Studio Gallery.

1. Install the Extension via Tools > Extensions and Updates> search for WbTstr.Net 
2. Simply create a new project of the type "Mirabeau WbTstr.Net"
3. Build the project to retrieve the dependencies

For more details see our [Wiki](https://github.com/mirabeau-nl/WbTstr.Net/wiki)

###Example Test
    using FluentAutomation;
    using NUnit.Framework;
    
    namespace FLuent101.Examples 
    {
      public class MyFirstTest : FluentTest
      {
        public MyFirstTest()
        {
          WbTstr.Configure()
          .PreferedBrowser().IsChrome();
          WbTstr.Bootstrap();
        }

        [TestCase]
        public void Test1()
        {
           I.Open("http://www.mirabeau.nl");
           I.Assert.Exists("H1");
        }
      }  
    }

