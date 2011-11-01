using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.IO;
using Newtonsoft.Json;
using FluentAutomation.RemoteConsole;
using GalaSoft.MvvmLight.Messaging;
using FluentAutomation.Server.Model;

namespace FluentAutomation.RemoteCommands
{
    public class ServiceEndpoint : ICloudService
    {
        public ServiceResponse RunTest(Stream requestBody)
        {
            StreamReader reader = new StreamReader(requestBody);
            var contents = reader.ReadToEnd();

            var commands = JsonConvert.DeserializeObject<List<RemoteCommandDetails>>(contents, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            RemoteCommandManager processor = new RemoteCommandManager();

            try
            {
                var testDetails = RemoteCommandManager.GetRemoteCommands(commands);
                Messenger.Default.Send<GenericMessage<TestDetails>>(new GenericMessage<TestDetails>(testDetails));
                
                //processor.Execute(new FluentAutomation.SeleniumWebDriver.AutomationProvider(), commands);
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Status = "Error", Message = ex.Message };
            }

            return new ServiceResponse { Status = "Complete" };
        }
    }

    [ServiceContract]
    public interface ICloudService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        ServiceResponse RunTest(Stream requestBody);
    }
}
