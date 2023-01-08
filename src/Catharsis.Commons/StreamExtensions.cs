using System.Collections;
using System.Text;
using System.IO.Compression;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for streaming I/O types.</para>
/// </summary>
/// <seealso cref="Stream"/>
public static class StreamExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool IsStart(this Stream stream) => stream is not null ? stream.Position == 0 : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool IsEnd(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    if (stream.CanSeek)
    {
      return stream.Position == stream.Length;
    }

    using var reader = stream.ToStreamReader(null, false);
    
    return reader.IsEnd();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool IsEmpty(this Stream stream) => stream is not null ? stream.CanSeek ? stream.Length == 0 : stream.IsEnd() : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static TStream Empty<TStream>(this TStream stream) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    stream.SetLength(0);

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static Stream Min(this Stream left, Stream right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    var first = left.CanSeek ? left.Length : left.ToEnumerable().Count();
    var second = right.CanSeek ? right.Length : right.ToEnumerable().Count();

    return first <= second ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static Stream Max(this Stream left, Stream right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    var first = left.CanSeek ? left.Length : left.ToEnumerable().Count();
    var second = right.CanSeek ? right.Length : right.ToEnumerable().Count();

    return first >= second ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static TStream MoveBy<TStream>(this TStream stream, long offset) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    stream.Seek(offset, SeekOrigin.Current);

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <param name="position"></param>
  /// <returns></returns>
  public static TStream MoveTo<TStream>(this TStream stream, long position) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    stream.Seek(position, SeekOrigin.Begin);

    return stream;
  }

  /// <summary>
  ///   <para>Sets the position within source <see cref="Stream"/> to the beginning of a stream, if this stream supports seeking operations.</para>
  /// </summary>
  /// <typeparam name="TStream">Type of source stream.</typeparam>
  /// <param name="stream">Source stream.</param>
  /// <returns>Back reference to <paramref name="stream"/> stream.</returns>
  /// <seealso cref="Stream.Seek(long, SeekOrigin)"/>
  public static TStream MoveToStart<TStream>(this TStream stream) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    stream.Seek(0, SeekOrigin.Begin);

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static TStream MoveToEnd<TStream>(this TStream stream) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    stream.Seek(0, SeekOrigin.End);

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string[] Lines(this Stream stream, Encoding encoding = null)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToStreamReader(encoding, false);
    
    return reader.Lines().AsArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<string> LinesAsync(this Stream stream, Encoding encoding = null)
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
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static TStream Skip<TStream>(this TStream stream, int count) where TStream : Stream
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
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, Stream destination, Encoding encoding = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToStreamWriter(encoding, false);

    return instance.Print(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, Stream destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = destination.ToStreamWriter(encoding, false);

    return await instance.PrintAsync(writer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static TStream TryFinallyClear<TStream>(this TStream stream, Action<TStream> action) where TStream : Stream
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return stream.TryFinally(action, stream => stream.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream AsSynchronized(this Stream stream) => stream is not null ? Stream.Synchronized(stream) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream AsReadOnly(this Stream stream) => stream is not null ? new ReadOnlyStream(stream) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream AsReadOnlyForward(this Stream stream) => stream is not null ? new ReadOnlyForwardStream(stream) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream AsWriteOnly(this Stream stream) => stream is not null ? new WriteOnlyStream(stream) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream AsWriteOnlyForward(this Stream stream) => stream is not null ? new WriteOnlyForwardStream(stream) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this Stream stream, bool close = false) => stream is not null ? stream.ToEnumerable(close) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Read the content of this <see cref="Stream"/> and return it as a <see cref="byte"/> array. The input is closed before this method returns.</para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns>The <see cref="byte"/> array from that <paramref name="stream"/></returns>
  public static IAsyncEnumerable<byte> ToBytesAsync(this Stream stream, bool close = false) => stream is not null ? stream.ToAsyncEnumerable(close) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string ToText(this Stream stream, Encoding encoding = null)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToStreamReader(encoding, false);
    
    return reader.ToText();
  }

  /// <summary>
  ///   <para>Returns all available text data from a source stream.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Encoding to be used for bytes-to-text conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Text data from a <see cref="stream"/> stream.</returns>
  public static async Task<string> ToTextAsync(this Stream stream, Encoding encoding = null)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToStreamReader(encoding, false);

    return await reader.ToTextAsync().ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static TStream WriteBytes<TStream>(this TStream destination, IEnumerable<byte> bytes) where TStream : Stream
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    foreach (var chunk in bytes.Chunk(4096))
    {
      destination.Write(chunk, 0, chunk.Length);
    }

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> WriteBytesAsync<TStream>(this TStream destination, IEnumerable<byte> bytes, CancellationToken cancellation = default) where TStream : Stream
  {
    if (destination is null)
      throw new ArgumentNullException(nameof(destination));
    if (bytes is null)
      throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    foreach (var chunk in bytes.Chunk(4096))
    {
      await destination.WriteAsync(chunk, 0, chunk.Length, cancellation).ConfigureAwait(false);
    }

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static TStream WriteText<TStream>(this TStream destination, string text, Encoding encoding = null) where TStream : Stream
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var writer = destination.ToStreamWriter(encoding, false);

    writer.Write(text);
    
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> WriteTextAsync<TStream>(this TStream destination, string text, Encoding encoding = null, CancellationToken cancellation = default) where TStream : Stream
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = destination.ToStreamWriter(encoding, false);
    
    await writer.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);
    
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
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
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Stream destination, CancellationToken cancellation = default)
  {
    if (bytes is null)
      throw new ArgumentNullException(nameof(bytes));
    if (destination is null)
      throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, Stream destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, Stream destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    
    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static BrotliStream CompressAsBrotli(this Stream stream) => stream is not null ? new BrotliStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using Deflate compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current <paramref name="stream"/> stream.</returns>
  public static DeflateStream CompressAsDeflate(this Stream stream) => stream is not null ? new DeflateStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using GZip compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current stream.</returns>
  public static GZipStream CompressAsGzip(this Stream stream) => stream is not null ? new GZipStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));

#if NET6_0
  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using Zlib compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current stream.</returns>
  public static ZLibStream CompressAsZlib(this Stream stream) => stream is not null ? new ZLibStream(stream, CompressionMode.Compress) : throw new ArgumentNullException(nameof(stream));
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static BrotliStream DecompressAsBrotli(this Stream stream) => stream is not null ? new BrotliStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Decompresses data from a stream, using Deflate algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  /// <remarks>After data decompression process, <paramref name="stream"/> will be closed.</remarks>
  public static DeflateStream DecompressAsDeflate(this Stream stream) => stream is not null ? new DeflateStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Decompresses data from a stream, using GZip algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  public static GZipStream DecompressAsGzip(this Stream stream) => stream is not null ? new GZipStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));

