using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    public class ShosLinkedList<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            public TValue Value { get; set; }
            public Node? Next { get; set; } = null;

            public Node()
            { }

            public Node(TValue value) => Value = value;
        }

        public Node? First { get; private set; } = null;

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

        public void Add(TValue value) => AddLast(value);

        public void AddFirst(TValue value)
        {
            var newNode = new Node(value);
            if (First == null) {
                First = newNode;
            } else {
                Debug.Assert(Last != null);
                newNode.Next = First;
                First = newNode;
            }
            Count++;
        }

        public void AddLast(TValue value)
        {
            var newNode = new Node(value);
            if (First == null) {
                First = newNode;
            } else {
                Debug.Assert(Last != null);
                Last.Next = newNode;
            }
            Count++;
        }

        public void AddAfter(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();


            var newNode = new Node(value);
            newNode.Next = node.Next;
            node.Next = newNode;
            Count++;
        }

        public void AddBefore(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode = new Node(value);

            var previousNode = PreviousNode(node);
            if (previousNode == null) {
                AddFirst(value);
            } else {
                Debug.Assert(ReferenceEquals(previousNode.Next, node));
                newNode.Next = node;
                previousNode.Next = newNode;
            }
            Count++;
        }

        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            var previousNode = PreviousNode(node);
            if (previousNode == null) {
                First = node.Next;
            } else {
                Debug.Assert(ReferenceEquals(previousNode.Next, node));
                previousNode.Next = node.Next;
            }
            Count--;
        }

        public bool Remove(TValue value)
        {
            var node = Find(value);
            if (node == null)
                return false;
            Remove(node);
            return true;
        }

        public void RemoveFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            Remove(First);
        }

        public void RemoveLast()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            Remove(Last);
        }

        public void Clear()
        {
            First = null;
            Count = 0;
        }

        public void CopyTo(TValue[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (index + Count > array.Length)
                throw new ArgumentException();

            for (var node = First; node != null; node = node.Next)
                array[index++] = node.Value;
        }

        public bool Contains(TValue value)
        {
            for (var node = First; node != null; node = node.Next) {
                if (node.Value.Equals(value))
                    return true;
            }
            return false;
        }

        public Node? Find(TValue value)
        {
            for (var node = First; node != null; node = node.Next) {
                if (node.Value.Equals(value))
                    return node;
            }
            return null;
        }

        public Node? FindLast(TValue value)
        {
            Node? foundNode = null;
            for (var node = First; node != null; node = node.Next) {
                if (node.Value.Equals(value))
                    foundNode = node;
            }
            return foundNode;
        }

        public IEnumerator<TValue> GetEnumerator()
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
                if (ReferenceEquals(previousNode.Next, node))
                    return previousNode;
            }
            return null;
        }
    }
}
