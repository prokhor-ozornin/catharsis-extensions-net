using System;
using System.Collections;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;


namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ListBase<T> : IList<T>, IList
  {
    private readonly bool isReadOnly;
    private readonly IList<T> items = new List<T>();
    private readonly object syncRoot = new object();


    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="allowNulls"></param>
    /// <param name="allowCopies"></param>
    public ListBase(bool allowNulls = true, bool allowCopies = true, bool isReadOnly = false)
    {
      if (!allowNulls)
      {
        this.items = this.items.AsNonNullable();
      }
      if (!allowCopies)
      {
        this.items = this.items.AsUnique();
      }

      this.isReadOnly = isReadOnly;
      if (isReadOnly)
      {
        this.items = this.items.AsImmutable();
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Count"/> property.</para>
    /// </summary>
    public virtual int Count
    {
      get
      {
        return this.items.Count;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.IsFixedSize"/> property.</para>
    /// </summary>
    public bool IsFixedSize
    {
      get
      {
        return this.IsReadOnly;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.IsReadOnly"/> property.</para>
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        return this.isReadOnly;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.IsSynchronized"/> property.</para>
    /// </summary>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.SyncRoot"/> property.</para>
    /// </summary>
    public object SyncRoot
    {
      get
      {
        return this.syncRoot;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Add(object)"/> method.</para>
    /// </summary>
    public int Add(object value)
    {
      this.Add(value.To<T>());
      return this.IndexOf(value.To<T>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.Add(T)"/> method.</para>
    /// </summary>
    public virtual void Add(T item)
    {
      this.items.Add(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Clear()"/> method.</para>
    /// </summary>
    public virtual void Clear()
    {
      this.items.Clear();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Contains(object)"/> method.</para>
    /// </summary>
    public bool Contains(object value)
    {
      return this.Contains(value.To<T>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.Contains(T)"/> method.</para>
    /// </summary>
    public virtual bool Contains(T item)
    {
      return this.items.Contains(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.CopyTo(Array, int)"/> method.</para>
    /// </summary>
    public void CopyTo(Array array, int index)
    {
      this.CopyTo(array.To<T[]>(), index);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.CopyTo(T[], int)"/> method.</para>
    /// </summary>
    public virtual void CopyTo(T[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.GetEnumerator()"/> method.</para>
    /// </summary>
    public virtual IEnumerator<T> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.IndexOf(object)"/> method.</para>
    /// </summary>
    public int IndexOf(object value)
    {
      return this.IndexOf(value.To<T>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.IndexOf(T)"/> method.</para>
    /// </summary>
    public virtual int IndexOf(T item)
    {
      return this.items.IndexOf(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Insert(int, object)"/> method.</para>
    /// </summary>
    public void Insert(int index, object value)
    {
      this.Insert(index, value.To<T>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.Insert(int, T)"/> method.</para>
    /// </summary>
    public virtual void Insert(int index, T item)
    {
      this.items.Insert(index, item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList.Remove(object)"/> method.</para>
    /// </summary>
    public void Remove(object value)
    {
      this.Remove(value.To<T>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.Remove(T)"/> method.</para>
    /// </summary>
    public virtual bool Remove(T item)
    {
      return this.items.Remove(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.RemoveAt(int)"/> method.</para>
    /// </summary>
    public virtual void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IList{T}.this[int]"/> indexer.</para>
    /// </summary>
    public virtual T this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.items[index] = value;
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


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}