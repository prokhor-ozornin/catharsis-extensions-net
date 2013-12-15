using System;
using System.Collections;
using System.Collections.Generic;


namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class CollectionBase<T> : ICollection<T>, ICollection
  {
    private readonly bool isReadOnly;
    private readonly ICollection<T> items;
    private readonly object syncRoot = new object();


    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="allowNulls"></param>
    /// <param name="allowCopies"></param>
    /// <param name="isReadOnly"></param>
    public CollectionBase(bool allowNulls = true, bool allowCopies = true, bool isReadOnly = false)
    {
      this.items = allowCopies ? (ICollection<T>) new List<T>() : new HashSet<T>();

      if (!allowNulls)
      {
        this.items = this.items.AsNonNullable();
      }

      this.isReadOnly = isReadOnly;
      if (isReadOnly)
      {
        this.items = this.items.AsImmutable();
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection.Count"/> property.</para>
    /// </summary>
    public virtual int Count
    {
      get
      {
        return this.items.Count;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.IsReadOnly"/> property.</para>
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        return this.isReadOnly;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection.IsSynchronized"/> property.</para>
    /// </summary>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection.SyncRoot"/> property.</para>
    /// </summary>
    public object SyncRoot
    {
      get
      {
        return this.syncRoot;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.Add(T)"/> method.</para>
    /// </summary>
    public virtual void Add(T item)
    {
      this.items.Add(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.Clear()"/> method.</para>
    /// </summary>
    public virtual void Clear()
    {
      this.items.Clear();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.Contains(T)"/> method.</para>
    /// </summary>
    public virtual bool Contains(T item)
    {
      return this.items.Contains(item);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection.CopyTo(Array, int)"/> method.</para>
    /// </summary>
    public void CopyTo(Array array, int index)
    {
      this.CopyTo(array.To<T[]>(), index);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.CopyTo(T[], int)"/> method.</para>
    /// </summary>
    public virtual void CopyTo(T[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.GetEnumerator()"/> method.</para>
    /// </summary>
    public virtual IEnumerator<T> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="ICollection{T}.Remove(T)"/> method.</para>
    /// </summary>
    public virtual bool Remove(T item)
    {
      return this.items.Remove(item);
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}