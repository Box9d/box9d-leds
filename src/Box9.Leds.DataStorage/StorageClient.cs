using System;
using DBreeze;
using Newtonsoft.Json;

namespace Box9.Leds.DataStorage
{
    public class StorageClient<TKey, TValue> : IStorageClient<TKey, TValue> where TValue : new()
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
                transaction.Insert(table, key, JsonConvert.SerializeObject(item));
                transaction.Commit();
            }
        }

        public bool LoadIfExists(TKey key, out TValue value)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<TKey, string>(table, key);
                if (row.Exists)
                {
                    value = JsonConvert.DeserializeObject<TValue>(row.Value);
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
