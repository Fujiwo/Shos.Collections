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
        ShosLinkedList2<int>? linkedList2;
        ShosLinkedList <int>? linkedList ;

        #region AddLastTest
        [Benchmark]
        public void AddLastTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddLast(number);
        }

        [Benchmark]
        public void AddLastTest2()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList2.AddLast(number);
        }

        [Benchmark]
        public void AddLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddLast(number);
        }
        #endregion // AddLastTest

        #region AddFirstTest
        [Benchmark]
        public void AddFirstTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddFirst(number);
        }

        [Benchmark]
        public void AddFirstTest2()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList2.AddFirst(number);
        }

        [Benchmark]
        public void AddFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddFirst(number);
        }
        #endregion // AddFirstTest

        #region RemoveFirstTest
        [Benchmark]
        public void RemoveFirstTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.RemoveFirst();
        }

        [Benchmark]
        public void RemoveFirstTest2()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList2.RemoveFirst();
        }

        [Benchmark]
        public void RemoveFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveFirst();
        }
        #endregion // RemoveFirstTest

        #region RemoveLastTest
        [Benchmark]
        public void RemoveLastTest1()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.RemoveLast();
        }

        [Benchmark]
        public void RemoveLastTest2()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList2.RemoveLast();
        }

        [Benchmark]
        public void RemoveLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveLast();
        }
        #endregion // RemoveLastTest

        [IterationSetup]
        public void Setup()
        {
            linkedList1 = new ShosLinkedList1<int>();
            for (var number = 1; number <= dataNumber; number++)
                linkedList1.AddLast(number);

            linkedList2 = new ShosLinkedList2<int>();
            for (var number = 1; number <= dataNumber; number++)
                linkedList2.AddLast(number);

            linkedList = new ShosLinkedList<int>();
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddLast(number);
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
