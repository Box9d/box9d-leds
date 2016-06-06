using System;
using System.IO;
using System.Net;
using NHttp;

namespace Box9.Leds.HttpServer
{
    public class LightweightHttpListener : IDisposable
    {
        private readonly NHttp.HttpServer server;

        public LightweightHttpListener(int port, Action<HttpRequest> requestHandler)
        {
            server = new NHttp.HttpServer();
            server.RequestReceived += (s, e) =>
            {
                using (var writer = new StreamWriter(e.Response.OutputStream))
                {
                    new RequestWrapper(e.Request).HandleRequest(requestHandler);

                    writer.Write(HttpStatusCode.OK.ToString());
                }
            };

            server.EndPoint = new IPEndPoint(IPAddress.Loopback, port);
        }

        public void Start()
        {
            server.Start();
        }
         
        public void Stop()
        {
            server.Stop();
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }
}
