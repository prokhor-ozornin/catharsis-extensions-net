using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArtsAlbum"/>.</para>
  /// </summary>
  public sealed class ArtsAlbumTests : EntityUnitTests<ArtsAlbum>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArtsAlbum.PublishedOn"/> property.</para>
    /// </summary>
    [Fact]
    public void PublishedOn_Property()
    {
      Assert.True(new ArtsAlbum { PublishedOn = DateTime.MinValue }.PublishedOn == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="ArtsAlbum()"/>
    ///   <seealso cref="ArtsAlbum(IDictionary{string, object})"/>
    ///   <seealso cref="ArtsAlbum(string, string, string, string, DateTime?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var album = new ArtsAlbum();
      Assert.True(album.Id == null);
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated <= DateTime.UtcNow);
      Assert.True(album.Language == null);
      Assert.True(album.LastUpdated <= DateTime.UtcNow);
      Assert.True(album.Name == null);
      Assert.False(album.PublishedOn.HasValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == null);

      Assert.Throws<ArgumentNullException>(() => new ArtsAlbum(null));
      album = new ArtsAlbum(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("PublishedOn", DateTime.MinValue));
      Assert.True(album.Id == "id");
      Assert.True(album.AuthorId == "authorId");
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated <= DateTime.UtcNow);
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated <= DateTime.UtcNow);
      Assert.True(album.Name == "name");
      Assert.True(album.PublishedOn == DateTime.MinValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new ArtsAlbum(null, "language", "name"));
      Assert.Throws<ArgumentNullException>(() => new ArtsAlbum("id", null, "name"));
      Assert.Throws<ArgumentNullException>(() => new ArtsAlbum("id", "language", null));
      Assert.Throws<ArgumentException>(() => new ArtsAlbum(string.Empty, "language", "name"));
      Assert.Throws<ArgumentException>(() => new ArtsAlbum("id", string.Empty, "name"));
      Assert.Throws<ArgumentException>(() => new ArtsAlbum("id", "language", string.Empty));
      album = new ArtsAlbum("id", "language", "name", "text", DateTime.MinValue);
      Assert.True(album.Id == "id");
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated <= DateTime.UtcNow);
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated <= DateTime.UtcNow);
      Assert.True(album.Name == "name");
      Assert.True(album.PublishedOn == DateTime.MinValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="ArtsAlbum"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtsAlbum.CompareTo(ArtsAlbum)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new ArtsAlbum { Name = "Name" }.CompareTo(new ArtsAlbum { Name = "Name" }) == 0);
      Assert.True(new ArtsAlbum { Name = "First" }.CompareTo(new ArtsAlbum { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ArtsAlbum.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="ArtsAlbum.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ArtsAlbum.Xml(null));

      var xml = new XElement("ArtsAlbum",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"));
      var album = ArtsAlbum.Xml(xml);
      Assert.True(album.Id == "id");
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(album.Name == "name");
      Assert.False(album.PublishedOn.HasValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == null);
      Assert.True(new ArtsAlbum("id", "language", "name") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(ArtsAlbum.Xml(album.Xml()).Equals(album));

      xml = new XElement("ArtsAlbum",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("PublishedOn", DateTime.MinValue.ToRfc1123()));
      album = ArtsAlbum.Xml(xml);
      Assert.True(album.Id == "id");
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(album.Name == "name");
      Assert.True(album.PublishedOn.GetValueOrDefault().ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == "text");
      Assert.True(new ArtsAlbum("id", "language", "name", "text", DateTime.MinValue) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(ArtsAlbum.Xml(album.Xml()).Equals(album));
    }
  }
}