using System.Collections;
using System.Runtime.CompilerServices;

#if NET10_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for asynchronous enumerators and sequences.</para>
/// </summary>
/// <seealso cref="IAsyncEnumerable{T}"/>
public static class IAsyncEnumerableExtensions
{
  /// <param name="enumerable"></param>
  extension(IAsyncEnumerable<byte> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStreamAsync(IAsyncEnumerable{byte}, CancellationToken)"/>
    public MemoryStream ToMemoryStream() => enumerable?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStream(IAsyncEnumerable{byte})"/>
    public async ValueTask<MemoryStream> ToMemoryStreamAsync(CancellationToken cancellation = default)
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
  }

  /// <param name="enumerable"></param>
  extension(IAsyncEnumerable<byte[]> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStreamAsync(IAsyncEnumerable{byte[]}, CancellationToken)"/>
    public MemoryStream ToMemoryStream() => enumerable?.ToMemoryStreamAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStream(IAsyncEnumerable{byte[]})"/>
    public async ValueTask<MemoryStream> ToMemoryStreamAsync(CancellationToken cancellation = default)
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
  }

  /// <param name="enumerable"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(IAsyncEnumerable<T> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty{T}(IAsyncEnumerable{T})"/>
    public bool IsUnset => enumerable is null || enumerable.IsEmpty;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmptyAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    /// <seealso cref="IsUnset{T}(IAsyncEnumerable{T})"/>
    public bool IsEmpty => enumerable?.IsEmptyAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEach{T}(IAsyncEnumerable{T}, Action{int, T})"/>
    public IAsyncEnumerable<T> ForEach(Action<T> action)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return enumerable.ForEachAsync(action).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEachAsync{T}(IAsyncEnumerable{T}, Action{int, T}, CancellationToken)"/>
    public async Task<IAsyncEnumerable<T>> ForEachAsync(Action<T> action, CancellationToken cancellation = default)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return await enumerable.ForEachAsync((_, element) => action(element), cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEach{T}(IAsyncEnumerable{T}, Action{T})"/>
    public IAsyncEnumerable<T> ForEach(Action<int, T> action)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return enumerable.ForEachAsync(action).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEachAsync{T}(IAsyncEnumerable{T}, Action{T}, CancellationToken)"/>
    public async Task<IAsyncEnumerable<T>> ForEachAsync(Action<int, T> action, CancellationToken cancellation = default)
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
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public async IAsyncEnumerable<T> WithEnforcedCancellation([EnumeratorCancellation] CancellationToken cancellation)
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
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmpty{T}(IAsyncEnumerable{T})"/>
    public async Task<bool> IsEmptyAsync(CancellationToken cancellation = default) => enumerable is not null ? !await enumerable.WithEnforcedCancellation(cancellation).ConfigureAwait(false).GetAsyncEnumerator().MoveNextAsync() : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> ToEnumerable() => enumerable is not null ? new AsyncEnumerable<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public T[] ToArray(CancellationToken cancellation = default) => enumerable?.ToArrayAsync(cancellation).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public List<T> ToList() => enumerable?.ToListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToLinkedListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public LinkedList<T> ToLinkedList() => enumerable?.ToLinkedListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="ToListAsync{T}"/>
    /// <seealso cref="ToImmutableListAsync{T}"/>
    /// <seealso cref="IEnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToLinkedList{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<LinkedList<T>> ToLinkedListAsync(CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">f <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlyListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public IReadOnlyList<T> ToReadOnlyList() => enumerable?.ToReadOnlyListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlyList{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<IReadOnlyList<T>> ToReadOnlyListAsync(CancellationToken cancellation = default) => enumerable is not null ? await enumerable.ToListAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
    public HashSet<T> ToHashSet(IEqualityComparer<T> comparer = null) => enumerable?.ToHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/>
    public SortedSet<T> ToSortedSet(IComparer<T> comparer = null) => enumerable?.ToSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="SortedSet{T}"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/>
    public async ValueTask<SortedSet<T>> ToSortedSetAsync(IComparer<T> comparer = null, CancellationToken cancellation = default)
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
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
    public Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToDictionaryAsync(key, comparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlyDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
    public IReadOnlyDictionary<TKey, T> ToReadOnlyDictionary<TKey>(Func<T, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToReadOnlyDictionaryAsync(key, comparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlyDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
    public async ValueTask<IReadOnlyDictionary<TKey, T>> ToReadOnlyDictionaryAsync<TKey>(Func<T, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return await enumerable.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToValueTupleAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public IEnumerable<(T item, int index)> ToValueTuple() => enumerable?.ToValueTupleAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToValueTupleAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, CancellationToken)"/>
    public IEnumerable<(TKey Key, T Value)> ToValueTuple<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToValueTupleAsync(key, comparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToValueTuple{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<IEnumerable<(T item, int index)>> ToValueTupleAsync(CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple() : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToValueTuple{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/>
    public async Task<IEnumerable<(TKey Key, T Value)>> ToValueTupleAsync<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToValueTuple(key, comparer);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTupleAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public IEnumerable<Tuple<T, int>> ToTuple() => enumerable?.ToTupleAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTupleAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, CancellationToken)"/>
    public IEnumerable<Tuple<TKey, T>> ToTuple<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToTupleAsync(key, comparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTuple{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<IEnumerable<Tuple<T, int>>> ToTupleAsync(CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToTuple() : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTuple{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/>
    public async ValueTask<IEnumerable<Tuple<TKey, T>>> ToTupleAsync<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToTuple(key, comparer);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStackAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public Stack<T> ToStack() => enumerable?.ToStackAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="Stack{T}"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStack{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<Stack<T>> ToStackAsync(CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public Queue<T> ToQueue() => enumerable?.ToQueueAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="Queue{T}"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToQueue{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<Queue<T>> ToQueueAsync(CancellationToken cancellation = default)
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
    
    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlySetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
    public IReadOnlySet<T> ToReadOnlySet(IEqualityComparer<T> comparer = null) => enumerable?.ToReadOnlySetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToReadOnlySet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
    public async ValueTask<IReadOnlySet<T>> ToReadOnlySetAsync(IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? await enumerable.ToHashSetAsync(comparer, cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public ImmutableArray<T> ToImmutableArray() => enumerable?.ToImmutableArrayAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="ToArrayAsync{T}"/>
    /// <seealso cref="ImmutableArray.ToImmutableArray{TSource}(IEnumerable{TSource})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableArray{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<ImmutableArray<T>> ToImmutableArrayAsync(CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableArray() : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public ImmutableList<T> ToImmutableList() => enumerable?.ToImmutableListAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="ToListAsync{T}"/>
    /// <seealso cref="ToLinkedListAsync{T}"/>
    /// <seealso cref="ImmutableList.ToImmutableList{TSource}(IEnumerable{TSource})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableList{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<ImmutableList<T>> ToImmutableListAsync(CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableList() : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/>
    public ImmutableHashSet<T> ToImmutableHashSet(IEqualityComparer<T> comparer = null) => enumerable?.ToImmutableHashSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="ToHashSetAsync{T}"/>
    /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource})"/>
    /// <seealso cref="ImmutableHashSet.ToImmutableHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
    public async ValueTask<ImmutableHashSet<T>> ToImmutableHashSetAsync(IEqualityComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableHashSet(comparer) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/>
    public ImmutableSortedSet<T> ToImmutableSortedSet(IComparer<T> comparer = null) => enumerable?.ToImmutableSortedSetAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/>
    public async ValueTask<ImmutableSortedSet<T>> ToImmutableSortedSetAsync(IComparer<T> comparer = null, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableSortedSet(comparer) : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/>
    public ImmutableDictionary<TKey, T> ToImmutableDictionary<TKey>(Func<T, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToImmutableDictionaryAsync(key, comparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
    public async ValueTask<ImmutableDictionary<TKey, T>> ToImmutableDictionaryAsync<TKey>(Func<T, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return (await enumerable.ToDictionaryAsync(key, comparer, cancellation).ConfigureAwait(false)).ToImmutableDictionary();
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="keyComparer"></param>
    /// <param name="valueComparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableSortedDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue}, CancellationToken)"/>
    public ImmutableSortedDictionary<TKey, T> ToImmutableSortedDictionary<TKey>(Func<T, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<T> valueComparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return enumerable.ToImmutableSortedDictionaryAsync(key, keyComparer, valueComparer).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="keyComparer"></param>
    /// <param name="valueComparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue})"/>
    public async ValueTask<ImmutableSortedDictionary<TKey, T>> ToImmutableSortedDictionaryAsync<TKey>(Func<T, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<T> valueComparer = null, CancellationToken cancellation = default) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return (await enumerable.ToDictionaryAsync(key, null, cancellation).ConfigureAwait(false)).ToImmutableSortedDictionary(keyComparer, valueComparer);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/>
    public ImmutableQueue<T> ToImmutableQueue() => enumerable?.ToImmutableQueueAsync().Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToImmutableQueue{T}(IAsyncEnumerable{T})"/>
    public async Task<ImmutableQueue<T>> ToImmutableQueueAsync(CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).ToImmutableQueue() : throw new ArgumentNullException(nameof(enumerable));
    #else
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
    public static async ValueTask<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellation = default) => enumerable is not null ? (await enumerable.ToListAsync(cancellation).ConfigureAwait(false)).AsArray() : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="Enumerable.ToList{TSource}(IEnumerable{TSource})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToList{T}(IAsyncEnumerable{T})"/>
    public async ValueTask<List<T>> ToListAsync<T>(CancellationToken cancellation = default)
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
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="ToImmutableHashSetAsync{T}"/>
    /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource})"/>
    /// <seealso cref="Enumerable.ToHashSet{TSource}(IEnumerable{TSource}, IEqualityComparer{TSource})"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/>
    public async ValueTask<HashSet<T>> ToHashSetAsync<T>(IEqualityComparer<T> comparer = null, CancellationToken cancellation = default)
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
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/>
    public static async ValueTask<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this IAsyncEnumerable<TValue> enumerable, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null, CancellationToken cancellation = default) where TKey : notnull
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
    #endif
  }
  
  #if NET10_0_OR_GREATER
  /// <param name="enumerable"></param>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  extension<TElement, TPriority>(IAsyncEnumerable<(TElement Element, TPriority Priority)> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToPriorityQueueAsync{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority}, CancellationToken)"/>
    public PriorityQueue<TElement, TPriority> ToPriorityQueue(IComparer<TPriority> comparer = null) => enumerable?.ToPriorityQueueAsync(comparer).Result ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <seealso cref="PriorityQueue{TElement, TPriority}"/>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority})"/>
    public async ValueTask<PriorityQueue<TElement, TPriority>> ToPriorityQueueAsync(IComparer<TPriority> comparer = null, CancellationToken cancellation = default)
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
  }
  #endif
  
  private sealed class AsyncEnumerable<T> : IEnumerable<T>
  {
    private IAsyncEnumerator<T> EnumeratorProperty { get; }

    public AsyncEnumerable(IAsyncEnumerable<T> enumerable)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      EnumeratorProperty = enumerable.GetAsyncEnumerator();
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<T>
    {
      private AsyncEnumerable<T> Parent { get; }

      public Enumerator(AsyncEnumerable<T> parent) => Parent = parent;

      public T Current => Parent.EnumeratorProperty.Current;

      public bool MoveNext() => Parent.EnumeratorProperty.MoveNextAsync().Result;

      public void Reset() => throw new NotSupportedException();

      public async void Dispose() { await Parent.EnumeratorProperty.DisposeAsync().ConfigureAwait(false); }

      object IEnumerator.Current => Current;
    }
  }
}