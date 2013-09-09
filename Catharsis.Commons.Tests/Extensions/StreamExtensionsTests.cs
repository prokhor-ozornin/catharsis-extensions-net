using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="StreamExtensions"/>.</para>
  /// </summary>
  public sealed class StreamExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.BinaryReader(Stream, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void BinaryReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.BinaryReader(null));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      stream.BinaryReader().With(reader =>
      {
        Assert.True(ReferenceEquals(reader.BaseStream, stream));
        Assert.True(reader.ReadBytes(bytes.Length).SequenceEqual(bytes));
      });
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.BinaryWriter(Stream, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void BinaryWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.BinaryWriter(null));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream();
      stream.BinaryWriter().With(writer =>
      {
        Assert.True(ReferenceEquals(writer.BaseStream, stream));
        writer.Write(bytes);
      });
      Assert.True(stream.ToArray().SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtendedExtensions.Buffered(Stream, int?)"/> method.</para>
    /// </summary>
    [Fact]
    public void Buffered_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.Buffered(null));
      Assert.Throws<ArgumentOutOfRangeException>(() => StreamExtendedExtensions.Buffered(Stream.Null, -1));
      Assert.Throws<ArgumentOutOfRangeException>(() => StreamExtendedExtensions.Buffered(Stream.Null, 0));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      stream.Buffered().With(buffered => buffered.Write(bytes));
      Assert.True(stream.ToArray().SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.Bytes(Stream, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Bytes(null));

      Assert.True(Stream.Null.Bytes().Length == 0);

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream(bytes);
      Assert.True(stream.Bytes().SequenceEqual(bytes));
      Assert.True(stream.ReadByte() == -1);
      stream.Close();
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      
      stream = new MemoryStream(bytes);
      Assert.True(stream.Bytes(true).SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }
    
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StreamExtendedExtensions.Deflate(Stream, CompressionMode)"/></description></item>
    ///     <item><description><see cref="StreamExtendedExtensions.Deflate{STREAM}(STREAM, byte[])"/></description></item>
    ///     <item><description><see cref="StreamExtendedExtensions.Deflate(Stream)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Deflate_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.Deflate(null, CompressionMode.Compress));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.Deflate<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.Deflate(Stream.Null, null));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.Deflate(null));

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream();
      var compressed = new byte[] { };
      stream.Deflate(CompressionMode.Compress).With(deflate =>
      {
        Assert.True(ReferenceEquals(deflate.BaseStream, stream));
        Assert.Throws<InvalidOperationException>(() => deflate.ReadByte());
        deflate.Write(bytes);
      });
      compressed = stream.ToArray();
      Assert.False(compressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(compressed);
      var decompressed = new byte[] { };
      stream.Deflate(CompressionMode.Decompress).With(deflate =>
      {
        Assert.True(ReferenceEquals(deflate.BaseStream, stream));
        decompressed = deflate.Bytes();
      });
      Assert.True(decompressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      new MemoryStream().With(x =>
      {
        Assert.True(ReferenceEquals(x.Deflate(bytes), x));
        Assert.True(x.ToArray().SequenceEqual(compressed));
        Assert.True(x.Bytes().Length == 0);
        Assert.True(x.CanRead);
        Assert.True(x.CanWrite);
      });

      new MemoryStream(compressed).With(x =>
      {
        Assert.True(x.Deflate().SequenceEqual(bytes));
        Assert.True(x.Bytes().Length == 0);
        Assert.True(x.CanRead);
        Assert.True(x.CanWrite);
      });

      Assert.True(new MemoryStream().Deflate(bytes).Rewind().Deflate().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StreamExtendedExtensions.GZip(Stream, CompressionMode)"/></description></item>
    ///     <item><description><see cref="StreamExtendedExtensions.GZip{STREAM}(STREAM, byte[])"/></description></item>
    ///     <item><description><see cref="StreamExtendedExtensions.GZip(Stream)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void GZip_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.GZip(null, CompressionMode.Compress));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.GZip<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.GZip(Stream.Null, null));
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.GZip(null));

      var bytes = Guid.NewGuid().ToByteArray();
      
      var stream = new MemoryStream();
      var compressed = new byte[] {};
      stream.GZip(CompressionMode.Compress).With(gzip =>
      {
        Assert.True(ReferenceEquals(gzip.BaseStream, stream));
        Assert.Throws<InvalidOperationException>(() => gzip.ReadByte());
        gzip.Write(bytes);
      });
      compressed = stream.ToArray();
      Assert.False(compressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(compressed);
      var decompressed = new byte[] {};
      stream.GZip(CompressionMode.Decompress).With(gzip =>
      {
        Assert.True(ReferenceEquals(gzip.BaseStream, stream));
        decompressed = gzip.Bytes();
      });
      Assert.True(decompressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      new MemoryStream().With(x =>
      {
        Assert.True(ReferenceEquals(x.GZip(bytes), x));
        Assert.True(x.ToArray().SequenceEqual(compressed));
        Assert.True(x.Bytes().Length == 0);
        Assert.True(x.CanRead);
        Assert.True(x.CanWrite);
      });

      new MemoryStream(compressed).With(x =>
      {
        Assert.True(x.GZip().SequenceEqual(bytes));
        Assert.True(x.Bytes().Length == 0);
        Assert.True(x.CanRead);
        Assert.True(x.CanWrite);
      });

      Assert.True(new MemoryStream().GZip(bytes).Rewind().GZip().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.Rewind{STREAM}(STREAM)"/> method.</para>
    /// </summary>
    [Fact]
    public void Rewind_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Rewind<Stream>(null));

      Assert.True(ReferenceEquals(Stream.Null.Rewind(), Stream.Null));
      
      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      Assert.True(stream.Position == 0);
      stream.Seek(0, SeekOrigin.End);
      Assert.True(stream.Position == stream.Length);
      stream.Rewind();
      Assert.True(stream.Position == 0);
      stream.Rewind();
      Assert.True(stream.Position == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.Shutdown{STREAM}(STREAM)"/> method.</para>
    /// </summary>
    [Fact]
    public void Shutdown_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Shutdown<Stream>(null));

      Assert.True(ReferenceEquals(Stream.Null.Shutdown(), Stream.Null));

      var stream = new MemoryStream();
      Assert.True(stream.CanRead);
      Assert.True(stream.CanWrite);
      stream.Shutdown();
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.Text"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Text(null));

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream(bytes);
      var text = stream.Text();
      Assert.True(text == stream.Rewind().TextReader().Text());
      Assert.True(text == bytes.String());
      Assert.True(stream.ReadByte() == -1);
      Assert.True(text == stream.Rewind().Text(close: true, encoding: null));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(bytes);
      text = stream.Text(encoding: Encoding.Unicode);
      Assert.True(text == stream.Rewind().TextReader(Encoding.Unicode).Text());
      Assert.True(text == bytes.String(Encoding.Unicode));
      Assert.True(stream.ReadByte() == -1);
      Assert.True(text == stream.Rewind().Text(close: true, encoding: Encoding.Unicode));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.TextReader(Stream, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void TextReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.TextReader(null));

      var text = Guid.NewGuid().ToString();
      
      var stream = new MemoryStream(text.Bytes());
      Assert.True(stream.TextReader().Text() == text);
      Assert.True(stream.ReadByte() == -1);
      
      stream = new MemoryStream(text.Bytes(Encoding.Unicode));
      Assert.True(stream.TextReader(Encoding.Unicode).Text() == text);
      Assert.True(stream.ReadByte() == -1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.TextWriter(Stream, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void TextWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.TextWriter(null));

      var text = Guid.NewGuid().ToString();
      
      var stream = new MemoryStream();
      stream.TextWriter().WriteObject(text).Close();
      Assert.True(stream.ToArray().SequenceEqual(text.Bytes()));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream();
      stream.TextWriter(Encoding.Unicode).WriteObject(text).Close();
      Assert.True(stream.ToArray().SequenceEqual(text.Bytes(Encoding.Unicode)));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StreamExtensions.Write{STREAM}(STREAM, byte[])"/></description></item>
    ///     <item><description><see cref="StreamExtensions.Write{STREAM}(STREAM, Stream)"/></description></item>
    ///     <item><description><see cref="StreamExtensions.Write{STREAM}(STREAM, string, Encoding)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Write_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write(Stream.Null, (byte[])null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write(Stream.Null, (Stream)null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, "text"));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write(Stream.Null, (string)null));

      var bytes = Guid.NewGuid().ToByteArray();
      var text = bytes.ToString();

      var stream = new MemoryStream();
      Assert.True(ReferenceEquals(stream.Write(bytes), stream));
      stream.Close();
      Assert.True(stream.ToArray().SequenceEqual(bytes));

      var from = new MemoryStream(bytes);
      var to = new MemoryStream();
      Assert.True(ReferenceEquals(to.Write(from), to));
      Assert.True(to.ToArray().SequenceEqual(bytes));
      Assert.True(from.Bytes().Length == 0);
      Assert.True(from.CanRead);
      Assert.True(to.Bytes().Length == 0);
      Assert.True(to.CanWrite);
      from.Close();
      to.Close();

      new MemoryStream().With(x =>
      {
        Assert.True(ReferenceEquals(x.Write(string.Empty), x));
        Assert.True(x.Text() == string.Empty);
      });

      new MemoryStream().With(x =>
      {
        Assert.True(ReferenceEquals(x.Write(text), x));
        Assert.True(x.Rewind().Text() == text);
      });

      new MemoryStream().With(x =>
      {
        Assert.True(ReferenceEquals(x.Write(text, Encoding.Unicode), x));
        Assert.True(x.Rewind().Text(encoding: Encoding.Unicode) == text);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.XDocument(Stream, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.XDocument(null));
      Assert.Throws<XmlException>(() => StreamExtensions.XDocument(Stream.Null));

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      new MemoryStream(xml.Bytes(Encoding.UTF32)).With(x => Assert.Throws<XmlException>(() => x.XDocument()));
      
      new MemoryStream(xml.Bytes(Encoding.Unicode)).With(stream =>
      {
        Assert.True(stream.XDocument().ToString() == "<article>text</article>");
        Assert.True(stream.Bytes().Length == 0);
        Assert.True(stream.ReadByte() == -1);
      });

      new MemoryStream(xml.Bytes(Encoding.Unicode)).With(stream =>
      {
        Assert.True(stream.XDocument(true).ToString() == "<article>text</article>");
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtendedExtensions.XmlDocument(Stream, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtendedExtensions.XmlDocument(null));
      Assert.Throws<XmlException>(() => StreamExtendedExtensions.XmlDocument(Stream.Null));

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      new MemoryStream(xml.Bytes(Encoding.UTF32)).With(x => Assert.Throws<XmlException>(() => x.XmlDocument()));
      
      new MemoryStream(xml.Bytes(Encoding.Unicode)).With(stream =>
      {
        Assert.True(stream.XmlDocument().String() == xml);
        Assert.True(stream.Bytes().Length == 0);
        Assert.True(stream.ReadByte() == -1);
      });

      new MemoryStream(xml.Bytes(Encoding.Unicode)).With(stream =>
      {
        Assert.True(stream.XmlDocument(true).String() == xml);
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.XmlReader(Stream, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.XmlReader(null));

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";

      new MemoryStream(xml.Bytes()).With(stream =>
      {
        var reader = stream.XmlReader();
        Assert.False(reader.Settings.CloseInput);
        Assert.True(reader.Settings.IgnoreComments);
        Assert.True(reader.Settings.IgnoreWhitespace);
        reader.ReadStartElement("article");
        Assert.True(reader.ReadString() == "text");
        reader.ReadEndElement();
        reader.Close();
        Assert.True(stream.Bytes().Length == 0);
        Assert.True(stream.ReadByte() == -1);
      });

      new MemoryStream(xml.Bytes()).With(stream =>
      {
        stream.XmlReader(true).Close();
        //Assert.True(reader.Settings.CloseInput);
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StreamExtensions.XmlWriter"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.XmlWriter(null));
      
      var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";
      new MemoryStream().With(stream =>
      {
        stream.XmlWriter().Write(writer =>
        {
          Assert.False(writer.Settings.CloseOutput);
          Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.UTF8.ToString()));
          Assert.False(writer.Settings.Indent);
          writer.WriteElementString("article", "text");
        });
        Assert.True(stream.ToArray().SequenceEqual(xml.Bytes(Encoding.UTF8, true)));
        Assert.True(stream.Bytes().Length == 0);
        Assert.True(stream.ReadByte() == -1);

        stream.Rewind().XmlWriter(close: true, encoding: null).Write(writer =>
        {
          Assert.True(writer.Settings.CloseOutput);
          Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.UTF8.ToString()));
        });
        Assert.Throws<ObjectDisposedException>(() =>stream.ReadByte());
      });

      xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      new MemoryStream().With(stream =>
      {
        stream.XmlWriter(encoding: Encoding.Unicode).Write(writer =>
        {
          Assert.False(writer.Settings.CloseOutput);
          Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.Unicode.ToString()));
          writer.WriteElementString("article", "text");
        });
        Assert.True(stream.ToArray().SequenceEqual(xml.Bytes(Encoding.Unicode, true)));
        Assert.True(stream.Bytes().Length == 0);
        Assert.True(stream.ReadByte() ==-1);

        stream.XmlWriter(close: true, encoding: Encoding.Unicode).Write(writer =>
        {
          Assert.True(writer.Settings.CloseOutput);
          Assert.True(writer.Settings.Encoding.ToString().Equals(Encoding.Unicode.ToString()));
        });
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
    }
  }
}