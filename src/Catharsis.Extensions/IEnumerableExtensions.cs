using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Xml;
using System.Diagnostics;
using System.Security;

#if NET7_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for synchronous enumerators and sequences.</para>
/// </summary>
/// <seealso cref="IEnumerable{T}"/>
public static class IEnumerableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence?.Any() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="superset"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsSubset<T>(this IEnumerable<T> sequence, IEnumerable<T> superset, IEqualityComparer<T> comparer = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (superset is null) throw new ArgumentNullException(nameof(superset));

    return !sequence.Except(superset, comparer).Any();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="subset"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsSuperset<T>(this IEnumerable<T> sequence, IEnumerable<T> subset, IEqualityComparer<T> comparer = null) => subset.IsSubset(sequence, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="reversed"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsReversed<T>(this IEnumerable<T> sequence, IEnumerable<T> reversed, IEqualityComparer<T> comparer = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (reversed is null) throw new ArgumentNullException(nameof(reversed));
    
    return sequence.SequenceEqual(reversed.Reverse(), comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<T> action) => action is not null ? sequence.ForEach((_, element) => action(element)) : throw new ArgumentNullException(nameof(action));

  /// <summary>
  ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence for iteration.</param>
  /// <param name="action">Delegate to be called for each element in a sequence.</param>
  /// <returns>Back reference to the current sequence.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<int, T> action)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (action is null) throw new ArgumentNullException(nameof(action));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> Min<T>(this IEnumerable<T> left, IEnumerable<T> right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Count() <= right.Count() ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> Max<T>(this IEnumerable<T> left, IEnumerable<T> right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Count() >= right.Count() ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool Contains<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return !sequence.Except(other, comparer).Any();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool ContainsUnique<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => !sequence?.GroupBy(sequence => sequence, comparer).Where(group => group.Count() > 1).Select(group => group.Key).Any() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool ContainsNull<T>(this IEnumerable<T> sequence)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    return sequence.Any(element => element is null);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool ContainsDefault<T>(this IEnumerable<T> sequence)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    return sequence.Any(element => element.Equals(default(T)));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<T> Range<T>(this IEnumerable<T> sequence, int? offset = null, int? count = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (offset is < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (offset is not null)
    {
      sequence = sequence.Skip(offset.Value);
    }

    if (count is not null)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static bool StartsWith<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return sequence.Take(other.Count()).SequenceEqual(other, comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool EndsWith<T>(this IEnumerable<T> sequence, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return sequence.TakeLast(other.Count()).SequenceEqual(other, comparer);
  }

  /// <summary>
  ///   <para>Concatenates all elements in a sequence into a string, using specified separator.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="separator">String to use as a separator between concatenated elements from <paramref name="sequence"/>.</param>
  /// <returns>String which is formed from string representation of each element in a <paramref name="sequence"/> with a <paramref name="separator"/> between them.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string Join<T>(this IEnumerable<T> sequence, string separator = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    separator ??= string.Empty;

    var result = new StringBuilder();

    foreach (var element in sequence)
    {
      var value = element?.ToInvariantString() ?? string.Empty;

      if (value.Length == 0)
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
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int count)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static T Random<T>(this IEnumerable<T> sequence, Random random = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence, Random random = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    var randomizer = random ?? new Random();

    return sequence.OrderBy(_ => randomizer.Next());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> WithCancellation<T>(this IEnumerable<T> sequence, CancellationToken cancellation)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    foreach (var element in sequence)
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
  public static T[] AsArray<T>(this IEnumerable<T> sequence) => sequence is not null ? sequence as T[] ?? sequence.ToArray() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<T> AsNotNullable<T>(this IEnumerable<T> sequence) => sequence?.Where(element => element is not null) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> sequence)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    foreach (var element in sequence)
    {
      yield return await Task.FromResult(element).ConfigureAwait(false);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> sequence) => sequence is not null ? new LinkedList<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> sequence) => sequence?.ToList() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para>Converts sequence of elements into a set collection type.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="comparer"></param>
  /// <returns>Set collection which contains elements from <paramref name="sequence"/> sequence without duplicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="sequence"/>'s enumerator.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> sequence, IComparer<T> comparer = null) => sequence is not null ? new SortedSet<T>(sequence, comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Stack<T> ToStack<T>(this IEnumerable<T> sequence) => sequence is not null ? new Stack<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Queue<T> ToQueue<T>(this IEnumerable<T> sequence) => sequence is not null ? new Queue<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ArraySegment<T> ToArraySegment<T>(this IEnumerable<T> sequence) => sequence is not null ? new ArraySegment<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Memory<T> ToMemory<T>(this IEnumerable<T> sequence) => sequence is not null ? new Memory<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this IEnumerable<T> sequence) => sequence is not null ? new ReadOnlyMemory<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Span<T> ToSpan<T>(this IEnumerable<T> sequence) => sequence is not null ? new Span<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ReadOnlySpan<T> ToReadOnlySpan<T>(this IEnumerable<T> sequence) => sequence is not null ? new ReadOnlySpan<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<int> ToRange(this IEnumerable<Range> sequence) => sequence?.SelectMany(range => range.ToEnumerable()).ToHashSet() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(T item, int index)> ToValueTuple<T>(this IEnumerable<T> sequence) => sequence?.Select((item, index) => (item, index)) ?? throw new ArgumentNullException(nameof(sequence));

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
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return comparer is not null ? sequence.OrderBy(key, comparer).Select(tuple => (Key: key(tuple), Value: tuple)) : sequence.Select(tuple => (Key: key(tuple), Value: tuple));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> sequence, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

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
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> sequence, IEqualityComparer<TKey> comparer = null) where TKey : notnull => sequence.ToDictionary(comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MemoryStream ToMemoryStream(this IEnumerable<byte> sequence) => sequence?.Chunk(4096).ToMemoryStream() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IEnumerable<byte> sequence, CancellationToken cancellation = default) => sequence is not null ? await sequence.Chunk(4096).ToMemoryStreamAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MemoryStream ToMemoryStream(this IEnumerable<byte[]> sequence)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    var stream = new MemoryStream();

    foreach (var bytes in sequence)
    {
      stream.Write(bytes, 0, bytes.Length);
    }

    stream.MoveToStart();

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IEnumerable<byte[]> sequence, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    foreach (var bytes in sequence)
    {
      await stream.WriteAsync(bytes, 0, bytes.Length, cancellation).ConfigureAwait(false);
    }

    stream.MoveToStart();

    return stream;
  }

  /// <summary>
  ///   <para>Returns BASE64-encoded representation of a bytes sequence.</para>
  /// </summary>
  /// <param name="bytes">Bytes to convert to BASE64 encoding.</param>
  /// <returns>BASE64 string representation of <paramref name="bytes"/> array.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToBase64(this IEnumerable<byte> bytes) => bytes is not null ? Convert.ToBase64String(bytes.AsArray()) : throw new ArgumentNullException(nameof(bytes));

  /// <summary>
  ///   <para></para>
  /// </summary>    
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToHex(this IEnumerable<byte> bytes)
  {
    if (bytes is null)
      throw new ArgumentNullException(nameof(bytes));

    #if NET7_0_OR_GREATER
    return Convert.ToHexString(bytes.AsArray());
    #else
      return BitConverter.ToString(bytes.AsArray()).Replace("-", "");
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, TextWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    bytes.AsArray().ToText(encoding).WriteTo(destination);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, TextWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await bytes.AsArray().ToText(encoding).WriteToAsync(destination).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, BinaryWriter destination)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, XmlWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes, encoding);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, XmlWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteBytesAsync(bytes, encoding).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, FileInfo destination)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, FileInfo destination, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, Uri destination, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes, timeout, headers);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Uri destination, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(bytes, timeout, cancellation, headers).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, Process process)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (process is null) throw new ArgumentNullException(nameof(process));

    process.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Process process, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (process is null) throw new ArgumentNullException(nameof(process));

    await process.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent WriteTo(this IEnumerable<byte> bytes, HttpClient http, Uri uri) => http.WriteBytes(bytes, uri);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> WriteToAsync(this IEnumerable<byte> bytes, HttpClient http, Uri uri, CancellationToken cancellation = default) => await http.WriteBytesAsync(bytes, uri, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="tcp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, TcpClient tcp)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    tcp.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="tcp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, TcpClient tcp, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    cancellation.ThrowIfCancellationRequested();

    await tcp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="udp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, UdpClient udp)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    udp.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="udp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, UdpClient udp, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    cancellation.ThrowIfCancellationRequested();

    await udp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<char> WriteTo(this IEnumerable<char> text, SecureString destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Encrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> EncryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Decrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Hash(this IEnumerable<byte> bytes, HashAlgorithm algorithm)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    return algorithm.ComputeHash(bytes.AsArray());
  }

  /// <summary>
  ///   <para>Computes hash digest for the given sequence of bytes, using <c>MD5</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashMd5(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = MD5.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA1</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha1(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA1.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA256</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha256(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA256.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha384(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA384.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA512</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha512(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA512.Create();

    return bytes.Hash(algorithm);
  }

#if NET7_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsOrdered<T>(this IEnumerable<T> sequence, IComparer<T> comparer = null) => sequence?.Order(comparer).SequenceEqual(sequence) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence?.ToHashSet(comparer) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null) => sequence is not null ? new PriorityQueue<TElement, TPriority>(sequence, comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IEnumerable<T> sequence) => sequence is not null ? ImmutableQueue.CreateRange(sequence) : throw new ArgumentNullException(nameof(sequence));
  #endif
}