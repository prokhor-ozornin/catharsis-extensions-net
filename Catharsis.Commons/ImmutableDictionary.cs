using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  internal sealed class ImmutableDictionary<TKey, TValue> : ImmutableCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> wrapped;


    public ImmutableDictionary(IDictionary<TKey, TValue> wrapped) : base(wrapped)
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
      throw new NotSupportedException();
    }


    public bool ContainsKey(TKey key)
    {
      return this.wrapped.ContainsKey(key);
    }


    public bool Remove(TKey key)
    {
      throw new NotSupportedException();
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
        throw new NotSupportedException();
      }
    }
  }
}