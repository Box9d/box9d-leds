using System;
using System.Drawing;
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

        public DisplayClientWrapper(VideoForm form)
        {
            this.form = form;
        }

        public async Task ConnectAsync()
        {
            await Task.Yield();
        }

        public void Dispose()
        {
            return;
        }

        public async Task<TResponse> SendMessage<TResponse>(IJsonRequest<TResponse> request) where TResponse : new()
        {
            await Task.Yield();

            throw new NotImplementedException("Unable to send message to a display client");
        }

        public async Task SendPixelUpdates(UpdatePixelsRequest request, CancellationToken cancelToken = default(CancellationToken))
        {
            await Task.Yield();
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

        public async Task CloseAsync()
        {
            await Task.Yield();
        }
    }
}
