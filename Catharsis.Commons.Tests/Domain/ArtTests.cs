using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Art"/>.</para>
  /// </summary>
  public sealed class ArtTests : EntityUnitTests<Art>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Art.Album"/> property.</para>
    /// </summary>
    [Fact]
    public void Album_Property()
    {
      var album = new ArtsAlbum();
      Assert.True(ReferenceEquals(new Art { Album = album }.Album, album));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Art { Image = null });
      var image = new Image();
      Assert.True(ReferenceEquals(new Art { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.Material"/> property.</para>
    /// </summary>
    [Fact]
    public void Material_Property()
    {
      Assert.True(new Art { Material = "material" }.Material == "material");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.Person"/> property.</para>
    /// </summary>
    [Fact]
    public void Person_Property()
    {
      var person = new Person();
      Assert.True(ReferenceEquals(new Art { Person = person }.Person, person));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.Place"/> property.</para>
    /// </summary>
    [Fact]
    public void Place_Property()
    {
      Assert.True(new Art { Place = "place" }.Place == "place");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Art()"/>
    ///   <seealso cref="Art(string, string, Image, ArtsAlbum, string, Person, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var art = new Art();
      Assert.True(art.Id == 0);
      Assert.True(art.Album == null);
      Assert.True(art.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.DateCreated <= DateTime.UtcNow);
      Assert.True(art.Image == null);
      Assert.True(art.Language == null);
      Assert.True(art.LastUpdated <= DateTime.UtcNow);
      Assert.True(art.Material == null);
      Assert.True(art.Name == null);
      Assert.True(art.Person == null);
      Assert.True(art.Place == null);
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Art(null, "name", new Image()));
      Assert.Throws<ArgumentNullException>(() => new Art("language", null, new Image()));
      Assert.Throws<ArgumentNullException>(() => new Art("language", "name", null));
      Assert.Throws<ArgumentException>(() => new Art(string.Empty, "name", new Image()));
      Assert.Throws<ArgumentException>(() => new Art("language", string.Empty, new Image()));
      art = new Art("language", "name", new Image(), new ArtsAlbum(), "text", new Person(), "place", "material");
      Assert.True(art.Id == 0);
      Assert.True(art.Album != null);
      Assert.True(art.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.DateCreated <= DateTime.UtcNow);
      Assert.True(art.Image != null);
      Assert.True(art.Language == "language");
      Assert.True(art.LastUpdated <= DateTime.UtcNow);
      Assert.True(art.Material == "material");
      Assert.True(art.Name == "name");
      Assert.True(art.Person != null);
      Assert.True(art.Place == "place");
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Art.Equals(Art)"/></description></item>
    ///     <item><description><see cref="Art.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Album", new ArtsAlbum { Name = "Name" }, new ArtsAlbum { Name = "Name_2" });
      this.TestEquality("Person", new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Album", new ArtsAlbum { Name = "Name" }, new ArtsAlbum { Name = "Name_2" });
      this.TestHashCode("Person", new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Art.CompareTo(Art)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Art { Name = "Name" }.CompareTo(new Art { Name = "Name" }) == 0);
      Assert.True(new Art { Name = "First" }.CompareTo(new Art { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Art.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Art.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Art.Xml(null));

      var xml = new XElement("Art",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Image",
          new XElement("Id", 2),
          new XElement("File",
            new XElement("Id", 3),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().Length)),
          new XElement("Height", 1),
          new XElement("Width", 2)));
      var art = Art.Xml(xml);
      Assert.True(art.Id == 1);
      Assert.True(art.Album == null);
      Assert.True(art.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(art.Image.Id == 2);
      Assert.True(art.Image.File.Id == 3);
      Assert.True(art.Image.File.ContentType == "image.file.contentType");
      Assert.True(art.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(art.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(art.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(art.Image.File.Name == "image.file.name");
      Assert.True(art.Image.File.OriginalName == "image.file.originalName");
      Assert.True(art.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(art.Image.Height == 1);
      Assert.True(art.Image.Width == 2);
      Assert.True(art.Language == "language");
      Assert.True(art.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(art.Material == null);
      Assert.True(art.Name == "name");
      Assert.True(art.Person == null);
      Assert.True(art.Place == null);
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == null);
      Assert.True(new Art("language", "name", new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 2 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Art.Xml(art.Xml()).Equals(art));

      xml = new XElement("Art",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("ArtsAlbum",
          new XElement("Id", 2),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("Language", "album.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "album.name")),
        new XElement("Image",
          new XElement("Id", 3),
          new XElement("File",
            new XElement("Id", 4),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().Length)),
          new XElement("Height", 1),
          new XElement("Width", 2)),
        new XElement("Material", "material"),
        new XElement("Person",
          new XElement("Id", 5),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")),
        new XElement("Place", "place"));
      art = Art.Xml(xml);
      Assert.True(art.Id == 1);
      Assert.True(art.Album.Id == 2);
      Assert.True(art.Album.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.Album.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(art.Album.Language == "album.language");
      Assert.True(art.Album.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(art.Album.Name == "album.name");
      Assert.True(art.AuthorId == null);
      Assert.True(art.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(art.Image.Id == 3);
      Assert.True(art.Image.File.Id == 4);
      Assert.True(art.Image.File.ContentType == "image.file.contentType");
      Assert.True(art.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(art.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(art.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(art.Image.File.Name == "image.file.name");
      Assert.True(art.Image.File.OriginalName == "image.file.originalName");
      Assert.True(art.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(art.Image.Height == 1);
      Assert.True(art.Image.Width == 2);
      Assert.True(art.Language == "language");
      Assert.True(art.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(art.Material == "material");
      Assert.True(art.Name == "name");
      Assert.True(art.Person.Id == 5);
      Assert.True(art.Person.NameFirst == "person.nameFirst");
      Assert.True(art.Person.NameLast == "person.nameLast");
      Assert.True(art.Place == "place");
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == "text");
      Assert.True(new Art("language", "name", new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 4, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 3 }, new ArtsAlbum("album.language", "album.name") { Id = 2, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, "text", new Person("person.nameFirst", "person.nameLast") { Id = 5 }, "place", "material") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Art.Xml(art.Xml()).Equals(art));
    }
  }
}