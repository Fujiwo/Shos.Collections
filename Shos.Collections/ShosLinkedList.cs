using System;
using System.Collections.Generic;

namespace Shos.Collections
{
    public class ShosLinkedList<TElement>
    {
        public int Count { get; private set; } = 0;

        public void AddLast(TElement element)
        {
            Count++;
        }
    }
}
