using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box9.Leds.DataStorage;

namespace Box9.Leds.Core.Multitasking
{
    public class StorageBasedMultiTasker<TKey, TValue> : IDisposable where TValue : new()
    {
        private ChunkedQueue<TValue> chunkedQueue;
        private TemporaryChunkedStorageClient<TKey, TValue> storageClient;

        public StorageBasedMultiTasker(string storageKey)
        {
            chunkedQueue = new ChunkedQueue<TValue>();
            storageClient = new TemporaryChunkedStorageClient<TKey, TValue>(StorageFactory.GetDBreezeEngine(), storageKey);
        }

        public void StartWriteToQueue(IEnumerable<TKey> keys)
        {
            Task.Run(() =>
            {
                foreach (var key in keys)
                {
                    IEnumerable<TValue> value = null;
                    if (storageClient.LoadIfExists(key, out value))
                    {
                        chunkedQueue.EnqueueChunk(value);
                    }
                }
            });
        }

        public IEnumerable<TValue> ReadFromQueue()
        {
            return chunkedQueue.DequeueChunk();
        }

        public void Dispose()
        {
            storageClient.Dispose();
        }
    }
}
