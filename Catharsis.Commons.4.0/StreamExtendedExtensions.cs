using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  ///   <seealso cref="Stream"/>
  /// </summary>
  public static class StreamExtendedExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="bufferSize"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static BufferedStream Buffered(this Stream stream, int? bufferSize = null)
    {
      Assertion.NotNull(stream);

      return bufferSize != null ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static DeflateStream Deflate(this Stream stream, CompressionMode mode)
    {
      Assertion.NotNull(stream);

      return new DeflateStream(stream, mode);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static T Deflate<T>(this T stream, byte[] bytes) where T : Stream
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
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static byte[] Deflate(this Stream stream)
    {
      Assertion.NotNull(stream);

      return new DeflateStream(stream, CompressionMode.Decompress, true).Bytes(true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static GZipStream GZip(this Stream stream, CompressionMode mode)
    {
      Assertion.NotNull(stream);

      return new GZipStream(stream, mode);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static T GZip<T>(this T stream, byte[] bytes) where T : Stream
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
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static byte[] GZip(this Stream stream)
    {
      Assertion.NotNull(stream);

      return new GZipStream(stream, CompressionMode.Decompress, true).Bytes(true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static XmlDocument XmlDocument(this Stream stream, bool close = false)
    {
      Assertion.NotNull(stream);

      var document = new XmlDocument();
      try
      {
        document.Load(stream);
      }
      finally
      {
        if (close)
        {
          stream.Close();
        }
      }
      return document;
    }
  }
}