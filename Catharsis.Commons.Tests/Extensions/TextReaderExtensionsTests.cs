using System;
using System.IO;
using System.Xml;
using Xunit;

namespace Catharsis.Commons.Extensions
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
        Assert.True(reader.Lines().Count == 0);
        Assert.True(reader.Read() == -1);
      });

      new StringReader(string.Empty).With(reader =>
      {
        Assert.True(reader.Lines(true).Count == 0);
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });

      new StringReader("First{0}Second{0}".FormatValue(Environment.NewLine)).With(reader =>
      {
        var lines = reader.Lines();
        Assert.True(lines.Count == 2);
        Assert.True(lines[0] == "First");
        Assert.True(lines[1] == "Second");
        Assert.True(reader.Lines().Count == 0);
        Assert.True(reader.Read() == -1);
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
        Assert.True(reader.Text() == text);
        Assert.True(reader.Read() == -1);
      });

      new StringReader(text).With(reader =>
      {
        Assert.True(reader.Text(true) == text);
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextReaderExtensions.XDocument(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.XDocument(null));
      Assert.Throws<XmlException>(() => TextReaderExtensions.XDocument(TextReader.Null));

      const string xml = "<?xml version=\"1.0\"?><article>text</article>";

      new StringReader(xml).With(reader =>
      {
        Assert.True(reader.XDocument().ToString() == "<article>text</article>");
        Assert.True(reader.Read() == -1);
      });

      new StringReader(xml).With(reader =>
      {
        Assert.True(reader.XDocument(true).ToString() == "<article>text</article>");
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
      Assert.Throws<XmlException>(() => TextReaderExtendedExtensions.XmlDocument(TextReader.Null));

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      new StringReader(xml).With(reader =>
      {
        Assert.True(reader.XmlDocument().String() == xml);
        Assert.True(reader.Read() == -1);
      });

      new StringReader(xml).With(reader =>
      {
        Assert.True(reader.XmlDocument(true).String() == xml);
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

      const string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      
      var textReader = new StringReader(xml);
      Assert.True(textReader.XmlReader().Read(reader =>
      {
        Assert.False(reader.Settings.CloseInput);
        Assert.True(reader.Settings.IgnoreComments);
        Assert.True(reader.Settings.IgnoreWhitespace);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }) == "text");
      Assert.True(textReader.Read() == -1);

      textReader = new StringReader(xml);
      Assert.True(textReader.XmlReader(true).Read(reader =>
      {
        //Assert.True(reader.Settings.CloseInput);
        reader.ReadStartElement("article");
        return reader.ReadString();
      }) == "text");
      Assert.Throws<ObjectDisposedException>(() => textReader.Read());
    }
  }
}