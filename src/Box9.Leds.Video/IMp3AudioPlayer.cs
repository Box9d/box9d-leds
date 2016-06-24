using System;
using System.Threading;

namespace Box9.Leds.Video
{
    public interface IMp3AudioPlayer : IDisposable
    {
        void Play();

        void Stop();
    }
}
