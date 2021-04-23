using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static labOS2.Program;

namespace NonBlockingQueueTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Contains()
        {
            var testQueue = new NonBlockingQueue<int>();
            Assert.IsTrue(testQueue.Empty());
        }
        [TestMethod]
        public void Push()
        {
            var testQueue = new NonBlockingQueue<int>();
            testQueue.Push(1);
            Assert.IsFalse(testQueue.Empty());
        }
        [TestMethod]
        public void Pop()
        {
            var testQueue = new NonBlockingQueue<int>();
            testQueue.Push(1);
            testQueue.Push(2);
            testQueue.Push(3);
            int result = testQueue.Pop();
            int expected = 1;
            StringAssert.Equals(expected, result);
        }
        [TestMethod]
        public void Peek()
        {
            var testQueue = new NonBlockingQueue<int>();
            testQueue.Push(3);
            testQueue.Push(2);
            testQueue.Push(1);
            int result = testQueue.Peek();
            int expected = 3;
            StringAssert.Equals(expected, result);
        }
        [TestMethod]
        public void NonBlockingQueueTesting()
        {
            var testQueue = new NonBlockingQueue<int>();
            var ints = Enumerable.Range(0, 10000).ToArray();
            Parallel.ForEach(ints, k => { testQueue.Push(k); });
            int ok = 0, notOk = 0;
            Parallel.ForEach(ints, k =>
            {
                try
                {
                    testQueue.Pop();
                    Interlocked.Increment(ref ok);
                }
                catch (Exception e)
                {          
                    Interlocked.Increment(ref notOk);
                }
            });
        }
    }
}
