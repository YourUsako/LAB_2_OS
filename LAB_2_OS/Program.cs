using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using labOS2;

namespace labOS2
{
    public class NonBlockingDictionary<TKey, TValue> where TValue : class

    {
        private Dictionary<TKey, TValue> _nonSyncDictionary = new Dictionary<TKey, TValue>();

        private void Update(Func<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>> newDict)
        {
            Dictionary<TKey, TValue> old;
            Dictionary<TKey, TValue> updated;
            do
            {
                old = _nonSyncDictionary;
                updated = newDict.Invoke(old);
            } while (Interlocked.CompareExchange
                (ref _nonSyncDictionary, updated, old) != old);

        }


        public void Push(TKey key, TValue value)
        {
            Update(
                dict => new Dictionary<TKey, TValue>(_nonSyncDictionary)
                { [key] = value }
            );
        }


        public TValue Pop(TKey key)
        {
            var modifiedDict = new Dictionary<TKey, TValue>(_nonSyncDictionary);
            return modifiedDict[key];
        }


        public bool Contains(TKey key)
        {
            return Pop(key) != null;
        }

        public bool Empty()
        {
            var modifiedDict = new Dictionary<TKey, TValue>(_nonSyncDictionary);
            if (modifiedDict.Count == 0)
                return true;
            else return false;
        }

        public TValue Peek()
        {
            var modifiedDict = new Dictionary<TKey, TValue>(_nonSyncDictionary);
            if (!Empty())
            {
                var first = modifiedDict.First();
                return first.Value;
            }
            else return null;

        }

    }

}





