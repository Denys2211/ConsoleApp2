﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class MyCollection<T> : IEnumerable<T>
    {
        private T[] list;

        public int Count { get; private set; } = 1;

        public MyCollection()
        {

            list = new T[4];

        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => new MyCollectionEnumerator<T>(list);
        
        public T this[int i]
        {
            get
            {
                return list[i];
            }
            set
            {
                list[i] = value;

            }
        }
        public void Add(T value)
        {
            if (Count == list.Length)
            {
                Array.Resize<T>(ref list, list.Length + 1);
            }

            list[Count] = value;
            Count++;
        }
        
        public void RemoveAt(int index) 
        {

            list = list.Where((val, idx) => idx != index).ToArray();

            Count--;
        }
    }
}
