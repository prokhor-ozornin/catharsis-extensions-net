#if NET8_0_OR_GREATER
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
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Set{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
  public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value = default) where TKey : notnull => dictionary is not null ? dictionary.TryGetValue(key, out var result) ? result : value : throw new ArgumentNullException(nameof(dictionary));

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
  /// <seealso cref="Get{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
  public static IDictionary<TKey, TValue> Set<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value = default) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    dictionary[key] = value;

    return dictionary;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="key"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="GetOrSet{TKey,TValue}(IDictionary{TKey, TValue}, TKey, Func{TValue})"/>
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
  /// <param name="key"></param>
  /// <param name="function"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="GetOrSet{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
  public static TValue GetOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> function) where TKey : notnull
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
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="key"></param>
  /// <param name="value"></param>
  /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/>
  public static IDictionary<TKey, TValue> With<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    dictionary[key] = value;

    return dictionary;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/>
  public static IDictionary<TKey, TValue> With<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey key, TValue value)> elements) where TKey : notnull
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
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
  /// <seealso cref="With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/>
  public static IDictionary<TKey, TValue> With<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params (TKey key, TValue value)[] elements) where TKey : notnull => dictionary.With(elements as IEnumerable<(TKey key, TValue value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without{TKey, TValue}(IDictionary{TKey, TValue}, TKey[])"/>
  public static IDictionary<TKey, TValue> Without<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> elements) where TKey : notnull
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
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="dictionary"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{TKey})"/>
  public static IDictionary<TKey, TValue> Without<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] elements) where TKey : notnull => dictionary.Without(elements as IEnumerable<TKey>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? new SortedList<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? new SortedDictionary<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToValueTuple()) : dictionary.Select(pair => pair.ToValueTuple());
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
  public static IEnumerable<Tuple<TKey, TValue>> ToTuple<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToTuple()) : dictionary.Select(pair => pair.ToTuple());
  }

#if NET8_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  public static FrozenDictionary<TKey, TValue> ToFrozenDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? FrozenDictionary.ToFrozenDictionary(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));
#endif
}