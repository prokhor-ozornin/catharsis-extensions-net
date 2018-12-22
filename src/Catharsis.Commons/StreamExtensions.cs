using System;
using System.IO;
using System.Text;
using System.Xml;

#if NET_40
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
#endif

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class StreamExtensions
  {
    /// <summary>
    ///   <para>Returns a <see cref="BinaryReader"/> for reading data from specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Source stream to read from.</param>
    /// <param name="encoding">Text encoding to use by <see cref="BinaryReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Binary reader instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryReader"/>
    /// <seealso cref="BinaryWriter(Stream, Encoding)"/>
    public static BinaryReader BinaryReader(this Stream self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return encoding != null ? new BinaryReader(self, encoding) : new BinaryReader(self);
    }

    /// <summary>
    ///   <para>Returns a <see cref="BinaryWriter"/> for writing data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Target stream to write to.</param>
    /// <param name="encoding">Text encoding to use by <see cref="BinaryWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Binary writer instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryWriter"/>
    /// <seealso cref="BinaryReader(Stream, Encoding)"/>
    public static BinaryWriter BinaryWriter(this Stream self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return encoding != null ? new BinaryWriter(self, encoding) : new BinaryWriter(self);
    }

    /// <summary>
    ///   <para>Read the content of this <see cref="Stream"/> and return it as a <see cref="byte"/> array. The input is closed before this method returns.</para>
    /// </summary>
    /// <param name="self"></param>
    /// <param name="close"></param>
    /// <returns>The <see cref="byte"/> array from that <paramref name="self"/></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this Stream self, bool close = false)
    {
      Assertion.NotNull(self);

      var destination = new MemoryStream();
      try
      {
        const int bufferSize = 4096;
        var buffer = new byte[bufferSize];
        int count;
        while ((count = self.Read(buffer, 0, bufferSize)) > 0)
        {
          destination.Write(buffer, 0, count);
        }
      }
      finally
      {
        destination.Dispose();
        if (close)
        {
          self.Dispose();
        }
      }
      return destination.ToArray();
    }

    /// <summary>
    ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
    /// <returns>List of strings which have been read from a <paramref name="self"/>.</returns>
    /// <param name="close">Whether to close a <paramref name="self"/> after all texts have been read.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string[] Lines(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      return self.Text(close).Lines();
    }

    /// <summary>
    ///   <para>Sets the position within source <see cref="Stream"/> to the beginning of a stream, if this stream supports seeking operations.</para>
    /// </summary>
    /// <typeparam name="T">Type of source stream.</typeparam>
    /// <param name="self">Source stream.</param>
    /// <returns>Back reference to <paramref name="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Stream.Seek(long, SeekOrigin)"/>
    public static T Rewind<T>(this T self) where T : Stream
    {
      Assertion.NotNull(self);

      self.Seek(0, SeekOrigin.Begin);
      return self;
    }

    /// <summary>
    ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
    /// <returns>Text content which have been read from a <paramref name="self"/>.</returns>
    /// <param name="close">Whether to close a <paramref name="self"/> after all text have been read.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string Text(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      var result = string.Empty;
      try
      {
        result = self.ReadToEnd();
      }
      finally
      {
        if (close)
        {
          self.Dispose();
        }
      }
      return result;
    }

    /// <summary>
    ///   <para>Returns all available text data from a source stream.</para>
    /// </summary>
    /// <param name="self">Source stream to read from.</param>
    /// <param name="close">Whether to automatically close source stream when all available data has been successfully read from it.</param>
    /// <param name="encoding">Encoding to be used for bytes-to-text conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Text data from a <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string Text(this Stream self, bool close = false, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return self.CanRead ? self.TextReader(encoding).Text(close) : string.Empty;
    }

    /// <summary>
    ///   <para>Returns a <see cref="TextReader"/> for reading text data from specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Source stream to read from.</param>
    /// <param name="encoding">Text encoding to use by <see cref="TextReader"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Text reader instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="TextReader"/>
    /// <seealso cref="TextWriter(Stream, Encoding)"/>
    public static TextReader TextReader(this Stream self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return encoding != null ? new StreamReader(self, encoding) : new StreamReader(self, Encoding.UTF8);
    }

    /// <summary>
    ///   <para>Returns a <see cref="TextWriter"/> for writing text data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Target stream to write to.</param>
    /// <param name="encoding">Text encoding to use by <see cref="TextWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Text writer instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="TextWriter"/>
    /// <seealso cref="TextReader(Stream, Encoding)"/>
    public static TextWriter TextWriter(this Stream self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return encoding != null ? new StreamWriter(self, encoding) : new StreamWriter(self, Encoding.UTF8);
    }

    /// <summary>
    ///   <para>Writes binary data to target stream.</para>
    /// </summary>
    /// <typeparam name="T">Type of target stream.</typeparam>
    /// <param name="self">Target stream to write to.</param>
    /// <param name="bytes">Binary data to write to a stream.</param>
    /// <returns>Back reference to <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Write{T}(T, Stream)"/>
    /// <seealso cref="Write{T}(T, string, Encoding)"/>
    public static T Write<T>(this T self, byte[] bytes) where T : Stream
    {
      Assertion.NotNull(self);
      Assertion.NotNull(bytes);

      if (self.CanWrite && bytes.Length > 0)
      {
        self.Write(bytes, 0, bytes.Length);
      }

      return self;
    }

    /// <summary>
    ///   <para>"Pipes" two streams by writing all available data from one to another.</para>
    /// </summary>
    /// <typeparam name="T">Type of target stream.</typeparam>
    /// <param name="self">Target stream to write data to.</param>
    /// <param name="from">Source stream to read data from.</param>
    /// <returns>Back reference to target stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="from"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Write{T}(T, byte[])"/>
    /// <seealso cref="Write{T}(T, string, Encoding)"/>
    public static T Write<T>(this T self, Stream from) where T : Stream
    {
      Assertion.NotNull(self);
      Assertion.NotNull(from);

      if (self.CanWrite && from.CanRead)
      {
        const int bufferSize = 4096;
        var buffer = new byte[bufferSize];
        int count;
        while ((count = from.Read(buffer, 0, bufferSize)) > 0)
        {
          self.Write(buffer, 0, count);
        }
      }

      return self;
    }

    /// <summary>
    ///   <para>Writes text data to target <see cref="Stream"/>, using specified <see cref="Encoding"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of target stream.</typeparam>
    /// <param name="self">Target stream to write to.</param>
    /// <param name="text">Text to write to a stream.</param>
    /// <param name="encoding">Encoding to be used for text-to-bytes conversion. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>Back reference to <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Write{T}(T, byte[])"/>
    /// <seealso cref="Write{T}(T, Stream)"/>
    public static T Write<T>(this T self, string text, Encoding encoding = null) where T : Stream
    {
      Assertion.NotNull(self);
      Assertion.NotNull(text);

      if (self.CanWrite && text.Length > 0)
      {
        var writer = self.TextWriter(encoding);
        writer.Write(text);
        writer.Flush();
      }

      return self;
    }

    /// <summary>
    ///   <para>Creates <see cref="XmlReader"/> that wraps specified <see cref="TextReader"/> instance.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> that wraps XML data source.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> when wrapping <see cref="XmlReader"/> will be closed.</param>
    /// <returns><see cref="XmlReader"/> instance that wraps a text <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlReader"/>
    public static XmlReader XmlReader(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      return System.Xml.XmlReader.Create(self, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true });
    }

    /// <summary>
    ///   <para>Returns a <see cref="XmlReader"/> for reading XML data from specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Source stream to read from.</param>
    /// <param name="close">Whether resulting <see cref="System.Xml.XmlReader"/> should close underlying stream when its <see cref="System.Xml.XmlReader.Close()"/> method will be called.</param>
    /// <returns>XML reader instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlReader"/>
    /// <seealso cref="XmlWriter(Stream, bool, Encoding)"/>
    public static XmlReader XmlReader(this Stream self, bool close = false)
    {
      Assertion.NotNull(self);

      return System.Xml.XmlReader.Create(self, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true });
    }

    /// <summary>
    ///   <para>Creates <see cref="XmlWriter"/> that wraps specified <see cref="TextWriter"/> instance.</para>
    /// </summary>
    /// <param name="self"><see cref="TextWriter"/> that wraps XML data destination.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> when wrapping <see cref="XmlWriter"/> will be closed.</param>
    /// <param name="encoding">Encoding to be used by <see cref="XmlWriter"/>. If not specified, default encoding will be used.</param>
    /// <returns><see cref="XmlWriter"/> instance that wraps a text <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlWriter"/>
    public static XmlWriter XmlWriter(this TextWriter self, bool close = false, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      var settings = new XmlWriterSettings { CloseOutput = close };
      if (encoding != null)
      {
        settings.Encoding = encoding;
      }

      return System.Xml.XmlWriter.Create(self, settings);
    }

    /// <summary>
    ///   <para>Returns a <see cref="XmlWriter"/> for writing XML data to specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <param name="self">Target stream to write to.</param>
    /// <param name="close">Whether resulting <see cref="System.Xml.XmlWriter"/> should close underlying stream when its <see cref="System.Xml.XmlWriter.Close()"/> method will be called.</param>
    /// <param name="encoding">Text encoding to use by <see cref="XmlWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>XML writer instance that wraps <see cref="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlWriter"/>
    /// <seealso cref="XmlReader(Stream, bool)"/>
    public static XmlWriter XmlWriter(this Stream self, bool close = false, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      var settings = new XmlWriterSettings { CloseOutput = close };
      if (encoding != null)
      {
        settings.Encoding = encoding;
      }
      return System.Xml.XmlWriter.Create(self, settings);
    }

