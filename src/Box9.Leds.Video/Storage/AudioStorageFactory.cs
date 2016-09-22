using Box9.Leds.DataStorage;
using DBreeze;

namespace Box9.Leds.Video.Storage
{
    public static class AudioStorageFactory
    {
        public static IStorageClient<string, AudioData> GetAudioMetadataStorageClient(DBreezeEngine engine)
        {
            return new StorageClient<string, AudioData>(engine, "audiometadata");
        }
    }
}
