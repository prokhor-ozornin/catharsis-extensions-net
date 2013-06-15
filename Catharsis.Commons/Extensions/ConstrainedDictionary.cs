using System;
using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  internal class ConstrainedDictionary<TKey, TValue> : ConstrainedCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> wrapped;


    public ConstrainedDictionary(IDictionary<TKey, TValue> wrapped, Predicate<KeyValuePair<TKey, TValue>> predicate) : base(wrapped, predicate)
    {
      Assertion.NotNull(wrapped);

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
      this.CheckElement(new KeyValuePair<TKey, TValue>(key, value));
      this.wrapped.Add(key, value);
    }


    public bool ContainsKey(TKey key)
    {
      return this.wrapped.ContainsKey(key);
    }


    public bool Remove(TKey key)
    {
      return this.wrapped.Remove(key);
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
        this.CheckElement(new KeyValuePair<TKey, TValue>(key, value));
        this.wrapped[key] = value;
      }
    }
  }
}