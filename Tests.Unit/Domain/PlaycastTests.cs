using System;
using System.Collections.Generic;
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
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Playcast()"/>
    ///   <seealso cref="Playcast(IDictionary{string, object})"/>
    ///   <seealso cref="Playcast(string, string, string, string, string, PlaycastsCategory, Audio, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var playcast = new Playcast();
      Assert.True(playcast.Id == null);
      Assert.True(playcast.AuthorId == null);
      Assert.True(playcast.Comments.Count == 0);
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == null);
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == null);
      Assert.True(playcast.Tags.Count == 0);
      Assert.True(playcast.Text == null);
      Assert.True(playcast.Audio == null);
      Assert.True(playcast.Category == null);
      Assert.True(playcast.Image == null);

      playcast = new Playcast(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Audio", new Audio())
        .AddNext("Category", new PlaycastsCategory())
        .AddNext("Image", new Image()));
      Assert.True(playcast.Id == "id");
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.Comments.Count == 0);
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Tags.Count == 0);
      Assert.True(playcast.Text == "text");
      Assert.True(playcast.Audio != null);
      Assert.True(playcast.Category != null);
      Assert.True(playcast.Image != null);

      playcast = new Playcast("id", "authorId", "language", "name", "text", new PlaycastsCategory(), new Audio(), new Image());
      Assert.True(playcast.Id == "id");
      Assert.True(playcast.AuthorId == "authorId");
      Assert.True(playcast.Comments.Count == 0);
      Assert.True(playcast.DateCreated <= DateTime.UtcNow);
      Assert.True(playcast.Language == "language");
      Assert.True(playcast.LastUpdated <= DateTime.UtcNow);
      Assert.True(playcast.Name == "name");
      Assert.True(playcast.Tags.Count == 0);
      Assert.True(playcast.Text == "text");
      Assert.True(playcast.Audio != null);
      Assert.True(playcast.Category != null);
      Assert.True(playcast.Image != null);
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new PlaycastsCategory { Name = "Name" }, new PlaycastsCategory { Name = "Name_2" } }));
    }
  }
}