using System;
using System.Collections;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    // 非ジェネリック版双方向連結リスト
    public class ShosLinkedList : IEnumerable
    {
        public class Node
        {
            internal object value;
            internal Node?  previous = null;
            internal Node?  next     = null;

            public object Value    => value;
            public Node?  Previous => previous;
            public Node?  Next     => next;

            public Node()
            {}

            public Node(object value) => this.value = value;
        }

        //struct Enumerator : IEnumerator
        //{
        //    readonly ShosLinkedList3 linkedList;
        //    Node                     currentNode;

        //    public object Current => currentNode.value;

        //    internal Enumerator(ShosLinkedList3 linkedList)
        //    {
        //        this.linkedList = linkedList;
        //        currentNode     = linkedList.top;
        //    }

        //    public bool MoveNext()
        //    {
        //        if (currentNode.next == linkedList.bottom)
        //            return false;
        //        currentNode = currentNode.next;
        //        return true;
        //    }

        //    public void Reset()
        //        => currentNode = linkedList.top;
        //}

        Node top    = new Node();
        Node bottom = new Node();

        public Node? First => Count == 0 ? null : top.next;
        public Node? Last  => Count == 0 ? null : bottom.previous;

        public int Count { get; private set; } = 0;

        public ShosLinkedList() => Connect(top, bottom);

        public ShosLinkedList(IEnumerable values) : this()
        {
            foreach (var value in values)
                AddLast(value);
        }

        public void Add(object value) => AddLast(value);
        public void AddFirst(object value) => AddAfter (top   , value);
        public void AddLast (object value) => AddBefore(bottom, value);

        public void AddAfter(Node node, object value)
        {
            if (node == null)
                throw new ArgumentNullException();

            Insert(node, node.next, new Node(value));
        }

        public void AddBefore(Node node, object value)
        {
            if (node == null)
                throw new ArgumentNullException();

            Insert(node.previous, node, new Node(value));
        }

        public void Remove(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            RemoveNode(node);
        }

        public bool Remove(object value)
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

        public void CopyTo(object[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (index + Count > array.Length)
                throw new ArgumentException();

            for (var node = top.next; node != bottom; node = node.next)
                array[index++] = node.value;
        }

        public bool Contains(object value)
            => Find(value) != null;

        public Node? Find(object value)
        {
            for (var node = top.next; node != bottom; node = node.next) {
                if (node.value.Equals(value))
                    return node;
            }
            return null;
        }

        public Node? FindLast(object value)
        {
            for (var node = bottom.previous; node != top; node = node.previous) {
                if (node.value.Equals(value))
                    return node;
            }
            return null;
        }

        //public IEnumerator GetEnumerator()
        //    => new Enumerator(this);

        public IEnumerator GetEnumerator()
        {
            for (var node = top.next; node != bottom; node = node.next)
                yield return node.value;
        }

        static void Connect(Node node1, Node node2)
        {
            node1.next     = node2;
            node2.previous = node1;
        }

        void Insert(Node node1, Node node2, Node newNode)
        {
            Connect(node1  , newNode);
            Connect(newNode, node2  );
            Count++;
        }

        void RemoveNode(Node node)
        {
            Connect(node.previous, node.next);
            Count--;
        }
    }
}
