using System.Collections;
using System.Text;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for streaming I/O types.</para>
/// </summary>
/// <seealso cref="Stream"/>
public static class StreamExtensions
{
  /// <param name="stream"></param>
  extension(Stream stream)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEnd"/>
    public bool IsStart => stream is not null ? stream.Position == 0 : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsStart"/>
    public bool IsEnd
    {
      get
      {
        if (stream is null) throw new ArgumentNullException(nameof(stream));

        if (stream.CanSeek)
        {
          return stream.Position == stream.Length;
        }

        using var reader = stream.ToStreamReader(null, false);

        return reader.IsEnd;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public bool IsReadOnly => stream is not null ? stream.CanRead && !stream.CanWrite : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public bool IsWriteOnly => stream is not null ? stream.CanWrite && !stream.CanRead : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public bool IsOperable => stream is not null ? stream.CanRead || stream.CanWrite : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="LinesAsync(Stream, Encoding)"/>
    public string[] Lines(Encoding encoding = null)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToStreamReader(encoding, false);

      return reader.Lines().AsArray();
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string[] Lines => stream.Lines();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Lines(Stream, Encoding)"/>
    public async IAsyncEnumerable<string> LinesAsync(Encoding encoding = null)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToStreamReader(encoding, false);

      await foreach (var line in reader.LinesAsync().ConfigureAwait(false))
      {
        yield return line;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public Stream AsSynchronized() => stream is not null ? Stream.Synchronized(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public Stream AsReadOnly() => stream is not null ? new ReadOnlyStream(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public Stream AsReadOnlyForward() => stream is not null ? new ReadOnlyForwardStream(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public Stream AsWriteOnly() => stream is not null ? new WriteOnlyStream(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public Stream AsWriteOnlyForward() => stream is not null ? new WriteOnlyForwardStream(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecompressAsBrotli(Stream)"/>
    public BrotliStream CompressAsBrotli() => stream is not null ? new BrotliStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CompressAsBrotli(Stream)"/>
    public BrotliStream DecompressAsBrotli() => stream is not null ? new BrotliStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using Deflate compression algorithm.</para>
    /// </summary>
    /// <returns>Back reference to the current <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecompressAsDeflate(Stream)"/>
    public DeflateStream CompressAsDeflate() => stream is not null ? new DeflateStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Decompresses data from a stream, using Deflate algorithm.</para>
    /// </summary>
    /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
    /// <remarks>After data decompression process, <paramref name="stream"/> will be closed.</remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CompressAsDeflate(Stream)"/>
    public DeflateStream DecompressAsDeflate() => stream is not null ? new DeflateStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using GZip compression algorithm.</para>
    /// </summary>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecompressAsGzip(Stream)"/>
    public GZipStream CompressAsGzip() => stream is not null ? new GZipStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Decompresses data from a stream, using GZip algorithm.</para>
    /// </summary>
    /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CompressAsGzip(Stream)"/>
    public GZipStream DecompressAsGzip() => stream is not null ? new GZipStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));
    
    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using Zlib compression algorithm.</para>
    /// </summary>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecompressAsZlib(Stream)"/>
    public ZLibStream CompressAsZlib() => stream is not null ? new ZLibStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Decompresses data from a stream, using Zlib algorithm.</para>
    /// </summary>
    /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CompressAsZlib(Stream)"/>
    public ZLibStream DecompressAsZlib() => stream is not null ? new ZLibStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));
    #endif
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="EncryptAsync(Stream, SymmetricAlgorithm, CancellationToken)"/>
    public byte[] Encrypt(SymmetricAlgorithm algorithm) => algorithm.Encrypt(stream);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Encrypt(Stream, SymmetricAlgorithm)"/>
    public async Task<byte[]> EncryptAsync(SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(stream, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecryptAsync(Stream, SymmetricAlgorithm, CancellationToken)"/>
    public byte[] Decrypt(SymmetricAlgorithm algorithm) => algorithm.Decrypt(stream);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Decrypt(Stream, SymmetricAlgorithm)"/>
    public async Task<byte[]> DecryptAsync(SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(stream, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="HashAsync(Stream, HashAlgorithm, CancellationToken)"/>
    public byte[] Hash(HashAlgorithm algorithm)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

      return algorithm.ComputeHash(stream);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Hash(Stream, HashAlgorithm)"/>
    public async Task<byte[]> HashAsync(HashAlgorithm algorithm, CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

      #if NET10_0_OR_GREATER
      return await algorithm.ComputeHashAsync(stream, cancellation).ConfigureAwait(false);
      #else
      return algorithm.ComputeHash(stream);
      #endif
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashMd5Async(Stream, CancellationToken)"/>
    public byte[] HashMd5()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var algorithm = MD5.Create();

      return stream.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashMd5(Stream)"/>
    public async Task<byte[]> HashMd5Async(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var algorithm = MD5.Create();

      return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha1Async(Stream, CancellationToken)"/>
    public byte[] HashSha1()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var algorithm = SHA1.Create();

      return stream.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha1(Stream)"/>
    public async Task<byte[]> HashSha1Async(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var algorithm = SHA1.Create();

      return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha256Async(Stream, CancellationToken)"/>
    public byte[] HashSha256()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var algorithm = SHA256.Create();

      return stream.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha256(Stream)"/>
    public async Task<byte[]> HashSha256Async(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var algorithm = SHA256.Create();

      return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha256Async(Stream, CancellationToken)"/>
    public byte[] HashSha384()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var algorithm = SHA384.Create();

      return stream.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha384(Stream)"/>
    public async Task<byte[]> HashSha384Async(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var algorithm = SHA384.Create();

      return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha512Async(Stream, CancellationToken)"/>
    public byte[] HashSha512()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var algorithm = SHA512.Create();

      return stream.Hash(algorithm);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="HashSha512(Stream)"/>
    public async Task<byte[]> HashSha512Async(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var algorithm = SHA512.Create();

      return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => stream is null || stream.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="Stream"/> instance can be considered "empty", meaning its length is zero or the end of the stream has been reached.</para>
    /// </summary>
    /// <value>If the specified <paramref name="stream"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => stream is not null ? stream.CanSeek ? stream.Length == 0 : stream.IsEnd : throw new ArgumentNullException(nameof(stream));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max(Stream, Stream)"/>
    /// <seealso cref="MinMax(Stream, Stream)"/>
    public Stream Min(Stream other)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (other is null) throw new ArgumentNullException(nameof(other));

      var leftCount = stream.CanSeek ? stream.Length : stream.ToEnumerable().Count();
      var rightCount = other.CanSeek ? other.Length : other.ToEnumerable().Count();

      return leftCount <= rightCount ? stream : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(Stream, Stream)"/>
    /// <seealso cref="MinMax(Stream, Stream)"/>
    public Stream Max(Stream other)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (other is null) throw new ArgumentNullException(nameof(other));

      var leftCount = stream.CanSeek ? stream.Length : stream.ToEnumerable().Count();
      var rightCount = other.CanSeek ? other.Length : other.ToEnumerable().Count();

      return leftCount >= rightCount ? stream : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(Stream, Stream)"/>
    /// <seealso cref="Max(Stream, Stream)"/>
    public (Stream Min, Stream Max) MinMax(Stream other)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (other is null) throw new ArgumentNullException(nameof(other));

      var leftCount = stream.CanSeek ? stream.Length : stream.ToEnumerable().Count();
      var rightCount = other.CanSeek ? other.Length : other.ToEnumerable().Count();

      return leftCount <= rightCount ? (left: stream, right: other) : (right: other, left: stream);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public T DeserializeAsDataContract<T>(params Type[] types)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToXmlReader(false);

      return reader.DeserializeAsDataContract<T>(types);
    }

    /// <summary>
    ///   <para>Deserializes XML contents of stream into object of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <returns>Deserialized XML contents of source <paramref name="stream"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public T DeserializeAsXml<T>(params Type[] types)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToXmlReader(false);

      return reader.DeserializeAsXml<T>(types);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(Stream, int, bool)"/>
    public IEnumerable<byte> ToEnumerable(bool close = false) => stream?.ToEnumerable(4096, close).SelectMany(bytes => bytes) ?? throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="ToEnumerable(Stream, bool)"/>
    public IEnumerable<byte[]> ToEnumerable(int count, bool close = false)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      return new StreamEnumerable(stream, count, close);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(Stream, int, bool)"/>
    public async IAsyncEnumerable<byte> ToAsyncEnumerable(bool close = false)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      await foreach (var elements in stream.ToAsyncEnumerable(4096, close).ConfigureAwait(false))
      {
        foreach (var element in elements)
        {
          yield return element;
        }
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="ToAsyncEnumerable(Stream, bool)"/>
    public IAsyncEnumerable<byte[]> ToAsyncEnumerable(int count, bool close = false)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      return new StreamAsyncEnumerable(stream, count, close);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(Stream, bool)"/>
    public IEnumerable<byte> ToBytes(bool close = false) => stream?.ToEnumerable(close) ?? throw new ArgumentNullException(nameof(stream));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => stream.ToBytes().ToArray();

    /// <summary>
    ///   <para>Read the content of this <see cref="Stream"/> and return it as a <see cref="byte"/> array. The input is closed before this method returns.</para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns>The <see cref="byte"/> array from that <paramref name="stream"/></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(Stream, bool)"/>
    public IAsyncEnumerable<byte> ToBytesAsync(bool close = false) => stream?.ToAsyncEnumerable(close) ?? throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(Stream, Encoding)"/>
    public string ToText(Encoding encoding = null, bool close = false)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToStreamReader(encoding, close);

      return reader.ToText();
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string Text => stream.ToText();

    /// <summary>
    ///   <para>Returns all available text data from a source stream.</para>
    /// </summary>
    /// <param name="encoding">Encoding to be used for bytes-to-text conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Text data from a <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(Stream, Encoding, bool)"/>
    public async Task<string> ToTextAsync(Encoding encoding = null)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToStreamReader(encoding, false);

      return await reader.ToTextAsync().ConfigureAwait(false);
    }

    /// <summary>
    ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
    /// </summary>
    /// <param name="bufferSize">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
    /// <returns>Buffer version of stream that wraps original <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public BufferedStream ToBufferedStream(int? bufferSize = null)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (bufferSize is <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));

      return bufferSize is not null ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
    }

    /// <summary>
    ///   <para>Returns a <see cref="ToBinaryReader"/> for reading data from specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to use by <see cref="ToBinaryReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <param name="close"></param>
    /// <returns>Binary reader instance that wraps <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public BinaryReader ToBinaryReader(Encoding encoding = null, bool close = true) => stream is not null ? new BinaryReader(stream, encoding ?? Encoding.Default, !close) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Returns a <see cref="ToBinaryWriter"/> for writing data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to use by <see cref="ToBinaryWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <param name="close"></param>
    /// <returns>Binary writer instance that wraps <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public BinaryWriter ToBinaryWriter(Encoding encoding = null, bool close = true) => stream is not null ? new BinaryWriter(stream, encoding ?? Encoding.Default, !close) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Returns a <see cref="ToStreamReader"/> for reading text data from specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to use by <see cref="ToStreamReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <param name="close"></param>
    /// <returns>Text reader instance that wraps <paramref name="stream"/> stream.</returns> 
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public StreamReader ToStreamReader(Encoding encoding = null, bool close = true) => stream is not null ? new StreamReader(stream, encoding ?? Encoding.Default, true, -1, !close) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para>Returns a <see cref="ToStreamWriter"/> for writing text data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to use by <see cref="ToStreamWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <param name="close"></param>
    /// <returns>Text writer instance that wraps <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public StreamWriter ToStreamWriter(Encoding encoding = null, bool close = true) => stream is not null ? new StreamWriter(stream, encoding ?? Encoding.Default, -1, !close) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public XmlReader ToXmlReader(bool close = true) => stream is not null ? XmlReader.Create(stream, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true }) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public StreamContent ToStreamContent() => stream is not null ? new StreamContent(stream) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public XmlDictionaryReader ToXmlDictionaryReader(bool close = true) => stream.ToXmlReader(close).ToXmlDictionaryReader();

    /// <summary>
    ///   <para>Returns a <see cref="XmlWriter"/> for writing XML data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to use by <see cref="XmlWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <param name="close"></param>
    /// <returns>XML writer instance that wraps <paramref name="stream"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public XmlWriter ToXmlWriter(Encoding encoding = null, bool close = true) => stream is not null ? XmlWriter.Create(stream, new XmlWriterSettings { CloseOutput = close, Indent = true, Encoding = encoding ?? Encoding.Default }) : throw new ArgumentNullException(nameof(stream));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public XmlDictionaryWriter ToXmlDictionaryWriter(Encoding encoding = null, bool close = true) => stream.ToXmlWriter(encoding, close).ToXmlDictionaryWriter();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public XmlDocument ToXmlDocument()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToXmlReader(false);

      return reader.ToXmlDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocumentAsync(Stream, CancellationToken)"/>
    public XDocument ToXDocument()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      using var reader = stream.ToXmlReader(false);

      return reader.ToXDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocument(Stream)"/>
    public async Task<XDocument> ToXDocumentAsync(CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      cancellation.ThrowIfCancellationRequested();

      using var reader = stream.ToXmlReader(false);

      return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => stream is not null && stream.CanSeek && stream.Length > 0;
  }

  /// <param name="stream"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T stream) where T : Stream
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T Skip(int count)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      if (count == 0)
      {
        return stream;
      }

      if (stream.CanSeek)
      {
        stream.MoveBy(count);
      }
      else
      {
        count.Times(() => stream.ReadByte());
      }

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public T MoveBy(long offset)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      stream.Seek(offset, SeekOrigin.Current);

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public T MoveTo(long position)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      stream.Seek(position, SeekOrigin.Begin);

      return stream;
    }

    /// <summary>
    ///   <para>Sets the position within source <see cref="Stream"/> to the beginning of a stream, if this stream supports seeking operations.</para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <seealso cref="Stream.Seek(long, SeekOrigin)"/>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="MoveToEnd{TStream}(TStream)"/>
    public T MoveToStart()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      stream.Seek(0, SeekOrigin.Begin);

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <seealso cref="MoveToStart{TStream}(TStream)"/>
    public T MoveToEnd()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      stream.Seek(0, SeekOrigin.End);

      return stream;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <see langword="null"/>.</exception>
    public T Empty()
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));

      stream.SetLength(0);

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public T TryFinallyClear(Action<T> action)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return stream.TryFinally(action, x => x.Empty());
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/>
    public T WriteBytes(IEnumerable<byte> bytes)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      foreach (var chunk in bytes.Chunk(4096))
      {
        stream.Write(chunk, 0, chunk.Length);
      }

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes{TStream}(TStream, IEnumerable{byte})"/>
    public async Task<T> WriteBytesAsync(IEnumerable<byte> bytes, CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      cancellation.ThrowIfCancellationRequested();

      foreach (var chunk in bytes.Chunk(4096))
      {
        await stream.WriteAsync(chunk, 0, chunk.Length, cancellation).ConfigureAwait(false);
      }

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync{TStream}(TStream, string, Encoding, CancellationToken)"/>
    public T WriteText(string text, Encoding encoding = null)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (text is null) throw new ArgumentNullException(nameof(text));

      using var writer = stream.ToStreamWriter(encoding, false);

      writer.Write(text);

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText{TStream}(TStream, string, Encoding)"/>
    public async Task<T> WriteTextAsync(string text, Encoding encoding = null, CancellationToken cancellation = default)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (text is null) throw new ArgumentNullException(nameof(text));

      cancellation.ThrowIfCancellationRequested();

      await using var writer = stream.ToStreamWriter(encoding, false);

      await writer.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

      return stream;
    }
  }

  private class ReadOnlyStream : Stream
  {
    private Stream Stream { get; }

    public ReadOnlyStream(Stream stream)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (!stream.CanRead) throw new NotSupportedException();

      Stream = stream;
    }

    public override bool CanRead => Stream.CanRead;

    public override bool CanWrite => false;

    public override bool CanSeek => Stream.CanSeek;

    public override long Length => Stream.Length;

    public override long Position
    {
      get => Stream.Position;
      set => Stream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) => Stream.Read(buffer, offset, count);

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);

    public override void SetLength(long value)
    {
      Stream.SetLength(value);
    }

    public override void Flush()
    {
      Stream.Flush();
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
      {
        return;
      }

      Stream.Dispose();
    }
  }

  private sealed class ReadOnlyForwardStream : ReadOnlyStream
  {
    public ReadOnlyForwardStream(Stream stream) : base(stream)
    {
    }

    public override bool CanSeek => false;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
      get => throw new NotSupportedException();
      set => throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();
  }

  private class WriteOnlyStream : Stream
  {
    private Stream Stream { get; }

    public WriteOnlyStream(Stream stream)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (!stream.CanWrite) throw new NotSupportedException();

      Stream = stream;
    }

    public override bool CanRead => false;

    public override bool CanWrite => Stream.CanWrite;

    public override bool CanSeek => Stream.CanSeek;

    public override long Length => Stream.Length;

    public override long Position
    {
      get => Stream.Position;
      set => Stream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => Stream.Write(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);

    public override void SetLength(long value)
    {
      Stream.SetLength(value);
    }

    public override void Flush()
    {
      Stream.Flush();
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
      {
        return;
      }

      Stream.Dispose();
    }
  }

  private class WriteOnlyForwardStream : WriteOnlyStream
  {
    public WriteOnlyForwardStream(Stream stream) : base(stream)
    {
    }

    public override bool CanSeek => false;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
      get => throw new NotSupportedException();
      set => throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();
  }

  private sealed class StreamEnumerable : IEnumerable<byte[]>
  {
    private Stream Stream { get; }
    private int Count { get; }
    private bool Close { get; }

    public StreamEnumerable(Stream stream, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      Stream = stream ?? throw new ArgumentNullException(nameof(stream));
      Count = count;
      Close = close;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private StreamEnumerable Parent { get; }
      private byte[] Buffer { get; }

      public Enumerator(StreamEnumerable parent)
      {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Buffer = new byte[parent.Count];
      }

      public byte[] Current { get; private set; } = [];

      public bool MoveNext()
      {
        var count = Parent.Stream.Read(Buffer, 0, Parent.Count);

        if (count > 0)
        {
          Current = Buffer.Range(0, count);
        }

        return count > 0;
      }

      public void Reset() => throw new NotSupportedException();

      public void Dispose()
      {
        if (Parent.Close)
        {
          Parent.Stream.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class StreamAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private Stream Stream { get; }
    private int Count { get; }
    private bool Close { get; }

    public StreamAsyncEnumerable(Stream stream, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      Stream = stream ?? throw new ArgumentNullException(nameof(stream));
      Count = count;
      Close = close;
    }

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this, cancellation);

    private sealed class Enumerator : IAsyncEnumerator<byte[]>
    {
      private StreamAsyncEnumerable Parent { get; }
      private CancellationToken Cancellation { get; }
      private byte[] Buffer { get; }

      public Enumerator(StreamAsyncEnumerable parent, CancellationToken cancellation)
      {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Cancellation = cancellation;
        Buffer = new byte[parent.Count];
      }

      public ValueTask DisposeAsync() => Parent.Close ? Parent.Stream.DisposeAsync() : default;

      public byte[] Current { get; private set; } = [];

      public async ValueTask<bool> MoveNextAsync()
      {
        var count = await Parent.Stream.ReadAsync(Buffer, 0, Parent.Count, Cancellation).ConfigureAwait(false);

        if (count > 0)
        {
          Current = Buffer.Range(0, count);
        }

        return count > 0;
      }
    }
  }

#if !NET10_0_OR_GREATER
  public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size) => ChunkIterator(source, size);

  private static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int size)
  {
    using var e = source.GetEnumerator();

    while (e.MoveNext())
    {
      var chunk = new TSource[size];
      chunk[0] = e.Current;

      var i = 1;
      for (; i < chunk.Length && e.MoveNext(); i++)
      {
        chunk[i] = e.Current;
      }

      if (i == chunk.Length)
      {
        yield return chunk;
      }
      else
      {
        Array.Resize(ref chunk, i);
        yield return chunk;
        yield break;
      }
    }
  }
#endif
}