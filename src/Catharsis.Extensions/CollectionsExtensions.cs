using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Math;

namespace Catharsis.Extensions;

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
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, IEnumerable<T> from)
  {
    if (to is null) throw new ArgumentNullException(nameof(to));
    if (from is null) throw new ArgumentNullException(nameof(from));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, params T[] from) => to.AddRange(from as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection AddRange(this NameValueCollection to, IEnumerable<(string Name, object Value)> from)
  {
    if (to is null) throw new ArgumentNullException(nameof(to));
    if (from is null) throw new ArgumentNullException(nameof(from));

    from.ForEach(tuple => to.Add(tuple.Name, tuple.Value?.ToInvariantString()));
    
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection AddRange(this NameValueCollection to, params (string Name, object Value)[] from) => to.AddRange(from as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="from">Collection from which elements are removed.</param>
  /// <param name="sequence">Elements enumerator that provider elements for removal from the collection <see cref="from"/>.</param>
  /// <seealso cref="ICollection{T}.Remove(T)"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> RemoveRange<T>(this ICollection<T> from, IEnumerable<T> sequence)
  {
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> RemoveRange<T>(this IList<T> from, int offset, int? count = null, Predicate<T> condition = null)
  {
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var i = offset; i < offset + (count ?? from.Count - offset); i++)
    {
      if (condition is null || condition(from[i]))
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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> InsertRange<T>(this IList<T> to, int offset, IEnumerable<T> from)
  {
    if (to is null) throw new ArgumentNullException(nameof(to));
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> InsertRange<T>(this IList<T> to, int offset, params T[] from) => to.InsertRange(offset, from as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> Empty<T>(this ICollection<T> collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    collection.Clear();

    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection Empty(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> Fill<T>(this IList<T> list, Func<T> filler, int? offset = null, int? count = null) => list?.Fill(_ => filler(), offset, count) ?? throw new ArgumentNullException(nameof(filler));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="filler"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> Fill<T>(this IList<T> list, Func<int, T> filler, int? offset = null, int? count = null)
  {
    if (list is null) throw new ArgumentNullException(nameof(list));
    if (filler is null) throw new ArgumentNullException(nameof(filler));
    if (offset is < 0 || offset > list.Count) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));
    
    var fromIndex = offset ?? 0;
    var toIndex = Min(list.Count, count is not null ? fromIndex + count.Value : list.Count - fromIndex);

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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IList<T> Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
  {
    if (list is null) throw new ArgumentNullException(nameof(list));
    if (firstIndex < 0 || firstIndex > list.Count) throw new ArgumentOutOfRangeException(nameof(firstIndex));
    if (secondIndex < 0 || secondIndex > list.Count) throw new ArgumentOutOfRangeException(nameof(secondIndex));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static IList<T> Randomize<T>(this IList<T> list, Random random = null)
  {
    if (list is null) throw new ArgumentNullException(nameof(list));

    if (list.Count == 0)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> TryFinallyClear<T>(this ICollection<T> collection, Action<ICollection<T>> action)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return collection.TryFinally(action, collection => collection.Clear());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection TryFinallyClear(this NameValueCollection collection, Action<NameValueCollection> action)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return collection.TryFinally(action, collection => collection.Clear());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <returns></returns>
  /// <seealso cref="AsReadOnly{TKey,TValue}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyList<T> AsReadOnly<T>(this IList<T> list) => list is not null ? new ReadOnlyCollection<T>(list) : throw new ArgumentNullException(nameof(list));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <returns></returns>
  /// <seealso cref="AsReadOnly{T}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull => dictionary is not null ? new ReadOnlyDictionary<TKey, TValue>(dictionary) : throw new ArgumentNullException(nameof(dictionary));

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
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    var result = new Dictionary<string, string>();

    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key is not null)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull => dictionary is not null ? new SortedDictionary<TKey, TValue>(dictionary, comparer) : throw new ArgumentNullException(nameof(dictionary));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => (pair.Key, pair.Value)) : dictionary.Select(pair => (pair.Key, pair.Value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(string Name, string Value)> ToValueTuple(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key is not null)
      {
        yield return (key, collection.Get(i));
      }
    }
  }
}