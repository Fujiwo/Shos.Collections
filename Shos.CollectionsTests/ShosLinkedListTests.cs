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

        [TestMethod()]
        public void foreachできる()
        {
            foreach (var element in linkedList)
                Assert.Fail();

            linkedList.AddLast(300);
            linkedList.AddLast(200);
            linkedList.AddLast(800);

            var index = 0;
            foreach (var element in linkedList) {
                switch (index++) {
                    case 0: Assert.AreEqual(300, element); break;
                    case 1: Assert.AreEqual(200, element); break;
                    case 2: Assert.AreEqual(800, element); break;
                    default: Assert.Fail(); break;
                }
            }
        }
    }
}
