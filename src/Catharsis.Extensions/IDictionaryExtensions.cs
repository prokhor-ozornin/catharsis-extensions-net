#if NET8_0
using System.Collections.Frozen;
#endif

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="IDictionary{TKey, TValue}"/>
public static class IDictionaryExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="key"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TValue GetOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value = default) where TKey : notnull
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
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? new SortedList<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? new SortedDictionary<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FrozenDictionary<TKey, TValue> ToFrozenDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? FrozenDictionary.ToFrozenDictionary(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));
#endif
}