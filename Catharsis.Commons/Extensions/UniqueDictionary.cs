using System;
using System.Collections;
using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  internal sealed class UniqueDictionary<TKey, TValue> : UniqueCollection<TValue>, IDictionary<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> wrapped;


    public UniqueDictionary(IDictionary<TKey, TValue> wrapped) : base(new HashSet<TValue>())
    {
      foreach (var value in wrapped.Values)
      {
        this.Add(value);
      }

      this.wrapped = wrapped;
    }


    public ICollection<TKey> Keys
    {
      get
      {
        return this.wrapped.Keys;
      }
    }


    public ICollection<TValue> Values
    {
      get
      {
        return this.wrapped.Values;
      }
    }


    public void Add(TKey key, TValue value)
    {
      Add(value);
      this.wrapped.Add(key, value);
    }


    public void Add(KeyValuePair<TKey, TValue> item)
    {
      Add(item.Value);
      this.wrapped.Add(item);
    }


    public override void Clear()
    {
      base.Clear();
      this.wrapped.Clear();
    }


    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return this.wrapped.Contains(item);
    }


    public bool ContainsKey(TKey key)
    {
      return this.wrapped.ContainsKey(key);
    }


    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      this.wrapped.CopyTo(array, arrayIndex);
    }


    public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this.wrapped.GetEnumerator();
    }


    public bool Remove(TKey key)
    {
      if (this.ContainsKey(key))
      {
        this.Remove(this.wrapped[key]);
      }
      return this.wrapped.Remove(key);
    }


    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      Remove(item.Value);
      return this.wrapped.Remove(item);
    }


    public bool TryGetValue(TKey key, out TValue value)
    {
      return this.wrapped.TryGetValue(key, out value);
    }


    public TValue this[TKey key]
    {
      get
      {
        return this.wrapped[key];
      }
      set
      {
        if (this.ContainsKey(key) && !this[key].Equals(value) && this.Contains(value))
        {
          throw new InvalidOperationException(string.Format("Dictionary already contains specified element for key '{0}'. Cannot insert dublicate value", key));
        }

        Add(value);
        this.wrapped[key] = value;
      }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}