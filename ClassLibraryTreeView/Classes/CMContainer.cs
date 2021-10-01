using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace ClassLibraryTreeView
{
    public class CMContainer<T> : IEnumerable<T>
    {
        protected Dictionary<string, T> dict = new Dictionary<string, T>();
        public virtual bool IsEmpty => dict.Count == 0;
        public List<T> Items() => dict.Values.ToList();
        public T Item(string id) => dict[id];
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
