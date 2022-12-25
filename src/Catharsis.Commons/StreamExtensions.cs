using System.Collections;
using System.Text;
using System.IO.Compression;
using System.Runtime.CompilerServices;

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
  public static bool IsStart(this Stream stream) => stream.Position == 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool IsEnd(this Stream stream)
  {
    if (stream.CanSeek)
    {
      return stream.Position == stream.Length;
    }

    using var reader = new StreamReader(stream, leaveOpen: true);
    
    return reader.IsEnd();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool IsEmpty(this Stream stream) => stream.CanSeek ? stream.Length <= 0 : stream.IsEnd();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static TStream Empty<TStream>(this TStream stream) where TStream : Stream
  {
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
    var first = left.CanSeek ? left.Length : left.ToEnumerable().Count();
    var second = right.CanSeek ? right.Length : right.ToEnumerable().Count();

    return first >= second ? left : right;
  }

  /// <summary>
  ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
  /// </summary>
  /// <param name="stream">Original stream that should be buffered.</param>
  /// <param name="size">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
  /// <returns>Buffer version of stream that wraps original <paramref name="stream"/>.</returns>
  public static BufferedStream Buffered(this Stream stream, int? size = null) => size != null ? new BufferedStream(stream, size.Value) : new BufferedStream(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream Synchronized(this Stream stream) => Stream.Synchronized(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream ReadOnly(this Stream stream) => new ReadOnlyStream(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream ReadOnlyForward(this Stream stream) => new ReadOnlyForwardStream(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream WriteOnly(this Stream stream) => new WriteOnlyStream(stream);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream WriteOnlyForward(this Stream stream) => new WriteOnlyForwardStream(stream);

  /// <summary>
  ///   <para>Read the content of this <see cref="Stream"/> and return it as a <see cref="byte"/> array. The input is closed before this method returns.</para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns>The <see cref="byte"/> array from that <paramref name="stream"/></returns>
  public static async IAsyncEnumerable<byte> Bytes(this Stream stream, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    var buffer = new byte[4096];

    for (int count; (count = await stream.ReadAsync(buffer, 0, buffer.Length, cancellation)) > 0;)
    {
      for (var i = 0; i < count; i++)
      {
        yield return buffer[i];
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> Bytes<TStream>(this TStream stream, IEnumerable<byte> bytes, CancellationToken cancellation = default) where TStream : Stream => await stream.Write(bytes, cancellation);

  /// <summary>
  ///   <para>Returns all available text data from a source stream.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Encoding to be used for bytes-to-text conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Text data from a <see cref="stream"/> stream.</returns>
  public static async Task<string> Text(this Stream stream, Encoding? encoding = null) => await stream.ToStreamReader(encoding).Text();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="stream"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> Text<TStream>(this TStream stream, string text, CancellationToken cancellation = default) where TStream : Stream => await stream.Write(text, cancellation: cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<string> Lines(this Stream stream, Encoding? encoding = null)
  {
    await foreach (var line in stream.ToStreamReader(encoding).Lines())
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static TStream UseTemporarily<TStream>(this TStream stream, Action<TStream> action) where TStream : Stream => stream.UseFinally(action, stream => stream.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> Print<TStream>(this TStream destination, object instance, Encoding? encoding = null, CancellationToken cancellation = default) where TStream : Stream
  {
    await using var writer = new StreamWriter(destination, encoding ?? Encoding.Default, 4096, true);

    await writer.Print(instance, cancellation);

    return destination;
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
    stream.Seek(0, SeekOrigin.End);

    return stream;
  }

  /// <summary>
  ///   <para>Writes binary data to target stream.</para>
  /// </summary>
  /// <typeparam name="TStream">Type of target stream.</typeparam>
  /// <param name="to">Target stream to write to.</param>
  /// <param name="bytes">Binary data to write to a stream.</param>
  /// <param name="cancellation"></param>
  /// <returns>Back reference to <see cref="to"/> stream.</returns>
  public static async Task<TStream> Write<TStream>(this TStream to, IEnumerable<byte> bytes, CancellationToken cancellation = default) where TStream : Stream
  {
    if (!bytes.Any())
    {
      return to;
    }

    foreach (var chunk in bytes.Chunk(4096))
    {
      await to.WriteAsync(chunk, 0, chunk.Length, cancellation);
    }

    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> Write<TStream>(this TStream from, Stream to, CancellationToken cancellation = default) where TStream : Stream
  {
    await using var fromStream = from;
    await using var toStream = to;

    await fromStream.CopyToAsync(toStream, cancellation);

    return from;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TStream> Write<TStream>(this TStream to, FileInfo from, CancellationToken cancellation = default) where TStream : Stream
  {
    await from.OpenRead().Write(to, cancellation);

    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TStream"></typeparam>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<TStream> Write<TStream>(this TStream to, Uri from, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers) where TStream : Stream
  {
    await (await from.ToStream(timeout, headers)).Write(to, cancellation);

    return to;
  }

  /// <summary>
  ///   <para>Writes text data to target <see cref="Stream"/>, using specified <see cref="Encoding"/>.</para>
  /// </summary>
  /// <typeparam name="TStream">Type of target stream.</typeparam>
  /// <param name="to">Target stream to write to.</param>
  /// <param name="text">Text to write to a stream.</param>
  /// <param name="encoding">Encoding to be used for text-to-bytes conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="cancellation"></param>
  /// <returns>Back reference to <see cref="to"/> stream.</returns>
  public static async Task<TStream> Write<TStream>(this TStream to, string text, Encoding? encoding = null, CancellationToken cancellation = default) where TStream : Stream
  {
    await using var writer = new StreamWriter(to, encoding ?? Encoding.Default, 4096, true);
    await writer.WriteAsync(text.ToReadOnlyMemory(), cancellation);

    return to;
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
    if (count <= 0)
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
  /// <param name="stream"></param>
  /// <returns></returns>
  public static BrotliStream CompressAsBrotli(this Stream stream) => new(stream, CompressionMode.Compress, false);

  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using Deflate compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current <paramref name="stream"/> stream.</returns>
  public static DeflateStream CompressAsDeflate(this Stream stream) => new(stream, CompressionMode.Compress, false);

  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using GZip compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current stream.</returns>
  public static GZipStream CompressAsGzip(this Stream stream) => new(stream, CompressionMode.Compress, false);

#if NET6_0
  /// <summary>
  ///   <para>Writes sequence of bytes into specified stream, using Zlib compression algorithm.</para>
  /// </summary>
  /// <param name="stream">Destination stream where compressed data should be written.</param>
  /// <returns>Back reference to the current stream.</returns>
  public static ZLibStream CompressAsZlib(this Stream stream) => new(stream, CompressionMode.Compress, false);
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static BrotliStream DecompressAsBrotli(this Stream stream) => new(stream, CompressionMode.Decompress, false);

  /// <summary>
  ///   <para>Decompresses data from a stream, using Deflate algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  /// <remarks>After data decompression process, <paramref name="stream"/> will be closed.</remarks>
  public static DeflateStream DecompressAsDeflate(this Stream stream) => new(stream, CompressionMode.Decompress, false);

  /// <summary>
  ///   <para>Decompresses data from a stream, using GZip algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  public static GZipStream DecompressAsGzip(this Stream stream) => new(stream, CompressionMode.Decompress, false);

#if NET6_0
  /// <summary>
  ///   <para>Decompresses data from a stream, using Zlib algorithm.</para>
  /// </summary>
  /// <param name="stream">Stream to read and decompress data from.</param>
  /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
  public static ZLibStream DecompressAsZlib(this Stream stream) => new(stream, CompressionMode.Decompress, false);
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToEnumerable(this Stream stream) => new StreamEnumerable(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this Stream stream, int count) => new StreamChunkEnumerable(stream, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this Stream stream) => new StreamAsyncEnumerable(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this Stream stream, int count) => new StreamChunkAsyncEnumerable(stream, count);

  /// <summary>
  ///   <para>Returns a <see cref="ToBinaryReader"/> for reading data from specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToBinaryReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Binary reader instance that wraps <see cref="stream"/> stream.</returns>
  public static BinaryReader ToBinaryReader(this Stream stream, Encoding? encoding = null) => encoding != null ? new BinaryReader(stream, encoding) : new BinaryReader(stream);

  /// <summary>
  ///   <para>Returns a <see cref="ToBinaryWriter"/> for writing data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToBinaryWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Binary writer instance that wraps <see cref="stream"/> stream.</returns>
  public static BinaryWriter ToBinaryWriter(this Stream stream, Encoding? encoding = null) => new(stream, encoding ?? Encoding.Default);

  /// <summary>
  ///   <para>Returns a <see cref="ToStreamReader"/> for reading text data from specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToStreamReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Text reader instance that wraps <see cref="stream"/> stream.</returns>
  public static StreamReader ToStreamReader(this Stream stream, Encoding? encoding = null) => new(stream, encoding ?? Encoding.Default);

  /// <summary>
  ///   <para>Returns a <see cref="ToStreamWriter"/> for writing text data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="ToStreamWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>Text writer instance that wraps <see cref="stream"/> stream.</returns>
  public static StreamWriter ToStreamWriter(this Stream stream, Encoding? encoding = null) => new(stream, encoding ?? Encoding.Default, 4096);

  private class ReadOnlyStream : Stream
  {
    private readonly Stream stream;

    public ReadOnlyStream(Stream stream)
    {
      if (!stream.CanRead)
      {
        throw new NotSupportedException();
      }

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
      if (!stream.CanWrite)
      {
        throw new NotSupportedException();
      }

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

  private sealed class StreamEnumerable : IEnumerable<byte>
  {
    private readonly Stream stream;

    public StreamEnumerable(Stream stream) => this.stream = stream;

    public IEnumerator<byte> GetEnumerator() => new StreamEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class StreamEnumerator : IEnumerator<byte>
    {
      private readonly StreamEnumerable parent;
      private readonly byte[] buffer = new byte[1];

      public StreamEnumerator(StreamEnumerable parent) => this.parent = parent;

      public byte Current { get; private set; }

      public bool MoveNext()
      {
        var result = parent.stream.Read(buffer, 0, 1);

        if (result <= 0)
        {
          return false;
        }

        Current = buffer[0];

        return true;
      }

      public void Reset()
      {
        if (!parent.stream.CanSeek)
        {
          throw new InvalidOperationException();
        }

        parent.stream.Seek(-1, SeekOrigin.Begin);
      }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class StreamChunkEnumerable : IEnumerable<byte[]>
  {
    private readonly Stream stream;
    private readonly int count;

    public StreamChunkEnumerable(Stream stream, int count)
    {
      this.stream = stream;
      this.count = count;
    }

    public IEnumerator<byte[]> GetEnumerator() => new StreamChunkEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class StreamChunkEnumerator : IEnumerator<byte[]>
    {
      private readonly StreamChunkEnumerable parent;

      public StreamChunkEnumerator(StreamChunkEnumerable parent)
      {
        this.parent = parent;

        Current = new byte[parent.count];
      }

      public byte[] Current { get; }

      public bool MoveNext() => parent.stream.Read(Current, 0, parent.count) > 0;

      public void Reset()
      {
        if (!parent.stream.CanSeek)
        {
          throw new InvalidOperationException();
        }

        parent.stream.Seek(-1, SeekOrigin.Begin);
      }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class StreamAsyncEnumerable : IAsyncEnumerable<byte>
  {
    private readonly Stream stream;

    public StreamAsyncEnumerable(Stream stream) => this.stream = stream;

    public IAsyncEnumerator<byte> GetAsyncEnumerator(CancellationToken cancellation = default) => new StreamAsyncEnumerator(this, cancellation);

    private sealed class StreamAsyncEnumerator : IAsyncEnumerator<byte>
    {
      private readonly StreamAsyncEnumerable parent;
      private readonly CancellationToken cancellation;
      private readonly byte[] buffer = new byte[1];

      public StreamAsyncEnumerator(StreamAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent;
        this.cancellation = cancellation;
      }

      public ValueTask DisposeAsync() => default;

      public byte Current { get; private set; }

      public async ValueTask<bool> MoveNextAsync()
      {
        var result = await parent.stream.ReadAsync(buffer, 0, 1, cancellation);

        if (result <= 0)
        {
          return false;
        }

        Current = buffer[0];

        return true;
      }
    }
  }

  private sealed class StreamChunkAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private readonly Stream stream;
    private readonly int count;

    public StreamChunkAsyncEnumerable(Stream stream, int count)
    {
      this.stream = stream;
      this.count = count;
    }

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new StreamChunkAsyncEnumerator(this, cancellation);

    private sealed class StreamChunkAsyncEnumerator : IAsyncEnumerator<byte[]>
    {
      private readonly StreamChunkAsyncEnumerable parent;
      private readonly CancellationToken cancellation;

      public StreamChunkAsyncEnumerator(StreamChunkAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent;
        this.cancellation = cancellation;

        Current = new byte[parent.count];
      }

      public ValueTask DisposeAsync() => default;

      public byte[] Current { get; }

      public async ValueTask<bool> MoveNextAsync()
      {
        var result = await parent.stream.ReadAsync(Current, 0, parent.count, cancellation);

        return result > 0;
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