#if NET_40
    /// <summary>
    ///   <para>Serializes an object, or graph of connected objects, to the given stream.</para>
    /// </summary>
    /// <param name="self">The object at the root of the graph to serialize.</param>
    /// <param name="destination">The stream to which the graph is to be serialized.</param>
    /// <param name="close"><c>true</c> to auto-close target <paramref name="destination"/> stream after serialization, <c>false</c> to leave it intact.</param>
    /// <returns>Back reference to the current serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="destination"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryFormatter"/>
    public static object Binary(this object self, Stream destination, bool close = false)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(destination);

      try
      {
        new BinaryFormatter().Serialize(destination, self);
      }
      finally
      {
        if (close)
        {
          destination.Close();
        }
      }

      return self;
    }

    /// <summary>
    ///   <para>Serializes an object, or graph of connected objects, and returns serialization data as an array of bytes.</para>
    /// </summary>
    /// <param name="self">The object at the root of the graph to serialize.</param>
    /// <returns>Binary data of serialized object graph.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BinaryFormatter"/>
    public static byte[] Binary(this object self)
    {
      Assertion.NotNull(self);

      using (var stream = new MemoryStream())
      {
        self.Binary(stream);
        return stream.ToArray();
      }
    }

    /// <summary>
    ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
    /// </summary>
    /// <param name="self">Original stream that should be buffered.</param>
    /// <param name="bufferSize">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
    /// <returns>Buffer version of stream that wraps original <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BufferedStream"/>
    public static BufferedStream Buffered(this Stream self, int? bufferSize = null)
    {
      Assertion.NotNull(self);

      return bufferSize != null ? new BufferedStream(self, bufferSize.Value) : new BufferedStream(self);
    }

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
        new DeflateStream(self, CompressionMode.Compress, true).Do(it => it.Write(bytes));
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
        new GZipStream(self, CompressionMode.Compress, true).Do(it => it.Write(bytes));
      }

      return self;
    }
#endif
  }
}