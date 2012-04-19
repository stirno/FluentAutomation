using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using FluentAutomation.Exceptions;
using Newtonsoft.Json.Linq;
using TinyIoC;

namespace FluentAutomation.Node
{
    public class NodeService : INodeService
    {
        public readonly static NodeService Current = null;
        static NodeService()
        {
            Current = new NodeService(new WebSocketServer("ws://0.0.0.0:8000"));
        }

        private readonly Dictionary<IWebSocketConnection, TestProcessor> openClientConnections = null;
        private readonly IWebSocketServer clientWebSocket = null;
        private TestProcessor processor = null;

        public NodeService(IWebSocketServer clientWebSocket)
        {
            this.openClientConnections = new Dictionary<IWebSocketConnection, TestProcessor>();
            this.clientWebSocket = clientWebSocket;
        }
        
        public void Start()
        {
            this.OpenClientWebSocket();
        }

        private void OpenClientWebSocket()
        {
            this.clientWebSocket.Start((socket) =>
            {
                var errorHandler = new Action<Exception>((exception) =>
                {
                    JObject returnObject = new JObject();
                    returnObject.Add("ExceptionType", exception.GetType().ToString());
                    returnObject.Add("ErrorMessage", exception.Message);

                    try
                    {
                        // if the socket is still open, send the Excception back.
                        socket.Send(returnObject.ToString());
                        socket.Close();
                    }
                    catch (Exception) { }
                });

                var actionCompleteHandler = new Action(() =>
                {
                    try
                    {
                        JObject responseObject = new JObject();
                        responseObject.Add("Response", "ActionCompleted");

                        socket.Send(responseObject.ToString());
                    }
                    catch (Exception) { }
                });

                socket.OnClose = () =>
                {
                    if (openClientConnections.ContainsKey(socket))
                    {
                        openClientConnections[socket].Dispose();
                        openClientConnections.Remove(socket);
                    }
                };
                socket.OnMessage = (message) =>
                {
                    // if this is the first message from a user, register it and provide
                    // an IoC container context
                    if (!openClientConnections.ContainsKey(socket))
                    {
                        var container = new TinyIoCContainer();
                        FluentAutomation.Settings.Registration(container);

                        var testProcessor = container.Resolve<TestProcessor>();
                        testProcessor.OnError(errorHandler);
                        testProcessor.OnActionComplete(actionCompleteHandler);
                        openClientConnections.Add(socket, testProcessor);
                    }

                    openClientConnections[socket].Execute(message);
                };
            });
        }

        public void Stop()
        {
            if (this.processor != null) this.processor.Dispose();
            this.clientWebSocket.Dispose();
        }
    }
}