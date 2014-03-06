using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XElementExtensions"/>.</para>
  /// </summary>
  public sealed class XElementExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XElementExtensions.Dictionary(XElement)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dictionary_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XElementExtensions.Dictionary(null));

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
      Assert.Equal(DateTime.MaxValue.RFC1123(), article["Date"].ToString().ToDateTime().RFC1123());
      Assert.Equal("description", article["Description"].ToString());
      Assert.Null(article["Notes"]);
      var tags = article["Tags"].To<IDictionary<string, object>>();
      Assert.Equal(1, tags.Keys.Count);
      Assert.Equal("tag2", tags["Tag"].ToString());
    }
  }
}