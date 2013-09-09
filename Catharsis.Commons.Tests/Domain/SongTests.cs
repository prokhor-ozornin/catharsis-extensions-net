using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Song"/>.</para>
  /// </summary>
  public sealed class SongTests : EntityUnitTests<Song>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Song.Album"/> property.</para>
    /// </summary>
    [Fact]
    public void Album_Property()
    {
      var album = new SongsAlbum();
      Assert.True(ReferenceEquals(new Song { Album = album }.Album, album));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Song.Audio"/> property.</para>
    /// </summary>
    [Fact]
    public void Audio_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Song { Audio = null });
      var audio = new Audio();
      Assert.True(ReferenceEquals(new Song { Audio = audio }.Audio, audio));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Song()"/>
    ///   <seealso cref="Song(IDictionary{string, object})"/>
    ///   <seealso cref="Song(string, string, string, string, Audio, SongsAlbum)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var song = new Song();
      Assert.True(song.Id == null);
      Assert.True(song.Album == null);
      Assert.True(song.Audio == null);
      Assert.True(song.AuthorId == null);
      Assert.True(song.Comments.Count == 0);
      Assert.True(song.DateCreated <= DateTime.UtcNow);
      Assert.True(song.Language == null);
      Assert.True(song.LastUpdated <= DateTime.UtcNow);
      Assert.True(song.Name == null);
      Assert.True(song.Tags.Count == 0);
      Assert.True(song.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Song(null));
      song = new Song(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Album", new SongsAlbum())
        .AddNext("Audio", new Audio())
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(song.Id == "id");
      Assert.True(song.Album != null);
      Assert.True(song.Audio != null);
      Assert.True(song.AuthorId == "authorId");
      Assert.True(song.Comments.Count == 0);
      Assert.True(song.DateCreated <= DateTime.UtcNow);
      Assert.True(song.Language == "language");
      Assert.True(song.LastUpdated <= DateTime.UtcNow);
      Assert.True(song.Name == "name");
      Assert.True(song.Tags.Count == 0);
      Assert.True(song.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Song(null, "language", "name","text", new Audio()));
      Assert.Throws<ArgumentNullException>(() => new Song("id", null, "name", "text", new Audio()));
      Assert.Throws<ArgumentNullException>(() => new Song("id", "language", null, "text", new Audio()));
      Assert.Throws<ArgumentNullException>(() => new Song("id", "language", "name", null, new Audio()));
      Assert.Throws<ArgumentNullException>(() => new Song("id", "language", "name", "text", null));
      Assert.Throws<ArgumentException>(() => new Song(string.Empty, "language", "name", "text", new Audio()));
      Assert.Throws<ArgumentException>(() => new Song("id", string.Empty, "name", "text", new Audio()));
      Assert.Throws<ArgumentException>(() => new Song("id", "language", string.Empty, "text", new Audio()));
      Assert.Throws<ArgumentException>(() => new Song("id", "language", "name", string.Empty, new Audio()));
      song = new Song("id", "language", "name", "text", new Audio(), new SongsAlbum());
      Assert.True(song.Id == "id");
      Assert.True(song.Album != null);
      Assert.True(song.Audio != null);
      Assert.True(song.AuthorId == null);
      Assert.True(song.Comments.Count == 0);
      Assert.True(song.DateCreated <= DateTime.UtcNow);
      Assert.True(song.Language == "language");
      Assert.True(song.LastUpdated <= DateTime.UtcNow);
      Assert.True(song.Name == "name");
      Assert.True(song.Tags.Count == 0);
      Assert.True(song.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Song"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Album", new[] { new SongsAlbum { Name = "Name" }, new SongsAlbum { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Song.CompareTo(Song)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Song { Name = "Name" }.CompareTo(new Song { Name = "Name" }) == 0);
      Assert.True(new Song { Name = "First" }.CompareTo(new Song { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Article.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Article.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Song.Xml(null));

      var xml = new XElement("Song",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Audio",
          new XElement("Id", "audio.id"),
          new XElement("Bitrate", 1),
          new XElement("Duration", 2),
          new XElement("File",
            new XElement("Id", "audio.file.id"),
            new XElement("ContentType", "audio.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "audio.file.name"),
            new XElement("OriginalName", "audio.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().LongLength))));
      var song = Song.Xml(xml);
      Assert.True(song.Id == "id");
      Assert.True(song.Album == null);
      Assert.True(song.Audio.Id == "audio.id");
      Assert.True(song.Audio.Bitrate == 1);
      Assert.True(song.Audio.Duration == 2);
      Assert.True(song.Audio.File.Id == "audio.file.id");
      Assert.True(song.Audio.File.ContentType == "audio.file.contentType");
      Assert.True(song.Audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(song.Audio.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(song.Audio.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(song.Audio.File.Name == "audio.file.name");
      Assert.True(song.Audio.File.OriginalName == "audio.file.originalName");
      Assert.True(song.Audio.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(song.AuthorId == null);
      Assert.True(song.Comments.Count == 0);
      Assert.True(song.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(song.Language == "language");
      Assert.True(song.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(song.Name == "name");
      Assert.True(song.Tags.Count == 0);
      Assert.True(song.Text == "text");
      Assert.True(new Song("id", "language", "name", "text", new Audio("audio.id", new File("audio.file.id", "audio.file.contentType", "audio.file.name", "audio.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Song.Xml(song.Xml()).Equals(song));

      xml = new XElement("Song",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("SongsAlbum",
          new XElement("Id", "album.id"),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Language", "album.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "album.name")),
        new XElement("Audio",
          new XElement("Id", "audio.id"),
          new XElement("Bitrate", 1),
          new XElement("Duration", 2),
          new XElement("File",
            new XElement("Id", "audio.file.id"),
            new XElement("ContentType", "audio.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
            new XElement("Name", "audio.file.name"),
            new XElement("OriginalName", "audio.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().LongLength))));
      song = Song.Xml(xml);
      Assert.True(song.Id == "id");
      Assert.True(song.Album.Id == "album.id");
      Assert.True(song.Album.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(song.Album.Language == "album.language");
      Assert.True(song.Album.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(song.Album.Name == "album.name");
      Assert.True(song.Audio.Id == "audio.id");
      Assert.True(song.Audio.Bitrate == 1);
      Assert.True(song.Audio.Duration == 2);
      Assert.True(song.Audio.File.Id == "audio.file.id");
      Assert.True(song.Audio.File.ContentType == "audio.file.contentType");
      Assert.True(song.Audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(song.Audio.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(song.Audio.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(song.Audio.File.Name == "audio.file.name");
      Assert.True(song.Audio.File.OriginalName == "audio.file.originalName");
      Assert.True(song.Audio.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(song.AuthorId == null);
      Assert.True(song.Comments.Count == 0);
      Assert.True(song.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(song.Language == "language");
      Assert.True(song.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(song.Name == "name");
      Assert.True(song.Tags.Count == 0);
      Assert.True(song.Text == "text");
      Assert.True(new Song("id", "language", "name", "text", new Audio("audio.id", new File("audio.file.id", "audio.file.contentType", "audio.file.name", "audio.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2), new SongsAlbum("album.id", "album.language", "album.name") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Song.Xml(song.Xml()).Equals(song));
    }
  }
}