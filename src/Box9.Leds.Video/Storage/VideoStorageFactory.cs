using Box9.Leds.DataStorage;
using DBreeze;

namespace Box9.Leds.Video.Storage
{
    public static class VideoStorageFactory
    {
        public static IChunkedStorageClient<int, FrameVideoData> GetVideoStorageClient(DBreezeEngine engine, string serverConfigurationKey)
        {
            return new ChunkedStorageClient<int, FrameVideoData>(engine, serverConfigurationKey);
        }

        public static IStorageClient<string, VideoData> GetVideoMetadataStorageClient(DBreezeEngine engine)
        {
            return new StorageClient<string, VideoData>(engine, "videometadata");
        }
    }
}
