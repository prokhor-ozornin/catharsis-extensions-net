using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Idea"/>.</para>
  /// </summary>
  public sealed class IdeaTests : EntityUnitTests<Idea>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Idea.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new IdeasCategory();
      Assert.True(ReferenceEquals(new Idea { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Idea.Description"/> property.</para>
    /// </summary>
    [Fact]
    public void Description_Property()
    {
      Assert.True(new Idea { Description = "description" }.Description == "description");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Idea.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Idea { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Idea.InspiredBy"/> property.</para>
    /// </summary>
    [Fact]
    public void InspiredBy_Property()
    {
      var inspiredBy = new Idea();
      Assert.True(ReferenceEquals(new Idea { InspiredBy = inspiredBy }.InspiredBy, inspiredBy));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Idea()"/>
    ///   <seealso cref="Idea(IDictionary{string, object})"/>
    ///   <seealso cref="Idea(string, string, string, string, string, IdeasCategory, Idea, string, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var idea = new Idea();
      Assert.True(idea.Id == null);
      Assert.True(idea.AuthorId == null);
      Assert.True(idea.DateCreated <= DateTime.UtcNow);
      Assert.True(idea.Language == null);
      Assert.True(idea.LastUpdated <= DateTime.UtcNow);
      Assert.True(idea.Name == null);
      Assert.True(idea.Text == null);
      Assert.True(idea.Category == null);
      Assert.True(idea.Description == null);
      Assert.True(idea.Image == null);
      Assert.True(idea.InspiredBy == null);

      Assert.Throws<ArgumentNullException>(() => new Idea(null));
      idea = new Idea(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new IdeasCategory())
        .AddNext("Description", "description")
        .AddNext("Image", new Image())
        .AddNext("InspiredBy", new Idea()));
      Assert.True(idea.Id == "id");
      Assert.True(idea.AuthorId == "authorId");
      Assert.True(idea.DateCreated <= DateTime.UtcNow);
      Assert.True(idea.Language == "language");
      Assert.True(idea.LastUpdated <= DateTime.UtcNow);
      Assert.True(idea.Name == "name");
      Assert.True(idea.Text == "text");
      Assert.True(idea.Category != null);
      Assert.True(idea.Description == "description");
      Assert.True(idea.Image != null);
      Assert.True(idea.InspiredBy != null);

      Assert.Throws<ArgumentNullException>(() => new Idea(null, "language", "name", "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Idea("id", null, "name", "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Idea("id", "language", null, "authorId", "text"));
      Assert.Throws<ArgumentNullException>(() => new Idea("id", "language", "name", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Idea("id", "language", "name", "authorId", null));
      Assert.Throws<ArgumentException>(() => new Idea(string.Empty, "language", "name", "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", string.Empty, "name", "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", string.Empty, "authorId", "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", "name", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Idea("id", "language", "name", "authorId", string.Empty));
      idea = new Idea("id", "language", "name", "authorId", "text", new IdeasCategory(), new Idea(), "description", new Image());
      Assert.True(idea.Id == "id");
      Assert.True(idea.AuthorId == "authorId");
      Assert.True(idea.DateCreated <= DateTime.UtcNow);
      Assert.True(idea.Language == "language");
      Assert.True(idea.LastUpdated <= DateTime.UtcNow);
      Assert.True(idea.Name == "name");
      Assert.True(idea.Text == "text");
      Assert.True(idea.Category != null);
      Assert.True(idea.Description == "description");
      Assert.True(idea.Image != null);
      Assert.True(idea.InspiredBy != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Idea"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new IdeasCategory { Name = "Name" }, new IdeasCategory { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Idea.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Idea.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Idea.Xml(null));

      var xml = new XElement("Idea",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var idea = Idea.Xml(xml);
      Assert.True(idea.Id == "id");
      Assert.True(idea.AuthorId == "authorId");
      Assert.True(idea.Category == null);
      Assert.True(idea.Comments.Count == 0);
      Assert.True(idea.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(idea.Description == null);
      Assert.True(idea.Image == null);
      Assert.True(idea.InspiredBy == null);
      Assert.True(idea.Language == "language");
      Assert.True(idea.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(idea.Name == "name");
      Assert.True(idea.Tags.Count == 0);
      Assert.True(idea.Text == "text");
      Assert.True(new Idea("id", "language", "name", "authorId", "text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Idea.Xml(idea.Xml()).Equals(idea));

      xml = new XElement("Idea",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("IdeasCategory",
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
          new XElement("Idea",
            new XElement("Id", "inspiredBy.id"),
            new XElement("AuthorId", "inspiredBy.authorId"),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("Language", "inspiredBy.language"),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "inspiredBy.name"),
            new XElement("Text", "inspiredBy.text"))));
      idea = Idea.Xml(xml);
      Assert.True(idea.Id == "id");
      Assert.True(idea.AuthorId == "authorId");
      Assert.True(idea.Category.Id == "category.id");
      Assert.True(idea.Category.Language == "category.language");
      Assert.True(idea.Category.Name == "category.name");
      Assert.True(idea.Comments.Count == 0);
      Assert.True(idea.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(idea.Description == "description");
      Assert.True(idea.Image.Id == "image.id");
      Assert.True(idea.Image.File.Id == "image.file.id");
      Assert.True(idea.Image.File.ContentType == "image.file.contentType");
      Assert.True(idea.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(idea.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(idea.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(idea.Image.File.Name == "image.file.name");
      Assert.True(idea.Image.File.OriginalName == "image.file.originalName");
      Assert.True(idea.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(idea.Image.Height == 1);
      Assert.True(idea.Image.Width == 2);
      Assert.True(idea.InspiredBy.Id == "inspiredBy.id");
      Assert.True(idea.InspiredBy.AuthorId == "inspiredBy.authorId");
      Assert.True(idea.InspiredBy.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(idea.InspiredBy.Language == "inspiredBy.language");
      Assert.True(idea.InspiredBy.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(idea.InspiredBy.Name == "inspiredBy.name");
      Assert.True(idea.InspiredBy.Text == "inspiredBy.text");
      Assert.True(idea.Language == "language");
      Assert.True(idea.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(idea.Name == "name");
      Assert.True(idea.Tags.Count == 0);
      Assert.True(idea.Text == "text");
      /*Assert.True(new Idea("id", "language", "name", "authorId", "text", new IdeasCategory("category.id", "category.language", "category.name"), new Idea("inspiredBy.id", "inspiredBy.language", "inspiredBy.name", "inspiredBy.authorId", "inspiredBy.text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, "description", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Idea.Xml(idea.Xml()).Equals(idea));*/
    }
  }
}