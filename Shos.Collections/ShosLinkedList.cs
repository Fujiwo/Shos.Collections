using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shos.Collections
{
    public class ShosLinkedList<TElement> : IEnumerable<TElement>
    {
        public class Node
        {
            public TElement Value { get; set; }
            public Node? Next { get; set; } = null;

            public Node() {}
            public Node(TElement value) => Value = value;
        }

        Node top    = new Node();
        Node bottom = new Node();

        public Node? First => Count == 0 ? null : top.Next;
        public Node? Last => Count == 0 ? null : PreviousNode(bottom);

        public int Count { get; private set; } = 0;

        public ShosLinkedList() => top.Next = bottom;

        public void Add(TElement element) => AddLast(element);
        public void AddFirst(TElement element) => AddAfter(top, element);
        public void AddLast(TElement element) => AddBefore(bottom, element);

        public void AddAfter(Node node, TElement element)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode  = new Node(element);
            newNode.Next = node.Next;
            node.Next    = newNode;
            Count++;
        }

        public void AddBefore(Node node, TElement element)
        {
            if (node == null)
                throw new ArgumentNullException();

            Debug.Assert(PreviousNode(node) != null);
            Debug.Assert(object.ReferenceEquals(PreviousNode(node).Next, node));

            var newNode             = new Node(element);
            newNode.Next            = node;
            PreviousNode(node).Next = newNode;
            Count++;
        }

        public Node? Find(TElement element)
        {
            for (var node = top.Next; !object.ReferenceEquals(node, bottom); node = node.Next) {
                if (node.Value.Equals(element))
                    return node;
            }
            return null;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            for (var node = top.Next; !object.ReferenceEquals(node, bottom); node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Node PreviousNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            for (var previousNode = top; previousNode != null; previousNode = previousNode.Next) {
                if (object.ReferenceEquals(previousNode.Next, node))
                    return previousNode;
            }
            throw new InvalidOperationException();
        }
    }
}
