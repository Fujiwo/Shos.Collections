#nullable enable

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Shos.Collections.Tests
{
    using Shos.Collections;

    static class AssertExtensions
    {
        public static void AreEqual<TElement>(IEnumerable<TElement> expects, IEnumerable<TElement> actuals)
        {
            var expectsList = expects.ToList();

            var index = 0;
            foreach (var element in actuals)
                Assert.AreEqual(expectsList[index++], element);
            Assert.AreEqual(expectsList.Count, index);
        }
    }

    [TestClass()]
    public class ShosLinkedListTests
    {
        ShosLinkedList<int>? linkedList = null;

        [TestInitialize]
        public void Setup() => linkedList = new ShosLinkedList<int>();

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
            AssertExtensions.AreEqual(new [] { 300, 200, 800 }, linkedList);
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
            linkedList = new ShosLinkedList<int> { 10, 60, 30 };
            AssertExtensions.AreEqual(new[] { 10, 60, 30 }, linkedList);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAfterでnullを渡すと例外が飛ぶ()
        {
            linkedList.AddAfter(null, 60);
        }

        [TestMethod()]
        public void AddAfterできる()
        {
            linkedList = new ShosLinkedList<int> { 10 };
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
        {
            linkedList.AddBefore(null, 10);
        }

        [TestMethod()]
        public void AddBeforeできる()
        {
            linkedList = new ShosLinkedList<int> { 10 };
            linkedList.AddBefore(linkedList.First, 30);
            AssertExtensions.AreEqual(new[] { 30, 10 }, linkedList);

            linkedList.AddBefore(linkedList.Last, 90);
            AssertExtensions.AreEqual(new[] { 30, 90, 10 }, linkedList);

            var node = linkedList.Find(90);
            linkedList.AddBefore(node, 110);
            AssertExtensions.AreEqual(new[] { 30, 110, 90, 10 }, linkedList);
        }
    }
}
