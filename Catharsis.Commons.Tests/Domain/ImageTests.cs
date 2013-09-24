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
    ///   <seealso cref="Image(File, short, short, ImagesCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var image = new Image();
      Assert.True(image.Id == 0);
      Assert.True(image.Category == null);
      Assert.True(image.File == null);
      Assert.True(image.Height == 0);
      Assert.True(image.Width == 0);

      Assert.Throws<ArgumentNullException>(() => new Image(null));
      image = new Image(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("Category", new ImagesCategory())
        .AddNext("File", new File())
        .AddNext("Height", (short) 1)
        .AddNext("Width", (short) 2));
      Assert.True(image.Id == 1);
      Assert.True(image.Category != null);
      Assert.True(image.File != null);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);

      Assert.Throws<ArgumentNullException>(() => new Image(null, 1, 2));
      image = new Image(new File(), 1, 2, new ImagesCategory());
      Assert.True(image.Id == 0);
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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Image.Equals(Image)"/></description></item>
    ///     <item><description><see cref="Image.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new ImagesCategory { Name = "Name" }, new ImagesCategory { Name = "Name_2" });
      this.TestEquality("File", new File { Name = "Name" }, new File { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new ImagesCategory { Name = "Name" }, new ImagesCategory { Name = "Name_2" });
      this.TestHashCode("File", new File { Name = "Name" }, new File { Name = "Name_2" });
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
        new XElement("Id", 1),
        new XElement("File",
          new XElement("Id", 2),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().Length)),
        new XElement("Height", 1),
        new XElement("Width", 2));
      var image = Image.Xml(xml);
      Assert.True(image.Id == 1);
      Assert.True(image.Category == null);
      Assert.True(image.File.Id == 2);
      Assert.True(image.File.ContentType == "file.contentType");
      Assert.True(image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(image.File.Name == "file.name");
      Assert.True(image.File.OriginalName == "file.originalName");
      Assert.True(image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
      Assert.True(new Image(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 2, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Image.Xml(image.Xml()).Equals(image));

      xml = new XElement("Image",
        new XElement("Id", 1),
        new XElement("ImagesCategory",
          new XElement("Id", 2),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("File",
          new XElement("Id", 3),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().Length)),
        new XElement("Height", 1),
        new XElement("Width", 2));
      image = Image.Xml(xml);
      Assert.True(image.Id == 1);
      Assert.True(image.Category.Id == 2);
      Assert.True(image.Category.Language == "category.language");
      Assert.True(image.Category.Name == "category.name");
      Assert.True(image.File.Id == 3);
      Assert.True(image.File.ContentType == "file.contentType");
      Assert.True(image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(image.File.Name == "file.name");
      Assert.True(image.File.OriginalName == "file.originalName");
      Assert.True(image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
      Assert.True(new Image(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, new ImagesCategory("category.language", "category.name") { Id = 2 }) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Image.Xml(image.Xml()).Equals(image));
    }
  }
}