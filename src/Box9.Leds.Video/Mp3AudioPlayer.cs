using System;
using System.Collections.Generic;
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
        private readonly BlockAlignReductionStream baStream;
        private readonly WaveOut wave;

        public delegate void PlaybackFinished();
        public event PlaybackFinished Stopped;

        public Mp3AudioPlayer(AudioData videoAudioData)
        {
            this.videoAudioData = videoAudioData;

            fileStream = File.OpenRead(videoAudioData.FilePath);
            mp3FileReader = new Mp3FileReader(fileStream);
            wavStream = WaveFormatConversionStream.CreatePcmStream(mp3FileReader);
            baStream = new BlockAlignReductionStream(wavStream);
            wave = new WaveOut(WaveCallbackInfo.FunctionCallback());

            wave.Init(baStream);           
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

        public void Play()
        {
            wave.Play();
            wave.PlaybackStopped += PlaybackStoppedHandler;
        }

        public void Stop()
        {
            if (wave.PlaybackState == PlaybackState.Playing)
            {
                wave.Stop();
            }
        }

        private void PlaybackStoppedHandler(object sender, StoppedEventArgs args)
        {
            Stopped();
        }

        private void StoppedHandler()
        {
        }
    }
}
