using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Catharsis.Commons.Extensions
{
  internal class ConstrainedCollection<T> : ICollection<T>, ICollection
  {
    private readonly Predicate<T> predicate;
    private readonly object syncRoot = new object();
    private readonly ICollection<T> wrapped;


    public ConstrainedCollection(ICollection<T> wrapped, Predicate<T> predicate)
    {
      Assertion.NotNull(wrapped);
      Assertion.NotNull(predicate);

      this.wrapped = wrapped;
      this.predicate = predicate;
      foreach (var item in this.wrapped)
      {
        if (!predicate(item))
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
      this.CheckElement(item);
      this.wrapped.Add(item);
    }


    public void Clear()
    {
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
      this.wrapped.CopyTo(array, arrayIndex);
    }


    public IEnumerator<T> GetEnumerator()
    {
      return this.wrapped.GetEnumerator();
    }


    public bool Remove(T item)
    {
      return this.wrapped.Remove(item);
    }


    protected void CheckElement(T item)
    {
      if (!this.predicate(item))
      {
        throw new ArgumentException(string.Format("Element {0} does not match collection predicate criteria and cannot be added", item), "item");
      }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}