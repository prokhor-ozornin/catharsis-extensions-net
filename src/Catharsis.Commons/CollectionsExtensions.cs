using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Math;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="ICollection{T}"/>
/// <seealso cref="IList{T}"/>
/// <seealso cref="IDictionary{TKey, TValue}"/>
/// <seealso cref="NameValueCollection"/>
public static class CollectionsExtensions
{
  /// <summary>
  ///   <para>Sequentially adds all elements, returned by the enumerator, to the specified collection.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="to">Collection to which elements are added.</param>
  /// <param name="from">Elements enumerator that provide elements for addition to the collection <paramref name="to"/>.</param>
  /// <returns>Reference to the supplied collection <paramref name="to"/>.</returns>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, IEnumerable<T> from)
  {
    from.ForEach(to.Add);
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, params T[] from) => to.AddRange(from as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static NameValueCollection AddRange(this NameValueCollection to, IEnumerable<(string Name, object Value)> from)
  {
    from.ForEach(tuple => to.Add(tuple.Name, tuple.Value?.ToInvariantString()));
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static NameValueCollection AddRange(this NameValueCollection to, params (string Name, object Value)[] from) => to.AddRange(from as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="from">Collection from which elements are removed.</param>
  /// <param name="sequence">Elements enumerator that provider elements for removal from the collection <see cref="from"/>.</param>
  /// <seealso cref="ICollection{T}.Remove(T)"/>
  public static ICollection<T> RemoveRange<T>(this ICollection<T> from, IEnumerable<T> sequence)
  {
    sequence.ForEach(element => from.Remove(element));
    return from;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="from"></param>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static ICollection<T> RemoveRange<T>(this ICollection<T> from, params T[] sequence) => from.RemoveRange(sequence as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="from"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  public static IList<T> RemoveRange<T>(this IList<T> from, int offset, int? count = null, Predicate<T> condition = null)
  {
    if (offset < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(offset));
    }

    if (count is < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(count));
    }

    for (var i = offset; i < offset + (count ?? from.Count - offset); i++)
    {
      if (condition == null || condition(from[i]))
      {
        from.RemoveAt(i);
      }
    }

    return from;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static IList<T> InsertRange<T>(this IList<T> to, int offset, IEnumerable<T> from)
  {
    if (offset < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(offset));
    }

    from.ForEach((index, element) => to.Insert(offset + index, element));

    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static IList<T> InsertRange<T>(this IList<T> to, int offset, params T[] from) => to.InsertRange(offset, from as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static ICollection<T> Empty<T>(this ICollection<T> collection)
  {
    collection.Clear();
    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static NameValueCollection Empty(this NameValueCollection collection)
  {
    collection.Clear();
    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="filler"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IList<T> Fill<T>(this IList<T> list, Func<T> filler, int? offset = null, int? count = null) => list.Fill(_ => filler(), offset, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="filler"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IList<T> Fill<T>(this IList<T> list, Func<int, T> filler, int? offset = null, int? count = null)
  {
    if (offset is < 0 || count is <= 0)
    {
      return list;
    }

    var fromIndex = offset ?? 0;
    var toIndex = Min(list.Count, count != null ? fromIndex + count.Value : list.Count - fromIndex);

    for (var index = fromIndex; index < toIndex; index++)
    {
      list[index] = filler(index);
    }

    return list;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="list"></param>
  /// <param name="firstIndex"></param>
  /// <param name="secondIndex"></param>
  /// <returns></returns>
  public static IList<T> Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
  {
    (list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);

    return list;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="random"></param>
  /// <returns></returns>
  public static IList<T> Randomize<T>(this IList<T> list, Random random = null)
  {
    if (list.Count <= 0)
    {
      return list;
    }

    var randomizer = random ?? new Random();

    var n = list.Count;

    while (n > 1)
    {
      n--;
      var k = randomizer.Next(n + 1);
      (list[k], list[n]) = (list[n], list[k]);
    }

    return list;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static ICollection<T> TryFinallyClear<T>(this ICollection<T> collection, Action<ICollection<T>> action) => collection.TryFinally(action, collection => collection.Clear());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static NameValueCollection TryFinallyClear(this NameValueCollection collection, Action<NameValueCollection> action) => collection.TryFinally(action, collection => collection.Clear());
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <returns></returns>
  /// <seealso cref="AsReadOnly{TKey,TValue}"/>
  public static IReadOnlyList<T> AsReadOnly<T>(this IList<T> list) => new ReadOnlyCollection<T>(list);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <returns></returns>
  /// <seealso cref="AsReadOnly{T}"/>
  public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull => new ReadOnlyDictionary<TKey, TValue>(dictionary);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => new(dictionary, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
  {
    var result = new Dictionary<string, string>();

    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key != null)
      {
        result.Add(key, collection.Get(i));
      }
    }

    return result;
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => new(dictionary, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey: notnull => comparer != null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => (pair.Key, pair.Value)) : dictionary.Select(pair => (pair.Key, pair.Value));

  /// <summary>
  /// 
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static IEnumerable<(string Name, string Value)> ToValueTuple(this NameValueCollection collection)
  {
    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key != null)
      {
        yield return (key, collection.Get(i));
      }
    }
  }
}