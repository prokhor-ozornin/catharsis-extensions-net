using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SongExtensions"/>.</para>
  /// </summary>
  public sealed class SongExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="SongExtensions.InAlbum(IEnumerable{Song}, SongsAlbum)"/> method.</para>
    /// </summary>
    [Fact]
    public void InAlbum_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.InAlbum(null, new SongsAlbum()));

      Assert.False(Enumerable.Empty<Song>().InAlbum(null).Any());
      Assert.False(Enumerable.Empty<Song>().InAlbum(new SongsAlbum()).Any());
      Assert.True(new[] { null, new Song { Album = new SongsAlbum { Id = 1 } }, null, new Song { Album = new SongsAlbum { Id = 2 } } }.InAlbum(new SongsAlbum { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongExtensions.OrderByAlbumName(IEnumerable{Song})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAlbumName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.OrderByAlbumName(null));
      Assert.Throws<NullReferenceException>(() => new Song[] { null }.OrderByAlbumName().Any());

      var songs = new[] { new Song { Album = new SongsAlbum { Name = "Second" } }, new Song { Album = new SongsAlbum { Name = "First" } } };
      Assert.True(songs.OrderByAlbumName().SequenceEqual(songs.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongExtensions.OrderByAlbumNameDescending(IEnumerable{Song})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAlbumNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.OrderByAlbumNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Song[] { null }.OrderByAlbumNameDescending().Any());

      var songs = new[] { new Song { Album = new SongsAlbum { Name = "First" } }, new Song { Album = new SongsAlbum { Name = "Second" } } };
      Assert.True(songs.OrderByAlbumNameDescending().SequenceEqual(songs.Reverse()));
    }
  }
}