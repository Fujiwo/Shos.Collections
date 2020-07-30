#nullable enable

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Shos.Collections.Tests
{
    using Shos.Collections;

    [TestClass()]
    public class ShosLinkedList1Tests
    {
        ShosLinkedList1<int>? linkedList = null;

        [TestInitialize]
        public void Setup() => linkedList = new ShosLinkedList1<int>();

        [TestMethod()]
        public void Createできる()
        {
            Assert.AreEqual(0, linkedList.Count);
            Assert.IsNull(linkedList.First);
            Assert.IsNull(linkedList.Last);
        }

        [TestMethod()]
        public void AddLastできる()
        {
            linkedList.AddLast(100);
            Assert.AreEqual(1, linkedList.Count);
            Assert.IsNotNull(linkedList.First);
            Assert.AreEqual(100, linkedList.First.Value);
            Assert.IsNotNull(linkedList.Last);
            Assert.AreEqual(100, linkedList.Last.Value);
            Assert.AreSame(linkedList.First, linkedList.Last);

            linkedList.AddLast(500);
            Assert.AreEqual(2, linkedList.Count);
            Assert.AreEqual(100, linkedList.First.Value);
            Assert.AreEqual(500, linkedList.Last.Value);
            Assert.AreNotSame(linkedList.First, linkedList.Last);
        }

        [TestMethod()]
        public void foreachできる()
        {
            foreach (var element in linkedList)
                Assert.Fail();

            linkedList.AddLast(300);
            linkedList.AddLast(200);
            linkedList.AddLast(800);
            AssertExtensions.AreEqual(new[] { 300, 200, 800 }, linkedList);
        }

        [TestMethod()]
        public void AddFirstできる()
        {
            linkedList.AddFirst(300);
            Assert.AreEqual(1, linkedList.Count);
            Assert.AreEqual(300, linkedList.First.Value);
            Assert.AreEqual(300, linkedList.Last.Value);

            linkedList.AddLast(600);
            Assert.AreEqual(2, linkedList.Count);
            linkedList.AddFirst(100);
            Assert.AreEqual(100, linkedList.First.Value);
            Assert.AreEqual(600, linkedList.Last.Value);
            AssertExtensions.AreEqual(new[] { 100, 300, 600 }, linkedList);
        }

        [TestMethod()]
        public void Findできる()
        {
            Assert.IsNull(linkedList.Find(100));
            linkedList.AddLast(200);
            Assert.IsNull(linkedList.Find(100));
            var node = linkedList.Find(200);
            Assert.IsNotNull(node);
            Assert.AreEqual(200, node.Value);
            Assert.AreSame(node, linkedList.First);

            linkedList.AddLast(800);
            Assert.AreEqual(200, linkedList.Find(200).Value);
            Assert.AreSame(linkedList.Find(200), linkedList.First);
            Assert.AreEqual(800, linkedList.Find(800).Value);
            Assert.AreSame(linkedList.Find(800), linkedList.Last);
        }

        [TestMethod()]
        public void 初期化リストが使える()
        {
            linkedList = new ShosLinkedList1<int> { 10, 60, 30 };
            AssertExtensions.AreEqual(new[] { 10, 60, 30 }, linkedList);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAfterでnullを渡すと例外が飛ぶ()
            => linkedList.AddAfter(null, 60);

        [TestMethod()]
        public void AddAfterできる()
        {
            linkedList = new ShosLinkedList1<int> { 10 };
            linkedList.AddAfter(linkedList.First, 70);
            AssertExtensions.AreEqual(new[] { 10, 70 }, linkedList);

            linkedList.AddAfter(linkedList.Last, 40);
            AssertExtensions.AreEqual(new[] { 10, 70, 40 }, linkedList);

            var node = linkedList.Find(70);
            linkedList.AddAfter(node, 100);
            AssertExtensions.AreEqual(new[] { 10, 70, 100, 40 }, linkedList);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBeforeでnullを渡すと例外が飛ぶ()
            => linkedList.AddBefore(null, 10);

        [TestMethod()]
        public void AddBeforeできる()
        {
            linkedList = new ShosLinkedList1<int> { 10 };
            linkedList.AddBefore(linkedList.First, 30);
            AssertExtensions.AreEqual(new[] { 30, 10 }, linkedList);

            linkedList.AddBefore(linkedList.Last, 90);
            AssertExtensions.AreEqual(new[] { 30, 90, 10 }, linkedList);

            var node = linkedList.Find(90);
            linkedList.AddBefore(node, 110);
            AssertExtensions.AreEqual(new[] { 30, 110, 90, 10 }, linkedList);
        }

        [TestMethod()]
        public void Clearできる()
        {
            linkedList = new ShosLinkedList1<int> { 10, 70, 30 };
            linkedList.Clear();
            Assert.AreEqual(0, linkedList.Count);
            Assert.IsNull(linkedList.First);
            Assert.IsNull(linkedList.Last);

            linkedList.AddLast(40);
            linkedList.AddLast(20);
            AssertExtensions.AreEqual(new[] { 40, 20 }, linkedList);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Removeでnullを渡すと例外が飛ぶ()
            => linkedList.Remove(null);

        [TestMethod()]
        public void Removeできる()
        {
            linkedList = new ShosLinkedList1<int> { 10, 70, 30, 40, 60, 90, 100 };

            Assert.IsTrue(linkedList.Remove(10));
            AssertExtensions.AreEqual(new[] { 70, 30, 40, 60, 90, 100 }, linkedList);
            Assert.IsTrue(linkedList.Remove(100));
            AssertExtensions.AreEqual(new[] { 70, 30, 40, 60, 90 }, linkedList);
            Assert.IsTrue(linkedList.Remove(40));
            AssertExtensions.AreEqual(new[] { 70, 30, 60, 90 }, linkedList);
            Assert.IsFalse(linkedList.Remove(40));
            AssertExtensions.AreEqual(new[] { 70, 30, 60, 90 }, linkedList);

            var node = linkedList.Find(70);
            linkedList.Remove(node);
            AssertExtensions.AreEqual(new[] { 30, 60, 90 }, linkedList);
            node = linkedList.Find(60);
            linkedList.Remove(node);
            AssertExtensions.AreEqual(new[] { 30, 90 }, linkedList);
            node = linkedList.Find(90);
            linkedList.Remove(node);
            AssertExtensions.AreEqual(new[] { 30 }, linkedList);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void 空のときRemoveFirstすると例外が飛ぶ()
            => linkedList.RemoveFirst();

        [TestMethod()]
        public void RemoveFirstできる()
        {
            linkedList = new ShosLinkedList1<int> { 60, 30, 20 };
            linkedList.RemoveFirst();
            AssertExtensions.AreEqual(new[] { 30, 20 }, linkedList);
            linkedList.RemoveFirst();
            AssertExtensions.AreEqual(new[] { 20 }, linkedList);
            linkedList.RemoveFirst();
            AssertExtensions.AreEqual(new int[] { }, linkedList);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void 空のときRemoveLastすると例外が飛ぶ()
            => linkedList.RemoveLast();

        [TestMethod()]
        public void RemoveLastできる()
        {
            linkedList = new ShosLinkedList1<int> { 20, 40, 80 };
            linkedList.RemoveLast();
            AssertExtensions.AreEqual(new[] { 20, 40 }, linkedList);
            linkedList.RemoveLast();
            AssertExtensions.AreEqual(new[] { 20 }, linkedList);
            linkedList.RemoveLast();
            AssertExtensions.AreEqual(new int[] { }, linkedList);
        }

        [TestMethod()]
        public void FindLastできる()
        {
            Assert.IsNull(linkedList.FindLast(10));
            linkedList = new ShosLinkedList1<int> { 40, 30, 60, 30, 40, 20, 40 };
            Assert.IsNull(linkedList.FindLast(100));

            var node = linkedList.FindLast(40);
            Assert.IsNotNull(node);
            Assert.AreEqual(40, node.Value);
            Assert.AreSame(node, linkedList.Last);
            linkedList.Remove(node);
            AssertExtensions.AreEqual(new[] { 40, 30, 60, 30, 40, 20 }, linkedList);
            linkedList.Remove(linkedList.FindLast(30));
            AssertExtensions.AreEqual(new[] { 40, 30, 60, 40, 20 }, linkedList);
        }

        [TestMethod()]
        public void Containsできる()
        {
            Assert.IsFalse(linkedList.Contains(10));
            linkedList = new ShosLinkedList1<int> { 30, 60 };
            Assert.IsTrue(linkedList.Contains(30));
            Assert.IsFalse(linkedList.Contains(40));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void nullにCopyToすると例外が飛ぶ()
            => linkedList.CopyTo(null, 0);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void 負のindexでCopyToすると例外が飛ぶ()
            => linkedList.CopyTo(new int[2], -1);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void 大きさが足りないarrayにCopyToすると例外が飛ぶ()
        {
            linkedList = new ShosLinkedList1<int> { 10, 30 };
            linkedList.CopyTo(new int[2], 1);
        }

        [TestMethod()]
        public void CopyToできる()
        {
            var array = new int[] { };
            linkedList.CopyTo(array, 0);
            AssertExtensions.AreEqual(new int[] { }, array);

            linkedList = new ShosLinkedList1<int> { 30, 60 };
            array = new int[] { 10, 20 };
            linkedList.CopyTo(array, 0);
            AssertExtensions.AreEqual(linkedList, array);
            array = new int[] { 10, 20, 30 };
            linkedList.CopyTo(array, 0);
            AssertExtensions.AreEqual(new[] { 30, 60, 30 }, array);
            linkedList.CopyTo(array, 1);
            AssertExtensions.AreEqual(new[] { 30, 30, 60 }, array);
        }
    }
}
