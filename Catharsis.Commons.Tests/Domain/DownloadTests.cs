using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Download"/>.</para>
  /// </summary>
  public sealed class DownloadTests : EntityUnitTests<Download>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Download.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new DownloadsCategory();
      Assert.True(ReferenceEquals(new Download { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Download.Downloads"/> property.</para>
    /// </summary>
    [Fact]
    public void Downloads_Property()
    {
      Assert.True(new Download { Downloads = 1 }.Downloads == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Download.Url"/> property.</para>
    /// </summary>
    [Fact]
    public void Url_Property()
    {
      Assert.True(new Download { Url = "url" }.Url == "url");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Download()"/>
    ///   <seealso cref="Download(string, string, string, DownloadsCategory, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var download = new Download();
      Assert.True(download.Id == 0);
      Assert.True(download.AuthorId == null);
      Assert.True(download.Category == null);
      Assert.True(download.Comments.Count == 0);
      Assert.True(download.DateCreated <= DateTime.UtcNow);
      Assert.True(download.Downloads == 0);
      Assert.True(download.Language == null);
      Assert.True(download.LastUpdated <= DateTime.UtcNow);
      Assert.True(download.Name == null);
      Assert.True(download.Tags.Count == 0);
      Assert.True(download.Text == null);
      Assert.True(download.Url == null);

      Assert.Throws<ArgumentNullException>(() => new Download(null, "name", "url"));
      Assert.Throws<ArgumentNullException>(() => new Download("language", null, "url"));
      Assert.Throws<ArgumentNullException>(() => new Download("language", "name", null));
      Assert.Throws<ArgumentException>(() => new Download(string.Empty, "name", "url"));
      Assert.Throws<ArgumentException>(() => new Download("language", string.Empty, "url"));
      Assert.Throws<ArgumentException>(() => new Download("language", "name", string.Empty));
      download = new Download("language", "name", "url", new DownloadsCategory(), "text");
      Assert.True(download.Id == 0);
      Assert.True(download.AuthorId == null);
      Assert.True(download.Category != null);
      Assert.True(download.Comments.Count == 0);
      Assert.True(download.DateCreated <= DateTime.UtcNow);
      Assert.True(download.Downloads == 0);
      Assert.True(download.Language == "language");
      Assert.True(download.LastUpdated <= DateTime.UtcNow);
      Assert.True(download.Name == "name");
      Assert.True(download.Tags.Count == 0);
      Assert.True(download.Text == "text");
      Assert.True(download.Url == "url");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Download.Equals(Download)"/></description></item>
    ///     <item><description><see cref="Download.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new DownloadsCategory { Name = "Name" }, new DownloadsCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Download.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new DownloadsCategory { Name = "Name" }, new DownloadsCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Download.CompareTo(Download)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Download { Name = "Name" }.CompareTo(new Download { Name = "Name" }) == 0);
      Assert.True(new Download { Name = "First" }.CompareTo(new Download { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Download.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Download.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Download.Xml(null));

      var xml = new XElement("Download",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Downloads", 1),
        new XElement("Url", "url"));
      var download = Download.Xml(xml);
      Assert.True(download.Id == 1);
      Assert.True(download.AuthorId == null);
      Assert.True(download.Category == null);
      Assert.True(download.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(download.Downloads == 1);
      Assert.True(download.Language == "language");
      Assert.True(download.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(download.Name == "name");
      Assert.True(download.Text == null);
      Assert.True(download.Url == "url");
      Assert.True(new Download("language", "name", "url") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue, Downloads = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Download.Xml(download.Xml()).Equals(download));
      
      xml = new XElement("Download",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("DownloadsCategory",
          new XElement("Id", 2),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Downloads", 1),
        new XElement("Url", "url"));
      download = Download.Xml(xml);
      Assert.True(download.Id == 1);
      Assert.True(download.AuthorId == null);
      Assert.True(download.Category.Id == 2);
      Assert.True(download.Category.Language == "category.language");
      Assert.True(download.Category.Name == "category.name");
      Assert.True(download.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(download.Downloads == 1);
      Assert.True(download.Language == "language");
      Assert.True(download.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(download.Name == "name");
      Assert.True(download.Text == "text");
      Assert.True(download.Url == "url");
      Assert.True(new Download("language", "name", "url", new DownloadsCategory("category.language", "category.name") { Id = 2 }, "text") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue, Downloads = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Download.Xml(download.Xml()).Equals(download));
    }
  }
}