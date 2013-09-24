using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="WebLink"/>.</para>
  /// </summary>
  public sealed class WebLinkTests : EntityUnitTests<WebLink>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="WebLink.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new WebLinksCategory();
      Assert.True(ReferenceEquals(new WebLink { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLink.Url"/> property.</para>
    /// </summary>
    [Fact]
    public void Url_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new WebLink { Url = null });
      Assert.Throws<ArgumentException>(() => new WebLink { Url = string.Empty });
      Assert.True(new WebLink { Url = "url" }.Url == "url");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="WebLink()"/>
    ///   <seealso cref="WebLink(IDictionary{string, object})"/>
    ///   <seealso cref="WebLink(string, string, string, string, WebLinksCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var weblink = new WebLink();
      Assert.True(weblink.Id == 0);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == null);
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == null);
      Assert.True(weblink.Text == null);
      Assert.True(weblink.Category == null);
      Assert.True(weblink.Url == null);

      Assert.Throws<ArgumentNullException>(() => new WebLink(null));
      weblink = new WebLink(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new WebLinksCategory())
        .AddNext("Url", "url"));
      Assert.True(weblink.Id == 1);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Text == "text");
      Assert.True(weblink.Category != null);
      Assert.True(weblink.Url == "url");

      Assert.Throws<ArgumentNullException>(() => new WebLink(null, "name", "text", "url"));
      Assert.Throws<ArgumentNullException>(() => new WebLink("language", null, "text", "url"));
      Assert.Throws<ArgumentNullException>(() => new WebLink("language", "name", null, "url"));
      Assert.Throws<ArgumentNullException>(() => new WebLink("language", "name", "text", null));
      Assert.Throws<ArgumentException>(() => new WebLink(string.Empty, "name", "text", "url"));
      Assert.Throws<ArgumentException>(() => new WebLink("language", string.Empty, "text", "url"));
      Assert.Throws<ArgumentException>(() => new WebLink("language", "name", string.Empty, "url"));
      Assert.Throws<ArgumentException>(() => new WebLink("language", "name", "text", string.Empty));
      weblink = new WebLink("language", "name", "text", "url", new WebLinksCategory());
      Assert.True(weblink.Id == 0);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Text == "text");
      Assert.True(weblink.Category != null);
      Assert.True(weblink.Url == "url");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLink.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new WebLink { Url = "Url" }.ToString() == "Url");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="WebLink.Equals(WebLink)"/></description></item>
    ///     <item><description><see cref="WebLink.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new WebLinksCategory { Name = "Name" }, new WebLinksCategory { Name = "Name_2" });
      this.TestEquality("Url", "Url", "Url_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLink.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new WebLinksCategory { Name = "Name" }, new WebLinksCategory { Name = "Name_2" });
      this.TestHashCode("Url", "Url", "Url_2");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="WebLink.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="WebLink.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => WebLink.Xml(null));

      var xml = new XElement("WebLink",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Url", "url"));
      var weblink = WebLink.Xml(xml);
      Assert.True(weblink.Id == 1);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.Category == null);
      Assert.True(weblink.Comments.Count == 0);
      Assert.True(weblink.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Tags.Count == 0);
      Assert.True(weblink.Text == "text");
      Assert.True(weblink.Url == "url");
      Assert.True(new WebLink("language", "name", "text", "url") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(WebLink.Xml(weblink.Xml()).Equals(weblink));

      xml = new XElement("WebLink",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("WebLinksCategory",
          new XElement("Id", 2),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Url", "url"));
      weblink = WebLink.Xml(xml);
      Assert.True(weblink.Id == 1);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.Category.Id == 2);
      Assert.True(weblink.Category.Language == "category.language");
      Assert.True(weblink.Category.Name == "category.name");
      Assert.True(weblink.Comments.Count == 0);
      Assert.True(weblink.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Tags.Count == 0);
      Assert.True(weblink.Text == "text");
      Assert.True(weblink.Url == "url");
      Assert.True(new WebLink("language", "name", "text", "url", new WebLinksCategory("category.language", "category.name") { Id = 2 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(WebLink.Xml(weblink.Xml()).Equals(weblink));
    }
  }
}