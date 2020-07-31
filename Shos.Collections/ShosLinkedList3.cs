using System;
using System.Collections;
using System.Collections.Generic;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    // 双方向連結リスト
    public class ShosLinkedList3<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            public TValue Value    { get; internal set; }
            public Node?  Previous { get; internal set; } = null;
            public Node?  Next     { get; internal set; } = null;

            public Node()
            {}

            public Node(TValue value) => this.Value = value;
        }

        struct Enumerator : IEnumerator<TValue>
        {
            readonly ShosLinkedList3<TValue> linkedList;
            Node currentNode;

            public TValue Current => currentNode.Value;

            object IEnumerator.Current => Current;

            internal Enumerator(ShosLinkedList3<TValue> linkedList)
            {
                this.linkedList = linkedList;
                currentNode = linkedList.top;
            }

            public void Dispose()
            { }

            public bool MoveNext()
            {
                if (currentNode.Next == linkedList.bottom)
                    return false;
                currentNode = currentNode.Next;
                return true;
            }

            public void Reset()
                => currentNode = linkedList.top;
        }

        Node top    = new Node();
        Node bottom = new Node();

        public Node? First => Count == 0 ? null : top.Next;
        public Node? Last  => Count == 0 ? null : bottom.Previous;

        public int Count { get; private set; } = 0;

        public ShosLinkedList3() => Connect(top, bottom);

        public ShosLinkedList3(IEnumerable<TValue> values) : this()
        {
            foreach (var value in values)
                AddLast(value);
        }

        public void Add(TValue value) => AddLast(value);
        public void AddFirst(TValue value) => AddAfter (top   , value);
        public void AddLast (TValue value) => AddBefore(bottom, value);

        public void AddAfter(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();

            Insert(node, node.Next, new Node(value));
        }

        public void AddBefore(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();

            Insert(node.Previous, node, new Node(value));
        }

        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            RemoveNode(node);
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

            for (var node = top.Next; node != bottom; node = node.Next)
                array[index++] = node.Value;
        }

        public bool Contains(TValue value)
            => Find(value) != null;

        public Node? Find(TValue value)
        {
            for (var node = top.Next; node != bottom; node = node.Next) {
                if (defaultEqualityComparer.Equals(node.Value, value))
                    return node;
            }
            return null;
        }

        public Node? FindLast(TValue value)
        {
            for (var node = bottom.Previous; node != top; node = node.Previous) {
                if (defaultEqualityComparer.Equals(node.Value, value))
                    return node;
            }
            return null;
        }

        public IEnumerator<TValue> GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        static void Connect(Node node1, Node node2)
        {
            node1.Next     = node2;
            node2.Previous = node1;
        }

        void Insert(Node node1, Node node2, Node newNode)
        {
            Connect(node1  , newNode);
            Connect(newNode, node2  );
            Count++;
        }

        void RemoveNode(Node node)
        {
            Connect(node.Previous, node.Next);
            Count--;
        }

        static readonly EqualityComparer<TValue> defaultEqualityComparer = EqualityComparer<TValue>.Default;
    }
}
