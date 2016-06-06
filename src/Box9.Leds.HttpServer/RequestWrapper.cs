using System;
using NHttp;

namespace Box9.Leds.HttpServer
{
    public class RequestWrapper
    {
        private readonly HttpRequest request;

        public RequestWrapper(HttpRequest request)
        {
            this.request = request;
        }

        public void HandleRequest(Action<HttpRequest> action)
        {
            lock (action)
            {
                action(request);
            }
        }
    }
}
