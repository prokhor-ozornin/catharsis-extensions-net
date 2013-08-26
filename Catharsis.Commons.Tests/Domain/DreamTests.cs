using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Dream"/>.</para>
  /// </summary>
  public sealed class DreamTests : EntityUnitTests<Dream>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Dream.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new DreamsCategory();
      Assert.True(ReferenceEquals(new Dream { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Dream.Description"/> property.</para>
    /// </summary>
    [Fact]
    public void Description_Property()
    {
      Assert.True(new Dream { Description = "description" }.Description == "description");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Dream.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Dream { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Dream.InspiredBy"/> property.</para>
    /// </summary>
    [Fact]
    public void InspiredBy_Property()
    {
      var inspiredBy = new Dream();
      Assert.True(ReferenceEquals(new Dream { InspiredBy = inspiredBy }.InspiredBy, inspiredBy));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Dream()"/>
    ///   <seealso cref="Dream(IDictionary{string, object})"/>
    ///   <seealso cref="Dream(string, string, string, string, string, DreamsCategory, Dream, string, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var dream = new Dream();
      Assert.True(dream.Id == null);
      Assert.True(dream.AuthorId == null);
      Assert.True(dream.DateCreated <= DateTime.UtcNow);
      Assert.True(dream.Language == null);
      Assert.True(dream.LastUpdated <= DateTime.UtcNow);
      Assert.True(dream.Name == null);
      Assert.True(dream.Text == null);
      Assert.True(dream.Category == null);
      Assert.True(dream.Description == null);
      Assert.True(dream.Image == null);
      Assert.True(dream.InspiredBy == null);

      Assert.Throws<ArgumentNullException>(() => new Dream(null));
      dream = new Dream(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new DreamsCategory())
        .AddNext("Description", "description")
        .AddNext("Image", new Image())
        .AddNext("InspiredBy", new Dream()));
      Assert.True(dream.Id == "id");
      Assert.True(dream.AuthorId == "authorId");
      Assert.True(dream.DateCreated <= DateTime.UtcNow);
      Assert.True(dream.Language == "language");
      Assert.True(dream.LastUpdated <= DateTime.UtcNow);
      Assert.True(dream.Name == "name");
      Assert.True(dream.Text == "text");
      Assert.True(dream.Category != null);
      Assert.True(dream.Description == "description");
      Assert.True(dream.Image != null);
      Assert.True(dream.InspiredBy != null);

      Assert.Throws<ArgumentNullException>(() => new Dream(null, "language", "name", "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Dream("id", null, "name", "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Dream("id", "language", null, "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Dream("id", "language", "name", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Dream("id", "language", "name", "authorId", null));
      Assert.Throws<ArgumentException>(() => new Idea(string.Empty, "language", "name", "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", string.Empty, "name", "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", string.Empty, "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", "name", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", "name", "authorId", string.Empty));
      dream = new Dream("id", "language", "name", "authorId", "text", new DreamsCategory(), new Dream(), "description", new Image());
      Assert.True(dream.Id == "id");
      Assert.True(dream.AuthorId == "authorId");
      Assert.True(dream.DateCreated <= DateTime.UtcNow);
      Assert.True(dream.Language == "language");
      Assert.True(dream.LastUpdated <= DateTime.UtcNow);
      Assert.True(dream.Name == "name");
      Assert.True(dream.Text == "text");
      Assert.True(dream.Category != null);
      Assert.True(dream.Description == "description");
      Assert.True(dream.Image != null);
      Assert.True(dream.InspiredBy != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Dream"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new DreamsCategory { Name = "Name" }, new DreamsCategory { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Dream.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Dream.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Dream.Xml(null));

      var xml = new XElement("Dream",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var dream = Dream.Xml(xml);
      Assert.True(dream.Id == "id");
      Assert.True(dream.AuthorId == "authorId");
      Assert.True(dream.Category == null);
      Assert.True(dream.Comments.Count == 0);
      Assert.True(dream.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(dream.Description == null);
      Assert.True(dream.Image == null);
      Assert.True(dream.InspiredBy == null);
      Assert.True(dream.Language == "language");
      Assert.True(dream.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(dream.Name == "name");
      Assert.True(dream.Tags.Count == 0);
      Assert.True(dream.Text == "text");
      Assert.True(new Dream("id", "language", "name", "authorId", "text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Dream.Xml(dream.Xml()).Equals(dream));

      xml = new XElement("Dream",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("DreamsCategory",
          new XElement("Id", "category.id"),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
       new XElement("Description", "description"),
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
        new XElement("InspiredBy",
          new XElement("Dream",
            new XElement("Id", "inspiredBy.id"),
            new XElement("AuthorId", "inspiredBy.authorId"),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("Language", "inspiredBy.language"),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "inspiredBy.name"),
            new XElement("Text", "inspiredBy.text"))));
      dream = Dream.Xml(xml);
      Assert.True(dream.Id == "id");
      Assert.True(dream.AuthorId == "authorId");
      Assert.True(dream.Category.Id == "category.id");
      Assert.True(dream.Category.Language == "category.language");
      Assert.True(dream.Category.Name == "category.name");
      Assert.True(dream.Comments.Count == 0);
      Assert.True(dream.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(dream.Description == "description");
      Assert.True(dream.Image.Id == "image.id");
      Assert.True(dream.Image.File.Id == "image.file.id");
      Assert.True(dream.Image.File.ContentType == "image.file.contentType");
      Assert.True(dream.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(dream.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(dream.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(dream.Image.File.Name == "image.file.name");
      Assert.True(dream.Image.File.OriginalName == "image.file.originalName");
      Assert.True(dream.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(dream.Image.Height == 1);
      Assert.True(dream.Image.Width == 2);
      Assert.True(dream.InspiredBy.Id == "inspiredBy.id");
      Assert.True(dream.InspiredBy.AuthorId == "inspiredBy.authorId");
      Assert.True(dream.InspiredBy.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(dream.InspiredBy.Language == "inspiredBy.language");
      Assert.True(dream.InspiredBy.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(dream.InspiredBy.Name == "inspiredBy.name");
      Assert.True(dream.InspiredBy.Text == "inspiredBy.text");
      Assert.True(dream.Language == "language");
      Assert.True(dream.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(dream.Name == "name");
      Assert.True(dream.Tags.Count == 0);
      Assert.True(dream.Text == "text");
      Assert.True(new Dream("id", "language", "name", "authorId", "text", new DreamsCategory("category.id", "category.language", "category.name"), new Dream("inspiredBy.id", "inspiredBy.language", "inspiredBy.name", "inspiredBy.authorId", "inspiredBy.text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, "description", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Dream.Xml(dream.Xml()).Equals(dream));
    }
  }
}