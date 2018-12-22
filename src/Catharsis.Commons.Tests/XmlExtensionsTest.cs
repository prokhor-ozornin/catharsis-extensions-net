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
  public sealed class XmlExtensionsTest
  {
    [Fact]
    public void as_x_document()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXDocument(null));
      Assert.Throws<XmlException>(() => Stream.Null.AsXDocument());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.UTF32)))
      {
        Assert.Throws<XmlException>(() => stream.AsXDocument());
      }

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
      {
        Assert.Equal("<article>text</article>", stream.AsXDocument().ToString());
        Assert.Empty(stream.Bytes());
        Assert.Equal(-1, stream.ReadByte());
      }

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
      {
        Assert.Equal("<article>text</article>", stream.AsXDocument(true).ToString());
        Assert.Equal(-1, stream.ReadByte());
      }
    }

    [Fact]
    public void as_xml_document()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXmlDocument(null));
      Assert.Throws<XmlException>(() => Stream.Null.AsXmlDocument());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.UTF32)))
      {
        Assert.Throws<XmlException>(() => stream.AsXmlDocument());
      }

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
      {
        Assert.Equal(Xml, stream.AsXmlDocument().String());
        Assert.Empty(stream.Bytes());
        Assert.Equal(-1, stream.ReadByte());
      }

      using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
      {
        Assert.Equal(Xml, stream.AsXmlDocument(true).String());
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }
    }

    [Fact]
    public void as_xml_string()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.AsXml<object>(null));
      Assert.Throws<ArgumentException>(() => string.Empty.AsXml<object>());

      var subject = Guid.Empty;
      Assert.Equal(subject, subject.ToXml().AsXml<Guid>());
    }

    [Fact]
    public void as_xml_stream()
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

    [Fact]
    public void as_xml_textreader()
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

    [Fact]
    public void deserialize()
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

    [Fact]
    public void dictionary_xml_document()
    {
      Assert.Throws<ArgumentNullException>(() => ((XmlDocument) null).Dictionary());

      var xml = new XmlDocument();
      var articlesXml = xml.CreateElement("Articles");
      xml.AppendChild(articlesXml);
      var articleXml = xml.CreateElement("Article");
      articlesXml.AppendChild(articleXml);
      articleXml.AppendChild(xml.CreateComment("Comment"));
      articleXml.SetAttribute("Id", "id");
      var nameXml = xml.CreateElement("Name");
      nameXml.InnerText = "name";
      articleXml.AppendChild(nameXml);
      var dateXml = xml.CreateElement("Date");
      dateXml.InnerText = DateTime.MaxValue.ToString();
      articleXml.AppendChild(dateXml);
      var descriptionXml = xml.CreateElement("Description");
      descriptionXml.AppendChild(xml.CreateCDataSection("description"));
      articleXml.AppendChild(descriptionXml);
      articleXml.AppendChild(xml.CreateElement("Notes"));
      var tagsXml = xml.CreateElement("Tags");
      articleXml.AppendChild(tagsXml);
      var tag1Xml = xml.CreateElement("Tag");
      tag1Xml.InnerText = "tag1";
      tagsXml.AppendChild(tag1Xml);
      var tag2Xml = xml.CreateElement("Tag");
      tag2Xml.InnerText = "tag2";
      tagsXml.AppendChild(tag2Xml);

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

    [Fact]
    public void dictionary_x_document()
    {
      Assert.Throws<ArgumentNullException>(() => ((XDocument)null).Dictionary());

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

    [Fact]
    public void dictionary_x_element()
    {
      Assert.Throws<ArgumentNullException>(() => ((XElement)null).Dictionary());

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

    [Fact]
    public void dictionary_xml_reader()
    {
      Assert.Throws<ArgumentNullException>(() => ((XmlReader)null).Dictionary());

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

    [Fact]
    public void write()
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

    [Fact]
    public void x_document()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.XDocument(null));
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

    [Fact]
    public void xml_string()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.String(null));

      Assert.Equal(string.Empty, new XmlDocument().String());

      var document = new XmlDocument();
      var element = document.CreateElement("article");
      element.SetAttribute("id", "1");
      element.InnerText = "Text";
      document.AppendChild(element);
      Assert.Equal("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>", document.String());
    }

    [Fact]
    public void xml_document()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.XmlDocument(null));
      Assert.Throws<XmlException>(() => TextReader.Null.XmlDocument());

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      using (var reader = new StringReader(Xml))
      {
        Assert.Equal(Xml, reader.XmlDocument().String());
        Assert.Equal(-1, reader.Read());
      }

      using (var reader = new StringReader(Xml))
      {
        Assert.Equal(Xml, reader.XmlDocument(true).String());
        Assert.Throws<ObjectDisposedException>(() => reader.Read());
      }
    }
  }
}