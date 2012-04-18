using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;

namespace FluentAutomation
{
    public class SeleniumWebDriver
    {
        public enum Browser
        {
            InternetExplorer = 1,
            Firefox = 2,
            Chrome = 4
        }

        public static Browser SelectedBrowser;

        public static void Bootstrap()
        {
            Bootstrap(Browser.InternetExplorer);
        }

        public static void Bootstrap(Browser browser)
        {
            SeleniumWebDriver.SelectedBrowser = browser;

            FluentAutomation.Settings.Registration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IExpectProvider, ExpectProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            
                switch (SeleniumWebDriver.SelectedBrowser)
                {
                    case Browser.InternetExplorer:
                        container.Register<IWebDriver, OpenQA.Selenium.IE.InternetExplorerDriver>().AsMultiInstance();
                        break;
                    case Browser.Firefox:
                        container.Register<IWebDriver, OpenQA.Selenium.Firefox.FirefoxDriver>().AsMultiInstance();
                        break;
                    case Browser.Chrome:
                        container.Register<IWebDriver, OpenQA.Selenium.Chrome.ChromeDriver>().AsMultiInstance();
                        break;
                }
            };
        }
    }
}