#if NET6_0
  /// <summary>
  ///   <para>Decompresses data from a stream, using Zlib algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  public static ZLibStream DecompressAsZlib(this Stream stream) => stream is not null ? new ZLibStream(stream, CompressionMode.Decompress) : throw new ArgumentNullException(nameof(stream));
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToEnumerable(this Stream stream, bool close = false) => stream is not null ? stream.ToEnumerable(4096, close).SelectMany(bytes => bytes) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this Stream stream, int count, bool close = false)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new StreamEnumerable(stream, count, close);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToAsyncEnumerable(this Stream stream, bool close = false)
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
  /// <param name="stream"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this Stream stream, int count, bool close = false)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new StreamAsyncEnumerable(stream, count, close);
  }

  /// <summary>
  ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
  /// </summary>
  /// <param name="stream">Original stream that should be buffered.</param>
  /// <param name="bufferSize">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
  /// <returns>Buffer version of stream that wraps original <paramref name="stream"/>.</returns>
  public static BufferedStream ToBufferedStream(this Stream stream, int? bufferSize = null)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (bufferSize is <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));

    return bufferSize != null ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
  }

  /// <summary>
  ///   <para>Returns a <see cref="ToBinaryReader"/> for reading data from specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToBinaryReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="close"></param>
  /// <returns>Binary reader instance that wraps <see cref="stream"/> stream.</returns>
  public static BinaryReader ToBinaryReader(this Stream stream, Encoding encoding = null, bool close = true) => stream is not null ? new BinaryReader(stream, encoding ?? Encoding.Default, !close) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Returns a <see cref="ToBinaryWriter"/> for writing data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToBinaryWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="close"></param>
  /// <returns>Binary writer instance that wraps <see cref="stream"/> stream.</returns>
  public static BinaryWriter ToBinaryWriter(this Stream stream, Encoding encoding = null, bool close = true) => stream is not null ? new BinaryWriter(stream, encoding ?? Encoding.Default, !close) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Returns a <see cref="ToStreamReader"/> for reading text data from specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToStreamReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="close"></param>
  /// <returns>Text reader instance that wraps <see cref="stream"/> stream.</returns> 
  public static StreamReader ToStreamReader(this Stream stream, Encoding encoding = null, bool close = true) => stream is not null ? new StreamReader(stream, encoding ?? Encoding.Default, true, -1, !close) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para>Returns a <see cref="ToStreamWriter"/> for writing text data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToStreamWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="close"></param>
  /// <returns>Text writer instance that wraps <see cref="stream"/> stream.</returns>
  public static StreamWriter ToStreamWriter(this Stream stream, Encoding encoding = null, bool close = true) => stream is not null ? new StreamWriter(stream, encoding ?? Encoding.Default, -1, !close) : throw new ArgumentNullException(nameof(stream));

  private class ReadOnlyStream : Stream
  {
    private readonly Stream stream;

    public ReadOnlyStream(Stream stream)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (!stream.CanRead) throw new NotSupportedException();

      this.stream = stream;
    }

    public override bool CanRead => stream.CanRead;

    public override bool CanWrite => false;

    public override bool CanSeek => stream.CanSeek;

    public override long Length => stream.Length;

    public override long Position
    {
      get => stream.Position;
      set => stream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) => stream.Read(buffer, offset, count);

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);

    public override void SetLength(long value)
    {
      stream.SetLength(value);
    }

    public override void Flush()
    {
      stream.Flush();
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
      {
        return;
      }

      stream.Dispose();
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
    private readonly Stream stream;

    public WriteOnlyStream(Stream stream)
    {
      if (stream is null) throw new ArgumentNullException(nameof(stream));
      if (!stream.CanWrite) throw new NotSupportedException();

      this.stream = stream;
    }

    public override bool CanRead => false;

    public override bool CanWrite => stream.CanWrite;

    public override bool CanSeek => stream.CanSeek;

    public override long Length => stream.Length;

    public override long Position
    {
      get => stream.Position;
      set => stream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => stream.Write(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);

    public override void SetLength(long value)
    {
      stream.SetLength(value);
    }

    public override void Flush()
    {
      stream.Flush();
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
      {
        return;
      }

      stream.Dispose();
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
    private readonly Stream stream;
    private readonly int count;
    private readonly bool close;

    public StreamEnumerable(Stream stream, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
      this.count = count;
      this.close = close;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private readonly StreamEnumerable parent;
      private readonly byte[] buffer;

      public Enumerator(StreamEnumerable parent)
      {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        buffer = new byte[parent.count];
      }

      public byte[] Current { get; private set; } = Array.Empty<byte>();

      public bool MoveNext()
      {
        var count = parent.stream.Read(buffer, 0, parent.count);

        if (count > 0)
        {
          Current = buffer.Range(0, count);
        }

        return count > 0;
      }

      public void Reset() => throw new NotSupportedException();

      public void Dispose()
      {
        if (parent.close)
        {
          parent.stream.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class StreamAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private readonly Stream stream;
    private readonly int count;
    private readonly bool close;

    public StreamAsyncEnumerable(Stream stream, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
      this.count = count;
      this.close = close;
    }

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this, cancellation);

    private sealed class Enumerator : IAsyncEnumerator<byte[]>
    {
      private readonly StreamAsyncEnumerable parent;
      private readonly CancellationToken cancellation;
      private readonly byte[] buffer;

      public Enumerator(StreamAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        this.cancellation = cancellation;
        buffer = new byte[parent.count];
      }

      public ValueTask DisposeAsync() => parent.close ? parent.stream.DisposeAsync() : default;

      public byte[] Current { get; private set; } = Array.Empty<byte>();

      public async ValueTask<bool> MoveNextAsync()
      {
        var count = await parent.stream.ReadAsync(buffer, 0, parent.count, cancellation).ConfigureAwait(false);

        if (count > 0)
        {
          Current = buffer.Range(0, count);
        }

        return count > 0;
      }
    }
  }

#if !NET6_0
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