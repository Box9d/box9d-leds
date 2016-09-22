using System;
using System.IO;
using NAudio.Wave;

namespace Box9.Leds.Video
{
    public class Mp3AudioPlayer : IMp3AudioPlayer
    {
        private readonly AudioData videoAudioData;

        private readonly FileStream fileStream;
        private readonly Mp3FileReader mp3FileReader;
        private readonly WaveStream wavStream;

        private BlockAlignReductionStream baStream;
        private WaveOut wave;

        public Mp3AudioPlayer(AudioData videoAudioData)
        {
            this.videoAudioData = videoAudioData;

            fileStream = File.OpenRead(videoAudioData.FilePath);
            mp3FileReader = new Mp3FileReader(fileStream);
            wavStream = WaveFormatConversionStream.CreatePcmStream(mp3FileReader);    
        }

        public void Dispose()
        {
            fileStream.Dispose();
            fileStream.Dispose();
            mp3FileReader.Dispose();
            wavStream.Dispose();
            baStream.Dispose();
            wave.Dispose();
        }

        public void Play(int minutes = 0, int seconds = 0)
        {
            wavStream.CurrentTime = wavStream.CurrentTime.Add(new TimeSpan(0, minutes, seconds));

            baStream = new BlockAlignReductionStream(wavStream);
            wave = new WaveOut(WaveCallbackInfo.FunctionCallback());
            wave.Init(baStream);
            wave.Play();
        }

        public void Stop()
        {
            if (wave != null && wave.PlaybackState == PlaybackState.Playing)
            {
                try
                {
                    wave.Stop();
                }
                catch
                {
                }
            }
        }
    }
}
