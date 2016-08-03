using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Messages;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.FcClient;

namespace Box9.Leds.Manager
{
    public class DisplayClientWrapper : IClientWrapper
    {
        private readonly Panel panel;
        private readonly ServerConfiguration serverConfiguration;

        public DisplayClientWrapper(Panel panel, ServerConfiguration serverConfiguration)
        {
            this.panel = panel;
            this.serverConfiguration = serverConfiguration;
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

        public async Task SendPixelUpdates(UpdatePixelsRequest request)
        {
            var bitmap = BitmapExtensions.CreateFromPixelInfo(request.PixelUpdates, serverConfiguration);

            panel.Invoke(new Action(() =>
            {
                panel.BackgroundImageLayout = ImageLayout.None;
                panel.BackgroundImage = bitmap;
            }));

            await Task.Yield();
        }

        public async Task CloseAsync()
        {
            await Task.Yield();
        }
    }
}
