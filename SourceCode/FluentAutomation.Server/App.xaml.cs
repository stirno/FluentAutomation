using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace FluentAutomation.Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
            
            var serviceHost = new System.ServiceModel.Web.WebServiceHost(typeof(FluentAutomation.RemoteCommands.ServiceEndpoint), new Uri("http://localhost:10001/"));
            serviceHost.Open();
        }
    }
}
