using System;

namespace Box9.Leds.DataStorage
{
    public interface IStorageClient<TKey, TValue> : IDisposable where TValue : new()
    {
        void Save(TKey key, TValue item);

        bool LoadIfExists(TKey key, out TValue value);
    }
}
