using System;
using System.Collections;
using System.Collections.Generic;

namespace XHSystem
{
    public class XHCollection : IXHCollection
    {
        private IList<XHDescriptor> _descriptors = new List<XHDescriptor>();
        public XHDescriptor this[int index] 
        { 
            get => this._descriptors[index]; 
            set => this._descriptors[index] = value;  
        }

        public int Count => this._descriptors.Count;

        public bool IsReadOnly => false;

        public void Add(XHDescriptor item)
        {          
            this._descriptors.Add(item);
        }

        public void Clear()
        {
            this._descriptors.Clear();
        }

        public bool Contains(XHDescriptor item)
        {
            return this._descriptors.Contains(item);
        }

        public void CopyTo(XHDescriptor[] array, int arrayIndex)
        {
            this._descriptors.CopyTo(array, arrayIndex);
        }

        public IEnumerator<XHDescriptor> GetEnumerator()
        {
            return (IEnumerator<XHDescriptor>)this._descriptors.GetEnumerator();
        }

        public int IndexOf(XHDescriptor item)
        {
            return this._descriptors.IndexOf(item);
        }

        public void Insert(int index, XHDescriptor item)
        {
            this._descriptors.Insert(index, item);
        }

        public bool Remove(XHDescriptor item)
        {
            return this._descriptors.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._descriptors.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }
    }
}