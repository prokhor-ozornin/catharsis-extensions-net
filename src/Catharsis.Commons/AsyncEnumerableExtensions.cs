#if NET6_0
using System.Collections.Immutable;
#endif

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for asynchronous enumerators and sequences.</para>
/// </summary>
/// <seealso cref="IAsyncEnumerable{T}"/>
public static class AsyncEnumerableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<bool> IsEmpty<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => !await sequence.WithCancellation(cancellation).GetAsyncEnumerator().MoveNextAsync();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  public static async Task<IAsyncEnumerable<T>> ForEach<T>(this IAsyncEnumerable<T> sequence, Action<int, T> action, CancellationToken cancellation = default)
  {
    var index = 0;

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      cancellation.ThrowIfCancellationRequested();

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
  /// <param name="action"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IAsyncEnumerable<T>> ForEach<T>(this IAsyncEnumerable<T> sequence, Action<T> action, CancellationToken cancellation = default) => await sequence.ForEach((_, element) => action(element), cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableArray{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/>
  public static async Task<T[]> ToArray<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToLinkedList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="ToImmutableList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="Enumerable.ToList{TSource}(IEnumerable{TSource})"/>
  public static async Task<List<T>> ToList<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    var result = new List<T>();

    await foreach (var element in sequence.WithCancellation(cancellation))
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="ToImmutableList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="EnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/>
  public static async Task<LinkedList<T>> ToLinkedList<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    var result = new LinkedList<T>();

    await foreach (var element in sequence.WithCancellation(cancellation))
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
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T}?, CancellationToken)"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource}?)"/>
  public static async Task<HashSet<T>> ToHashSet<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T>? comparer = null, CancellationToken cancellation = default)
  {
    var result = new HashSet<T>(comparer);

    await foreach (var element in sequence.WithCancellation(cancellation))
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="SortedSet{T}"/>
  public static async Task<SortedSet<T>> ToSortedSet<T>(this IAsyncEnumerable<T> sequence, IComparer<T>? comparer = null, CancellationToken cancellation = default)
  {
    var result = new SortedSet<T>(comparer);

    await foreach (var element in sequence.WithCancellation(cancellation))
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Dictionary<TKey, TValue>> ToDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey>? comparer = null, CancellationToken cancellation = default) where TKey : notnull
  {
    var result = new Dictionary<TKey, TValue>(comparer);

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      result.Add(key(element), element);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Stack{T}"/>
  public static async Task<Stack<T>> ToStack<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    var result = new Stack<T>();

    await foreach (var element in sequence.WithCancellation(cancellation))
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="Queue{T}"/>
  public static async Task<Queue<T>> ToQueue<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default)
  {
    var result = new Queue<T>();

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      result.Enqueue(element);
    }

    return result;
  }

#if NET6_0
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
  public static async Task<PriorityQueue<TElement, TPriority>> ToPriorityQueue<TElement, TPriority>(this IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority>? comparer = null, CancellationToken cancellation = default)
  {
    var result = new PriorityQueue<TElement, TPriority>(comparer);

    await foreach (var (element, priority) in sequence.WithCancellation(cancellation))
    {
      result.Enqueue(element, priority);
    }

    return result;
  }
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<MemoryStream> ToMemoryStream(this IAsyncEnumerable<byte> sequence, CancellationToken cancellation = default)
  {
    var stream = new MemoryStream();

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      stream.WriteByte(element);
    }

    return stream.MoveToStart();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<MemoryStream> ToMemoryStream(this IAsyncEnumerable<byte[]> sequence, CancellationToken cancellation = default)
  {
    var stream = new MemoryStream();

    await foreach (var element in sequence.WithCancellation(cancellation))
    {
      await stream.WriteAsync(element, 0, element.Length, cancellation);
    }

    return stream.MoveToStart();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<ArraySegment<T>> ToArraySegment<T>(this IAsyncEnumerable<T> sequence, int? offset = null, int? count = null, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToArraySegment(offset, count);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Memory<T>> ToMemory<T>(this IAsyncEnumerable<T> sequence, int? offset = null, int? count = null, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToMemory(offset, count);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<ReadOnlyMemory<T>> ToReadOnlyMemory<T>(this IAsyncEnumerable<T> sequence, int? offset = null, int? count = null, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToReadOnlyMemory(offset, count);

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToArray{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="ImmutableArray.ToImmutableArray{TSource}(IEnumerable{TSource})"/>
  public static async Task<ImmutableArray<T>> ToImmutableArray<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToImmutableArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="ToLinkedList{T}(IAsyncEnumerable{T}, CancellationToken)"/>
  /// <seealso cref="ImmutableList.ToImmutableList{TSource}(IEnumerable{TSource})"/>
  public static async Task<ImmutableList<T>> ToImmutableList<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToImmutableList();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <seealso cref="ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T}?, CancellationToken)"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource})"/>
  /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource}?)"/>
  public static async Task<ImmutableHashSet<T>> ToImmutableHashSet<T>(this IAsyncEnumerable<T> sequence, IEqualityComparer<T>? comparer = null, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToImmutableHashSet(comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<ImmutableSortedSet<T>> ToImmutableSortedSet<T>(this IAsyncEnumerable<T> sequence, IComparer<T>? comparer = null, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToImmutableSortedSet(comparer);

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
  public static async Task<ImmutableDictionary<TKey, TValue>> ToImmutableDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey>? comparer = null, CancellationToken cancellation = default) where TKey : notnull => (await sequence.ToDictionary(key, comparer, cancellation)).ToImmutableDictionary();
  
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
  public static async Task<ImmutableSortedDictionary<TKey, TValue>> ToImmutableSortedDictionary<TKey, TValue>(this IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null, CancellationToken cancellation = default) where TKey : notnull => (await sequence.ToDictionary(key, null, cancellation)).ToImmutableSortedDictionary(keyComparer, valueComparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<ImmutableQueue<T>> ToImmutableQueue<T>(this IAsyncEnumerable<T> sequence, CancellationToken cancellation = default) => (await sequence.ToList(cancellation)).ToImmutableQueue();
#endif
}