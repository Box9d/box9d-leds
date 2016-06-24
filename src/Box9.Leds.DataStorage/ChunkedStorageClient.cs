using System.Collections.Generic;
using System.Linq;
using DBreeze;
using Newtonsoft.Json;

namespace Box9.Leds.DataStorage
{
    public class ChunkedStorageClient<TKey, TValue> : IChunkedStorageClient<TKey, TValue> where TValue : new()
    {
        private readonly DBreezeEngine engine;
        private readonly string table;

        public ChunkedStorageClient(DBreezeEngine engine, string table)
        {
            this.engine = engine;
            this.table = table;
        }

        public void Save(TKey key, IEnumerable<TValue> item)
        {
            using (var transaction = engine.GetTransaction())
            {
                transaction.Insert(table, key, JsonConvert.SerializeObject(item));
                transaction.Commit();
            }
        }

        public bool LoadIfExists(TKey key, out IEnumerable<TValue> value)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<TKey, string>(table, key);
                if (row.Exists)
                {
                    value = JsonConvert.DeserializeObject<IEnumerable<TValue>>(row.Value);
                    return true;
                }
            }

            value = null;
            return false;
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
