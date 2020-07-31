using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;

namespace Shos.Collections.BenchMarks
{
    using Shos.Collections;

    #region BenchmarkTest
    [ShortRunJob]
    [HtmlExporter]
    [CsvExporter]
    public class BenchmarkTest
    {
        const int dataNumber = 10000;

        LinkedList     <int>? linkedListDotNet;
        ShosLinkedList1<int>? linkedList1;
        ShosLinkedList2<int>? linkedList2;
        ShosLinkedList3<int>? linkedList3;
        ShosLinkedList <int>? linkedList ;
        ShosLinkedList      ? linkedListNonGeneric;

        int temporaryValue;

        #region ForEachTest
        [Benchmark]
        public void ForEachDotNetTest()
        {
            foreach (var element in linkedListDotNet)
                temporaryValue = element;
        }

        [Benchmark]
        public void ForEachTest1()
        {
            foreach (var element in linkedList1)
                temporaryValue = element;
        }

        [Benchmark]
        public void ForEachTest2()
        {
            foreach (var element in linkedList2)
                temporaryValue = element;
        }

        [Benchmark]
        public void ForEachTest()
        {
            foreach (var element in linkedList)
                temporaryValue = element;
        }

        [Benchmark]
        public void ForEachTest3()
        {
            foreach (var element in linkedList3)
                temporaryValue = element;
        }

        [Benchmark]
        public void ForEachTestNonGeneric()
        {
            foreach (var element in linkedList3)
                temporaryValue = (int)element;
        }
        #endregion // ForEachTest

        #region AddLastTest
        [Benchmark]
        public void AddLastDotNetTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListDotNet.AddLast(number);
        }

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
        public void AddLastTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.AddLast(number);
        }

        [Benchmark]
        public void AddLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddLast(number);
        }

        [Benchmark]
        public void AddLastTestNonGeneric()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListNonGeneric.AddLast(number);
        }
        #endregion // AddLastTest

        #region AddFirstTest
        [Benchmark]
        public void AddFirstDotNetTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListDotNet.AddFirst(number);
        }

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
        public void AddFirstTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.AddFirst(number);
        }

        [Benchmark]
        public void AddFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.AddFirst(number);
        }

        [Benchmark]
        public void AddFirstTestNonGeneric()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListNonGeneric.AddFirst(number);
        }
        #endregion // AddFirstTest

        #region RemoveFirstTest
        [Benchmark]
        public void RemoveFirstDotNetTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListDotNet.RemoveFirst();
        }

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
        public void RemoveFirstTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.RemoveFirst();
        }

        [Benchmark]
        public void RemoveFirstTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveFirst();
        }

        [Benchmark]
        public void RemoveFirstTestNonGeneric()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListNonGeneric.RemoveFirst();
        }
        #endregion // RemoveFirstTest

        #region RemoveLastTest
        [Benchmark]
        public void RemoveLastDotNetTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListDotNet.RemoveLast();
        }

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
        public void RemoveLastTest3()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList3.RemoveLast();
        }

        [Benchmark]
        public void RemoveLastTest()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedList.RemoveLast();
        }

        [Benchmark]
        public void RemoveLastTestNonGeneric()
        {
            for (var number = 1; number <= dataNumber; number++)
                linkedListNonGeneric.RemoveLast();
        }
        #endregion // RemoveLastTest

        #region FindTest
        [Benchmark]
        public void FindDotNetTest()
        {
            LinkedListNode<int>? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedListDotNet.Find(dataNumber / 2);
        }

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
        public void FindTest3()
        {
            ShosLinkedList3<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList3.Find(dataNumber / 2);
        }

        [Benchmark]
        public void FindTest()
        {
            ShosLinkedList<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList.Find(dataNumber / 2);
        }

        [Benchmark]
        public void FindTestNonGeneric()
        {
            ShosLinkedList.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedListNonGeneric.Find(dataNumber / 2);
        }
        #endregion // FindTest

        #region FindLastTest
        [Benchmark]
        public void FindLastDotNetTest()
        {
            LinkedListNode<int>? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedListDotNet.FindLast(dataNumber / 2);
        }

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
        public void FindLastTest3()
        {
            ShosLinkedList3<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList3.FindLast(dataNumber / 2);
        }

        [Benchmark]
        public void FindLastTest()
        {
            ShosLinkedList<int>.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedList.FindLast(dataNumber / 2);
        }

        [Benchmark]
        public void FindLastTestNonGeneric()
        {
            ShosLinkedList.Node? node = null;
            for (var number = 1; number <= dataNumber; number++)
                node = linkedListNonGeneric.FindLast(dataNumber / 2);
        }
        #endregion // FindLastTest

        [IterationSetup]
        public void Setup()
        {
            static void Initialize(dynamic linkedList)
            {
                for (var number = 1; number <= dataNumber; number++)
                    linkedList.AddLast(number);
            }

            linkedListDotNet = new LinkedList<int>();
            Initialize(linkedListDotNet);

            linkedList1 = new ShosLinkedList1<int>();
            Initialize(linkedList1);

            linkedList2 = new ShosLinkedList2<int>();
            Initialize(linkedList2);

            linkedList3 = new ShosLinkedList3<int>();
            Initialize(linkedList3);

            linkedList = new ShosLinkedList<int>();
            Initialize(linkedList);

            linkedListNonGeneric = new ShosLinkedList();
            Initialize(linkedListNonGeneric);
        }

        [GlobalSetup]
        public void Initialize() {}
    }
#endregion // BenchmarkTest

    class Program
    {
#region Main
        static void Main() => BenchmarkRunner.Run<BenchmarkTest>();
#endregion // Main
    }
}
