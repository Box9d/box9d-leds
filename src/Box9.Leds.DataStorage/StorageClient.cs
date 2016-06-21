using System;
using DBreeze;

namespace Box9.Leds.DataStorage
{
    public class StorageClient<TKey, TValue> : IDisposable where TValue : new()
    {
        private readonly DBreezeEngine engine;
        private readonly string table;

        public StorageClient(DBreezeEngine engine, string table)
        {
            this.engine = engine;
            this.table = table;
        }

        public void Save(TKey key, TValue item)
        {
            using (var transaction = engine.GetTransaction())
            {
                transaction.Insert(table, key, item);
                transaction.Commit();
            }
        }

        public bool LoadIfExists(TKey key, out TValue value)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<TKey, TValue>(table, key);
                if (row.Exists)
                {
                    value = row.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
