using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XDocumentExtensions"/>.</para>
  /// </summary>
  public sealed class XDocumentExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XDocumentExtensions.Dictionary(XDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dictionary_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XDocumentExtensions.Dictionary(null));

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
      Assert.True(dictionary.Keys.Count == 1);
      Assert.True(dictionary.ContainsKey("Articles"));
      var article = dictionary["Articles"].To<IDictionary<string, object>>()["Article"].To<IDictionary<string, object>>();
      Assert.True(article.Keys.Count == 6);
      Assert.False(article.ContainsKey("Comment"));
      Assert.True(article["Id"].ToString() == "id");
      Assert.True(article["Name"].ToString() == "name");
      Assert.True(article["Date"].ToString().ToDateTime().ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(article["Description"].ToString() == "description");
      Assert.True(article["Notes"] == null);
      var tags = article["Tags"].To<IDictionary<string, object>>();
      Assert.True(tags.Keys.Count == 1);
      Assert.True(tags["Tag"].ToString() == "tag2");
    }
  }
}