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
    ///   <seealso cref="Art(IDictionary{string, object)"/>
    ///   <seealso cref="Art(string, string, string, Image, ArtsAlbum, string, Person, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var art = new Art();
      Assert.True(art.Id == null);
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

      Assert.Throws<ArgumentNullException>(() => new Art(null));
      art = new Art(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Album", new ArtsAlbum())
        .AddNext("Image", new Image())
        .AddNext("Material", "material")
        .AddNext("Person", new Person())
        .AddNext("Place", "place"));
      Assert.True(art.Id == "id");
      Assert.True(art.Album != null);
      Assert.True(art.AuthorId == "authorId");
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

      Assert.Throws<ArgumentNullException>(() => new Art(null, "language", "name", new Image()));
      Assert.Throws<ArgumentNullException>(() => new Art("id", null, "name", new Image()));
      Assert.Throws<ArgumentNullException>(() => new Art("id", "language", null, new Image()));
      Assert.Throws<ArgumentNullException>(() => new Art("id", "language", "name", null));
      Assert.Throws<ArgumentException>(() => new Art(string.Empty, "language", "name", new Image()));
      Assert.Throws<ArgumentException>(() => new Art("id", string.Empty, "name", new Image()));
      Assert.Throws<ArgumentException>(() => new Art("id", "language", string.Empty, new Image()));
      art = new Art("id", "language", "name", new Image(), new ArtsAlbum(), "text", new Person(), "place", "material");
      Assert.True(art.Id == "id");
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
    ///   <para>Performs testing of <see cref="Art.Equals(object)"/> and <see cref="Art.GetHashCode()"/> methods.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Album", new[] { new ArtsAlbum { Name = "Name" }, new ArtsAlbum { Name = "Name_2" } })
        .AddNext("Person", new[] {new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" } }));
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
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
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
          new XElement("Width", 2)));
      var art = Art.Xml(xml);
      Assert.True(art.Id == "id");
      Assert.True(art.Album == null);
      Assert.True(art.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(art.Image.Id == "image.id");
      Assert.True(art.Image.File.Id == "image.file.id");
      Assert.True(art.Image.File.ContentType == "image.file.contentType");
      Assert.True(art.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(art.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(art.Image.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(art.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(art.Image.File.Name == "image.file.name");
      Assert.True(art.Image.File.OriginalName == "image.file.originalName");
      Assert.True(art.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(art.Image.Height == 1);
      Assert.True(art.Image.Width == 2);
      Assert.True(art.Language == "language");
      Assert.True(art.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(art.Material == null);
      Assert.True(art.Name == "name");
      Assert.True(art.Person == null);
      Assert.True(art.Place == null);
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == null);
      Assert.True(new Art("id", "language", "name", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Art.Xml(art.Xml()).Equals(art));

      xml = new XElement("Art",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("ArtsAlbum",
          new XElement("Id", "album.id"),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Language", "album.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "album.name")),
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
        new XElement("Material", "material"),
        new XElement("Person",
          new XElement("Id", "person.id"),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")),
        new XElement("Place", "place"));
      art = Art.Xml(xml);
      Assert.True(art.Id == "id");
      Assert.True(art.Album.Id == "album.id");
      Assert.True(art.Album.AuthorId == null);
      Assert.True(art.Comments.Count == 0);
      Assert.True(art.Album.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(art.Album.Language == "album.language");
      Assert.True(art.Album.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(art.Album.Name == "album.name");
      Assert.True(art.AuthorId == null);
      Assert.True(art.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(art.Image.Id == "image.id");
      Assert.True(art.Image.File.Id == "image.file.id");
      Assert.True(art.Image.File.ContentType == "image.file.contentType");
      Assert.True(art.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(art.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(art.Image.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(art.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(art.Image.File.Name == "image.file.name");
      Assert.True(art.Image.File.OriginalName == "image.file.originalName");
      Assert.True(art.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(art.Image.Height == 1);
      Assert.True(art.Image.Width == 2);
      Assert.True(art.Language == "language");
      Assert.True(art.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(art.Material == "material");
      Assert.True(art.Name == "name");
      Assert.True(art.Person.Id == "person.id");
      Assert.True(art.Person.NameFirst == "person.nameFirst");
      Assert.True(art.Person.NameLast == "person.nameLast");
      Assert.True(art.Place == "place");
      Assert.True(art.Tags.Count == 0);
      Assert.True(art.Text == "text");
      Assert.True(new Art("id", "language", "name", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2), new ArtsAlbum("album.id", "album.language", "album.name") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, "text", new Person("person.id", "person.nameFirst", "person.nameLast"), "place", "material") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Art.Xml(art.Xml()).Equals(art));
    }
  }
}