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
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty<T>(this IAsyncEnumerable<T> sequence) => sequence?.IsEmptyAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<bool> IsEmptyAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? !await sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false).GetAsyncEnumerator().MoveNextAsync() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<T> ForEach<T>(this IAsyncEnumerable<T> sequence, Action<T> action)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return sequence.ForEachAsync(action).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IAsyncEnumerable<T>> ForEachAsync<T>(this IAsyncEnumerable<T> sequence, Action<T> action, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return await sequence.ForEachAsync((_, element) => action(element), cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<T> ForEach<T>(this IAsyncEnumerable<T> sequence, Action<int, T> action)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return sequence.ForEachAsync(action).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IAsyncEnumerable<T>> ForEachAsync<T>(this IAsyncEnumerable<T> sequence, Action<int, T> action, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (action is null) throw new ArgumentNullException(nameof(action));

    cancellation.ThrowIfCancellationRequested();

    var index = 0;

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      action(index, element);
      index++;
    }

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<T> WithEnforcedCancellation<T>(this IAsyncEnumerable<T> sequence, [EnumeratorCancellation] CancellationToken cancellation)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      cancellation.ThrowIfCancellationRequested();

      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> ToEnumerable<T>(this IAsyncEnumerable<T> sequence) => sequence is not null ? new AsyncEnumerable<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T[] ToArray<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToArrayAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableArrayAsync{T}"/>
  /// <seealso cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).AsArray() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static List<T> ToList<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToListAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Enumerable.ToList{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new List<T>();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static LinkedList<T> ToLinkedList<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToLinkedListAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToListAsync{T}"/>
  /// <seealso cref="ToImmutableListAsync{T}"/>
  /// <seealso cref="IEnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<LinkedList<T>> ToLinkedListAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new LinkedList<T>();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.AddLast(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToReadOnlyListAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IReadOnlyList<T>> ToReadOnlyListAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? await sequence.ToListAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HashSet<T> ToHashSet<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence?.ToHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableHashSetAsync{T}"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new HashSet<T>(comparer);

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Add(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SortedSet<T> ToSortedSet<T>(this IAsyncEnumerable<T> sequence, IComparer<T> comparer = null) => sequence?.ToSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="SortedSet{T}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<SortedSet<T>> ToSortedSetAsync<T>(this IAsyncEnumerable<T> sequence, IComparer<T> comparer = null, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new SortedSet<T>(comparer);

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
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
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return sequence.ToDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    cancellation.ThrowIfCancellationRequested();

    var result = new Dictionary<TKey, TValue>(comparer);

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
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
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return sequence.ToReadOnlyDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IReadOnlyDictionary<TKey, TValue>> ToReadOnlyDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return await sequence.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(T item, int index)> ToValueTuple<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToValueTupleAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return sequence.ToValueTupleAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<(T item, int index)>> ToValueTupleAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<(TKey Key, TValue Value)>> ToValueTupleAsync<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple(key, comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Stack<T> ToStack<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToStackAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Stack{T}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Stack<T>> ToStackAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new Stack<T>();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Push(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Queue<T> ToQueue<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToQueueAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Queue{T}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Queue<T>> ToQueueAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new Queue<T>();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Enqueue(element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MemoryStream ToMemoryStream(this IAsyncEnumerable<byte> sequence) => sequence?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IAsyncEnumerable<byte> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      stream.WriteByte(element);
    }

    return stream.MoveToStart();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MemoryStream ToMemoryStream(this IAsyncEnumerable<byte[]> sequence) => sequence?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IAsyncEnumerable<byte[]> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    await foreach (var element in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
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
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence?.ToReadOnlySetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IReadOnlySet<T>> ToReadOnlySetAsync<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => sequence is not null ? await sequence.ToHashSetAsync(comparer, cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null) => sequence?.ToPriorityQueueAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="PriorityQueue{TElement, TPriority}"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<PriorityQueue<TElement, TPriority>> ToPriorityQueueAsync<TElement, TPriority>(this IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var result = new PriorityQueue<TElement, TPriority>(comparer);

    await foreach (var (element, priority) in sequence.WithEnforcedCancellation(cancellation).ConfigureAwait(false))
    {
      result.Enqueue(element, priority);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableArray<T> ToImmutableArray<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToImmutableArrayAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToArrayAsync{T}"/>
  /// <seealso cref="ImmutableArray.ToImmutableArray{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableArray<T>> ToImmutableArrayAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableArray() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableList<T> ToImmutableList<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToImmutableListAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToListAsync{T}"/>
  /// <seealso cref="ToLinkedListAsync{T}"/>
  /// <seealso cref="ImmutableList.ToImmutableList{TSource}(IEnumerable{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableList<T>> ToImmutableListAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableList() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableHashSet<T> ToImmutableHashSet<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence?.ToImmutableHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToHashSetAsync{T}"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableHashSet<T>> ToImmutableHashSetAsync<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableHashSet(comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableSortedSet<T> ToImmutableSortedSet<T>(this IAsyncEnumerable<T> sequence, IComparer<T> comparer = null) => sequence?.ToImmutableSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableSortedSet<T>> ToImmutableSortedSetAsync<T>(this IAsyncEnumerable<T> sequence, IComparer<T> comparer = null, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableSortedSet(comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return sequence.ToImmutableDictionaryAsync(key, comparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <typeparam name="TKey"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableDictionary<TKey, TValue>> ToImmutableDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await sequence.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false)).ToImmutableDictionary();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="keyComparer"></param>
  /// <param name="valueComparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return sequence.ToImmutableSortedDictionaryAsync(key, keyComparer, valueComparer).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="keyComparer"></param>
  /// <param name="valueComparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return (await sequence.ToDictionaryAsync(key, null, cancellation).ConfigureAwait(false)).ToImmutableSortedDictionary(keyComparer, valueComparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IAsyncEnumerable<T> sequence) => sequence?.ToImmutableQueueAsync().Result ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<ImmutableQueue<T>> ToImmutableQueueAsync<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => sequence is not null ? (await sequence.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableQueue() : throw new ArgumentNullException(nameof(sequence));
#endif

  private sealed class AsyncEnumerable<T> : IEnumerable<T>
  {
    private readonly IAsyncEnumerator<T> enumerator;

    public AsyncEnumerable(IAsyncEnumerable<T> sequence)
    {
      if (sequence is null) throw new ArgumentNullException(nameof(sequence));

      enumerator = sequence.GetAsyncEnumerator();
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