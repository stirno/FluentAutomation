using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.RemoteCommands
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new System.ServiceModel.Web.WebServiceHost(typeof(ServiceEndpoint), new Uri("http://localhost:10001/"));
            serviceHost.Open();

            Console.WriteLine("Service started... press any key to stop.");
            Console.ReadKey();
        }
    }
}
