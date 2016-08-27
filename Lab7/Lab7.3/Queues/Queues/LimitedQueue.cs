using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    //What about dispose?
    public class LimitedQueue<T>
    {
        //You could have used ConcurrentQueue
        private Queue<T> _queue;

        private object _lock;
        private Semaphore _semaphore;

        public LimitedQueue(int maxSize)
        {
            _lock = new object();

            _semaphore = new Semaphore(maxSize, maxSize);
            _queue = new Queue<T>(maxSize);
        }


        public void Enque(T value)
        {
            _semaphore.WaitOne();

            //Interesting.
			lock(_lock)
                _queue.Enqueue(value);
        }

        public T Deque()
        {
            T result;
			lock(_lock)
            {
                result = _queue.Dequeue();

                //You shoudl always use try-catch-finaly with acquire-release patterns in case you'll have an exception that will prevent you from releasing
                _semaphore.Release();
			}
            return result;
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }
    }
}
