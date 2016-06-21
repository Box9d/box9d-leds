using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Box9.Leds.Core.Multitasking
{
    internal class ChunkedQueue<T>
    {
        private readonly ConcurrentQueue<IEnumerable<T>> queue;
        private const int MaxDequeueAttempts = 3;

        internal ChunkedQueue()
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
            while (!queue.TryDequeue(out item) || dequeueAttempts < MaxDequeueAttempts)
            {
                dequeueAttempts++;
            }

            return item;
        }
    }
}
