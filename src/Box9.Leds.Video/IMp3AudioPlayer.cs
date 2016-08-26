using System;
using System.Threading;

namespace Box9.Leds.Video
{
    public interface IMp3AudioPlayer : IDisposable
    {
        void Play(int minutes = 0, int seconds = 0);

        void Stop();
    }
}
