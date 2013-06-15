using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Catharsis.Commons.Extensions
{
  internal class ImmutableCollection<T> : ICollection<T>, ICollection
  {
    private readonly object syncRoot = new object();
    private readonly ICollection<T> wrapped;


    public ImmutableCollection(ICollection<T> wrapped)
    {
      Assertion.NotNull(wrapped);

      this.wrapped = wrapped;
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
        return true;
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
      throw new NotSupportedException();
    }


    public void Clear()
    {
      throw new NotSupportedException();
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
      throw new NotSupportedException();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}