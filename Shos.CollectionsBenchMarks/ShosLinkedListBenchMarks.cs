using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Shos.Collections.BenchMarks
{
    using Shos.Collections;

    #region BechmarkTest
    [ShortRunJob]
    [HtmlExporter]
    [CsvExporter]
    public class BechmarkTest
    {
        const int dataNumber = 10000;

        ShosLinkedList1<int>? linkedList1;
        ShosLinkedList2<int>? linkedList2;
        ShosLinkedList <int>? linkedList ;
        ShosLinkedList3     ? linkedList3;

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

        [Benchmark]
        public void AddLastTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.AddLast(number);
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

        [Benchmark]
        public void AddFirstTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.AddFirst(number);
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

        [Benchmark]
        public void RemoveFirstTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.RemoveFirst();
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

        [Benchmark]
        public void RemoveLastTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.RemoveLast();
        }
        #endregion // RemoveLastTest

        #region FindTest
        [Benchmark]
        public void FindTest1()
        {
            ShosLinkedList1<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList1.Find(dataNumber / 2);
        }

        [Benchmark]
        public void FindTest2()
        {
            ShosLinkedList2<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList2.Find(dataNumber / 2);
        }

        [Benchmark]
        public void FindTest()
        {
            ShosLinkedList<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList.Find(dataNumber / 2);
        }

        [Benchmark]
        public void FindTest3()
        {
            ShosLinkedList3.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList3.Find(dataNumber / 2);
        }
        #endregion // FindTest

        #region FindLastTest
        [Benchmark]
        public void FindLastTest1()
        {
            ShosLinkedList1<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList1.FindLast(dataNumber / 2);
        }

        [Benchmark]
        public void FindLastTest2()
        {
            ShosLinkedList2<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList2.FindLast(dataNumber / 2);
        }

        [Benchmark]
        public void FindLastTest()
        {
            ShosLinkedList<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList.FindLast(dataNumber / 2);
        }

        [Benchmark]
        public void FindLastTest3()
        {
            ShosLinkedList3.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList3.FindLast(dataNumber / 2);
        }
        #endregion // FindLastTest

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

            linkedList3 = new ShosLinkedList3();
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.AddLast(number);
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
