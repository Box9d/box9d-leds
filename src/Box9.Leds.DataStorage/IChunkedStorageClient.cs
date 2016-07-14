using System;
using System.Collections.Generic;

namespace Box9.Leds.DataStorage
{
    public interface IChunkedStorageClient<TKey, TValue> : IDisposable where TValue : new()
    {
        bool ContainsData();

        void Save(TKey key, IEnumerable<TValue> item);

        bool LoadIfExists(TKey key, out IEnumerable<TValue> value);
    }
}
