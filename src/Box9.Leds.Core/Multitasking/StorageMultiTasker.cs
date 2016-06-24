using System;
using System.Collections.Generic;
using Box9.Leds.DataStorage;

namespace Box9.Leds.Core.Multitasking
{
    public class StorageBasedMultiTasker<TValue> : IDisposable where TValue : new()
    {
        private ChunkedQueue<TValue> chunkedQueue;
        private ChunkedStorageClient<int, TValue> storageClient;

        public StorageBasedMultiTasker(string storageKey)
        {
            chunkedQueue = new ChunkedQueue<TValue>();
            storageClient = new ChunkedStorageClient<int, TValue>(StorageFactory.GetDBreezeEngine(), storageKey);
        }

        public void StoreChunk(int key, IEnumerable<TValue> chunk)
        {
            storageClient.Save(key, chunk);
        }

        public void WriteChunkToQueue(int key)
        {
            IEnumerable<TValue> value = null;
            if (storageClient.LoadIfExists(key, out value))
            {
                chunkedQueue.EnqueueChunk(value);
            }
        }

        public IEnumerable<TValue> ReadChunkFromQueue()
        {
            return chunkedQueue.DequeueChunk();
        }

        public void Dispose()
        {
            storageClient.Dispose();
        }
    }
}
