using System;
using System.Collections.Generic;
using DBreeze;

namespace Box9.Leds.DataStorage
{
    public class TemporaryChunkedStorageClient<TKey, TValue> : IDisposable where TValue : new()
    {
        private readonly DBreezeEngine engine;
        private readonly string table;

        public TemporaryChunkedStorageClient(DBreezeEngine engine, string table)
        {
            this.engine = engine;
            this.table = table;
        }

        public void Save(TKey key, IEnumerable<TValue> item)
        {
            using (var transaction = engine.GetTransaction())
            {
                transaction.Insert(table, key, item);
                transaction.Commit();
            }
        }

        public bool LoadIfExists(TKey key, out IEnumerable<TValue> value)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<TKey, IEnumerable<TValue>>(table, key);
                if (row.Exists)
                {
                    value = row.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public void Dispose()
        {
            using (var transaction = engine.GetTransaction())
            {
                transaction.RemoveAllKeys(table, false);
                transaction.Commit();
            }

            engine.Dispose();
        }
    }
}
