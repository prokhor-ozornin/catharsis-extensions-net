using System.Collections;
using System.Runtime.CompilerServices;

#if NET8_0
using System.Collections.Immutable;
#endif

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for asynchronous enumerators and sequences.</para>
/// </summary>
/// <seealso cref="IAsyncEnumerable{T}"/>
public static class IAsyncEnumerableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IAsyncEnumerable{T}, Action{int, T})"/>
  public static IAsyncEnumerable<T> ForEach<T>(this IAsyncEnumerable<T> enumerable, Action<T> action)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return enumerable.ForEachAsync(action).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEachAsync{T}(IAsyncEnumerable{T}, Action{int, T}, CancellationToken)"/>
  public static async Task<IAsyncEnumerable<T>> ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Action<T> action, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return await enumerable.ForEachAsync((_, element) => action(element), cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IAsyncEnumerable{T}, Action{T})"/>
  public static IAsyncEnumerable<T> ForEach<T>(this IAsyncEnumerable<T> enumerable, Action<int, T> action)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return enumerable.ForEachAsync(action).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEachAsync{T}(IAsyncEnumerable{T}, Action{T}, CancellationToken)"/>
  public static async Task<IAsyncEnumerable<T>> ForEachAsync<T>(this IAsyncEnumerable<T> enumerable, Action<int, T> action, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (action is null) throw new ArgumentNullException(nameof(action));

    cancellation.ThrowIfCancellationRequested();

    var index = 0;

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      action(index, element);
      index++;
    }

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static async IAsyncEnumerable<T> WithEnforcedCancellation<T>(this IAsyncEnumerable<T> enumerable, [EnumeratorCancellation] CancellationToken cancellation)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    await foreach (var element in enumerable.WithCancellation(cancellation))
    {
      cancellation.ThrowIfCancellationRequested();

      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty{T}(IAsyncEnumerable{T})"/>
  public static bool IsUnset<T>(this IAsyncEnumerable<T> enumerable) => enumerable is null || enumerable.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEmptyAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="IsUnset{T}(IAsyncEnumerable{T})"/>
  public static bool IsEmpty<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.IsEmptyAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEmpty{T}(IAsyncEnumerable{T})"/>
  public static async Task<bool> IsEmptyAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? !await enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false).GetAsyncEnumerator().MoveNextAsync() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<T> ToEnumerable<T>(this IAsyncEnumerable<T> enumerable) => enumerable is not null ? new AsyncEnumerable<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static T[] ToArray<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToArrayAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableArrayAsync{T}"/>
  /// <seealso cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToArray{T}(IAsyncEnumerable{T})"/>
  public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).AsArray() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static List<T> ToList<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Enumerable.ToList{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToList{T}(IAsyncEnumerable{T})"/>
  public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new List<T>();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToLinkedListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static LinkedList<T> ToLinkedList<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToLinkedListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToListAsync{T}"/>
  /// <seealso cref="ToImmutableListAsync{T}"/>
  /// <seealso cref="IEnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToLinkedList{T}(IAsyncEnumerable{T})"/>
  public static async Task<LinkedList<T>> ToLinkedListAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new LinkedList<T>();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.AddLast(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">f <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlyListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToReadOnlyListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlyList{T}(IAsyncEnumerable{T})"/>
  public static async Task<IReadOnlyList<T>> ToReadOnlyListAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? await enumerable.ToListAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
  public static HashSet<T> ToHashSet<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable?.ToHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableHashSetAsync{T}"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
  public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new HashSet<T>(comparer);

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/>
  public static SortedSet<T> ToSortedSet<T>(this IAsyncEnumerable<T> enumerable, IComparer<T> comparer = null) => enumerable?.ToSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="SortedSet{T}"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/>
  public static async Task<SortedSet<T>> ToSortedSetAsync<T>(this IAsyncEnumerable<T> enumerable, IComparer<T> comparer = null, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new SortedSet<T>(comparer);

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return enumerable.ToDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
  public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    cancellation.ThrowIfCancellationRequested();

    var result = new Dictionary<TKey, TValue>(comparer);

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(key(element), element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlyDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return enumerable.ToReadOnlyDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlyDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
  public static async Task<IReadOnlyDictionary<TKey, TValue>> ToReadOnlyDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return await enumerable.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToValueTupleAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static IEnumerable<(T item, int index)> ToValueTuple<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToValueTupleAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToValueTupleAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, CancellationToken)"/>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return enumerable.ToValueTupleAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToValueTuple{T}(IAsyncEnumerable{T})"/>
  public static async Task<IEnumerable<(T item, int index)>> ToValueTupleAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToValueTuple{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/>
  public static async Task<IEnumerable<(TKey Key, TValue Value)>> ToValueTupleAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple(key, comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToStackAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static Stack<T> ToStack<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToStackAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Stack{T}"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToStack{T}(IAsyncEnumerable{T})"/>
  public static async Task<Stack<T>> ToStackAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new Stack<T>();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Push(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static Queue<T> ToQueue<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToQueueAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Queue{T}"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToQueue{T}(IAsyncEnumerable{T})"/>
  public static async Task<Queue<T>> ToQueueAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new Queue<T>();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Enqueue(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IAsyncEnumerable{byte}, CancellationToken)"/>
  public static MemoryStream ToMemoryStream(this IAsyncEnumerable<byte> enumerable) => enumerable?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IAsyncEnumerable{byte})"/>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IAsyncEnumerable<byte> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      stream.WriteByte(element);
    }

    return stream.MoveToStart();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IAsyncEnumerable{byte[]}, CancellationToken)"/>
  public static MemoryStream ToMemoryStream(this IAsyncEnumerable<byte[]> enumerable) => enumerable?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IAsyncEnumerable{byte[]})"/>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IAsyncEnumerable<byte[]> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    await foreach (var element in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      await stream.WriteAsync(element, 0, element.Length, cancellation).ConfigureAwait(false);
    }

    return stream.MoveToStart();
  }

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlySetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable?.ToReadOnlySetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToReadOnlySet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
  public static async Task<IReadOnlySet<T>> ToReadOnlySetAsync<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? await enumerable.ToHashSetAsync(comparer, cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToPriorityQueueAsync{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority}, CancellationToken)"/>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IAsyncEnumerable<(TElement Element, TPriority Priority)> enumerable, IComparer<TPriority> comparer = null) => enumerable?.ToPriorityQueueAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="PriorityQueue{TElement, TPriority}"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority})"/>
  public static async Task<PriorityQueue<TElement, TPriority>> ToPriorityQueueAsync<TElement, TPriority>(this IAsyncEnumerable<(TElement Element, TPriority Priority)> enumerable, IComparer<TPriority> comparer = null, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var result = new PriorityQueue<TElement, TPriority>(comparer);

    await foreach (var (element, priority) in enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Enqueue(element, priority);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static ImmutableArray<T> ToImmutableArray<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToImmutableArrayAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToArrayAsync{T}"/>
  /// <seealso cref="ImmutableArray.ToImmutableArray{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableArray{T}(IAsyncEnumerable{T})"/>
  public static async Task<ImmutableArray<T>> ToImmutableArrayAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableArray() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static ImmutableList<T> ToImmutableList<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToImmutableListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToListAsync{T}"/>
  /// <seealso cref="ToLinkedListAsync{T}"/>
  /// <seealso cref="ImmutableList.ToImmutableList{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableList{T}(IAsyncEnumerable{T})"/>
  public static async Task<ImmutableList<T>> ToImmutableListAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableList() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
  public static ImmutableHashSet<T> ToImmutableHashSet<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable?.ToImmutableHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToHashSetAsync{T}"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
  public static async Task<ImmutableHashSet<T>> ToImmutableHashSetAsync<T>(this IAsyncEnumerable<T> enumerable, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableHashSet(comparer) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/>
  public static ImmutableSortedSet<T> ToImmutableSortedSet<T>(this IAsyncEnumerable<T> enumerable, IComparer<T> comparer = null) => enumerable?.ToImmutableSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/>
  public static async Task<ImmutableSortedSet<T>> ToImmutableSortedSetAsync<T>(this IAsyncEnumerable<T> enumerable, IComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableSortedSet(comparer) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
  public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return enumerable.ToImmutableDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <typeparam name="TKey"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
  public static async Task<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await enumerable.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false)).ToImmutableDictionary();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="keyComparer"></param>
  /// <param name="valueComparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableSortedDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue}, CancellationToken)"/>
  public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return enumerable.ToImmutableSortedDictionaryAsync(key, keyComparer, valueComparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="key"></param>
  /// <param name="keyComparer"></param>
  /// <param name="valueComparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue})"/>
  public static async Task<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await enumerable.ToDictionaryAsync(key, null, cancellation).ConfigureAwait(false)).ToImmutableSortedDictionary(keyComparer, valueComparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IAsyncEnumerable<T> enumerable) => enumerable?.ToImmutableQueueAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToImmutableQueue{T}(IAsyncEnumerable{T})"/>
  public static async Task<ImmutableQueue<T>> ToImmutableQueueAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableQueue() : throw new ArgumentNullException(nameof(enumerable));
#endif

  private sealed class AsyncEnumerable<T> : IEnumerable<T>
  {
    private readonly IAsyncEnumerator<T> enumerator;

    public AsyncEnumerable(IAsyncEnumerable<T> enumerable)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      enumerator = enumerable.GetAsyncEnumerator();
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<T>
    {
      private readonly AsyncEnumerable<T> parent;

      public Enumerator(AsyncEnumerable<T> parent) => this.parent = parent;

      public T Current => parent.enumerator.Current;

      public bool MoveNext() => parent.enumerator.MoveNextAsync().Result;

      public void Reset() => throw new NotSupportedException();

      public async void Dispose() { await parent.enumerator.DisposeAsync().ConfigureAwait(false); }

      object IEnumerator.Current => Current;
    }
  }
}