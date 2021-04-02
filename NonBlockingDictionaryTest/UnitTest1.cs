using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static labOS2.Program;

namespace NonBlockingDictionaryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Contains()
        {
            var testDictionary = new NonBlockingDictionary<int, String>();
            Assert.IsTrue(testDictionary.Empty());
        }
        [TestMethod]
        public void Push()
        {
            var testDictionary = new NonBlockingDictionary<int, String>();
            testDictionary.Push(1, "first");
            Assert.IsFalse(testDictionary.Empty());
        }
        [TestMethod]
        public void Pop()
        {
            var testDictionary = new NonBlockingDictionary<int, String>();
            testDictionary.Push(1, "first");
            testDictionary.Push(2, "second");
            testDictionary.Push(3, "third");
            String result = testDictionary.Pop(3);
            String expected = "third";
            StringAssert.Equals(expected, result);
        }
        [TestMethod]
        public void Peek()
        {
            var testDictionary = new NonBlockingDictionary<int, String>();
            testDictionary.Push(1, "first");
            testDictionary.Push(2, "second");
            testDictionary.Push(3, "third");
            String result = testDictionary.Peek();
            String expected = "first";
            StringAssert.Equals(expected, result);
        }
        [TestMethod]
        public void NonBlockingDictionaryTesting()
        {
            var testDictionary = new NonBlockingDictionary<int, String>();
            var ints = Enumerable.Range(0, 10000).ToArray();
            Parallel.ForEach(ints, k => { testDictionary.Push( k, "i=" + k); });
            int ok = 0, notOk = 0;
            Parallel.ForEach(ints, k =>
            {
                try
                {
                    testDictionary.Pop(k);
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
