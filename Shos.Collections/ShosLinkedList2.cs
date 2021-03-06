﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Shos.Collections
{
    // 参考: LinkedList<T> クラス (System.Collections.Generic) | Microsoft Docs
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.collections.generic.linkedlist-1?view=netcore-3.1

    // 単方向連結リスト (ダミーノード版)
    public class ShosLinkedList2<TValue> : IEnumerable<TValue>
    {
        public class Node
        {
            public TValue Value { get; internal set; }
            public Node?  Next  { get; internal set; } = null;

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

        public ShosLinkedList2(IEnumerable<TValue> values) : this()
        {
            foreach (var value in values)
                AddLast(value);
        }

        public void Add(TValue value) => AddLast(value);
        public void AddFirst(TValue value) => AddAfter(top, value);
        public void AddLast(TValue value) => AddBefore(bottom, value);

        public void AddAfter(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode  = new Node(value);
            newNode.Next = node.Next;
            node.Next    = newNode;
            Count++;
        }

        public void AddBefore(Node node, TValue value)
        {
            if (node == null)
                throw new ArgumentNullException();

            var newNode             = new Node(value);
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
            Node? foundNode = null;
            for (var node = top.Next; node != bottom; node = node.Next) {
                if (defaultEqualityComparer.Equals(node.Value, value))
                    foundNode = node;
            }
            return foundNode;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            for (var node = top.Next; node != bottom; node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Node PreviousNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();

            for (var previousNode = top; previousNode != null; previousNode = previousNode.Next) {
                if (previousNode.Next == node)
                    return previousNode;
            }
            throw new InvalidOperationException();
        }

        static void Connect(Node node1, Node node2)
            => node1.Next = node2;

        static readonly EqualityComparer<TValue> defaultEqualityComparer = EqualityComparer<TValue>.Default;
    }
}
