﻿using System.Linq;
using System.Collections.Generic;
using System.Collections;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;
using System;

namespace ClassLibraryTreeView
{
    public class CMContainer<T> : IEnumerable<T> where T : class, IIdentifiable, new()
    {
        protected Dictionary<string, T> dict = new Dictionary<string, T>();
        public virtual bool IsEmpty => dict.Count == 0;
        public List<T> Items() => dict.Values.ToList();
        public T Item(string id) => dict[id];
        public static object CreateCollection(Type collectionType, Type itemType)
        {
            var closedType = collectionType.MakeGenericType(itemType);
            // dict = (Dictionary<string, T>)Activator.CreateInstance(closedType);
            return Activator.CreateInstance(closedType);
        }
        public bool AddItem(string key, T item)
        {
            if (dict.ContainsKey(key))
            {
                return false;
            }
            dict.Add(key, item);
            return true;
        }
        public bool RemoveItem(string key)
        {
            if (dict.ContainsKey(key))
            {
                return false;
            }
            dict.Remove(key);
            return true;
        }
        public void Clear() => dict.Clear();
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in dict.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}