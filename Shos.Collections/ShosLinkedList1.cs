using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    // 単方向連結リスト
    public class ShosLinkedList1<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            internal TValue value;
            internal Node?  next = null;

            public TValue Value => value;
            public Node?  Next  => next;

            public Node()
            { }

            public Node(TValue value) => this.value = value;
        }

        public Node? First { get; private set; } = null;

        public Node? Last {
            get {
                for (var node = First; node != null; node = node.next) {
                    if (node.next == null)
                        return node;
                }
                return null;
            }
        }

        public int Count { get; private set; } = 0;

        public ShosLinkedList1()
        {}

        public ShosLinkedList1(IEnumerable<TValue> values)
        {
            foreach (var value in values)
                AddLast(value);
        }

        public void Add(TValue value) => AddLast(value);

        public void AddFirst(TValue value)
        {
            var newNode = new Node(value);
            if (First == null) {
                First = newNode;
            } else {
                Debug.Assert(Last != null);
                newNode.next = First;
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
                Last.next = newNode;
            }
            Count++;
        }

        public void AddAfter(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();


            var newNode = new Node(value);
            newNode.next = node.next;
            node.next = newNode;
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
                Debug.Assert(previousNode.next == node);
                newNode.next = node;
                previousNode.next = newNode;
            }
            Count++;
        }

        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            var previousNode = PreviousNode(node);
            if (previousNode == null) {
                First = node.next;
            } else {
                Debug.Assert(previousNode.next == node);
                previousNode.next = node.next;
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

            for (var node = First; node != null; node = node.next)
                array[index++] = node.value;
        }

        public bool Contains(TValue value)
            => Find(value) != null;

        public Node? Find(TValue value)
        {

            for (var node = First; node != null; node = node.next) {
                if (defaultEqualityComparer.Equals(node.value, value))
                    return node;
            }
            return null;
        }

        public Node? FindLast(TValue value)
        {
            Node? foundNode = null;
            for (var node = First; node != null; node = node.next) {
                if (defaultEqualityComparer.Equals(node.value, value))
                    foundNode = node;
            }
            return foundNode;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            for (var node = First; node != null; node = node.next)
                yield return node.value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Node? PreviousNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            for (var previousNode = First; previousNode != null; previousNode = previousNode.next) {
                if (previousNode.next == node)
                    return previousNode;
            }
            return null;
        }

        static readonly EqualityComparer<TValue> defaultEqualityComparer = EqualityComparer<TValue>.Default;
    }
}
