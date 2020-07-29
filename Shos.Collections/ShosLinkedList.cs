using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shos.Collections
{
    public class ShosLinkedList<TElement> : IEnumerable<TElement>
    {
        class Node
        {
            public TElement Value { get; set; }
            public Node? Next { get; set; } = null;

            public Node()
            {}

            public Node(TElement value) => Value = value;
        }

        Node? first = null;

        Node? Last {
            get {
                for (var node = first; node != null; node = node.Next) {
                    if (node.Next == null)
                        return node;
                }
                return null;
            }
        }

        public int Count { get; private set; } = 0;

        public void AddLast(TElement element)
        {
            var newNode = new Node(element);
            if (first == null) {
                first = newNode;
            } else {
                Debug.Assert(Last != null);
                Last.Next = newNode;
            }
            Count++;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            for (var node = first; node != null; node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
