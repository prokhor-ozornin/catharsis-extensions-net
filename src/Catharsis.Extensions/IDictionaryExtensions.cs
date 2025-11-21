#if NET10_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="IDictionary{TKey, TValue}"/>
public static class IDictionaryExtensions
{
  /// <param name="dictionary"></param>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary) where TKey : notnull
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Set{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
    public TValue Get(TKey key, TValue value = default) => dictionary is not null ? dictionary.TryGetValue(key, out var result) ? result : value : throw new ArgumentNullException(nameof(dictionary));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Get{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
    public IDictionary<TKey, TValue> Set(TKey key, TValue value = default)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      dictionary[key] = value;

      return dictionary;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetOrSet{TKey,TValue}(IDictionary{TKey, TValue}, TKey, Func{TValue})"/>
    public TValue GetOrSet(TKey key, TValue value = default)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      if (dictionary.TryGetValue(key, out var result))
      {
        return result;
      }

      dictionary[key] = value;

      return value;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="function"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetOrSet{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
    public TValue GetOrSet(TKey key, Func<TValue> function)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      if (dictionary.TryGetValue(key, out var result))
      {
        return result;
      }
    
      var value = function();

      dictionary[key] = value;

      return value;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/>
    public IDictionary<TKey, TValue> With(TKey key, TValue value)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      dictionary[key] = value;

      return dictionary;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/>
    public IDictionary<TKey, TValue> With(IEnumerable<(TKey key, TValue value)> elements)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
      if (elements is null) throw new ArgumentNullException(nameof(elements));

      foreach (var element in elements)
      {
        dictionary[element.key] = element.value;
      }

      return dictionary;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
    /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/>
    public IDictionary<TKey, TValue> With(params (TKey key, TValue value)[] elements) => dictionary.With(elements as IEnumerable<(TKey key, TValue value)>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{TKey, TValue}(IDictionary{TKey, TValue}, TKey[])"/>
    public IDictionary<TKey, TValue> Without(IEnumerable<TKey> elements)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
      if (elements is null) throw new ArgumentNullException(nameof(elements));

      foreach (var element in elements)
      {
        dictionary.Remove(element);
      }

      return dictionary;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{TKey})"/>
    public IDictionary<TKey, TValue> Without(params TKey[] elements) => dictionary.Without(elements as IEnumerable<TKey>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public SortedList<TKey, TValue> ToSortedList(IComparer<TKey> comparer = null) => dictionary is not null ? new SortedList<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public SortedDictionary<TKey, TValue> ToSortedDictionary(IComparer<TKey> comparer = null) => dictionary is not null ? new SortedDictionary<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public IEnumerable<(TKey Key, TValue Value)> ToValueTuple(IComparer<TKey> comparer = null)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToValueTuple()) : dictionary.Select(pair => pair.ToValueTuple());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public IEnumerable<Tuple<TKey, TValue>> ToTuple(IComparer<TKey> comparer = null)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToTuple()) : dictionary.Select(pair => pair.ToTuple());
    }

  #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public FrozenDictionary<TKey, TValue> ToFrozenDictionary(IEqualityComparer<TKey> comparer = null) => dictionary is not null ? FrozenDictionary.ToFrozenDictionary(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));
  #endif
  }
}