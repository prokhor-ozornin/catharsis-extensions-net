using System.Text;

#if NET6_0
using System.Collections.Immutable;
#endif

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for synchronous enumerators and sequences.</para>
/// </summary>
/// <seealso cref="IEnumerable{T}"/>
public static class EnumerableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<T> action) => sequence.ForEach((_, element) => action(element));

  /// <summary>
  ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence for iteration.</param>
  /// <param name="action">Delegate to be called for each element in a sequence.</param>
  /// <returns>Back reference to the current sequence.</returns>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<int, T> action)
  {
    var index = 0;

    foreach (var element in sequence)
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
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IEnumerable<T> Min<T>(this IEnumerable<T> left, IEnumerable<T> right) => left.Count() <= right.Count() ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IEnumerable<T> Max<T>(this IEnumerable<T> left, IEnumerable<T> right) => left.Count() >= right.Count() ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static bool Contains<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => !sequence.Except(other, comparer).Any();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<T> Range<T>(this IEnumerable<T> sequence, int? offset = null, int? count = null)
  {
    if (offset != null)
    {
      sequence = sequence.Skip(offset.Value);
    }

    if (count != null)
    {
      sequence = sequence.Take(count.Value);
    }

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static bool StartsWith<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => sequence.Take(other.Count()).SequenceEqual(other, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static bool EndsWith<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => sequence.TakeLast(other.Count()).SequenceEqual(other, comparer);
  
  /// <summary>
  ///   <para>Concatenates all elements in a sequence into a string, using specified separator.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="separator">String to use as a separator between concatenated elements from <paramref name="sequence"/>.</param>
  /// <returns>String which is formed from string representation of each element in a <paramref name="sequence"/> with a <paramref name="separator"/> between them.</returns>
  public static string Join<T>(this IEnumerable<T> sequence, string separator = null)
  {
    separator ??= string.Empty;

    var result = new StringBuilder();

    foreach (var element in sequence)
    {
      var value = element?.ToInvariantString() ?? string.Empty;

      if (value.Length <= 0)
      {
        continue;
      }

      result.Append(value);

      if (separator.Length > 0)
      {
        result.Append(separator);
      }
    }

    return result.Length > 0 ? result.ToString(0, result.Length - separator.Length) : string.Empty;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int count)
  {
    switch (count)
    {
      case < 0:
        throw new ArgumentOutOfRangeException(nameof(count));

      case 0:
        return Enumerable.Empty<T>();
    }

    var result = sequence;

    (count - 1).Times(() => result = result.Concat(sequence));

    return result;
  }

  /// <summary>
  ///   <para>Picks up random element from a specified sequence and returns it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="random"></param>
  /// <returns>Random member of <paramref name="sequence"/> sequence. If <paramref name="sequence"/> contains no elements, returns <c>null</c>.</returns>
  public static T Random<T>(this IEnumerable<T> sequence, Random random = null)
  {
    var randomizer = random ?? new Random();
    var count = sequence.Count();

    return count > 0 ? sequence.ElementAt(randomizer.Next(count)) : default;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="random"></param>
  /// <returns></returns>
  public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence, Random random = null)
  {
    var randomizer = random ?? new Random();

    return sequence.OrderBy(_ => randomizer.Next());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static T[] AsArray<T>(this IEnumerable<T> sequence) => sequence as T[] ?? sequence.ToArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static IEnumerable<T> AsNotNullable<T>(this IEnumerable<T> sequence) => sequence.Where(element => element != null);
  
  /// <summary>
  ///   <para>Returns BASE64-encoded representation of a bytes sequence.</para>
  /// </summary>
  /// <param name="bytes">Bytes to convert to BASE64 encoding.</param>
  /// <returns>BASE64 string representation of <paramref name="bytes"/> array.</returns>
  public static string ToBase64(this IEnumerable<byte> bytes) => System.Convert.ToBase64String(bytes.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>    
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static string ToHex(this IEnumerable<byte> bytes)
  {
#if NET6_0
    return System.Convert.ToHexString(bytes.AsArray());
#else
    return BitConverter.ToString(bytes.AsArray()).Replace("-", "");
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> sequence)
  {
    foreach (var element in sequence)
    {
      yield return await Task.FromResult(element);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> sequence) => new(sequence);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> sequence) => sequence.ToList();

  /// <summary>
  ///   <para>Converts sequence of elements into a set collection type.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="comparer"></param>
  /// <returns>Set collection which contains elements from <paramref name="sequence"/> sequence without duplicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="sequence"/>'s enumerator.</returns>
  public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> sequence, IComparer<T> comparer = null) => new(sequence, comparer);

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence.ToHashSet(comparer);
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static Stack<T> ToStack<T>(this IEnumerable<T> sequence) => new(sequence);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static Queue<T> ToQueue<T>(this IEnumerable<T> sequence) => new(sequence);

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null) => new(sequence, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IEnumerable<T> sequence) => ImmutableQueue.CreateRange(sequence);
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<MemoryStream> ToMemoryStream(this IEnumerable<byte> sequence, CancellationToken cancellation = default) => await sequence.Chunk(4096).ToMemoryStream(cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<MemoryStream> ToMemoryStream(this IEnumerable<byte[]> sequence, CancellationToken cancellation = default)
  {
    var stream = new MemoryStream();

    foreach (var bytes in sequence)
    {
      await stream.WriteAsync(bytes, 0, bytes.Length, cancellation).ConfigureAwait(false);
    }

    stream.MoveToStart();

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static ArraySegment<T> ToArraySegment<T>(this IEnumerable<T> sequence) => new(sequence.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static Memory<T> ToMemory<T>(this IEnumerable<T> sequence) => new(sequence.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this IEnumerable<T> sequence) => new(sequence.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static Span<T> ToSpan<T>(this IEnumerable<T> sequence) => new(sequence.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static ReadOnlySpan<T> ToReadOnlySpan<T>(this IEnumerable<T> sequence) => new(sequence.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static IEnumerable<int> ToRange(this IEnumerable<Range> sequence) => sequence.SelectMany(range => range.ToEnumerable()).ToHashSet();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  public static IEnumerable<(T item, int index)> ToValueTuple<T>(this IEnumerable<T> sequence) => sequence.Select((item, index) => (item, index));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="key"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull => comparer != null ? sequence.OrderBy(key, comparer).Select(tuple => (Key: key(tuple), Value: tuple)) : sequence.Select(tuple => (Key: key(tuple), Value: tuple));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> sequence, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    var result = new Dictionary<TKey, TValue>(comparer);

    sequence.ForEach(tuple => result.Add(tuple.Key, tuple.Value));

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> sequence, IEqualityComparer<TKey> comparer = null) where TKey : notnull => sequence.ToDictionary(comparer);

}