using System;
using System.Net;
using Glimr.Plugins.Custom.WebSocketServerOutputDevice.Exceptions;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plotting;
using Glimr.Plugins.Sdk.Plugins;
using WebSocketSharp.Server;

namespace Glimr.Plugins.Custom.WebSocketServerOutputDevice
{
    public class WebSocketServerPlugin : IOutputDevicePlugin
    {
        private readonly IPAddress webSocketIp;
        private readonly OutputData outputData;
        private WebSocketServer webSocketServer;

        public WebSocketServerPlugin()
        {
            webSocketIp = IPAddress.Loopback;
            outputData = new OutputData();
        }

        public IPluginConfiguration Configure()
        {
            return PluginConfigurationFactory
                .NewConfiguration("Web socket server")
                .AddIntegerInput("Listening port")
                .AddStringInput("Path");
        }

        public void Dispose()
        {
            webSocketServer.Stop().Wait();
        }

        public void Run(IOutputDevicePluginContext context)
        {
            if (webSocketServer == null)
            {
                webSocketServer = new WebSocketServer(webSocketIp, context.GetInput<int>("Listening port"));
                webSocketServer.AddWebSocketService(context.GetInput<string>("Path"), () => { return outputData; });
                webSocketServer.Start();
            }

            if (!webSocketServer.IsListening)
            {
                throw new WebSocketConnectionException(webSocketIp, webSocketServer.Port);
            }

            outputData.SetPointsCollection(context.PointsCollection);
        }
    }
}
