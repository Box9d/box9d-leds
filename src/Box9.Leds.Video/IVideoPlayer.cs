using System;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public interface IVideoPlayer
    {
        void Load(IClientWrapper fcClient, string videoFilePath, LedLayout ledLayout);

        Task Play(CancellationToken token, int startFrame = 0);
    }
}
