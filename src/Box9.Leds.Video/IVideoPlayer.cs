using System;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public interface IVideoPlayer
    {
        void Load(IClientWrapper fcClient, string videoFilePath, LedLayout ledLayout);

        void Play(int startFrame = 0);

        void Pause();

        void Stop();
    }
}
