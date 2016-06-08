using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Video
{
    public interface IVideoPlayer
    {
        void Load(string videoFilePath, LedLayout ledLayout);

        void Play(int startFrame = 0);

        void Pause();

        void Stop();
    }
}
