using System.Net;

namespace Glimr.Plugins.ManagementApi.Client
{
    public class PluginUploadResult
    {
        public string Message { get; }

        public bool UploadWasAttempted { get; }

        public HttpStatusCode Status { get; }

        public PluginUploadResult(string message, HttpStatusCode? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                Status = statusCode.Value;
                UploadWasAttempted = true;
            }
            else
            {
                UploadWasAttempted = false;
            }

            Message = message;
        }

        public override string ToString()
        {
            return UploadWasAttempted ? string.Format("Status: {0}, Message: {1}", Status, Message) : Message;
        }
    }
}
