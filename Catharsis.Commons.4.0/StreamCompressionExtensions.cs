using System;
using System.IO;
using System.IO.Compression;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class StreamCompressionExtensions
  {
    /// <summary>
    ///   <para>Creates a Deflate compression stream from a specified one, using either compression or uncompression logic.</para>
    /// </summary>
    /// <param name="stream">Wrapped stream which contents will be compressed/uncompressed.</param>
    /// <param name="mode">Deflate algorithm working mode, specifying whether to automatically compress data when writing to a stream, or uncompress data when reading from it.</param>
    /// <returns>Deflate compression stream which wraps original <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate{T}(T, byte[])"/>
    /// <seealso cref="Deflate(Stream)"/>
    public static DeflateStream Deflate(this Stream stream, CompressionMode mode)
    {
      Assertion.NotNull(stream);

      return new DeflateStream(stream, mode);
    }

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using Deflate compression algorithm.</para>
    /// </summary>
    /// <typeparam name="STREAM">Type of target <see cref="Stream"/>.</typeparam>
    /// <param name="stream">Destination stream where compressed data should be written.</param>
    /// <param name="bytes">Data to be compressed and written to the stream.</param>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate(Stream, CompressionMode)"/>
    /// <seealso cref="Deflate(Stream)"/>
    public static STREAM Deflate<STREAM>(this STREAM stream, byte[] bytes) where STREAM : Stream
    {
      Assertion.NotNull(stream);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        new DeflateStream(stream, CompressionMode.Compress, true).Write(bytes).Close();
      }

      return stream;
    }

    /// <summary>
    ///   <para>Decompresses data from a stream, using Deflate algorithm.</para>
    /// </summary>
    /// <param name="stream">Stream to read and decompress data from.</param>
    /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate(Stream, CompressionMode)"/>
    /// <seealso cref="Deflate{T}(T, byte[])"/>
    /// <remarks>After data decompression process, <paramref name="stream"/> will be closed.</remarks>
    public static byte[] Deflate(this Stream stream)
    {
      Assertion.NotNull(stream);

      return new DeflateStream(stream, CompressionMode.Decompress, true).Bytes(true);
    }

    /// <summary>
    ///   <para>Creates a GZip compression stream from a specified one, using either compression or uncompression logic.</para>
    /// </summary>
    /// <param name="stream">Wrapped stream which contents will be compressed/uncompressed.</param>
    /// <param name="mode">GZip algorithm working mode, specifying whether to automatically compress data when writing to a stream, or uncompress data when reading from it.</param>
    /// <returns>GZip compression stream which wraps original <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip{T}(T, byte[])"/>
    /// <seealso cref="GZip(Stream)"/>
    public static GZipStream GZip(this Stream stream, CompressionMode mode)
    {
      Assertion.NotNull(stream);

      return new GZipStream(stream, mode);
    }

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using GZip compression algorithm.</para>
    /// </summary>
    /// <typeparam name="STREAM">Type of target <see cref="Stream"/>.</typeparam>
    /// <param name="stream">Destination stream where compressed data should be written.</param>
    /// <param name="bytes">Data to be compressed and written to the stream.</param>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip(Stream, CompressionMode)"/>
    /// <seealso cref="GZip(Stream)"/>
    public static STREAM GZip<STREAM>(this STREAM stream, byte[] bytes) where STREAM : Stream
    {
      Assertion.NotNull(stream);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        new GZipStream(stream, CompressionMode.Compress, true).Write(bytes).Close();
      }

      return stream;
    }

    /// <summary>
    ///   <para>Decompresses data from a stream, using GZip algorithm.</para>
    /// </summary>
    /// <param name="stream">Stream to read and decompress data from.</param>
    /// <returns>Decompressed contents of current <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip(Stream, CompressionMode)"/>
    /// <seealso cref="GZip{T}(T, byte[])"/>
    public static byte[] GZip(this Stream stream)
    {
      Assertion.NotNull(stream);

      return new GZipStream(stream, CompressionMode.Decompress, true).Bytes(true);
    }
  }
}