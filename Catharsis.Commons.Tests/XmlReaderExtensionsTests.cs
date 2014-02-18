using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XmlReaderExtensions"/>.</para>
  /// </summary>
  public sealed class XmlReaderExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="XmlReaderExtendedExtensions.Deserialize(XmlReader, Type, IEnumerable{Type}, bool)"/></description></item>
    ///     <item><description><see cref="XmlReaderExtendedExtensions.Deserialize{T}(XmlReader, IEnumerable{Type}, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Deserialize_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => XmlReaderExtendedExtensions.Deserialize<object>(null));
      Assert.Throws<InvalidOperationException>(() => new StringReader(string.Empty).XmlReader(true).Deserialize<object>(null, true));

      var serialized = Guid.NewGuid().ToString();
      var xml = new StringWriter().With(writer =>
      {
        new XmlSerializer(serialized.GetType()).Serialize(writer, serialized);
        return writer.ToString();
      });
      
      var xmlReader = new StringReader(xml);
      new StringReader(xml).XmlReader(true).With(reader =>
      {
        var deserialized = reader.Deserialize(serialized.GetType());
        Assert.False(ReferenceEquals(serialized, deserialized));
        Assert.Equal(serialized, deserialized);
      });
      xmlReader.ReadToEnd();
      xmlReader.Close();

      xmlReader = new StringReader(xml);
      xmlReader.XmlReader(true).With(reader =>
      {
        var deserialized = reader.Deserialize(serialized.GetType(), null, true);
        Assert.False(ReferenceEquals(serialized, deserialized));
        Assert.Equal(serialized, deserialized);
        Assert.False(reader.Read());
        Assert.Throws<ObjectDisposedException>(() => xmlReader.Read());
      });

      Assert.Equal(serialized, new StringReader(xml).XmlReader(true).Deserialize<string>(null, true));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlReaderExtensions.Dictionary(XmlReader, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dictionary_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlReaderExtensions.Dictionary(null));

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
      new StringReader(xml.ToString()).XmlReader(true).With(reader =>
      {
        dictionary = reader.Dictionary();
        Assert.True(dictionary.Keys.SequenceEqual(xmlDictionary.Keys));
      });

      var xmlReader = new StringReader(xml.ToString()).XmlReader(true);
      dictionary = xmlReader.Dictionary(true);
      Assert.False(xmlReader.Read());
      Assert.True(dictionary.Keys.SequenceEqual(xmlDictionary.Keys));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlReaderExtensions.Read{READER, RESULT}(READER, Func{READER, RESULT})"/> method.</para>
    /// </summary>
    [Fact]
    public void Read_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlReaderExtensions.Read<XmlReader, object>(null, x => new object()));
      Assert.Throws<ArgumentNullException>(() => XmlReader.Create(Stream.Null).Read<XmlReader, object>(null));

      const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      var reader = new StringReader(Xml).XmlReader();
      Assert.Equal("text", reader.Read(x => x.ReadElementString("article")));
      Assert.False(reader.Read());
    }
  }
}