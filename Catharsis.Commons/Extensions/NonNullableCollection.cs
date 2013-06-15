using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Catharsis.Commons.Extensions
{
  internal class NonNullableCollection<T> : ICollection<T>, ICollection
  {
    private readonly object syncRoot = new object();
    private readonly ICollection<T> wrapped;


    public NonNullableCollection(ICollection<T> wrapped)
    {
      Assertion.NotNull(wrapped);

      this.wrapped = wrapped;

      foreach (var item in this.wrapped)
      {
        if (item == null)
        {
          this.wrapped.Remove(item);
        }
      }
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
      Assertion.NotNull(item);

      this.wrapped.Add(item);
    }


    public void Clear()
    {
      this.wrapped.Clear();
    }


    public bool Contains(T item)
    {
      Assertion.NotNull(item);

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
      Assertion.NotNull(item);

      return this.wrapped.Remove(item);
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}