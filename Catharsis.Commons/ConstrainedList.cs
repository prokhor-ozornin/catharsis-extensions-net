using System;
using System.Collections;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  internal sealed class ConstrainedList<T> : ConstrainedCollection<T>, IList<T>, IList
  {
    private readonly IList<T> wrapped;


    public ConstrainedList(IList<T> wrapped, Predicate<T> predicate) : base(wrapped, predicate)
    {
      Assertion.NotNull(wrapped);

      this.wrapped = wrapped;
    }


    public bool IsFixedSize
    {
      get
      {
        return this.wrapped.To<IList>().IsFixedSize;
      }
    }


    public int Add(object value)
    {
      var item = value.To<T>();
      base.Add(item);
      return this.IndexOf(item);
    }


    public bool Contains(object value)
    {
      return base.Contains(value.To<T>());
    }


    public int IndexOf(object value)
    {
      return this.IndexOf(value.To<T>());
    }


    public int IndexOf(T item)
    {
      return this.wrapped.IndexOf(item);
    }


    public void Insert(int index, object value)
    {
      this.Insert(index, value.To<T>());
    }


    public void Insert(int index, T item)
    {
      this.CheckElement(item);
      this.wrapped.Insert(index, item);
    }


    public void Remove(object value)
    {
      base.Remove(value.To<T>());
    }


    public void RemoveAt(int index)
    {
      this.wrapped.RemoveAt(index);
    }


    public T this[int index]
    {
      get
      {
        return this.wrapped[index];
      }
      set
      {
        this.CheckElement(value);
        this.wrapped[index] = value;
      }
    }

    
    object IList.this[int index]
    {
      get
      {
        return this[index];
      }
      set
      {
        this[index] = value.To<T>();
      }
    }
  }
}