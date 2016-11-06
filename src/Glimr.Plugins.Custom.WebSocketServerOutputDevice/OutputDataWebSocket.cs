using System.Threading.Tasks;
using Glimr.Plugins.Sdk.Plotting;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Glimr.Plugins.Custom.WebSocketServerOutputDevice
{
    public class OutputData : WebSocketBehavior
    {
        private PointsCollection pointsCollection;

        internal void SetPointsCollection(PointsCollection pointsCollection)
        {
            lock(pointsCollection)
            {
                this.pointsCollection = pointsCollection;
            }
        }

        protected override Task OnMessage(MessageEventArgs e)
        {
            lock(pointsCollection)
            {
                if (pointsCollection != null)
                {
                    Send(pointsCollection.ToByteArray());
                }
            }

            return base.OnMessage(e);
        }
    }
}
