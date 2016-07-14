using System;
using System.Collections.Generic;
using Box9.Leds.Core.Configuration;
using Box9.Leds.DataStorage;
using Box9.Leds.Manager.Events;
using Box9.Leds.Video;
using Box9.Leds.Video.Storage;

namespace Box9.Leds.Manager.Playback
{
    public class DisposablePlaybackFactory
    {
        public delegate void Status(EventMessage message);
        public event Status StatusUpdate;

        public DisposablePlaybackFactory()
        {
            StatusUpdate += StatusUpdateHandler;
        }

        public DisposablePlayback InitializeFromConfig(LedManager ledManager, LedConfiguration configuration)
        {
            StatusUpdate(new EventMessage(EventStatus.InProgress, "Initializing video playback from configuration"));

            var disposableVideoPlaybacks = new List<DisposableVideoPlayback>();

            try
            {
                foreach (var serverConfig in configuration.Servers)
                {
                    var storageKey = configuration.GetServerVideoStorageKey(serverConfig);

                    using (var engine = DBreezeEngineFactory.GetDBreezeEngine())
                    using (var metadataClient = VideoStorageFactory.GetVideoMetadataStorageClient(engine))
                    {
                        VideoData videoData;
                        if (!metadataClient.LoadIfExists(storageKey, out videoData))
                        {
                            StatusUpdate(new EventMessage(EventStatus.InProgress, string.Format("Transforming video for server {0}", serverConfig)));

                            using (var videoTransformer = new VideoTransformer(
                                VideoStorageFactory.GetVideoStorageClient(engine, storageKey), configuration.VideoConfig.SourceFilePath))
                            {
                                videoData = videoTransformer.ExtractAndSaveTransformedVideoInChunks(serverConfig);
                                metadataClient.Save(storageKey, videoData);
                            }
                        }

                        StatusUpdate(new EventMessage(EventStatus.InProgress, string.Format("Video transformation ready for server {0}", serverConfig)));

                        disposableVideoPlaybacks.Add(new DisposableVideoPlayback(videoData, serverConfig));
                    }
                }

                StatusUpdate(new EventMessage(EventStatus.InProgress, "Initializing audio playback from configuration"));

                AudioData audioData;
                using (var engine = DBreezeEngineFactory.GetDBreezeEngine())
                using (var metadataClient = AudioStorageFactory.GetAudioMetadataStorageClient(engine))
                {
                    var storageKey = configuration.GetAudioStorageKey();

                    if (!metadataClient.LoadIfExists(storageKey, out audioData))
                    {
                        IVideoAudioTransformer audioTransformer = new VideoAudioTransformer(configuration.AudioConfig.SourceFilePath);
                        audioData = audioTransformer.ExtractAndSaveAudio();
                        metadataClient.Save(storageKey, audioData);
                    }
                }

                StatusUpdate(new EventMessage(EventStatus.InProgress, "Initializing video players and servers"));

                var playback = new DisposablePlayback(ledManager, configuration, disposableVideoPlaybacks, audioData);
                StatusUpdate(new EventMessage(EventStatus.Completed, "Playback ready"));
                return playback;
            }
            catch (Exception ex)
            {
                StatusUpdate(new EventMessage(EventStatus.Failed, "Failed to initialize"));
                throw;
            }
        }

        private void StatusUpdateHandler(EventMessage message)
        {
        }
    }
}
