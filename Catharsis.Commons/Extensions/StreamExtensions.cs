using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  ///   <seealso cref="Stream"/>
  /// </summary>
  public static class StreamExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static BinaryReader BinaryReader(this Stream stream, Encoding encoding = null)
    {
      Assertion.NotNull(stream);

      return encoding != null ? new BinaryReader(stream, encoding) : new BinaryReader(stream);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static BinaryWriter BinaryWriter(this Stream stream, Encoding encoding = null)
    {
      Assertion.NotNull(stream);

      return encoding != null ? new BinaryWriter(stream, encoding) : new BinaryWriter(stream);
    }

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

      return bufferSize.HasValue ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
    }

    /// <summary>
    ///   <para>Read the content of this <see cref="Stream"/> and return it as a <see cref="byte[]"/>. The input is closed before this method returns.</para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <returns>The <see cref="byte[]"/> from that <paramref name="stream"/></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this Stream stream, bool close = false)
    {
      Assertion.NotNull(stream);

      var destination = new MemoryStream();
      try
      {
        stream.CopyTo(destination);
      }
      finally
      {
        destination.Close();
        if (close)
        {
          stream.Close();
        }
      }
      return destination.ToArray();
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
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="stream"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
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
    /// <param name="stream"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
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
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static STREAM Rewind<STREAM>(this STREAM stream) where STREAM : Stream
    {
      Assertion.NotNull(stream);

      stream.Seek(0, SeekOrigin.Begin);
      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static STREAM Shutdown<STREAM>(this STREAM stream) where STREAM : Stream
    {
      Assertion.NotNull(stream);

      stream.Close();
      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static string Text(this Stream stream, Encoding encoding = null, bool close = false)
    {
      Assertion.NotNull(stream);

      return stream.CanRead ? stream.TextReader(encoding).Text(close) : string.Empty;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static TextReader TextReader(this Stream stream, Encoding encoding = null)
    {
      Assertion.NotNull(stream);

      return encoding != null ? new StreamReader(stream, encoding) : new StreamReader(stream, Encoding.Default);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static TextWriter TextWriter(this Stream stream, Encoding encoding = null)
    {
      Assertion.NotNull(stream);

      return encoding != null ? new StreamWriter(stream, encoding) : new StreamWriter(stream, Encoding.Default);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="stream"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static STREAM Write<STREAM>(this STREAM stream, byte[] bytes) where STREAM : Stream
    {
      Assertion.NotNull(stream);
      Assertion.NotNull(bytes);

      if (stream.CanWrite && bytes.Length > 0)
      {
        stream.Write(bytes, 0, bytes.Length);
      }

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="to"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="to"/> or <paramref name="from"/> is a <c>null</c> reference.</exception>
    public static STREAM Write<STREAM>(this STREAM to, Stream from) where STREAM : Stream
    {
      Assertion.NotNull(to);
      Assertion.NotNull(from);

      if (to.CanWrite && from.CanRead)
      {
        from.CopyTo(to);
      }

      return to;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="STREAM"></typeparam>
    /// <param name="stream"></param>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stream"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    public static STREAM Write<STREAM>(this STREAM stream, string text, Encoding encoding = null) where STREAM : Stream
    {
      Assertion.NotNull(stream);
      Assertion.NotNull(text);

      if (stream.CanWrite && text.Length > 0)
      {
        stream.TextWriter(encoding).WriteObject(text).Flush();
      }

      return stream;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static XDocument XDocument(this Stream stream, bool close = false)
    {
      Assertion.NotNull(stream);

      return System.Xml.XmlReader.Create(stream, new XmlReaderSettings { CloseInput = close }).Read(System.Xml.Linq.XDocument.Load);
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static XmlReader XmlReader(this Stream stream, bool close = false)
    {
      Assertion.NotNull(stream);

      return System.Xml.XmlReader.Create(stream, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static XmlWriter XmlWriter(this Stream stream, Encoding encoding = null, bool close = false)
    {
      Assertion.NotNull(stream);

      var settings = new XmlWriterSettings { CloseOutput = close };
      if (encoding != null)
      {
        settings.Encoding = encoding;
      }
      return System.Xml.XmlWriter.Create(stream, settings);
    }
  }
}