using System;
using System.Collections;
using System.Collections.Generic;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    public class ShosLinkedList2<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            public TValue Value { get; set; }
            public Node? Next { get; set; } = null;

            public Node()
            { }

            public Node(TValue value) => Value = value;
        }

        Node top    = new Node();
        Node bottom = new Node();

        public Node? First => Count == 0 ? null : top.Next;
        public Node? Last  => Count == 0 ? null : PreviousNode(bottom);

        public int Count { get; private set; } = 0;

        public ShosLinkedList2() => Connect(top, bottom);

        public void Add(TValue element) => AddLast(element);
        public void AddFirst(TValue element) => AddAfter(top, element);
        public void AddLast(TValue element) => AddBefore(bottom, element);

        public void AddAfter(Node node, TValue element)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode  = new Node(element);
            newNode.Next = node.Next;
            node.Next    = newNode;
            Count++;
        }

        public void AddBefore(Node node, TValue element)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode             = new Node(element);
            newNode.Next            = node;
            PreviousNode(node).Next = newNode;
            Count++;
        }

        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            var previousNode = PreviousNode(node);
            previousNode.Next = node.Next;
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
            Connect(top, bottom);
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

            for (var node = top.Next; !ReferenceEquals(node, bottom); node = node.Next)
                array[index++] = node.Value;
        }

        public bool Contains(TValue value)
        {
            for (var node = top.Next; !ReferenceEquals(node, bottom); node = node.Next) {
                if (node.Value.Equals(value))
                    return true;
            }
            return false;
        }

        public Node? Find(TValue element)
        {
            for (var node = top.Next; !ReferenceEquals(node, bottom); node = node.Next) {
                if (node.Value.Equals(element))
                    return node;
            }
            return null;
        }

        public Node? FindLast(TValue value)
        {
            Node? foundNode = null;
            for (var node = top.Next; !ReferenceEquals(node, bottom); node = node.Next) {
                if (node.Value.Equals(value))
                    foundNode = node;
            }
            return foundNode;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            for (var node = top.Next; !ReferenceEquals(node, bottom); node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Node PreviousNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            for (var previousNode = top; previousNode != null; previousNode = previousNode.Next) {
                if (ReferenceEquals(previousNode.Next, node))
                    return previousNode;
            }
            throw new InvalidOperationException();
        }

        void Connect(Node node1, Node node2)
            => node1.Next = node2;
    }
}
