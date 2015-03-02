using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TextStreamExtensions"/>.</para>
  /// </summary>
  public sealed class TextStreamExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TextStreamExtensions.Lines(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Lines_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextStreamExtensions.Lines(null));

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
    ///   <para>Performs testing of <see cref="TextStreamExtensions.Text(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(()  => TextStreamExtensions.Text(null));

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
    ///   <para>Performs testing of <see cref="TextStreamExtensions.XDocument(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextStreamExtensions.XDocument(null));
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
        Assert.Equal(-1, reader.Read());
      }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextStreamExtensions.XmlReader(TextReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextStreamExtensions.XmlReader(null));

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

      /// <summary>
    ///   <para>Performs testing of <see cref="TextStreamExtensions.XmlWriter"/> method.</para>
    /// </summary>
    [Fact]
    public void XmlWriter_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextStreamExtensions.XmlWriter(null));

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
}
}