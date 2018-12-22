using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class StreamExtensionsTest
  {
    [Fact]
    public void binary_reader()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.BinaryReader(null));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      using (var reader = stream.BinaryReader())
      {
        Assert.True(ReferenceEquals(reader.BaseStream, stream));
        Assert.True(reader.ReadBytes(bytes.Length).SequenceEqual(bytes));
      }
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Fact]
    public void binary_writer()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.BinaryWriter(null));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream();
      using (var writer = stream.BinaryWriter())
      {
        Assert.True(ReferenceEquals(writer.BaseStream, stream));
        writer.Write(bytes);
      }
      Assert.True(stream.ToArray().SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Fact]
    public void buffered()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Buffered(null));
      Assert.Throws<ArgumentOutOfRangeException>(() => Stream.Null.Buffered(-1));
      Assert.Throws<ArgumentOutOfRangeException>(() => Stream.Null.Buffered(0));

      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      using (var buffered = stream.Buffered())
      {
        buffered.Write(bytes);
      }
      Assert.True(stream.ToArray().SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Fact]
    public void bytes()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Bytes(null));

      Assert.Empty(Stream.Null.Bytes());

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream(bytes);
      Assert.True(stream.Bytes().SequenceEqual(bytes));
      Assert.Equal(-1, stream.ReadByte());
      stream.Close();
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      
      stream = new MemoryStream(bytes);
      Assert.True(stream.Bytes(true).SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }
    
    [Fact]
    public void deflate()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Deflate(null, CompressionMode.Compress));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Deflate<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Stream.Null.Deflate(null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Deflate(null));

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream();
      var compressed = new byte[] { };
      using (var deflate = stream.Deflate(CompressionMode.Compress))
      {
        Assert.True(ReferenceEquals(deflate.BaseStream, stream));
        Assert.Throws<InvalidOperationException>(() => deflate.ReadByte());
        deflate.Write(bytes);
      }
      compressed = stream.ToArray();
      Assert.False(compressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(compressed);
      var decompressed = new byte[] { };
      using (var deflate = stream.Deflate(CompressionMode.Decompress))
      {
        Assert.True(ReferenceEquals(deflate.BaseStream, stream));
        decompressed = deflate.Bytes();
      }
      Assert.True(decompressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      using (var s = new MemoryStream())
      {
        Assert.True(ReferenceEquals(s.Deflate(bytes), s));
        Assert.True(s.ToArray().SequenceEqual(compressed));
        Assert.Empty(s.Bytes());
        Assert.True(s.CanRead);
        Assert.True(s.CanWrite);
      }

      using (var s = new MemoryStream(compressed))
      {
        Assert.True(s.Deflate().SequenceEqual(bytes));
        Assert.Empty(s.Bytes());
        Assert.True(s.CanRead);
        Assert.True(s.CanWrite);
      }

      Assert.True(new MemoryStream().Deflate(bytes).Rewind().Deflate().SequenceEqual(bytes));
    }

    [Fact]
    public void gzip()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.GZip(null, CompressionMode.Compress));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.GZip<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Stream.Null.GZip(null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.GZip(null));

      var bytes = Guid.NewGuid().ToByteArray();
      
      var stream = new MemoryStream();
      var compressed = new byte[] {};
      using (var gzip = stream.GZip(CompressionMode.Compress))
      {
        Assert.True(ReferenceEquals(gzip.BaseStream, stream));
        Assert.Throws<InvalidOperationException>(() => gzip.ReadByte());
        gzip.Write(bytes);
      }
      compressed = stream.ToArray();
      Assert.False(compressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(compressed);
      var decompressed = new byte[] {};
      using (var gzip = stream.GZip(CompressionMode.Decompress))
      {
        Assert.True(ReferenceEquals(gzip.BaseStream, stream));
        decompressed = gzip.Bytes();
      }
      Assert.True(decompressed.SequenceEqual(bytes));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      using (var s = new MemoryStream())
      {
        Assert.True(ReferenceEquals(s.GZip(bytes), s));
        Assert.True(s.ToArray().SequenceEqual(compressed));
        Assert.Empty(s.Bytes());
        Assert.True(s.CanRead);
        Assert.True(s.CanWrite);
      }

      using (var s = new MemoryStream(compressed))
      {
        Assert.True(s.GZip().SequenceEqual(bytes));
        Assert.Empty(s.Bytes());
        Assert.True(s.CanRead);
        Assert.True(s.CanWrite);
      }

      Assert.True(new MemoryStream().GZip(bytes).Rewind().GZip().SequenceEqual(bytes));
    }

    [Fact]
    public void rewind()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Rewind<Stream>(null));

      Assert.True(ReferenceEquals(Stream.Null.Rewind(), Stream.Null));
      
      var bytes = Guid.NewGuid().ToByteArray();
      var stream = new MemoryStream(bytes);
      Assert.Equal(0, stream.Position);
      stream.Seek(0, SeekOrigin.End);
      Assert.Equal(stream.Length, stream.Position);
      stream.Rewind();
      Assert.Equal(0, stream.Position);
      stream.Rewind();
      Assert.Equal(0, stream.Position);
    }

    [Fact]
    public void text_stream()
    {
      Assert.Throws<ArgumentNullException>(() => ((Stream) null).Text());

      var bytes = Guid.NewGuid().ToByteArray();

      var stream = new MemoryStream(bytes);
      var text = stream.Text();
      Assert.Equal(text, stream.Rewind().TextReader().Text());
      Assert.Equal(-1, stream.ReadByte());
      Assert.Equal(text, stream.Rewind().Text(true));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream(bytes);
      text = stream.Text(encoding: Encoding.Unicode);
      Assert.Equal(text, stream.Rewind().TextReader(Encoding.Unicode).Text());
      Assert.Equal(text, bytes.String(Encoding.Unicode));
      Assert.Equal(-1, stream.ReadByte());
      Assert.Equal(text, stream.Rewind().Text(true, Encoding.Unicode));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Fact]
    public void text_text_reader()
    {
      Assert.Throws<ArgumentNullException>(() => ((TextReader) null).Text());

      var text = Guid.NewGuid().ToString();

      using (var reader = new StringReader(text))
      {
        Assert.Equal(text, reader.Text());
        Assert.Equal(-1, reader.Read());
      }

      using (var reader = new StringReader(text))
      {
        Assert.Equal(text, reader.Text(true));
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      }
    }

    [Fact]
    public void text_reader()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.TextReader(null));

      var text = Guid.NewGuid().ToString();
      
      var stream = new MemoryStream(text.Bytes());
      Assert.Equal(text, stream.TextReader().Text());
      Assert.Equal(-1, stream.ReadByte());
      
      stream = new MemoryStream(text.Bytes(Encoding.Unicode));
      Assert.Equal(text, stream.TextReader(Encoding.Unicode).Text());
      Assert.Equal(-1, stream.ReadByte());
    }

    [Fact]
    public void text_writer()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.TextWriter(null));

      var text = Guid.NewGuid().ToString();
      
      var stream = new MemoryStream();
      using (var writer = stream.TextWriter())
      {
        writer.Write(text);
      }
      Assert.True(stream.ToArray().SequenceEqual(text.Bytes()));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());

      stream = new MemoryStream();
      using (var writer = stream.TextWriter(Encoding.Unicode))
      {
        writer.Write(text);
      }
      Assert.True(stream.ToArray().SequenceEqual(text.Bytes(Encoding.Unicode)));
      Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
    }

    [Fact]
    public void write()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Stream.Null.Write((byte[])null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => Stream.Null.Write((Stream)null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Write<Stream>(null, "text"));
      Assert.Throws<ArgumentNullException>(() => Stream.Null.Write((string)null));

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
      Assert.Empty(from.Bytes());
      Assert.True(from.CanRead);
      Assert.Empty(to.Bytes());
      Assert.True(to.CanWrite);
      from.Close();
      to.Close();

      using (var s = new MemoryStream())
      {
        Assert.True(ReferenceEquals(s.Write(string.Empty), s));
        Assert.Equal(string.Empty, s.Text());
      }

      using (var s = new MemoryStream())
      {
        Assert.True(ReferenceEquals(s.Write(text), s));
        Assert.Equal(text, s.Rewind().Text());
      }

      using (var s = new MemoryStream())
      {
        Assert.True(ReferenceEquals(s.Write(text, Encoding.Unicode), s));
        Assert.Equal(text, s.Rewind().Text(encoding: Encoding.Unicode));
      }
    }

    [Fact]
    public void xml_reader_stream()
    {
      Assert.Throws<ArgumentNullException>(() => ((Stream)(null)).XmlReader());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.Bytes()))
      {
        var reader = stream.XmlReader();
        Assert.False(reader.Settings.CloseInput);
        Assert.True(reader.Settings.IgnoreComments);
        Assert.True(reader.Settings.IgnoreWhitespace);
        reader.ReadStartElement("article");
        Assert.Equal("text", reader.ReadString());
        reader.ReadEndElement();
        reader.Close();
        Assert.Empty(stream.Bytes());
        Assert.Equal(-1, stream.ReadByte());
      }

      using (var stream = new MemoryStream(Xml.Bytes()))
      {
        stream.XmlReader(true).Close();
        //Assert.True(reader.Settings.CloseInput);
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }
    }

    [Fact]
    public void xml_reader_text_reader()
    {
      Assert.Throws<ArgumentNullException>(() => ((TextReader) null).XmlReader());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      var textReader = new StringReader(Xml);
      Assert.Equal("text", textReader.XmlReader().Do(reader =>
      {
        Assert.False(reader.Settings.CloseInput);
        Assert.True(reader.Settings.IgnoreComments);
        Assert.True(reader.Settings.IgnoreWhitespace);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }));
      Assert.Equal(-1, textReader.Read());

      textReader = new StringReader(Xml);
      Assert.Equal("text", textReader.XmlReader(true).Do(reader =>
      {
        //Assert.True(reader.Settings.CloseInput);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }));
      Assert.Throws<ObjectDisposedException>(() => textReader.Read());
    }

    [Fact]
    public void xml_writer_stream()
    {
      Assert.Throws<ArgumentNullException>(() => ((Stream) null).XmlWriter());
      
      var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";
      using (var stream = new MemoryStream())
      {
        using (var writer = stream.XmlWriter())
        {
          Assert.False(writer.Settings.CloseOutput);
          Assert.Equal(Encoding.UTF8.ToString(), writer.Settings.Encoding.ToString());
          Assert.False(writer.Settings.Indent);
          writer.WriteElementString("article", "text");
        }
        Assert.True(stream.ToArray().SequenceEqual(xml.Bytes(Encoding.UTF8)));
        Assert.Empty(stream.Bytes());
        Assert.Equal(-1, stream.ReadByte());

        using (var writer = stream.Rewind().XmlWriter(true))
        {
          Assert.True(writer.Settings.CloseOutput);
          Assert.Equal(Encoding.UTF8.ToString(), writer.Settings.Encoding.ToString());
        }
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }

      xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      using (var stream = new MemoryStream())
      {
        using (var writer = stream.XmlWriter(encoding: Encoding.Unicode))
        {
          Assert.False(writer.Settings.CloseOutput);
          Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
          writer.WriteElementString("article", "text");
        }
        Assert.True(stream.ToArray().SequenceEqual(xml.Bytes(Encoding.Unicode)));
        Assert.Empty(stream.Bytes());
        Assert.Equal(-1, stream.ReadByte());

        using (var writer = stream.XmlWriter(true, Encoding.Unicode))
        {
          Assert.True(writer.Settings.CloseOutput);
          Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
        }
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }
    }

    [Fact]
    public void xml_writer_text_writer()
    {
      Assert.Throws<ArgumentNullException>(() => ((TextWriter) null).XmlWriter());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      var textWriter = new StringWriter();
      textWriter.XmlWriter().Write(writer =>
      {
        Assert.False(writer.Settings.CloseOutput);
        Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
        Assert.False(writer.Settings.Indent);
        writer.WriteElementString("article", "text");
      });
      Assert.Equal(Xml, textWriter.ToString());
      textWriter.Write(string.Empty);
      textWriter.Close();

      textWriter = new StringWriter();
      textWriter.XmlWriter(true).Write(writer =>
      {
        Assert.True(writer.Settings.CloseOutput);
        Assert.Equal(Encoding.Unicode.ToString(), writer.Settings.Encoding.ToString());
        Assert.False(writer.Settings.Indent);
      });
      textWriter.Write(string.Empty);
    }

    [Fact]
    public void lines()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Lines(null));

      using (var reader = new StringReader(string.Empty))
      {
        Assert.False(reader.Lines().Any());
        Assert.Equal(-1, reader.Read());
      }

      using (var reader = new StringReader(string.Empty))
      {
        Assert.False(reader.Lines(true).Any());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      }

      using (var reader = new StringReader($"First{Environment.NewLine}Second{Environment.NewLine}"))
      {
        var lines = reader.Lines();
        Assert.Equal(3, lines.Count());
        Assert.Equal("First", lines[0]);
        Assert.Equal("Second", lines[1]);
        Assert.Equal(string.Empty, lines[2]);
        Assert.False(reader.Lines().Any());
        Assert.Equal(-1, reader.Read());
      }
    }
  }
}