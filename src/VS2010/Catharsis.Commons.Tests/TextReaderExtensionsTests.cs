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

      using (var reader = new StringReader("First{0}Second{0}".FormatSelf(Environment.NewLine)))
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

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.Text(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(()  => TextReaderExtensions.Text(null));

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

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderXmlExtensions.AsXml{T}(TextReader, bool, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AsXml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderXmlExtensions.AsXml<object>(null));

      var subject = Guid.Empty;
      using (var reader = new StringReader(subject.ToXml()))
      {
        Assert.True(reader.AsXml<Guid>() == subject);
        reader.ReadLine();
      }
      using (var reader = new StringReader(subject.ToXml()))
      {
        Assert.Equal(subject, reader.AsXml<Guid>(true));
        Assert.Throws<ObjectDisposedException>(() => reader.ReadLine());
      }
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

      using (var reader = new StringReader(Xml))
      {
        Assert.Equal("<article>text</article>", reader.XDocument().ToString());
        Assert.Equal(-1, reader.Read());
      }

      using (var reader = new StringReader(Xml))
      {
        Assert.Equal("<article>text</article>", reader.XDocument(true).ToString());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      }
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