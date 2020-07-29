using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shos.Collections.Tests
{
    using Shos.Collections;

    [TestClass()]
    public class ShosLinkedListTests
    {
        ShosLinkedList<int> linkedList = null;

        [TestInitialize]
        public void Setup() => linkedList = new ShosLinkedList<int>();


        [TestMethod()]
        public void Createできる()
        {
            Assert.AreEqual(0, linkedList.Count);
        }

        [TestMethod()]
        public void AddLastできる()
        {
            linkedList.AddLast(100);
            Assert.AreEqual(1, linkedList.Count);
            linkedList.AddLast(500);
            Assert.AreEqual(2, linkedList.Count);
        }
    }
}
