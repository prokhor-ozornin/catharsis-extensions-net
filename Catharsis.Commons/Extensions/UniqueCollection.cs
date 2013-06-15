using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Catharsis.Commons.Extensions
{
  internal class UniqueCollection<T> : ICollection<T>, ICollection
  {
    private readonly object syncRoot = new object();
    private readonly ICollection<T> wrapped;
    private ICollection<T> elementsSet = new HashSet<T>();


    public UniqueCollection(ICollection<T> wrapped)
    {
      Assertion.NotNull(wrapped);

      this.ElementsSet = new HashSet<T>(wrapped);
      this.wrapped = wrapped;
      this.wrapped.Clear();
      this.wrapped.AddAll(this.ElementsSet);
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
      if (this.Contains(item))
      {
        throw new InvalidOperationException("Collection already contains specified element. Cannot insert dublicate value");
      }

      this.wrapped.Add(item);
      this.elementsSet.Add(item);
    }


    public virtual void Clear()
    {
      this.wrapped.Clear();
      this.elementsSet.Clear();
    }


    public bool Contains(T item)
    {
      return this.elementsSet.Contains(item);
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
      if (this.wrapped.Remove(item))
      {
        this.elementsSet.Remove(item);
        return true;
      }
      return false;
    }


    protected ICollection<T> ElementsSet
    {
      get
      {
        return this.elementsSet;
      }
      private set
      {
        Assertion.NotNull(value);

        this.elementsSet = value;
      }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}