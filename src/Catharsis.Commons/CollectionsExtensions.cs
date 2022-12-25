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
  public static ICollection<T> AddAll<T>(this ICollection<T> to, IEnumerable<T> from)
  {
    from.ForEach(to.Add);
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public static NameValueCollection AddAll(this NameValueCollection to, IEnumerable<(string? Name, object? Value)> from)
  {
    from.ForEach(tuple => to.Add(tuple.Name, tuple.Value?.ToStringInvariant()));
    return to;
  }

  /// <summary>
  ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="to">Collection from which elements are removed.</param>
  /// <param name="from">Elements enumerator that provider elements for removal from the collection <see cref="to"/>.</param>
  /// <seealso cref="ICollection{T}.Remove(T)"/>
  public static ICollection<T> RemoveAll<T>(this ICollection<T> to, IEnumerable<T> from)
  {
    from.ForEach(item => to.Remove(item));
    return to;
  }

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
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static ICollection<T> UseTemporarily<T>(this ICollection<T> collection, Action<ICollection<T>> action) => collection.UseFinally(action, collection => collection.Clear());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static NameValueCollection UseTemporarily(this NameValueCollection collection, Action<NameValueCollection> action) => collection.UseFinally(action, collection => collection.Clear());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <returns></returns>
  public static IList<T> Randomize<T>(this IList<T> list)
  {
    if (list.Count <= 0)
    {
      return list;
    }

    var random = new Random();

    var n = list.Count;

    while (n > 1)
    {
      n--;
      var k = random.Next(n + 1);
      (list[k], list[n]) = (list[n], list[k]);
    }

    return list;
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
  public static IList<T> Fill<T>(this IList<T> list, Func<int, T> filler, int? offset = null, int? count = null)
  {
    if (offset is < 0 || count is <= 0)
    {
      return list;
    }

    var from = offset ?? 0;
    var to = Min(list.Count, count != null ? from + count.Value : list.Count - from); 

    for (var index = from; index < to; index++)
    {
      list[index] = filler(index);
    }

    return list;
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
  /// <param name="list"></param>
  /// <param name="first"></param>
  /// <param name="second"></param>
  /// <returns></returns>
  public static IList<T> Swap<T>(this IList<T> list, int first, int second)
  {
    (list[first], list[second]) = (list[second], list[first]);

    return list;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  /// <seealso cref="Discard{T}(IList{T}, Range, Predicate{T}?)"/>
  /// <seealso cref="Discard{T}(IList{T}, int)"/>
  /// <seealso cref="DiscardLast{T}(IList{T}, int)"/>
  public static IList<T> Discard<T>(this IList<T> list, int from, int to, Predicate<T>? condition = null)
  {
    for (var i = Max(0, from); i < Min(list.Count, to); i++)
    {
      if (condition == null || condition(list[i]))
      {
        list.RemoveAt(i);
      } 
    }

    return list;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="range"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  /// <seealso cref="Discard{T}(IList{T}, int, int, Predicate{T}?)"/>
  /// <seealso cref="Discard{T}(IList{T}, int)"/>
  /// <seealso cref="DiscardLast{T}(IList{T}, int)"/>
  public static IList<T> Discard<T>(this IList<T> list, Range range, Predicate<T>? condition = null) => list.Discard(range.Start.Value, range.End.Value, condition);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <seealso cref="Discard{T}(IList{T}, int, int, Predicate{T}?)"/>
  /// <seealso cref="Discard{T}(IList{T}, Range, Predicate{T}?)"/>
  /// <seealso cref="DiscardLast{T}(IList{T}, int)"/>
  public static IList<T> Discard<T>(this IList<T> list, int count) => list.Discard(..count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <seealso cref="Discard{T}(IList{T}, int, int, Predicate{T}?)"/>
  /// <seealso cref="Discard{T}(IList{T}, Range, Predicate{T}?)"/>
  /// <seealso cref="Discard{T}(IList{T}, int)"/>
  public static IList<T> DiscardLast<T>(this IList<T> list, int count) => list.Discard((list.Count - count)..count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <returns></returns>
  /// <seealso cref="ReadOnly{TKey, TValue}(IDictionary{TKey, TValue})"/>
  public static IList<T> ReadOnly<T>(this IList<T> list) => new ReadOnlyCollection<T>(list);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <returns></returns>
  /// <seealso cref="ReadOnly{T}(IList{T})"/>
  public static IDictionary<TKey, TValue> ReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull => new ReadOnlyDictionary<TKey, TValue>(dictionary);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IDictionary<TKey, TValue> Sorted<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey>? comparer = null) where TKey : notnull => new SortedDictionary<TKey, TValue>(dictionary, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IDictionary<TKey, TValue?> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, IComparer<TKey>? comparer = null) where TKey : notnull => new SortedList<TKey, TValue?>(dictionary, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IEnumerable<(TKey Key, TValue? Value)> ToTuple<TKey, TValue>(this IDictionary<TKey, TValue?> dictionary, IComparer<TKey>? comparer = null) where TKey: notnull => comparer != null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => (pair.Key, pair.Value)) : dictionary.Select(pair => (pair.Key, pair.Value));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static IDictionary<string, string?> ToDictionary(this NameValueCollection collection)
  {
    var result = new Dictionary<string, string?>();

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
  /// 
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  public static IEnumerable<(string Name, string? Value)> ToTuple(this NameValueCollection collection)
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