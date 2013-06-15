using System;
using System.Collections.Generic;
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
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="WebLink()"/>
    ///   <seealso cref="WebLink(IDictionary{string, object})"/>
    ///   <seealso cref="WebLink(string, string, string, string, string, WebLinksCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var weblink = new WebLink();
      Assert.True(weblink.Id == null);
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.Comments.Count == 0);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == null);
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == null);
      Assert.True(weblink.Tags.Count == 0);
      Assert.True(weblink.Text == null);
      Assert.True(weblink.Category == null);
      Assert.True(weblink.Url == null);

      weblink = new WebLink(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new WebLinksCategory())
        .AddNext("Url", "url"));
      Assert.True(weblink.Id == "id");
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.Comments.Count == 0);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Tags.Count == 0);
      Assert.True(weblink.Text == "text");
      Assert.True(weblink.Category != null);
      Assert.True(weblink.Url == "url");

      weblink = new WebLink("id", "language", "name", "text", "url", new WebLinksCategory());
      Assert.True(weblink.Id == "id");
      Assert.True(weblink.AuthorId == null);
      Assert.True(weblink.Comments.Count == 0);
      Assert.True(weblink.DateCreated <= DateTime.UtcNow);
      Assert.True(weblink.Language == "language");
      Assert.True(weblink.LastUpdated <= DateTime.UtcNow);
      Assert.True(weblink.Name == "name");
      Assert.True(weblink.Tags.Count == 0);
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

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new [] { new WebLinksCategory { Name = "Name" }, new WebLinksCategory { Name = "Name_2" } })
        .AddNext("Url", new [] { "Url", "Url_2" }));
    }
  }
}