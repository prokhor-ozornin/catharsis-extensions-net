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
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{int, T})"/>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) => action is not null ? enumerable.ForEach((_, element) => action(element)) : throw new ArgumentNullException(nameof(action));

  /// <summary>
  ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="enumerable">Source sequence for iteration.</param>
  /// <param name="action">Delegate to be called for each element in a sequence.</param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{T})"/>
  public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<int, T> action)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (action is null) throw new ArgumentNullException(nameof(action));

    var index = 0;

    foreach (var element in enumerable)
    {
      action(index, element);
      index++;
    }

    return enumerable;
  }

  /// <summary>
  ///   <para>Concatenates all elements in a sequence into a string, using specified separator.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="enumerable">Source sequence of elements.</param>
  /// <param name="separator">String to use as a separator between concatenated elements from <paramref name="enumerable"/>.</param>
  /// <returns>String which is formed from string representation of each element in a <paramref name="enumerable"/> with a <paramref name="separator"/> between them.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static string Join<T>(this IEnumerable<T> enumerable, string separator = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    separator ??= string.Empty;

    var result = new StringBuilder();

    foreach (var element in enumerable)
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
  /// <param name="enumerable"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<T> Repeat<T>(this IEnumerable<T> enumerable, int count)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    switch (count)
    {
      case < 0:
        throw new ArgumentOutOfRangeException(nameof(count));

      case 0:
        return [];
    }

    var result = enumerable;

    (count - 1).Times(() => result = result.Concat(enumerable));

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<T> Range<T>(this IEnumerable<T> enumerable, int? offset = null, int? count = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (offset is < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (offset is not null)
    {
      enumerable = enumerable.Skip(offset.Value);
    }

    if (count is not null)
    {
      enumerable = enumerable.Take(count.Value);
    }

    return enumerable;
  }

  /// <summary>
  ///   <para>Picks up random element from a specified sequence and returns it.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="enumerable">Source sequence of elements.</param>
  /// <param name="random"></param>
  /// <returns>Random member of <paramref name="enumerable"/> sequence. If <paramref name="enumerable"/> contains no elements, returns <c>null</c>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static T Random<T>(this IEnumerable<T> enumerable, Random random = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    var randomizer = random ?? new Random();
    var count = enumerable.Count();

    return count > 0 ? enumerable.ElementAt(randomizer.Next(count)) : default;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="random"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable, Random random = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    var randomizer = random ?? new Random();

    return enumerable.OrderBy(_ => randomizer.Next());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
  /// <seealso cref="EndsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
  public static bool StartsWith<T>(this IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return enumerable.Take(other.Count()).SequenceEqual(other, comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
  /// <seealso cref="StartsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
  public static bool EndsWith<T>(this IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return enumerable.TakeLast(other.Count()).SequenceEqual(other, comparer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="other"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
  public static bool Contains<T>(this IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (other is null) throw new ArgumentNullException(nameof(other));

    return !enumerable.Except(other, comparer).Any();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static bool ContainsUnique<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => !enumerable?.GroupBy(x => x, comparer).Where(group => group.Count() > 1).Select(group => group.Key).Any() ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static bool ContainsNull<T>(this IEnumerable<T> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    return enumerable.Any(element => element is null);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static bool ContainsDefault<T>(this IEnumerable<T> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    return enumerable.Any(element => element.Equals(default(T)));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="superset"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="superset"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsSuperset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
  public static bool IsSubset<T>(this IEnumerable<T> enumerable, IEnumerable<T> superset, IEqualityComparer<T> comparer = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (superset is null) throw new ArgumentNullException(nameof(superset));

    return !enumerable.Except(superset, comparer).Any();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="subset"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="subset"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsSubset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
  public static bool IsSuperset<T>(this IEnumerable<T> enumerable, IEnumerable<T> subset, IEqualityComparer<T> comparer = null) => subset.IsSubset(enumerable, comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="reversed"></param>
  /// <param name="comparer"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="reversed"/> is <see langword="null"/>.</exception>
  public static bool IsReversed<T>(this IEnumerable<T> enumerable, IEnumerable<T> reversed, IEqualityComparer<T> comparer = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (reversed is null) throw new ArgumentNullException(nameof(reversed));

    return enumerable.SequenceEqual(reversed.Reverse(), comparer);
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
  public static bool IsOrdered<T>(this IEnumerable<T> enumerable, IComparer<T> comparer = null) => enumerable?.Order(comparer).SequenceEqual(enumerable) ?? throw new ArgumentNullException(nameof(enumerable));
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static T[] AsArray<T>(this IEnumerable<T> enumerable) => enumerable is not null ? enumerable as T[] ?? enumerable.ToArray() : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<T> AsNotNullable<T>(this IEnumerable<T> enumerable) => enumerable?.Where(element => element is not null) ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<T> WithCancellation<T>(this IEnumerable<T> enumerable, CancellationToken cancellation)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    foreach (var element in enumerable)
    {
      cancellation.ThrowIfCancellationRequested();

      yield return element;
    }
  }

  /// <summary>
  ///   <para>Returns BASE64-encoded representation of a sequence sequence.</para>
  /// </summary>
  /// <param name="enumerable">Bytes to convert to BASE64 encoding.</param>
  /// <returns>BASE64 string representation of <paramref name="enumerable"/> array.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static string ToBase64(this IEnumerable<byte> enumerable) => enumerable is not null ? Convert.ToBase64String(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>    
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static string ToHex(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

  #if NET8_0
    return Convert.ToHexString(enumerable.AsArray());
  #else
    return BitConverter.ToString(enumerable.AsArray()).Replace("-", "");
  #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="EncryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
  public static byte[] Encrypt(this IEnumerable<byte> enumerable, SymmetricAlgorithm algorithm) => algorithm.Encrypt(enumerable);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
  public static async Task<byte[]> EncryptAsync(this IEnumerable<byte> enumerable, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(enumerable, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="DecryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
  public static byte[] Decrypt(this IEnumerable<byte> enumerable, SymmetricAlgorithm algorithm) => algorithm.Decrypt(enumerable);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Decrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
  public static async Task<byte[]> DecryptAsync(this IEnumerable<byte> enumerable, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(enumerable, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  public static byte[] Hash(this IEnumerable<byte> enumerable, HashAlgorithm algorithm)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    return algorithm.ComputeHash(enumerable.AsArray());
  }

  /// <summary>
  ///   <para>Computes hash digest for the given sequence of sequence, using <c>MD5</c> algorithm.</para>
  /// </summary>
  /// <param name="enumerable">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashMd5(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    using var algorithm = MD5.Create();

    return enumerable.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA1</c> algorithm.</para>
  /// </summary>
  /// <param name="enumerable">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha1(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    using var algorithm = SHA1.Create();

    return enumerable.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA256</c> algorithm.</para>
  /// </summary>
  /// <param name="enumerable">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha256(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    using var algorithm = SHA256.Create();

    return enumerable.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha384(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    using var algorithm = SHA384.Create();

    return enumerable.Hash(algorithm);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of sequence, using <c>SHA512</c> algorithm.</para>
  /// </summary>
  /// <param name="enumerable">Source sequence sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha512(this IEnumerable<byte> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    using var algorithm = SHA512.Create();

    return enumerable.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty{T}(IEnumerable{T})"/>
  public static bool IsUnset<T>(this IEnumerable<T> enumerable) => enumerable is null || enumerable.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset{T}(IEnumerable{T})"/>
  public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable?.Any() ?? throw new ArgumentNullException(nameof(enumerable));


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
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="bytes"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, Stream, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, Stream destination)
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
  /// <exception cref="ArgumentNullException">If either <paramref name="bytes"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, Stream)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Stream destination, CancellationToken cancellation = default)
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
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, TextWriter, Encoding)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, TextWriter destination, Encoding encoding = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    enumerable.AsArray().ToText(encoding).WriteTo(destination);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, TextWriter, Encoding)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, TextWriter destination, Encoding encoding = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await enumerable.AsArray().ToText(encoding).WriteToAsync(destination).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, BinaryWriter destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, XmlWriter, Encoding)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, XmlWriter destination, Encoding encoding = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable, encoding);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, XmlWriter, Encoding)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, XmlWriter destination, Encoding encoding = null)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteBytesAsync(enumerable, encoding).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, FileInfo, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, FileInfo destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, FileInfo)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, FileInfo destination, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(enumerable, cancellation).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, Uri destination, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable, timeout, headers);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, Uri, TimeSpan?, ValueTuple{string, object}[])"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, Uri destination, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(enumerable, timeout, cancellation, headers).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, Process destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, Process)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, Process destination, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteBytesAsync(enumerable, cancellation).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="client"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/>
  public static HttpContent WriteTo(this IEnumerable<byte> enumerable, HttpClient client, Uri destination) => client.WriteBytes(enumerable, destination);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="client"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, HttpClient, Uri)"/>
  public static async Task<HttpContent> WriteToAsync(this IEnumerable<byte> enumerable, HttpClient client, Uri destination, CancellationToken cancellation = default) => await client.WriteBytesAsync(enumerable, destination, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, TcpClient, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, TcpClient destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, TcpClient)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, TcpClient destination, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(enumerable, cancellation).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(IEnumerable{byte}, UdpClient, CancellationToken)"/>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> enumerable, UdpClient destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(IEnumerable{byte}, UdpClient)"/>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> enumerable, UdpClient destination, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(enumerable, cancellation).ConfigureAwait(false);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  public static IEnumerable<char> WriteTo(this IEnumerable<char> enumerable, SecureString destination)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.With(enumerable);

    return enumerable;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    foreach (var element in enumerable)
    {
      yield return await Task.FromResult(element).ConfigureAwait(false);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new LinkedList<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> enumerable) => enumerable?.ToList() ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para>Converts sequence of elements into a set collection type.</para>
  /// </summary>
  /// <typeparam name="T">Type of elements in a sequence.</typeparam>
  /// <param name="enumerable">Source sequence of elements.</param>
  /// <param name="comparer"></param>
  /// <returns>Set collection which contains elements from <paramref name="enumerable"/> sequence without duplicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="enumerable"/>'s enumerator.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> enumerable, IComparer<T> comparer = null) => enumerable is not null ? new SortedSet<T>(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable?.ToHashSet(comparer) ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static FrozenSet<T> ToFrozenSet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable is not null ? FrozenSet.ToFrozenSet(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new Stack<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new Queue<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TElement"></typeparam>
  /// <typeparam name="TPriority"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static PriorityQueue<TElement, TPriority> ToPriorityQueue<TElement, TPriority>(this IEnumerable<(TElement Element, TPriority Priority)> enumerable, IComparer<TPriority> comparer = null) => enumerable is not null ? new PriorityQueue<TElement, TPriority>(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static ImmutableQueue<T> ToImmutableQueue<T>(this IEnumerable<T> enumerable) => enumerable is not null ? ImmutableQueue.CreateRange(enumerable) : throw new ArgumentNullException(nameof(enumerable));
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static ArraySegment<T> ToArraySegment<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new ArraySegment<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static Memory<T> ToMemory<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new Memory<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new ReadOnlyMemory<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static Span<T> ToSpan<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new Span<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static ReadOnlySpan<T> ToReadOnlySpan<T>(this IEnumerable<T> enumerable) => enumerable is not null ? new ReadOnlySpan<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<int> ToRange(this IEnumerable<Range> enumerable) => enumerable?.SelectMany(range => range.ToEnumerable()).ToHashSet() ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<(T item, int index)> ToValueTuple<T>(this IEnumerable<T> enumerable) => enumerable?.Select((item, index) => (item, index)) ?? throw new ArgumentNullException(nameof(enumerable));

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
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
    if (key is null) throw new ArgumentNullException(nameof(key));

    return comparer is not null ? enumerable.OrderBy(key, comparer).Select(tuple => (Key: key(tuple), Value: tuple)) : enumerable.Select(tuple => (Key: key(tuple), Value: tuple));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> enumerable, IEqualityComparer<TKey> comparer = null) where TKey : notnull
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    var result = new Dictionary<TKey, TValue>(comparer);

    enumerable.ForEach(tuple => result.Add(tuple.Key, tuple.Value));

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> enumerable, IEqualityComparer<TKey> comparer = null) where TKey : notnull => enumerable.ToDictionary(comparer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static string ToText(this IEnumerable<char> enumerable) => enumerable is not null ? new string(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IEnumerable{byte[]})"/>
  public static MemoryStream ToMemoryStream(this IEnumerable<byte> enumerable) => enumerable?.Chunk(4096).ToMemoryStream() ?? throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte[]}, CancellationToken)"/>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IEnumerable<byte> enumerable, CancellationToken cancellation = default) => enumerable is not null ? await enumerable.Chunk(4096).ToMemoryStreamAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStream(IEnumerable{byte})"/>
  public static MemoryStream ToMemoryStream(this IEnumerable<byte[]> enumerable)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    var stream = new MemoryStream();

    foreach (var bytes in enumerable)
    {
      stream.Write(bytes, 0, bytes.Length);
    }

    stream.MoveToStart();

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="enumerable"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte}, CancellationToken)"/>
  public static async Task<MemoryStream> ToMemoryStreamAsync(this IEnumerable<byte[]> enumerable, CancellationToken cancellation = default)
  {
    if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    foreach (var bytes in enumerable)
    {
      await stream.WriteAsync(bytes, 0, bytes.Length, cancellation).ConfigureAwait(false);
    }

    stream.MoveToStart();

    return stream;
  }
}