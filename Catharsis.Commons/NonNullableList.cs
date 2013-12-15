using System.Collections;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  internal sealed class NonNullableList<T> : NonNullableCollection<T>, IList<T>, IList
  {
    private readonly IList<T> wrapped;


    public NonNullableList(IList<T> wrapped) : base(wrapped)
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
      Assertion.NotNull(value);

      var item = value.To<T>();
      base.Add(item);
      return this.IndexOf(item);
    }


    public bool Contains(object value)
    {
      Assertion.NotNull(value);

      return base.Contains(value.To<T>());
    }


    public int IndexOf(object value)
    {
      Assertion.NotNull(value);

      return this.IndexOf(value.To<T>());
    }


    public int IndexOf(T item)
    {
      Assertion.NotNull(item);

      return this.wrapped.IndexOf(item);
    }


    public void Insert(int index, object value)
    {
      Assertion.NotNull(value);

      this.Insert(index, value.To<T>());
    }


    public void Insert(int index, T item)
    {
      Assertion.NotNull(item);

      this.wrapped.Insert(index, item);
    }


    public void Remove(object value)
    {
      Assertion.NotNull(value);

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
        Assertion.NotNull(value);

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
        Assertion.NotNull(value);

        this[index] = value.To<T>();
      }
    }
  }
}