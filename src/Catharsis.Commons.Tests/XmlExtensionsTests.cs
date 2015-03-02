using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XmlExtensions"/>.</para>
  /// </summary>
  public sealed class XmlExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.AsXml{T}(string, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AsXmlString_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXml<object>(null));
      Assert.Throws<ArgumentException>(() => string.Empty.AsXml<object>());

      var subject = Guid.Empty;
      Assert.Equal(subject, subject.ToXml().AsXml<Guid>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.AsXml{T}(Stream, bool, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AsXmlStream_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXml<object>(null));

      var subject = Guid.Empty;
      using (var stream = new MemoryStream())
      {
        subject.ToXml(stream, Encoding.Unicode);
        Assert.Equal(subject, stream.Rewind().AsXml<Guid>());
        Assert.True(stream.CanWrite);
      }
      using (var stream = new MemoryStream())
      {
        subject.ToXml(stream, Encoding.Unicode);
        Assert.Equal(subject, stream.Rewind().AsXml<Guid>(true));
        Assert.False(stream.CanWrite);
      }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.AsXml{T}(TextReader, bool, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AsXmlTextReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXml<object>(null));

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
    ///   <para>Performs testing of <see cref="XmlExtensions.Dictionary(XDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void DictionaryXDocument_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Dictionary((XDocument) null));

      var xml = new XDocument(
        new XElement("Articles",
          new XElement("Article",
            new XComment("Comment"),
            new XAttribute("Id", "id"),
            new XElement("Name", "name"),
            new XElement("Date", DateTime.MaxValue),
            new XElement("Description", new XCData("description")),
            new XElement("Notes", string.Empty),
            new XElement("Tags",
            new XElement("Tag", "tag1"),
            new XElement("Tag", "tag2")))));
      var dictionary = xml.Dictionary();
      Assert.Equal(1, dictionary.Keys.Count);
      Assert.True(dictionary.ContainsKey("Articles"));
      var article = dictionary["Articles"].To<IDictionary<string, object>>()["Article"].To<IDictionary<string, object>>();
      Assert.Equal(6, article.Keys.Count);
      Assert.False(article.ContainsKey("Comment"));
      Assert.Equal("id", article["Id"].ToString());
      Assert.Equal("name", article["Name"].ToString());
      Assert.Equal(DateTime.MaxValue.RFC1121(), article["Date"].ToString().ToDateTime().RFC1121());
      Assert.Equal("description", article["Description"].ToString());
      Assert.Null(article["Notes"]);
      var tags = article["Tags"].To<IDictionary<string, object>>();
      Assert.Equal(1, tags.Keys.Count);
      Assert.Equal("tag2", tags["Tag"].ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.Dictionary(XElement)"/> method.</para>
    /// </summary>
    [Fact]
    public void DictionaryXElement_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Dictionary((XElement) null));

      var xml = new XDocument(
        new XElement("Articles",
          new XElement("Article",
            new XComment("Comment"),
            new XAttribute("Id", "id"),
            new XElement("Name", "name"),
            new XElement("Date", DateTime.MaxValue),
            new XElement("Description", new XCData("description")),
            new XElement("Notes", string.Empty),
            new XElement("Tags",
              new XElement("Tag", "tag1"),
              new XElement("Tag", "tag2")))));
      var dictionary = xml.Root.Dictionary();
      Assert.Equal(1, dictionary.Keys.Count);
      Assert.True(dictionary.ContainsKey("Articles"));
      var article = dictionary["Articles"].To<IDictionary<string, object>>()["Article"].To<IDictionary<string, object>>();
      Assert.Equal(6, article.Keys.Count);
      Assert.False(article.ContainsKey("Comment"));
      Assert.Equal("id", article["Id"].ToString());
      Assert.Equal("name", article["Name"].ToString());
      Assert.Equal(DateTime.MaxValue.RFC1121(), article["Date"].ToString().ToDateTime().RFC1121());
      Assert.Equal("description", article["Description"].ToString());
      Assert.Null(article["Notes"]);
      var tags = article["Tags"].To<IDictionary<string, object>>();
      Assert.Equal(1, tags.Keys.Count);
      Assert.Equal("tag2", tags["Tag"].ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.Dictionary(XmlReader)"/> method.</para>
    /// </summary>
    [Fact]
    public void DictionaryXmlReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Dictionary((XmlReader) null));

      var xml = new XDocument(
        new XElement("Articles",
          new XElement("Article",
            new XComment("Comment"),
            new XAttribute("Id", "id"),
            new XElement("Name", "name"),
            new XElement("Date", DateTime.MaxValue),
            new XElement("Description", new XCData("description")),
            new XElement("Tags",
              new XElement("Tag", "tag1"),
              new XElement("Tag", "tag2")))));
      var xmlDictionary = xml.Dictionary();
      
      IDictionary<string, object> dictionary;
      using (var reader = new StringReader(xml.ToString()).XmlReader(true))
      {
        dictionary = reader.Dictionary();
        Assert.True(dictionary.Keys.SequenceEqual(xmlDictionary.Keys));
      }

      var xmlReader = new StringReader(xml.ToString()).XmlReader(true);
      dictionary = xmlReader.Dictionary();
      Assert.False(xmlReader.Read());
      Assert.True(dictionary.Keys.SequenceEqual(xmlDictionary.Keys));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="XmlExtensions.Deserialize(XmlReader, Type, IEnumerable{Type})"/></description></item>
    ///     <item><description><see cref="XmlExtensions.Deserialize{T}(XmlReader, IEnumerable{Type})"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Deserialize_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Deserialize<object>(null));
      Assert.Throws<InvalidOperationException>(() => new StringReader(string.Empty).XmlReader(true).Deserialize<object>());

      var serialized = Guid.NewGuid().ToString();
      var xml = new StringWriter().Do(writer =>
      {
        new XmlSerializer(serialized.GetType()).Serialize(writer, serialized);
        return writer.ToString();
      });
      
      var xmlReader = new StringReader(xml);
      using (var reader = new StringReader(xml).XmlReader(true))
      {
        var deserialized = reader.Deserialize(serialized.GetType());
        Assert.False(ReferenceEquals(serialized, deserialized));
        Assert.Equal(serialized, deserialized);
      }
      xmlReader.ReadToEnd();
      xmlReader.Close();

      xmlReader = new StringReader(xml);
      using (var reader = xmlReader.XmlReader(true))
      {
        var deserialized = reader.Deserialize(serialized.GetType());
        Assert.False(ReferenceEquals(serialized, deserialized));
        Assert.Equal(serialized, deserialized);
        Assert.False(reader.Read());
        Assert.Equal(-1, xmlReader.Read());
      }

      Assert.Equal(serialized, new StringReader(xml).XmlReader(true).Deserialize<string>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlExtensions.Write{WRITER}(WRITER, Action{WRITER})"/> method.</para>
    /// </summary>
    [Fact]
    public void Write_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Write<XmlWriter>(null, writer => {}));
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.Write(XmlWriter.Create(Path.GetTempFileName()), null));

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      var stringWriter = new StringWriter();
      var xmlWriter = stringWriter.XmlWriter(true, Encoding.Unicode);
      Assert.True(ReferenceEquals(xmlWriter.Write(writer =>
      {
        writer.WriteStartDocument();
        writer.WriteElementString("article", "text");
        writer.WriteEndDocument();
      }), xmlWriter));
      Assert.Throws<InvalidOperationException>(() => xmlWriter.WriteRaw(string.Empty));
      Assert.Equal(Xml, stringWriter.ToString());
      stringWriter.WriteLine();
    }
  }
}