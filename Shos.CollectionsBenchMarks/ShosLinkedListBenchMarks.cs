using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Shos.Collections.BenchMarks
{
    using Shos.Collections;

    #region BechmarkTest
    [ShortRunJob]
    public class BechmarkTest
    {
        const int dataNumber = 1000_000;

        ShosLinkedList1<int>? linkedList1;
        ShosLinkedList<int>? linkedList;

        [Benchmark]
        public void AddLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddLast(number);
        }

        [Benchmark]
        public void AddLastTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddLast(number);
        }

        [Benchmark]
        public void AddFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddFirst(number);
        }

        [Benchmark]
        public void AddFirstTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddFirst(number);
        }

        [Benchmark]
        public void RemoveFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveFirst();
        }

        [Benchmark]
        public void RemoveFirstTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.RemoveFirst();
        }

        [Benchmark]
        public void RemoveLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveLast();
        }

        [Benchmark]
        public void RemoveLastTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.RemoveLast();
        }

        [IterationSetup]
        public void Setup()
        {
            linkedList = new ShosLinkedList<int>();
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddLast(number);
            linkedList1 = new ShosLinkedList1<int>();
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddLast(number);
        }

        [GlobalSetup]
        public void Initialize() {}
    }
    #endregion // BechmarkTest

    class Program
    {
        #region Main
        static void Main() => BenchmarkRunner.Run<BechmarkTest>();
        #endregion // Main
    }
}
