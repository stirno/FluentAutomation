using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Node.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            //SeleniumWebDriver.Bootstrap(FluentAutomation.SeleniumWebDriver.Browser.Firefox);
            PhantomJS.Bootstrap();
            NodeService.Current.Start();
            System.Console.ReadLine();
            NodeService.Current.Stop();
        }
    }
}
