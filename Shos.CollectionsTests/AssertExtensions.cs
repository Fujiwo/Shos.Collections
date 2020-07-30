using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Shos.Collections.Tests
{
    public static class AssertExtensions
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
}
