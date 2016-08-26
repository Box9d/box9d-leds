using System;
using System.IO;
using NAudio.Wave;

namespace Box9.Leds.Video
{
    public class VideoAudioTransformer : IVideoAudioTransformer
    {
        private readonly string videoFilePath;

        public VideoAudioTransformer(string videoFilePath)
        {
            this.videoFilePath = videoFilePath;
        }

        public AudioData ExtractAndSaveAudio()
        {
            var converter = new NReco.VideoConverter.FFMpegConverter();
            var audioFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp3");

            string storageKey = Guid.NewGuid().ToString();
            converter.ConvertMedia(videoFilePath, audioFilePath, "mp3");

            var durationInSeconds = new Mp3FileReader(audioFilePath).TotalTime.TotalSeconds;

            return new AudioData
            {
                FilePath = audioFilePath,
                AudioFormat = "mp3",
                DurationInSeconds = durationInSeconds
            };
        }
    }
}
