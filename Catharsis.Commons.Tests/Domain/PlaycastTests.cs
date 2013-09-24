using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Playcast"/>.</para>
  /// </summary>
  public sealed class PlaycastTests : EntityUnitTests<Playcast>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Playcast.Audio"/> property.</para>
    /// </summary>
    [Fact]
    public void Audio_Property()
    {
      var audio = new Audio();
      Assert.True(ReferenceEquals(new Playcast { Audio = audio }.Audio, audio));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Playcast.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new PlaycastsCategory();
      Assert.True(ReferenceEquals(new Playcast { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Playcast.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Playcast { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Playcast()"/>
    ///   <seealso cref="Playcast(IDictionary{string, object})"/>
    ///   <seealso cref="Playcast(string, string, string, string, PlaycastsCategory, Audio, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var playcast = new Playcast();
      Assert.True(playcast.Id == 0);
      Assert.True(playcast.AuthorId == null);
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == null);
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == null);
      Assert.True(playcast.Text == null);
      Assert.True(playcast.Audio == null);
      Assert.True(playcast.Category == null);
      Assert.True(playcast.Image == null);

      Assert.Throws<ArgumentNullException>(() => new Playcast(null));
      playcast = new Playcast(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Audio", new Audio())
        .AddNext("Category", new PlaycastsCategory())
        .AddNext("Image", new Image()));
      Assert.True(playcast.Id == 1);
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Text == "text");
      Assert.True(playcast.Audio != null);
      Assert.True(playcast.Category != null);
      Assert.True(playcast.Image != null);
      
      Assert.Throws<ArgumentNullException>(() => new Playcast(null, "language", "name", "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Playcast("authorId", null, "name", "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Playcast("authorId", "language", null, "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentNullException>(() => new Playcast("authorId", "language", "name", null, new PlaycastsCategory()));
      Assert.Throws<ArgumentException>(() => new Playcast(string.Empty, "language", "name", "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentException>(() => new Playcast("authorId", string.Empty, "name", "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentException>(() => new Playcast("authorId", "language", string.Empty, "text", new PlaycastsCategory()));
      Assert.Throws<ArgumentException>(() => new Playcast("authorId", "language", "name", string.Empty, new PlaycastsCategory()));
      playcast = new Playcast("authorId", "language", "name", "text", new PlaycastsCategory(), new Audio(), new Image());
      Assert.True(playcast.Id == 0);
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Text == "text");
      Assert.True(playcast.Audio != null);
      Assert.True(playcast.Category != null);
      Assert.True(playcast.Image != null);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Playcast.Equals(Playcast)"/></description></item>
    ///     <item><description><see cref="Playcast.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new PlaycastsCategory { Name = "Name" }, new PlaycastsCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Playcast.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new PlaycastsCategory { Name = "Name" }, new PlaycastsCategory { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Playcast.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Playcast.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Playcast.Xml(null));

      var xml = new XElement("Playcast",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var playcast = Playcast.Xml(xml);
      Assert.True(playcast.Id == 1);
      Assert.True(playcast.Audio == null);
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.Category == null);
      Assert.True(playcast.Comments.Count == 0);
      Assert.True(playcast.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(playcast.Image == null);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Tags.Count == 0);
      Assert.True(playcast.Text == "text");
      Assert.True(new Playcast("authorId", "language", "name", "text") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Playcast.Xml(playcast.Xml()).Equals(playcast));

      xml = new XElement("Playcast",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Audio",
          new XElement("Id", 2),
          new XElement("Bitrate", 1),
          new XElement("Duration", 2),
            new XElement("File",
              new XElement("Id", 3),
              new XElement("ContentType", "audio.file.contentType"),
              new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
              new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
              new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
              new XElement("Name", "audio.file.name"),
              new XElement("OriginalName", "audio.file.originalName"),
              new XElement("Size", Guid.Empty.ToByteArray().Length))),
        new XElement("PlaycastsCategory",
          new XElement("Id", 4),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Image",
          new XElement("Id", 5),
            new XElement("File",
              new XElement("Id", 6),
              new XElement("ContentType", "image.file.contentType"),
              new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
              new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
              new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
              new XElement("Name", "image.file.name"),
              new XElement("OriginalName", "image.file.originalName"),
              new XElement("Size", Guid.Empty.ToByteArray().Length)),
            new XElement("Height", 1),
            new XElement("Width", 2)));
      playcast = Playcast.Xml(xml);
      Assert.True(playcast.Id == 1);
      Assert.True(playcast.Audio.Id == 2);
      Assert.True(playcast.Audio.Bitrate == 1);
      Assert.True(playcast.Audio.Duration == 2);
      Assert.True(playcast.Audio.File.Id == 3);
      Assert.True(playcast.Audio.File.ContentType == "audio.file.contentType");
      Assert.True(playcast.Audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(playcast.Audio.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(playcast.Audio.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(playcast.Audio.File.Name == "audio.file.name");
      Assert.True(playcast.Audio.File.OriginalName == "audio.file.originalName");
      Assert.True(playcast.Audio.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.Category.Id == 4);
      Assert.True(playcast.Category.Language == "category.language");
      Assert.True(playcast.Category.Name == "category.name");
      Assert.True(playcast.Comments.Count == 0);
      Assert.True(playcast.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(playcast.Image.Id == 5);
      Assert.True(playcast.Image.Category == null);
      Assert.True(playcast.Image.File.Id == 6);
      Assert.True(playcast.Image.File.ContentType == "image.file.contentType");
      Assert.True(playcast.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(playcast.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(playcast.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(playcast.Image.File.Name == "image.file.name");
      Assert.True(playcast.Image.File.OriginalName == "image.file.originalName");
      Assert.True(playcast.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(playcast.Image.Height == 1);
      Assert.True(playcast.Image.Width == 2);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Tags.Count == 0);
      Assert.True(playcast.Text == "text");
      Assert.True(new Playcast("authorId", "language", "name", "text", new PlaycastsCategory("category.language", "category.name") { Id = 4 }, new Audio(new File("audio.file.contentType", "audio.file.name", "audio.file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 2 }, new Image(new File("image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { Id = 6, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 5 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Playcast.Xml(playcast.Xml()).Equals(playcast));
    }
  }
}