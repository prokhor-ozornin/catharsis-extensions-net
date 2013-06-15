using System.Collections;
using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  internal sealed class SizedList<T> : SizedCollection<T>, IList<T>, IList
  {
    private readonly IList<T> wrapped;


    public SizedList(IList<T> wrapped, int? maxSize = null, int minSize = 0) : base(wrapped, maxSize, minSize)
    {
      Assertion.NotNull(wrapped);

      this.wrapped = wrapped;
    }


    public bool IsFixedSize
    {
      get
      {
        return this.MaxSize != null;
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
      this.CheckMaxSize();
      this.wrapped.Insert(index, item);
    }


    public void Remove(object value)
    {
      base.Remove(value.To<T>());
    }


    public void RemoveAt(int index)
    {
      this.CheckMinSize();
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