using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Core.Multitasking
{
    public class ChunkedConcurrentQueue<T>
    {
        private readonly ConcurrentQueue<IEnumerable<T>> queue;
        private const int MaxDequeueAttempts = 3;

        public int NumberOfItemsInAllChunks
        {
            get
            {
                return queue.Sum(q => q.Count());
            }
        }

        public ChunkedConcurrentQueue()
        {
            queue = new ConcurrentQueue<IEnumerable<T>>();
        }

        public void EnqueueChunk(IEnumerable<T> chunk)
        {
            queue.Enqueue(chunk);
        }

        public IEnumerable<T> DequeueChunk()
        {
            IEnumerable<T> item;
            int dequeueAttempts = 0;
            while (!queue.TryDequeue(out item) && dequeueAttempts < MaxDequeueAttempts)
            {
                dequeueAttempts++;
            }

            return item;
        }
    }
}
