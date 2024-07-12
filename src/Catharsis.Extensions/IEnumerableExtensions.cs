using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Xml;
using System.Diagnostics;
using System.Security;

#if NET8_0
using System.Collections.Immutable;
using System.Collections.Frozen;
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
  /// <seealso cref="IsEmpty{T}(IEnumerable{T})"/>
  public static bool IsUnset<T>(this IEnumerable<T> sequence) => sequence is null || sequence.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset{T}(IEnumerable{T})"/>
  public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence?.Any() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="superset"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="superset"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsSuperset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="subset"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsSubset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
  public static bool IsSuperset<T>(this IEnumerable<T> sequence, IEnumerable<T> subset, IEqualityComparer<T> comparer = null) => subset.IsSubset(sequence, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="reversed"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="reversed"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{int, T})"/>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<T> action) => action is not null ? sequence.ForEach((_, element) => action(element)) : throw new ArgumentNullException(nameof(action));

  /// <summary>
  ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence for iteration.</param>
  /// <param name="action">Delegate to be called for each element in a sequence.</param>
  /// <returns>Back reference to the current sequence.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{T})"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Max{T}(IEnumerable{T}, IEnumerable{T})"/>
  /// <seealso cref="MinMax{T}(IEnumerable{T}, IEnumerable{T})"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min{T}(IEnumerable{T}, IEnumerable{T})"/>
  /// <seealso cref="MinMax{T}(IEnumerable{T}, IEnumerable{T})"/>
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
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min{T}(IEnumerable{T}, IEnumerable{T})"/>
  /// <seealso cref="Max{T}(IEnumerable{T}, IEnumerable{T})"/>
  public static (IEnumerable<T> Min, IEnumerable<T> Max) MinMax<T>(this IEnumerable<T> left, IEnumerable<T> right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Count() <= right.Count() ? (left, right) : (right, left);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static bool ContainsUnique<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => !sequence?.GroupBy(x => x, comparer).Where(group => group.Count() > 1).Select(group => group.Key).Any() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
  /// <seealso cref="EndsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
  /// <seealso cref="StartsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int count)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    switch (count)
    {
      case < 0:
        throw new ArgumentOutOfRangeException(nameof(count));

      case 0:
        return [];
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static T[] AsArray<T>(this IEnumerable<T> sequence) => sequence is not null ? sequence as T[] ?? sequence.ToArray() : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static IEnumerable<T> AsNotNullable<T>(this IEnumerable<T> sequence) => sequence?.Where(element => element is not null) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> sequence) => sequence is not null ? new LinkedList<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> sequence) => sequence?.ToList() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para>Converts sequence of elements into a set collection type.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="sequence">Source sequence of elements.</param>
  /// <param name="comparer"></param>
  /// <returns>Set collection which contains elements from <paramref name="sequence"/> sequence without duplicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="sequence"/>'s enumerator.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> sequence, IComparer<T> comparer = null) => sequence is not null ? new SortedSet<T>(sequence, comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static Stack<T> ToStack<T>(this IEnumerable<T> sequence) => sequence is not null ? new Stack<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static Queue<T> ToQueue<T>(this IEnumerable<T> sequence) => sequence is not null ? new Queue<T>(sequence) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static ArraySegment<T> ToArraySegment<T>(this IEnumerable<T> sequence) => sequence is not null ? new ArraySegment<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static Memory<T> ToMemory<T>(this IEnumerable<T> sequence) => sequence is not null ? new Memory<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this IEnumerable<T> sequence) => sequence is not null ? new ReadOnlyMemory<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static Span<T> ToSpan<T>(this IEnumerable<T> sequence) => sequence is not null ? new Span<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static ReadOnlySpan<T> ToReadOnlySpan<T>(this IEnumerable<T> sequence) => sequence is not null ? new ReadOnlySpan<T>(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static IEnumerable<int> ToRange(this IEnumerable<Range> sequence) => sequence?.SelectMany(range => range.ToEnumerable()).ToHashSet() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> sequence, IEqualityComparer<TKey> comparer = null) where TKey : notnull => sequence.ToDictionary(comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static string ToText(this IEnumerable<char> sequence) => sequence is not null ? new string(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IEnumerable{byte[]})"/>
  public static MemoryStream ToMemoryStream(this IEnumerable<byte> sequence) => sequence?.Chunk(4096).ToMemoryStream() ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte[]}, CancellationToken)"/>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IEnumerable<byte> sequence, CancellationToken cancellation = default) => sequence is not null ? await sequence.Chunk(4096).ToMemoryStreamAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IEnumerable{byte})"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte}, CancellationToken)"/>
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
  ///   <para>Returns BASE64-encoded representation of a sequence sequence.</para>
  /// </summary>
  /// <param name="sequence">Bytes to convert to BASE64 encoding.</param>
  /// <returns>BASE64 string representation of <paramref name="sequence"/> array.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static string ToBase64(this IEnumerable<byte> sequence) => sequence is not null ? Convert.ToBase64String(sequence.AsArray()) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>    
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static string ToHex(this IEnumerable<byte> sequence)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    #if NET8_0
    return Convert.ToHexString(sequence.AsArray());
    #else
      return BitConverter.ToString(sequence.AsArray()).Replace("-", "");
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, TextWriter, Encoding)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, TextWriter to, Encoding encoding = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    sequence.AsArray().ToText(encoding).WriteTo(to);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, TextWriter, Encoding)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, TextWriter to, Encoding encoding = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    await sequence.AsArray().ToText(encoding).WriteToAsync(to).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, BinaryWriter to)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, XmlWriter, Encoding)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, XmlWriter to, Encoding encoding = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence, encoding);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, XmlWriter, Encoding)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, XmlWriter to, Encoding encoding = null)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    await to.WriteBytesAsync(sequence, encoding).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, FileInfo, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, FileInfo to)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, FileInfo)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, FileInfo to, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    cancellation.ThrowIfCancellationRequested();

    await to.WriteBytesAsync(sequence, cancellation).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, Uri to, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence, timeout, headers);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, Uri, TimeSpan?, ValueTuple{string, object}[])"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, Uri to, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    cancellation.ThrowIfCancellationRequested();

    await to.WriteBytesAsync(sequence, timeout, cancellation, headers).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, Process to)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, Process)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, Process to, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    await to.WriteBytesAsync(sequence, cancellation).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="client"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/>, <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/>
  public static HttpContent WriteTo(this IEnumerable<byte> sequence, HttpClient client, Uri uri) => client.WriteBytes(sequence, uri);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="client"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/>, <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, HttpClient, Uri)"/>
  public static async Task<HttpContent> WriteToAsync(this IEnumerable<byte> sequence, HttpClient client, Uri uri, CancellationToken cancellation = default) => await client.WriteBytesAsync(sequence, uri, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, TcpClient, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, TcpClient client)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (client is null) throw new ArgumentNullException(nameof(client));

    client.WriteBytes(sequence);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, TcpClient)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, TcpClient to, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    cancellation.ThrowIfCancellationRequested();

    await to.WriteBytesAsync(sequence, cancellation).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, UdpClient, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> sequence, UdpClient to)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.WriteBytes(sequence);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="sequence"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="sequence"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, UdpClient)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> sequence, UdpClient to, CancellationToken cancellation = default)
  {
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));
    if (to is null) throw new ArgumentNullException(nameof(to));

    cancellation.ThrowIfCancellationRequested();

    await to.WriteBytesAsync(sequence, cancellation).ConfigureAwait(false);

    return sequence;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="to"/> is <see langword="null"/>.</exception>
  public static IEnumerable<char> WriteTo(this IEnumerable<char> text, SecureString to)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (to is null) throw new ArgumentNullException(nameof(to));

    to.With(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="EncryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
  public static byte[] Encrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Encrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
  public static async Task<byte[]> EncryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="DecryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
  public static byte[] Decrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Decrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Decrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
  public static async Task<byte[]> DecryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  public static byte[] Hash(this IEnumerable<byte> bytes, HashAlgorithm algorithm)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    return algorithm.ComputeHash(bytes.AsArray());
  }

  /// <summary>
  ///   <para>Computes hash digest for the given sequence of sequence, using <c>MD5</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashMd5(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = MD5.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA1</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha1(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA1.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA256</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha384(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA384.Create();

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA512</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha512(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = SHA512.Create();

    return bytes.Hash(algorithm);
  }

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static bool IsOrdered<T>(this IEnumerable<T> sequence, IComparer<T> comparer = null) => sequence?.Order(comparer).SequenceEqual(sequence) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence?.ToHashSet(comparer) ?? throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static FrozenSet<T> ToFrozenSet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null) => sequence is not null ? FrozenSet.ToFrozenSet(sequence, comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="sequence"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null) => sequence is not null ? new PriorityQueue<TElement, TPriority>(sequence, comparer) : throw new ArgumentNullException(nameof(sequence));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="sequence"/> is <see langword="null"/>.</exception>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IEnumerable<T> sequence) => sequence is not null ? ImmutableQueue.CreateRange(sequence) : throw new ArgumentNullException(nameof(sequence));
  #endif
}