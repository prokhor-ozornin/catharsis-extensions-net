using System;
using System.Collections;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  internal sealed class ImmutableList<T> : ImmutableCollection<T>, IList<T>, IList
  {
    private readonly IList<T> wrapped;


    public ImmutableList(IList<T> wrapped) : base(wrapped)
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
      throw new NotSupportedException();
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
      throw new NotSupportedException();
    }


    public void Insert(int index, T item)
    {
      throw new NotSupportedException();
    }


    public void Remove(object value)
    {
      throw new NotSupportedException();
    }


    public void RemoveAt(int index)
    {
      throw new NotSupportedException();
    }


    public T this[int index]
    {
      get
      {
        return this.wrapped[index];
      }
      set
      {
        throw new NotSupportedException();
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
        throw new NotSupportedException();
      }
    }
  }
}