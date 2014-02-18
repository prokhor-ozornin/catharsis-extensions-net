using System;
using System.Collections.Generic;
using System.Xml;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XmlDocumentExtensions"/>.</para>
  /// </summary>
  public sealed class XmlDocumentExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Dictionary(XmlDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dictionary_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlDocumentExtensions.Dictionary(null));

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
      Assert.Equal(DateTime.MaxValue.ToRfc1123(), article["Date"].ToString().ToDateTime().ToRfc1123());
      Assert.Equal("description", article["Description"].ToString());
      Assert.Null(article["Notes"]);
      var tags = article["Tags"].To<IDictionary<string, object>>();
      Assert.Equal(1, tags.Keys.Count);
      Assert.Equal("tag2", tags["Tag"].ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="XmlDocumentExtensions.String(XmlDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void String_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlDocumentExtensions.String(null));

      Assert.Equal(string.Empty, new XmlDocument().String());
      
      var document = new XmlDocument();
      var element = document.CreateElement("article");
      element.SetAttribute("id", "1");
      element.InnerText = "Text";
      document.AppendChild(element);
      Assert.Equal("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>", document.String());
    }
  }
}