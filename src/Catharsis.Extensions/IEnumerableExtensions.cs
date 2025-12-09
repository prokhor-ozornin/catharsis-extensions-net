using System.Diagnostics;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

#if NET10_0_OR_GREATER
using System.Collections.Frozen;
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
  /// <param name="enumerable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
  public static IEnumerable<int> ToRange(this IEnumerable<Range> enumerable) => enumerable?.SelectMany(range => range.ToEnumerable()).ToHashSet() ?? throw new ArgumentNullException(nameof(enumerable));

  /// <param name="enumerable"></param>
  extension(IEnumerable<char> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public IEnumerable<char> WriteTo(SecureString destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.With(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public string ToText() => enumerable is not null ? new string(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));
  }

  /// <param name="enumerable"></param>
  extension(IEnumerable<byte> enumerable)
  {
    /// <summary>
    ///   <para>Returns BASE64-encoded representation of a sequence sequence.</para>
    /// </summary>
    /// <returns>BASE64 string representation of <paramref name="enumerable"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public string ToBase64() => enumerable is not null ? Convert.ToBase64String(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="EncryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
    public byte[] Encrypt(SymmetricAlgorithm algorithm) => algorithm.Encrypt(enumerable);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
    public async Task<byte[]> EncryptAsync(SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(enumerable, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/>
    public byte[] Decrypt(SymmetricAlgorithm algorithm) => algorithm.Decrypt(enumerable);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Decrypt(IEnumerable{byte}, SymmetricAlgorithm)"/>
    public async Task<byte[]> DecryptAsync(SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(enumerable, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    public byte[] Hash(HashAlgorithm algorithm)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

      return algorithm.ComputeHash(enumerable.AsArray());
    }

    /// <summary>
    ///   <para>Computes hash digest for the given sequence of sequence, using <c>MD5</c> algorithm.</para>
    /// </summary>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public byte[] HashMd5()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      using var algorithm = MD5.Create();

      return enumerable.Hash(algorithm);
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of sequence, using <c>SHA1</c> algorithm.</para>
    /// </summary>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public byte[] HashSha1()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      using var algorithm = SHA1.Create();

      return enumerable.Hash(algorithm);
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of sequence, using <c>SHA256</c> algorithm.</para>
    /// </summary>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public byte[] HashSha256()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      using var algorithm = SHA256.Create();

      return enumerable.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public byte[] HashSha384()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      using var algorithm = SHA384.Create();

      return enumerable.Hash(algorithm);
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of sequence, using <c>SHA512</c> algorithm.</para>
    /// </summary>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public byte[] HashSha512()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      using var algorithm = SHA512.Create();

      return enumerable.Hash(algorithm);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>    
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public string ToHex()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      #if NET10_0_OR_GREATER
      return Convert.ToHexString(enumerable.AsArray());
      #else
      return BitConverter.ToString(enumerable.AsArray()).Replace("-", "");
      #endif
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, Stream, CancellationToken)"/>
    public IEnumerable<byte> WriteTo(Stream destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, Stream)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(Stream destination, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, TextWriter, Encoding)"/>
    public IEnumerable<byte> WriteTo(TextWriter destination, Encoding encoding = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      enumerable.AsArray().ToText(encoding).WriteTo(destination);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, TextWriter, Encoding)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(TextWriter destination, Encoding encoding = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      await enumerable.AsArray().ToText(encoding).WriteToAsync(destination).ConfigureAwait(false);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public IEnumerable<byte> WriteTo(BinaryWriter destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, XmlWriter, Encoding)"/>
    public IEnumerable<byte> WriteTo(XmlWriter destination, Encoding encoding = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable, encoding);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, XmlWriter, Encoding)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(XmlWriter destination, Encoding encoding = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      await destination.WriteBytesAsync(enumerable, encoding).ConfigureAwait(false);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, FileInfo, CancellationToken)"/>
    public IEnumerable<byte> WriteTo(FileInfo destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, FileInfo)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(FileInfo destination, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
    public IEnumerable<byte> WriteTo(Uri destination, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable, timeout, headers);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<IEnumerable<byte>> WriteToAsync(Uri destination, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
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
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/>
    public IEnumerable<byte> WriteTo(Process destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, Process)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(Process destination, CancellationToken cancellation = default)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      await destination.WriteBytesAsync(enumerable, cancellation).ConfigureAwait(false);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/>
    public HttpContent WriteTo(HttpClient client, Uri destination) => client.WriteBytes(enumerable, destination);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, HttpClient, Uri)"/>
    public async Task<HttpContent> WriteToAsync(HttpClient client, Uri destination, CancellationToken cancellation = default) => await client.WriteBytesAsync(enumerable, destination, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, TcpClient, CancellationToken)"/>
    public IEnumerable<byte> WriteTo(TcpClient destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, TcpClient)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(TcpClient destination, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteToAsync(IEnumerable{byte}, UdpClient, CancellationToken)"/>
    public IEnumerable<byte> WriteTo(UdpClient destination)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.WriteBytes(enumerable);

      return enumerable;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTo(IEnumerable{byte}, UdpClient)"/>
    public async Task<IEnumerable<byte>> WriteToAsync(UdpClient destination, CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStream(IEnumerable{byte[]})"/>
    public MemoryStream ToMemoryStream() => enumerable?.Chunk(4096).ToMemoryStream() ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte[]}, CancellationToken)"/>
    public async Task<MemoryStream> ToMemoryStreamAsync(CancellationToken cancellation = default) => enumerable is not null ? await enumerable.Chunk(4096).ToMemoryStreamAsync(cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(enumerable));
  }

  /// <param name="enumerable"></param>
  extension(IEnumerable<byte[]> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStream(IEnumerable{byte})"/>
    public MemoryStream ToMemoryStream()
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
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToMemoryStreamAsync(IEnumerable{byte}, CancellationToken)"/>
    public async Task<MemoryStream> ToMemoryStreamAsync(CancellationToken cancellation = default)
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

  /// <param name="enumerable"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(IEnumerable<T> enumerable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => enumerable is null || enumerable.IsEmpty;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => !enumerable?.Any() ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public bool ContainsDefault
    {
      get
      {
        if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

        return enumerable.Any(element => element.Equals(default(T)));
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public bool ContainsNull
    {
      get
      {
        if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

        return enumerable.Any(element => element is null);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{int, T})"/>
    public IEnumerable<T> ForEach(Action<T> action) => action is not null ? enumerable.ForEach((_, element) => action(element)) : throw new ArgumentNullException(nameof(action));

    /// <summary>
    ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
    /// </summary>
    /// <param name="action">Delegate to be called for each element in a sequence.</param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ForEach{T}(IEnumerable{T}, Action{T})"/>
    public IEnumerable<T> ForEach(Action<int, T> action)
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
    /// <param name="separator">String to use as a separator between concatenated elements from <paramref name="enumerable"/>.</param>
    /// <returns>String which is formed from string representation of each element in a <paramref name="enumerable"/> with a <paramref name="separator"/> between them.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public string Join(string separator = null)
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
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IEnumerable<T> Repeat(int count)
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
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IEnumerable<T> Range(int? offset = null, int? count = null)
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
    /// <param name="random"></param>
    /// <returns>Random member of <paramref name="enumerable"/> sequence. If <paramref name="enumerable"/> contains no elements, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public T Random(Random random = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      var randomizer = random ?? new Random();
      var count = enumerable.Count();

      return count > 0 ? enumerable.ElementAt(randomizer.Next(count)) : default;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="random"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> Randomize(Random random = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      var randomizer = random ?? new Random();

      return enumerable.OrderBy(_ => randomizer.Next());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="EndsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
    public bool StartsWith(IEnumerable<T> other, IEqualityComparer<T> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return enumerable.Take(other.Count()).SequenceEqual(other, comparer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="StartsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
    public bool EndsWith(IEnumerable<T> other, IEqualityComparer<T> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return enumerable.TakeLast(other.Count()).SequenceEqual(other, comparer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    public bool Contains(IEnumerable<T> other, IEqualityComparer<T> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return !enumerable.Except(other, comparer).Any();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public bool ContainsUnique(IEqualityComparer<T> comparer = null) => !enumerable?.GroupBy(x => x, comparer).Where(group => group.Count() > 1).Select(group => group.Key).Any() ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="superset"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="superset"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsSuperset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
    public bool IsSubset(IEnumerable<T> superset, IEqualityComparer<T> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (superset is null) throw new ArgumentNullException(nameof(superset));

      return !enumerable.Except(superset, comparer).Any();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subset"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="subset"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsSubset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
    public bool IsSuperset(IEnumerable<T> subset, IEqualityComparer<T> comparer = null) => subset.IsSubset(enumerable, comparer);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reversed"></param>
    /// <param name="comparer"></param>
    /// <returns>Back self-reference to the given <paramref name="enumerable"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="reversed"/> is <see langword="null"/>.</exception>
    public bool IsReversed(IEnumerable<T> reversed, IEqualityComparer<T> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (reversed is null) throw new ArgumentNullException(nameof(reversed));

      return enumerable.SequenceEqual(reversed.Reverse(), comparer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public T[] AsArray() => enumerable is not null ? enumerable as T[] ?? enumerable.ToArray() : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> AsNotNullable() => enumerable?.Where(element => element is not null) ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> WithCancellation(CancellationToken cancellation)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      cancellation.ThrowIfCancellationRequested();

      foreach (var element in enumerable)
      {
        cancellation.ThrowIfCancellationRequested();

        yield return element;
      }
    }
    
    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public bool IsOrdered(IComparer<T> comparer = null) => enumerable?.Order(comparer).SequenceEqual(enumerable) ?? throw new ArgumentNullException(nameof(enumerable));
    #endif

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max{T}(IEnumerable{T}, IEnumerable{T})"/>
    /// <seealso cref="MinMax{T}(IEnumerable{T}, IEnumerable{T})"/>
    public IEnumerable<T> Min(IEnumerable<T> other)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return enumerable.Count() <= other.Count() ? enumerable : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min{T}(IEnumerable{T}, IEnumerable{T})"/>
    /// <seealso cref="MinMax{T}(IEnumerable{T}, IEnumerable{T})"/>
    public IEnumerable<T> Max(IEnumerable<T> other)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return enumerable.Count() >= other.Count() ? enumerable : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min{T}(IEnumerable{T}, IEnumerable{T})"/>
    /// <seealso cref="Max{T}(IEnumerable{T}, IEnumerable{T})"/>
    public (IEnumerable<T> Min, IEnumerable<T> Max) MinMax(IEnumerable<T> other)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return enumerable.Count() <= other.Count() ? (enumerable, other) : (other, enumerable);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public LinkedList<T> ToLinkedList() => enumerable is not null ? new LinkedList<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IReadOnlyList<T> ToReadOnlyList() => enumerable?.ToList() ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para>Converts sequence of elements into a set collection type.</para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns>Set collection which contains elements from <paramref name="enumerable"/> sequence without duplicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="enumerable"/>'s enumerator.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public SortedSet<T> ToSortedSet(IComparer<T> comparer = null) => enumerable is not null ? new SortedSet<T>(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public Stack<T> ToStack() => enumerable is not null ? new Stack<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public Queue<T> ToQueue() => enumerable is not null ? new Queue<T>(enumerable) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public ArraySegment<T> ToArraySegment() => enumerable is not null ? new ArraySegment<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public Memory<T> ToMemory() => enumerable is not null ? new Memory<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public ReadOnlyMemory<T> ToReadOnlyMemory() => enumerable is not null ? new ReadOnlyMemory<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public Span<T> ToSpan() => enumerable is not null ? new Span<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public ReadOnlySpan<T> ToReadOnlySpan() => enumerable is not null ? new ReadOnlySpan<T>(enumerable.AsArray()) : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<(T item, int index)> ToValueTuple() => enumerable?.Select((item, index) => (item, index)) ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IEnumerable<Tuple<T, int>> ToTuple() => enumerable?.Select((item, index) => new Tuple<T, int>(item, index)) ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    public IEnumerable<Tuple<TKey, T>> ToTuple<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return comparer is not null ? enumerable.OrderBy(key, comparer).Select(tuple => new Tuple<TKey, T>(key(tuple), tuple)) : enumerable.Select(tuple => new Tuple<TKey, T>(key(tuple), tuple));
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="key"/> is <see langword="null"/>.</exception>
    public IEnumerable<(TKey Key, T Value)> ToValueTuple<TKey>(Func<T, TKey> key, IComparer<TKey> comparer = null) where TKey : notnull
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
      if (key is null) throw new ArgumentNullException(nameof(key));

      return comparer is not null ? enumerable.OrderBy(key, comparer).Select(tuple => (Key: key(tuple), Value: tuple)) : enumerable.Select(tuple => (Key: key(tuple), Value: tuple));
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => enumerable is not null && !enumerable.IsEmpty;
    
    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IReadOnlySet<T> ToReadOnlySet(IEqualityComparer<T> comparer = null) => enumerable?.ToHashSet(comparer) ?? throw new ArgumentNullException(nameof(enumerable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public FrozenSet<T> ToFrozenSet(IEqualityComparer<T> comparer = null) => enumerable is not null ? FrozenSet.ToFrozenSet(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public ImmutableQueue<T> ToImmutableQueue() => enumerable is not null ? ImmutableQueue.CreateRange(enumerable) : throw new ArgumentNullException(nameof(enumerable));
    #else
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public async IAsyncEnumerable<T> ToAsyncEnumerable()
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      foreach (var element in enumerable)
      {
        yield return await Task.FromResult(element).ConfigureAwait(false);
      }
    }
    #endif
  }
  
  /// <param name="enumerable"></param>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  extension<TKey, TValue>(IEnumerable<(TKey Key, TValue Value)> enumerable) where TKey : notnull
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public Dictionary<TKey, TValue> ToDictionary(IEqualityComparer<TKey> comparer = null)
    {
      if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));

      var result = new Dictionary<TKey, TValue>(comparer);

      enumerable.ForEach(tuple => result.Add(tuple.Key, tuple.Value));

      return result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary(IEqualityComparer<TKey> comparer = null) => enumerable.ToDictionary(comparer);
    
    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is <see langword="null"/>.</exception>
    public PriorityQueue<TKey, TValue> ToPriorityQueue(IComparer<TValue> comparer = null) => enumerable is not null ? new PriorityQueue<TKey, TValue>(enumerable, comparer) : throw new ArgumentNullException(nameof(enumerable));
    #endif
  }
}