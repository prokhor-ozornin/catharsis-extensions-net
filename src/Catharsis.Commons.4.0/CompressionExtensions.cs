using System;
using System.IO;
using System.IO.Compression;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension compression-related extensions methods.</para>
  /// </summary>
  public static class CompressionExtensions
  {
    /// <summary>
    ///   <para>Creates a Deflate compression stream from a specified one, using either compression or uncompression logic.</para>
    /// </summary>
    /// <param name="self">Wrapped stream which contents will be compressed/uncompressed.</param>
    /// <param name="mode">Deflate algorithm working mode, specifying whether to automatically compress data when writing to a stream, or uncompress data when reading from it.</param>
    /// <returns>Deflate compression stream which wraps original <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate{T}(T, byte[])"/>
    /// <seealso cref="Deflate(Stream)"/>
    public static DeflateStream Deflate(this Stream self, CompressionMode mode)
    {
      Assertion.NotNull(self);

      return new DeflateStream(self, mode);
    }

    /// <summary>
    ///   <para>Decompresses data from a stream, using Deflate algorithm.</para>
    /// </summary>
    /// <param name="self">Stream to read and decompress data from.</param>
    /// <returns>Decompressed contents of current <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate(Stream, CompressionMode)"/>
    /// <seealso cref="Deflate{T}(T, byte[])"/>
    /// <remarks>After data decompression process, <paramref name="self"/> will be closed.</remarks>
    public static byte[] Deflate(this Stream self)
    {
      Assertion.NotNull(self);

      return new DeflateStream(self, CompressionMode.Decompress, true).Bytes(true);
    }

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using Deflate compression algorithm.</para>
    /// </summary>
    /// <typeparam name="STREAM">Type of target <see cref="Stream"/>.</typeparam>
    /// <param name="self">Destination stream where compressed data should be written.</param>
    /// <param name="bytes">Data to be compressed and written to the stream.</param>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DeflateStream"/>
    /// <seealso cref="Deflate(Stream, CompressionMode)"/>
    /// <seealso cref="Deflate(Stream)"/>
    public static STREAM Deflate<STREAM>(this STREAM self, byte[] bytes) where STREAM : Stream
    {
      Assertion.NotNull(self);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        new DeflateStream(self, CompressionMode.Compress, true).Write(bytes).Close();
      }

      return self;
    }

    /// <summary>
    ///   <para>Creates a GZip compression stream from a specified one, using either compression or uncompression logic.</para>
    /// </summary>
    /// <param name="self">Wrapped stream which contents will be compressed/uncompressed.</param>
    /// <param name="mode">GZip algorithm working mode, specifying whether to automatically compress data when writing to a stream, or uncompress data when reading from it.</param>
    /// <returns>GZip compression stream which wraps original <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip{T}(T, byte[])"/>
    /// <seealso cref="GZip(Stream)"/>
    public static GZipStream GZip(this Stream self, CompressionMode mode)
    {
      Assertion.NotNull(self);

      return new GZipStream(self, mode);
    }

    /// <summary>
    ///   <para>Decompresses data from a stream, using GZip algorithm.</para>
    /// </summary>
    /// <param name="self">Stream to read and decompress data from.</param>
    /// <returns>Decompressed contents of current <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip(Stream, CompressionMode)"/>
    /// <seealso cref="GZip{T}(T, byte[])"/>
    public static byte[] GZip(this Stream self)
    {
      Assertion.NotNull(self);

      return new GZipStream(self, CompressionMode.Decompress, true).Bytes(true);
    }

    /// <summary>
    ///   <para>Writes sequence of bytes into specified stream, using GZip compression algorithm.</para>
    /// </summary>
    /// <typeparam name="STREAM">Type of target <see cref="Stream"/>.</typeparam>
    /// <param name="self">Destination stream where compressed data should be written.</param>
    /// <param name="bytes">Data to be compressed and written to the stream.</param>
    /// <returns>Back reference to the current stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="GZipStream"/>
    /// <seealso cref="GZip(Stream, CompressionMode)"/>
    /// <seealso cref="GZip(Stream)"/>
    public static STREAM GZip<STREAM>(this STREAM self, byte[] bytes) where STREAM : Stream
    {
      Assertion.NotNull(self);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        new GZipStream(self, CompressionMode.Compress, true).Write(bytes).Close();
      }

      return self;
    }
  }
}