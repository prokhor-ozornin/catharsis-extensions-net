using System;
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
    ///   <seealso cref="Announcement(string, string, string, long, AnnouncementsCategory, Image, string, decimal?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var announcement = new Announcement();
      Assert.True(announcement.Id == 0);
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

      Assert.Throws<ArgumentNullException>(() => new Announcement(null, "name", "text", 1, new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("language", null, "text", 1, new AnnouncementsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Announcement("language", "name", null, 1, new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement(string.Empty, "name", "text", 1, new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("language", string.Empty, "text", 1, new AnnouncementsCategory()));
      Assert.Throws<ArgumentException>(() => new Announcement("language", "name", string.Empty, 1, new AnnouncementsCategory()));
      announcement = new Announcement("language", "name", "text", 1, new AnnouncementsCategory(), new Image(), "currency", decimal.One);
      Assert.True(announcement.Id == 0);
      Assert.True(announcement.AuthorId == 1);
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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Announcement.Equals(Announcement)"/></description></item>
    ///     <item><description><see cref="Announcement.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new AnnouncementsCategory { Name = "Name" }, new AnnouncementsCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Announcement.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new AnnouncementsCategory { Name = "Name" }, new AnnouncementsCategory { Name = "Name_2" });
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
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var announcement = Announcement.Xml(xml);
      Assert.True(announcement.Id == 1);
      Assert.True(announcement.AuthorId == 2);
      Assert.True(announcement.Category == null);
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == null);
      Assert.True(announcement.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(announcement.Image == null);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(announcement.Name == "name");
      Assert.False(announcement.Price.HasValue);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");
      Assert.True(new Announcement("language", "name", "text", 2) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Announcement.Xml(announcement.Xml()).Equals(announcement));

      xml = new XElement("Announcement",
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("AnnouncementsCategory",
          new XElement("Id", 3),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Currency", "currency"),
        new XElement("Image",
          new XElement("Id", 4),
          new XElement("File",
            new XElement("Id", 5),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().Length)),
          new XElement("Height", 1),
          new XElement("Width", 2)),
        new XElement("Price", decimal.One));
      announcement = Announcement.Xml(xml);
      Assert.True(announcement.Id == 1);
      Assert.True(announcement.AuthorId == 2);
      Assert.True(announcement.Category.Id == 3);
      Assert.True(announcement.Category.Language == "category.language");
      Assert.True(announcement.Category.Name == "category.name");
      Assert.True(announcement.Comments.Count == 0);
      Assert.True(announcement.Currency == "currency");
      Assert.True(announcement.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(announcement.Image.Id == 4);
      Assert.True(announcement.Image.File.Id == 5);
      Assert.True(announcement.Image.File.ContentType == "image.file.contentType");
      Assert.True(announcement.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(announcement.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(announcement.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(announcement.Image.File.Name == "image.file.name");
      Assert.True(announcement.Image.File.OriginalName == "image.file.originalName");
      Assert.True(announcement.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(announcement.Image.Height == 1);
      Assert.True(announcement.Image.Width == 2);
      Assert.True(announcement.Language == "language");
      Assert.True(announcement.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(announcement.Name == "name");
      Assert.True(announcement.Price == decimal.One);
      Assert.True(announcement.Tags.Count == 0);
      Assert.True(announcement.Text == "text");
      Assert.True(new Announcement("language", "name", "text", 2, new AnnouncementsCategory("category.language", "category.name") { Id = 3 }, new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 5, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 4 }, "currency", decimal.One) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Announcement.Xml(announcement.Xml()).Equals(announcement));
    }
  }
}