﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    // 双方向連結リスト
    public class ShosLinkedList<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            internal TValue value;
            internal Node?  previous = null;
            internal Node?  next     = null;

            public TValue Value    => value;
            public Node?  Previous => previous;
            public Node?  Next     => next;
            
            public Node()
            {}

            public Node(TValue value) => this.value = value;
        }

        //struct Enumerator : IEnumerator<TValue>
        //{
        //    readonly ShosLinkedList<TValue> linkedList;
        //    Node                            currentNode;

        //    public TValue Current => currentNode.value;

        //    object IEnumerator.Current => Current;

        //    internal Enumerator(ShosLinkedList<TValue> linkedList)
        //    {
        //        this.linkedList = linkedList;
        //        currentNode     = linkedList.top;
        //    }

        //    public void Dispose()
        //    {}

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

        int count            = 0;

        readonly Node top    = new Node();
        readonly Node bottom = new Node();

        public Node? First => count == 0 ? null : top   .next    ;
        public Node? Last  => count == 0 ? null : bottom.previous;

        public int Count => count;

        public ShosLinkedList() => Connect(top, bottom);

        public ShosLinkedList(IEnumerable<TValue> values) : this()
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

            Insert(node, node.next, new Node(value));
        }

        public void AddBefore(Node node, TValue value)
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
            if (count == 0)
                throw new InvalidOperationException();
            Remove(First);
        }

        public void RemoveLast()
        {
            if (count == 0)
                throw new InvalidOperationException();
            Remove(Last);
        }

        public void Clear()
        {
            Connect(top, bottom);
            count = 0;
        }

        public void CopyTo(TValue[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (index + count > array.Length)
                throw new ArgumentException();

            for (var node = top.next; node != bottom; node = node.next)
                array[index++] = node.value;
        }

        public bool Contains(TValue value)
            => Find(value) != null;

        public Node? Find(TValue value)
        {
            for (var node = top.next; node != bottom; node = node.next) {
                if (defaultEqualityComparer.Equals(node.value, value))
                    return node;
            }
            return null;
        }

        public Node? FindLast(TValue value)
        {
            for (var node = bottom.previous; node != top; node = node.previous) {
                if (defaultEqualityComparer.Equals(node.value, value))
                    return node;
            }
            return null;
        }

        //public IEnumerator<TValue> GetEnumerator()
        //    => new Enumerator(this);

        public IEnumerator<TValue> GetEnumerator()
        {
            for (var node = top.Next; node != bottom; node = node.Next)
                yield return node.Value;

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        static void Connect(Node node1, Node node2)
        {
            node1.next     = node2;
            node2.previous = node1;
        }

        void Insert(Node node1, Node node2, Node newNode)
        {
            Connect(node1  , newNode);
            Connect(newNode, node2  );
            count++;
        }

        void RemoveNode(Node node)
        {
            Connect(node.previous, node.next);
            count--;
        }

        static readonly EqualityComparer<TValue> defaultEqualityComparer = EqualityComparer<TValue>.Default;
    }
}
