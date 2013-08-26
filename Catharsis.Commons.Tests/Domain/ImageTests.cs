using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Image"/>.</para>
  /// </summary>
  public sealed class ImageTests : EntityUnitTests<Image>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Image.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new ImagesCategory();
      Assert.True(ReferenceEquals(new Image { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.File"/> property.</para>
    /// </summary>
    [Fact]
    public void File_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Image { File = null });
      var file = new File();
      Assert.True(ReferenceEquals(new Image { File = file }.File, file));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.Height"/> property.</para>
    /// </summary>
    [Fact]
    public void Height_Property()
    {
      Assert.True(new Image { Height = 1 }.Height == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.Width"/> property.</para>
    /// </summary>
    [Fact]
    public void Width_Property()
    {
      Assert.True(new Image { Width = 1 }.Width == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Image()"/>
    ///   <seealso cref="Image(IDictionary{string, object})"/>
    ///   <seealso cref="Image(string, File, short, short, ImagesCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var image = new Image();
      Assert.True(image.Id == null);
      Assert.True(image.Category == null);
      Assert.True(image.File == null);
      Assert.True(image.Height == 0);
      Assert.True(image.Width == 0);

      Assert.Throws<ArgumentNullException>(() => new Image(null));
      image = new Image(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Category", new ImagesCategory())
        .AddNext("File", new File())
        .AddNext("Height", (short) 1)
        .AddNext("Width", (short) 2));
      Assert.True(image.Id == "id");
      Assert.True(image.Category != null);
      Assert.True(image.File != null);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);

      Assert.Throws<ArgumentNullException>(() => new Image(null, new File(), 1, 2));
      Assert.Throws<ArgumentNullException>(() => new Image("id", null, 1, 2));
      Assert.Throws<ArgumentException>(() => new Image(string.Empty, new File(), 1, 2));
      image = new Image("id", new File(), 1, 2, new ImagesCategory());
      Assert.True(image.Id == "id");
      Assert.True(image.Category != null);
      Assert.True(image.File != null);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Image { File = new File { Name = "name" } }.ToString() == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Image"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new ImagesCategory { Name = "Name" }, new ImagesCategory { Name = "Name_2" } })
        .AddNext("File", new[] { new File { Name = "Name" }, new File { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.CompareTo(Image)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }) == 0);
      Assert.True(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Image { File = new File { DateCreated = new DateTime(2000, 1, 2) } }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Image.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Image.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Image.Xml(null));

      var xml = new XElement("Image",
        new XElement("Id", "id"),
        new XElement("File",
          new XElement("Id", "file.id"),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
        new XElement("Height", 1),
        new XElement("Width", 2));
      var image = Image.Xml(xml);
      Assert.True(image.Id == "id");
      Assert.True(image.Category == null);
      Assert.True(image.File.Id == "file.id");
      Assert.True(image.File.ContentType == "file.contentType");
      Assert.True(image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(image.File.Name == "file.name");
      Assert.True(image.File.OriginalName == "file.originalName");
      Assert.True(image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
      Assert.True(new Image("id", new File("file.id", "file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2).Xml().ToString() == xml.ToString());
      Assert.True(Image.Xml(image.Xml()).Equals(image));

      xml = new XElement("Image",
        new XElement("Id", "id"),
        new XElement("ImagesCategory",
          new XElement("Id", "category.id"),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("File",
          new XElement("Id", "file.id"),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
        new XElement("Height", 1),
        new XElement("Width", 2));
      image = Image.Xml(xml);
      Assert.True(image.Id == "id");
      Assert.True(image.Category.Id == "category.id");
      Assert.True(image.Category.Language == "category.language");
      Assert.True(image.Category.Name == "category.name");
      Assert.True(image.File.Id == "file.id");
      Assert.True(image.File.ContentType == "file.contentType");
      Assert.True(image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(image.File.Name == "file.name");
      Assert.True(image.File.OriginalName == "file.originalName");
      Assert.True(image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
      Assert.True(new Image("id", new File("file.id", "file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, new ImagesCategory("category.id", "category.language", "category.name")).Xml().ToString() == xml.ToString());
      Assert.True(Image.Xml(image.Xml()).Equals(image));
    }
  }
}