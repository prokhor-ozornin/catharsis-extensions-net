using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SongsAlbum"/>.</para>
  /// </summary>
  public sealed class SongsAlbumTests : EntityUnitTests<SongsAlbum>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbum.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new SongsAlbum { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbum.PublishedOn"/> property.</para>
    /// </summary>
    [Fact]
    public void PublishedOn_Property()
    {
      Assert.True(new SongsAlbum { PublishedOn = DateTime.MinValue }.PublishedOn == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="SongsAlbum()"/>
    ///   <seealso cref="SongsAlbum(string, string, string, Image, DateTime?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var album = new SongsAlbum();
      Assert.True(album.Id == 0);
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated <= DateTime.UtcNow);
      Assert.True(album.Image == null);
      Assert.True(album.Language == null);
      Assert.True(album.LastUpdated <= DateTime.UtcNow);
      Assert.True(album.Name == null);
      Assert.False(album.PublishedOn.HasValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == null);

      Assert.Throws<ArgumentNullException>(() => new SongsAlbum(null, "name"));
      Assert.Throws<ArgumentNullException>(() => new SongsAlbum("language",null));
      Assert.Throws<ArgumentException>(() => new SongsAlbum(string.Empty, "name"));
      Assert.Throws<ArgumentException>(() => new SongsAlbum("language", string.Empty));
      album = new SongsAlbum("language", "name", "text", new Image(), DateTime.MinValue);
      Assert.True(album.Id == 0);
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated <= DateTime.UtcNow);
      Assert.True(album.Image != null);
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated <= DateTime.UtcNow);
      Assert.True(album.Name == "name");
      Assert.True(album.PublishedOn == DateTime.MinValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbum.CompareTo(SongsAlbum)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new SongsAlbum { Name = "Name" }.CompareTo(new SongsAlbum { Name = "Name" }) == 0);
      Assert.True(new SongsAlbum { Name = "First" }.CompareTo(new SongsAlbum { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="SongsAlbum.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="SongsAlbum.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => SongsAlbum.Xml(null));

      var xml = new XElement("SongsAlbum",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"));
      var album = SongsAlbum.Xml(xml);
      Assert.True(album.Id == 1);
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Image == null);
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(album.Name == "name");
      Assert.False(album.PublishedOn.HasValue);
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == null);
      Assert.True(new SongsAlbum("language", "name") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(SongsAlbum.Xml(album.Xml()).Equals(album));

      xml = new XElement("SongsAlbum",
        new XElement("Id", 1),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
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
          new XElement("Width", 2)),
        new XElement("PublishedOn", DateTime.MinValue.ToRfc1123()));
      album = SongsAlbum.Xml(xml);
      Assert.True(album.Id == 1);
      Assert.True(album.AuthorId == null);
      Assert.True(album.Comments.Count == 0);
      Assert.True(album.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Image.Id == 2);
      Assert.True(album.Image.File.Id == 3);
      Assert.True(album.Image.File.ContentType == "image.file.contentType");
      Assert.True(album.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(album.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(album.Image.File.Name == "image.file.name");
      Assert.True(album.Image.File.OriginalName == "image.file.originalName");
      Assert.True(album.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(album.Image.Height == 1);
      Assert.True(album.Image.Width == 2);
      Assert.True(album.Language == "language");
      Assert.True(album.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(album.Name == "name");
      Assert.True(album.PublishedOn.GetValueOrDefault().ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(album.Tags.Count == 0);
      Assert.True(album.Text == "text");
      Assert.True(new SongsAlbum("language", "name", "text", new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 2 }, DateTime.MinValue) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(SongsAlbum.Xml(album.Xml()).Equals(album));
    }
  }
}