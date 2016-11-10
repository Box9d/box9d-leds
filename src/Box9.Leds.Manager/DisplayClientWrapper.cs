using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.Messages;
using Box9.Leds.FcClient.Messages.UpdatePixels;
using Box9.Leds.Manager.Forms;

namespace Box9.Leds.Manager
{
    public class DisplayClientWrapper : IClientWrapper
    {
        private readonly VideoForm form;

        public WebSocketState State
        {
            get
            {
                return WebSocketState.Open;
            }
        }

        public bool IsConnected
        {
            get
            {
                return true;
            }
        }

        public bool AttemptingToConnect
        {
            get
            {
                return false;
            }
        }

        public string Host
        {
            get
            {
                return "Display Client";
            }
        }

        public DisplayClientWrapper(VideoForm form)
        {
            this.form = form;
        }

        public void Connect(CancellationToken? cancellationToken = null)
        {
        }

        public void Dispose()
        {
            return;
        }

        public TResponse SendMessage<TResponse>(IJsonRequest<TResponse> request) where TResponse : new()
        {
            throw new NotImplementedException("Unable to send message to a display client");
        }

        public void SendPixelUpdates(UpdatePixelsRequest request, CancellationToken cancelToken = default(CancellationToken))
        {
        }

        public void SendBitmap(Bitmap bitmap)
        {
            try
            {
                var clone = bitmap.GetThumbnailImage(0, 0, null, IntPtr.Zero);

                form.Invoke(new Action(() =>
                {
                    form.DisplayPanel.BackgroundImage = clone;
                    form.DisplayPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }));
            }
            catch
            {
            } 
        }

        public void CloseAsync()
        {
        }
    }
}
