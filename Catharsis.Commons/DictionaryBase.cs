using System;
using System.Collections;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;


namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="KEY"></typeparam>
  /// <typeparam name="VALUE"></typeparam>
  public class DictionaryBase<KEY, VALUE> : IDictionary<KEY, VALUE>, IDictionary
  {
    private readonly bool allowCopies;
    private readonly bool allowNulls;
    private readonly bool isReadOnly;
    private readonly IDictionary<KEY, VALUE> items = new Dictionary<KEY, VALUE>();
    private readonly object syncRoot = new object();


    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="allowNulls"></param>
    /// <param name="allowCopies"></param>
    /// <param name="isReadOnly"></param>
    public DictionaryBase(bool allowNulls = true, bool allowCopies = true, bool isReadOnly = false)
    {
      this.allowNulls = allowNulls;
      if (!allowNulls)
      {
        this.items = this.items.AsNonNullable();
      }

      this.allowCopies = allowCopies;
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
    ///   <para>Implementation of <see cref="IDictionary.Count"/> property.</para>
    /// </summary>
    public virtual int Count
    {
      get
      {
        return this.items.Count;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.IsFixedSize"/> property.</para>
    /// </summary>
    public bool IsFixedSize
    {
      get
      {
        return this.IsReadOnly;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.IsReadOnly"/> property.</para>
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        return this.isReadOnly;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.IsSynchronized"/> property.</para>
    /// </summary>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Keys"/> property.</para>
    /// </summary>
    public virtual ICollection<KEY> Keys
    {
      get
      {
        return this.items.Keys;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.SyncRoot"/> property.</para>
    /// </summary>
    public object SyncRoot
    {
      get
      {
        return this.syncRoot;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Values"/> property.</para>
    /// </summary>
    public virtual ICollection<VALUE> Values
    {
      get
      {
        return this.items.Values;
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.Add(object, object)"/> method.</para>
    /// </summary>
    public void Add(object key, object value)
    {
      this.Add(key.To<KEY>(), value.To<VALUE>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/> method.</para>
    /// </summary>
    public virtual void Add(KEY key, VALUE value)
    {
      this.items.Add(key, value);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/> method.</para>
    /// </summary>
    public void Add(KeyValuePair<KEY, VALUE> item)
    {
      this.Add(item.Key, item.Value);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.Clear()"/> method.</para>
    /// </summary>
    public virtual void Clear()
    {
      this.items.Clear();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.Contains(object)"/> method.</para>
    /// </summary>
    public bool Contains(object key)
    {
      return this.ContainsKey(key.To<KEY>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Contains(KeyValuePair{TKey, TValue})"/> method.</para>
    /// </summary>
    public bool Contains(KeyValuePair<KEY, VALUE> item)
    {
      return this.Contains(item.Key);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/> method.</para>
    /// </summary>
    public virtual bool ContainsKey(KEY key)
    {
      return this.items.ContainsKey(key);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.CopyTo(Array, int)"/> method.</para>
    /// </summary>
    public void CopyTo(Array array, int index)
    {
      this.CopyTo(array.To<KeyValuePair<KEY, VALUE>[]>(), index);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.CopyTo(KeyValuePair{TKey, TValue}[], int)"/> method.</para>
    /// </summary>
    public virtual void CopyTo(KeyValuePair<KEY, VALUE>[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.GetEnumerator()"/> method.</para>
    /// </summary>
    public virtual IEnumerator<KeyValuePair<KEY, VALUE>> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.Remove(object)"/> method.</para>
    /// </summary>
    public void Remove(object key)
    {
      this.Remove(key.To<KEY>());
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Remove(TKey)"/> method.</para>
    /// </summary>
    public virtual bool Remove(KEY key)
    {
      return this.items.Remove(key);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.Remove(KeyValuePair{TKey, TValue})"/> method.</para>
    /// </summary>
    public bool Remove(KeyValuePair<KEY, VALUE> item)
    {
      return this.Remove(item.Key);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/> method.</para>
    /// </summary>
    public virtual bool TryGetValue(KEY key, out VALUE value)
    {
      return this.items.TryGetValue(key, out value);
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary.this[object]"/> indexer.</para>
    /// </summary>
    public object this[object key]
    {
      get
      {
        return this[key.To<KEY>()];
      }
      set
      {
        this[key.To<KEY>()] = value.To<VALUE>();
      }
    }


    /// <summary>
    ///   <para>Implementation of <see cref="IDictionary{TKey, TValue}.this[TKey]"/> indexer.</para>
    /// </summary>
    public virtual VALUE this[KEY key]
    {
      get
      {
        return this.items[key];
      }
      set
      {
        this.items[key] = value;
      }
    }

    
    ICollection IDictionary.Keys
    {
      get
      {
        var collection = new CollectionBase<KEY>(false, false, true);
        collection.AddAll(this.Keys);
        return collection;
      }
    }


    ICollection IDictionary.Values
    {
      get
      {
        var collection = new CollectionBase<VALUE>(this.allowNulls, this.allowCopies, true);
        collection.AddAll(this.Values);
        return collection;
      }
    }


    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      throw new NotSupportedException();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}