using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace labOS2
{
    public class Program
    {

        public class NonBlockingQueue<TValue> 

        {
            private Queue<TValue> _nonSyncQueue = new Queue<TValue>();

            private void Update(Func<Queue<TValue>, Queue<TValue>> newQueue)
            {
                Queue<TValue> old;
                Queue<TValue> updated;
                do
                {
                    old = _nonSyncQueue;
                    updated = newQueue.Invoke(old);
                } while (Interlocked.CompareExchange
                    (ref _nonSyncQueue, updated, old) != old);

            }

            

            public void Push(TValue value)
            {               
                Update(queue =>
                {
                    var updated = new Queue<TValue>(queue);
                    updated.Enqueue(value);
                    return updated;
                }) ;
                
            }


            public TValue Pop()
            {
                var modifiedQueue = new Queue<TValue>(_nonSyncQueue);
                Update(queue =>
                {
                    var updated = new Queue<TValue>(queue);
                    updated.Dequeue();
                    return updated;
                });
                return modifiedQueue.Dequeue();
            }


            public bool Contains(TValue value)
            {
                var modifiedQueue  = new Queue<TValue>(_nonSyncQueue);
                return modifiedQueue .Contains(value);
            }

            public bool Empty()
            {
                var modifiedQueue = new Queue<TValue>(_nonSyncQueue);
                if (modifiedQueue.Count == 0)
                    return true;
                else return false;
            }

            public TValue Peek()
            {
                var modifiedQueue = new Queue<TValue>(_nonSyncQueue);                      
                return modifiedQueue.Peek();
               
            }

        }
        static void Main(string[] args)
        {           
          
        }
    }
}



