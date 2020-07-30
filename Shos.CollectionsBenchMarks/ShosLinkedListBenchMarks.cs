using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Shos.Collections.BenchMarks
{
    using Shos.Collections;

    #region BechmarkTest
    [ShortRunJob]
    public class BechmarkTest
    {
        ShosLinkedList<int>? linkedList;

        [Benchmark]
        public void AddLastTest()
        {
            for (var number = 1; number <= 1000; number++)
                linkedList.AddLast(number);
        }

        [Benchmark]
        public void AddFirstTest()
        {
            for (var number = 1; number <= 1000; number++)
                linkedList.AddFirst(number);
        }

        [GlobalSetup]
        public void Initialize()
            => linkedList = new ShosLinkedList<int>();
    }
    #endregion // BechmarkTest

    class Program
    {
        #region Main
        static void Main()
        {
            BenchmarkRunner.Run<BechmarkTest>();
        }
        #endregion // Main
    }
}
