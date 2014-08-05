using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class StreamExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML data from source <see cref="Stream"/> as <see cref="XDocument"/> object.</para>
    /// </summary>
    /// <param name="self">Source stream with XML data.</param>
    /// <param name="close">Whether to automatically close source stream after successfull deserialization.</param>
    /// <returns><see cref="XDocument"/> document, constructed from data in <paramref name="self"/> stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XDocument"/>
    public static XDocument AsXDocument(this Stream self, bool close = false)
    {
      Assertion.NotNull(self);

      return System.Xml.XmlReader.Create(self, new XmlReaderSettings { CloseInput = close }).Read(XDocument.Load);
    }

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
        destination.Close();
        if (close)
        {
          self.Close();
        }
      }
      return destination.ToArray();
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
  }
}