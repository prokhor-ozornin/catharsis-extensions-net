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
    ///   <para>Performs testing of <see cref="SongExtensions.InSongsAlbum(IEnumerable{Song}, SongsAlbum)"/> method.</para>
    /// </summary>
    [Fact]
    public void InSongsAlbum_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.InSongsAlbum(null, new SongsAlbum()));

      Assert.False(Enumerable.Empty<Song>().InSongsAlbum(null).Any());
      Assert.False(Enumerable.Empty<Song>().InSongsAlbum(new SongsAlbum()).Any());
      Assert.True(new[] { null, new Song { Album = new SongsAlbum { Id = 1 } }, null, new Song { Album = new SongsAlbum { Id = 2 } } }.InSongsAlbum(new SongsAlbum { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongExtensions.OrderBySongsAlbumName(IEnumerable{Song})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderBySongsAlbumName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.OrderBySongsAlbumName(null));
      Assert.Throws<NullReferenceException>(() => new Song[] { null }.OrderBySongsAlbumName().Any());

      var songs = new[] { new Song { Album = new SongsAlbum { Name = "Second" } }, new Song { Album = new SongsAlbum { Name = "First" } } };
      Assert.True(songs.OrderBySongsAlbumName().SequenceEqual(songs.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongExtensions.OrderBySongsAlbumNameDescending(IEnumerable{Song})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderBySongsAlbumNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongExtensions.OrderBySongsAlbumNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Song[] { null }.OrderBySongsAlbumNameDescending().Any());

      var songs = new[] { new Song { Album = new SongsAlbum { Name = "First" } }, new Song { Album = new SongsAlbum { Name = "Second" } } };
      Assert.True(songs.OrderBySongsAlbumNameDescending().SequenceEqual(songs.Reverse()));
    }
  }
}