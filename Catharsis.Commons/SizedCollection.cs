using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons
{
  internal class SizedCollection<T> : ICollection<T>, ICollection
  {
    private readonly int? maxSize;
    private readonly int minSize;
    private readonly object syncRoot = new object();
    private readonly ICollection<T> wrapped;


    public SizedCollection(ICollection<T> wrapped, int? maxSize = null, int minSize = 0)
    {
      Assertion.NotNull(wrapped);

      if (maxSize != null && maxSize.Value < 0)
      {
        maxSize = 0;
      }

      if (minSize < 0)
      {
        minSize = 0;
      }

      if (maxSize != null && maxSize < minSize)
      {
        throw new ArgumentException(string.Format("Max size cannot be lesser than min size. Max size : {0}. Min size : {1}", maxSize, minSize));
      }

      if (this.Count < minSize)
      {
        throw new InvalidOperationException(string.Format("Specified minimum size is {0}, but collection size is {1}. Cannot create collection with specified minimum size", this.minSize, this.Count));
      }

      if (this.maxSize != null && this.Count > maxSize)
      {
        throw new InvalidOperationException(string.Format("Specified maximum size is {0}, but collection size is {1}. Cannot create collection with specified maximum size", this.maxSize, this.Count));
      }

      this.wrapped = wrapped;
      this.maxSize = maxSize;
      this.minSize = minSize;
    }


    public int Count
    {
      get
      {
        return this.wrapped.Count;
      }
    }


    public bool IsReadOnly
    {
      get
      {
        return this.wrapped.IsReadOnly;
      }
    }


    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }


    public object SyncRoot
    {
      get
      {
        return this.syncRoot;
      }
    }


    public void Add(T item)
    {
      this.CheckMaxSize();
      this.wrapped.Add(item);
    }


    public void Clear()
    {
      if (this.minSize > 0)
      {
        throw new InvalidOperationException(string.Format("Collection's minimum size is {0} and is positive. Cannot remove all elements", this.minSize));
      }

      this.wrapped.Clear();
    }


    public bool Contains(T item)
    {
      return this.wrapped.Contains(item);
    }


    public void CopyTo(Array array, int index)
    {
      this.CopyTo(array.Cast<T>().To<T[]>(), index);
    }


    public void CopyTo(T[] array, int arrayIndex)
    {
      Assertion.NotNull(array);

      this.wrapped.CopyTo(array, arrayIndex);
    }


    public IEnumerator<T> GetEnumerator()
    {
      return this.wrapped.GetEnumerator();
    }


    public bool Remove(T item)
    {
      this.CheckMinSize();
      return this.wrapped.Remove(item);
    }


    protected int? MaxSize
    {
      get
      {
        return this.maxSize;
      }
    }


    protected int MinSize
    {
      get
      {
        return this.minSize;
      }
    }


    protected void CheckMaxSize()
    {
      if (this.maxSize != null && this.Count >= this.maxSize)
      {
        throw new InvalidOperationException(string.Format("Collection's maximum size of {0} is reached. Cannot add new elements", this.maxSize));
      }
    }


    protected void CheckMinSize()
    {
      if (this.Count <= this.minSize)
      {
        throw new InvalidOperationException(string.Format("Collection's minimum size of {0} is reached. Cannot remove elements", this.minSize));
      }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}