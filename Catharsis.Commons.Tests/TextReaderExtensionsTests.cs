using System;
using System.IO;
using System.Linq;
using System.Xml;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TextReaderExtensions"/>.</para>
  /// </summary>
  public sealed class TextReaderExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.Lines(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Lines_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.Lines(null));

      new StringReader(string.Empty).With(reader =>
      {
        Assert.False(reader.Lines().Any());
        Assert.Equal(-1, reader.Read());
      });

      new StringReader(string.Empty).With(reader =>
      {
        Assert.False(reader.Lines(true).Any());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });

      new StringReader("First{0}Second{0}".FormatValue(Environment.NewLine)).With(reader =>
      {
        var lines = reader.Lines();
        Assert.Equal(2, lines.Count);
        Assert.Equal("First", lines[0]);
        Assert.Equal("Second", lines[1]);
        Assert.False(reader.Lines().Any());
        Assert.Equal(-1, reader.Read());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.Text(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(()  => TextReaderExtensions.Text(null));

      var text = Guid.NewGuid().ToString();
      
      new StringReader(text).With(reader =>
      {
        Assert.Equal(text, reader.Text());
        Assert.Equal(-1, reader.Read());
      });

      new StringReader(text).With(reader =>
      {
        Assert.Equal(text, reader.Text(true));
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.Xml{T}(TextReader, bool, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Xml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.Xml<object>(null));

      var subject = Guid.Empty;
      new StringReader(subject.Xml()).With(reader =>
      {
        Assert.True(reader.Xml<Guid>() == subject);
        reader.ReadLine();
      });
      new StringReader(subject.Xml()).With(reader =>
      {
        Assert.Equal(subject, reader.Xml<Guid>(true));
        Assert.Throws<ObjectDisposedException>(() => reader.ReadLine());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.XDocument(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.XDocument(null));
      Assert.Throws<XmlException>(() => TextReader.Null.XDocument());

      const string Xml = "<?xml version=\"1.0\"?><article>text</article>";

      new StringReader(Xml).With(reader =>
      {
        Assert.Equal("<article>text</article>", reader.XDocument().ToString());
        Assert.Equal(-1, reader.Read());
      });

      new StringReader(Xml).With(reader =>
      {
        Assert.Equal("<article>text</article>", reader.XDocument(true).ToString());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtendedExtensions.XmlDocument(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtendedExtensions.XmlDocument(null));
      Assert.Throws<XmlException>(() => TextReader.Null.XmlDocument());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      new StringReader(Xml).With(reader =>
      {
        Assert.Equal(Xml, reader.XmlDocument().String());
        Assert.Equal(-1, reader.Read());
      });

      new StringReader(Xml).With(reader =>
      {
        Assert.Equal(Xml, reader.XmlDocument(true).String());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.XmlReader(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.XmlReader(null));

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      var textReader = new StringReader(Xml);
      Assert.Equal("text", textReader.XmlReader().Read(reader =>
      {
        Assert.False(reader.Settings.CloseInput);
        Assert.True(reader.Settings.IgnoreComments);
        Assert.True(reader.Settings.IgnoreWhitespace);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }));
      Assert.Equal(-1, textReader.Read());

      textReader = new StringReader(Xml);
      Assert.Equal("text", textReader.XmlReader(true).Read(reader =>
      {
        //Assert.True(reader.Settings.CloseInput);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }));
      Assert.Throws<ObjectDisposedException>(() => textReader.Read());
    }
  }
}