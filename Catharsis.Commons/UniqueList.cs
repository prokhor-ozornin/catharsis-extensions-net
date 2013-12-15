using System;
using System.Collections;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  internal sealed class UniqueList<T> : UniqueCollection<T>, IList<T>, IList
  {
    private readonly IList<T> wrapped;


    public UniqueList(IList<T> wrapped) : base(wrapped)
    {
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
      var itemIndex = this.IndexOf(item);
      if (this.Contains(item) && itemIndex != index)
      {
        throw new InvalidOperationException(string.Format("Collection already contains specified element at position {0}. Cannot insert dublicate value", itemIndex));
      }

      this.wrapped.Insert(index, item);
      this.ElementsSet.Add(item);
    }


    public void Remove(object value)
    {
      base.Remove(value.To<T>());
    }


    public void RemoveAt(int index)
    {
      var item = this.wrapped[index];
      this.wrapped.RemoveAt(index);
      this.ElementsSet.Remove(item);
    }


    public T this[int index]
    {
      get
      {
        return this.wrapped[index];
      }
      set
      {
        var itemIndex = this.IndexOf(value);
        if (this.Contains(value) && itemIndex != index)
        {
          throw new InvalidOperationException(string.Format("List already contains specified element at position {0}. Cannot insert dublicate value", itemIndex));
        }

        this.wrapped[index] = value;
        this.ElementsSet.Add(value);
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