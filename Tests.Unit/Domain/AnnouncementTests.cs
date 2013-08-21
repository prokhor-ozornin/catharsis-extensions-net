using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Announcement"/>.</para>
  /// </summary>
  public sealed class AnnouncementTests : EntityUnitTests<Announcement>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new AnnouncementsCategory();
      Assert.True(ReferenceEquals(new Announcement { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.Currency"/> property.</para>
    /// </summary>
    [Fact]
    public void Currency_Property()
    {
      Assert.True(new Announcement { Currency = "currency" }.Currency == "currency");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Announcement { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.Price"/> property.</para>
    /// </summary>
    [Fact]
    public void Price_Property()
    {
      Assert.True(new Announcement { Price = decimal.One }.Price == decimal.One);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Announcement()"/>
    ///   <seealso cref="Announcement(IDictionary{string, object)"/>
    ///   <seealso cref="Announcement(string, string, string, string, string, AnnouncementsCategory, Image, string, decimal?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var announcement = new Announcement();
      Assert.True(announcement.Id == null);
      Assert.True(announcement.AuthorId == null);
      Assert.True(announcement.Category == null);
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == null);
      Assert.True(announcement.DateCreated <= DateTime.UtcNow);
      Assert.True(announcement.Image == null);
      Assert.True(announcement.Language == null);
      Assert.True(announcement.LastUpdated <= DateTime.UtcNow);
      Assert.True(announcement.Name == null);
      Assert.False(announcement.Price.HasValue);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Announcement(null));
      announcement = new Announcement(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new AnnouncementsCategory())
        .AddNext("Currency", "currency")
        .AddNext("Image", new Image())
        .AddNext("Price", decimal.One));
      Assert.True(announcement.Id == "id");
      Assert.True(announcement.AuthorId == "authorId");
      Assert.True(announcement.Category != null);
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == "currency");
      Assert.True(announcement.DateCreated <= DateTime.UtcNow);
      Assert.True(announcement.Image != null);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated <= DateTime.UtcNow);
      Assert.True(announcement.Name == "name");
      Assert.True(announcement.Price == decimal.One);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Announcement(null, "language", "name", "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("id", null, "name", "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("id", "language", null, "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("id", "language", "name", null, "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("id", "language", "name", "text", null, new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement(string.Empty, "language", "name", "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("id", string.Empty, "name", "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("id", "language", string.Empty, "text", "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("id", "language", "name", string.Empty, "authorId", new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("id", "language", "name", "text", string.Empty, new AnnouncementsCategory()));
      announcement = new Announcement("id", "language", "name", "text", "authorId", new AnnouncementsCategory(), new Image(), "currency", decimal.One);
      Assert.True(announcement.Id == "id");
      Assert.True(announcement.AuthorId == "authorId");
      Assert.True(announcement.Category != null);
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == "currency");
      Assert.True(announcement.DateCreated <= DateTime.UtcNow);
      Assert.True(announcement.Image != null);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated <= DateTime.UtcNow);
      Assert.True(announcement.Name == "name");
      Assert.True(announcement.Price == decimal.One);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.Equals(object)"/> and <see cref="Announcement.GetHashCode()"/> methods.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new AnnouncementsCategory { Name = "Name" }, new AnnouncementsCategory { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Announcement.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Announcement.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Announcement.Xml(null));

      var xml = new XElement("Announcement",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var announcement = Announcement.Xml(xml);
      Assert.True(announcement.Id == "id");
      Assert.True(announcement.AuthorId == "authorId");
      Assert.True(announcement.Category == null);
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == null);
      Assert.True(announcement.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(announcement.Image == null);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(announcement.Name == "name");
      Assert.False(announcement.Price.HasValue);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");
      Assert.True(new Announcement("id", "language", "name", "text", "authorId") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Announcement.Xml(announcement.Xml()).Equals(announcement));

      xml = new XElement("Announcement",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("AnnouncementsCategory",
          new XElement("Id", "category.id"),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Currency", "currency"),
        new XElement("Image",
          new XElement("Id", "image.id"),
          new XElement("File",
            new XElement("Id", "image.file.id"),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
          new XElement("Height", 1),
          new XElement("Width", 2)),
        new XElement("Price", decimal.One));
      announcement = Announcement.Xml(xml);
      Assert.True(announcement.Id == "id");
      Assert.True(announcement.AuthorId == "authorId");
      Assert.True(announcement.Category.Id == "category.id");
      Assert.True(announcement.Category.Language == "category.language");
      Assert.True(announcement.Category.Name == "category.name");
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == "currency");
      Assert.True(announcement.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(announcement.Image.Id == "image.id");
      Assert.True(announcement.Image.File.Id == "image.file.id");
      Assert.True(announcement.Image.File.ContentType == "image.file.contentType");
      Assert.True(announcement.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(announcement.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(announcement.Image.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(announcement.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(announcement.Image.File.Name == "image.file.name");
      Assert.True(announcement.Image.File.OriginalName == "image.file.originalName");
      Assert.True(announcement.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(announcement.Image.Height == 1);
      Assert.True(announcement.Image.Width == 2);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(announcement.Name == "name");
      Assert.True(announcement.Price == decimal.One);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");
      Assert.True(new Announcement("id", "language", "name", "text", "authorId", new AnnouncementsCategory("category.id", "category.language", "category.name"), new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue}, 1, 2), "currency", decimal.One) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Announcement.Xml(announcement.Xml()).Equals(announcement));
    }
  }
}