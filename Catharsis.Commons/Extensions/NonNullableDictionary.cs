using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  internal sealed class NonNullableDictionary<TKey, TValue> : NonNullableCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> wrapped;


    public NonNullableDictionary(IDictionary<TKey, TValue> wrapped) : base(wrapped)
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
      Assertion.NotNull(key);
      Assertion.NotNull(value);

      this.wrapped.Add(key, value);
    }


    public bool ContainsKey(TKey key)
    {
      Assertion.NotNull(key);

      return this.wrapped.ContainsKey(key);
    }


    public bool Remove(TKey key)
    {
      Assertion.NotNull(key);

      return this.wrapped.Remove(key);
    }


    public bool TryGetValue(TKey key, out TValue value)
    {
      Assertion.NotNull(key);

      return this.wrapped.TryGetValue(key, out value);
    }


    public TValue this[TKey key]
    {
      get
      {
        Assertion.NotNull(key);

        return this.wrapped[key];
      }
      set
      {
        Assertion.NotNull(key);
        Assertion.NotNull(value);

        this.wrapped[key] = value;
      }
    }
  }
}