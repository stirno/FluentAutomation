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
using FluentAutomation.API;
using FluentAutomation.Server;

namespace FluentAutomation.RemoteCommands
{
    public class ServiceEndpoint : ICloudService
    {
        public ServiceResponse RunTest(Stream requestBody)
        {
            StreamReader reader = new StreamReader(requestBody);
            var contents = reader.ReadToEnd();

            var testSettings = JsonConvert.DeserializeObject<RemoteTestRunDetails>(contents, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            RemoteCommandManager processor = new RemoteCommandManager();

            try
            {
                var testDetails = RemoteCommandManager.GetRemoteCommands(testSettings);

                if (testDetails.ServiceModeEnabled)
                {
                    TestExecutionManager manager = new TestExecutionManager(testDetails);
                    manager.Execute();
                }
                else
                {
                    Messenger.Default.Send<GenericMessage<TestDetails>>(new GenericMessage<TestDetails>(testDetails));
                }
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
