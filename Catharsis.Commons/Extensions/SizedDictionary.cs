using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  internal sealed class SizedDictionary<TKey, TValue> : SizedCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> wrapped;


    public SizedDictionary(IDictionary<TKey, TValue> wrapped, int? maxSize = null, int minSize = 0) : base(wrapped, maxSize, minSize)
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
      this.CheckMaxSize();

      this.wrapped.Add(key, value);
    }


    public bool ContainsKey(TKey key)
    {
      return this.wrapped.ContainsKey(key);
    }


    public bool Remove(TKey key)
    {
      this.CheckMinSize();

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
        if (!this.ContainsKey(key))
        {
          this.CheckMaxSize();
        }

        this.wrapped[key] = value;
      }
    }
  }
}