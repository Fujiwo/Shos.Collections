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

            public Node()
            {}

            public Node(TElement value) => Value = value;
        }

        public Node? First { get; private set; }  = null;

        public Node? Last {
            get {
                for (var node = First; node != null; node = node.Next) {
                    if (node.Next == null)
                        return node;
                }
                return null;
            }
        }

        public int Count { get; private set; } = 0;

        public void Add(TElement element) => AddLast(element);

        public void AddFirst(TElement element)
        {
            var newNode = new Node(element);
            if (First == null) {
                First = newNode;
            } else {
                Debug.Assert(Last != null);
                newNode.Next = First;
                First = newNode;
            }
            Count++;
        }

        public void AddLast(TElement element)
        {
            var newNode = new Node(element);
            if (First == null) {
                First = newNode;
            } else {
                Debug.Assert(Last != null);
                Last.Next = newNode;
            }
            Count++;
        }

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

            var newNode = new Node(element);

            var previousNode = PreviousNode(node);
            if (previousNode == null) {
                AddFirst(element);
            } else {
                Debug.Assert(object.ReferenceEquals(previousNode.Next, node));
                newNode.Next = node;
                previousNode.Next = newNode;
            }
            Count++;
        }

        public Node? Find(TElement element)
        {
            for (var node = First; node != null; node = node.Next) {
                if (node.Value.Equals(element))
                    return node;
            }
            return null;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            for (var node = First; node != null; node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Node? PreviousNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            for (var previousNode = First; previousNode != null; previousNode = previousNode.Next) {
                if (object.ReferenceEquals(previousNode.Next, node))
                    return previousNode;
            }
            return null;
        }
    }
}
