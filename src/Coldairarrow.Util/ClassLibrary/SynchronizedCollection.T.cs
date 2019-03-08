namespace System.Collections.Generic
{
    public class SynchronizedCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        private List<T> _list = new List<T>();

        public T this[int index] { get => ((IList<T>)_list)[index]; set => ((IList<T>)_list)[index] = value; }
        object IList.this[int index] { get => ((IList)_list)[index]; set => ((IList)_list)[index] = value; }

        public int Count => ((IList<T>)_list).Count;

        public bool IsReadOnly => ((IList<T>)_list).IsReadOnly;

        public bool IsFixedSize => ((IList)_list).IsFixedSize;

        public bool IsSynchronized => ((IList)_list).IsSynchronized;

        public object SyncRoot => ((IList)_list).SyncRoot;

        public void Add(T item)
        {
            ((IList<T>)_list).Add(item);
        }

        public int Add(object value)
        {
            return ((IList)_list).Add(value);
        }

        public void Clear()
        {
            ((IList<T>)_list).Clear();
        }

        public bool Contains(T item)
        {
            return ((IList<T>)_list).Contains(item);
        }

        public bool Contains(object value)
        {
            return ((IList)_list).Contains(value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)_list).CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)_list).CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)_list).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_list).IndexOf(item);
        }

        public int IndexOf(object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)_list).Insert(index, item);
        }

        public void Insert(int index, object value)
        {
            ((IList)_list).Insert(index, value);
        }

        public bool Remove(T item)
        {
            return ((IList<T>)_list).Remove(item);
        }

        public void Remove(object value)
        {
            ((IList)_list).Remove(value);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_list).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)_list).GetEnumerator();
        }
    }
}