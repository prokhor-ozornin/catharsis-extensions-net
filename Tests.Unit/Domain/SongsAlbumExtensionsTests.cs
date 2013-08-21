using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SongsAlbumExtensions"/>.</para>
  /// </summary>
  public sealed class SongsAlbumExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbumExtensions.PublishedOn(IEnumerable{SongsAlbum}, DateTime?, DateTime?)"/> method.</para>
    /// </summary>
    [Fact]
    public void PublishedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongsAlbumExtensions.PublishedOn(null));

      Assert.False(Enumerable.Empty<SongsAlbum>().PublishedOn().Any());
      Assert.False(Enumerable.Empty<SongsAlbum>().PublishedOn(DateTime.MinValue).Any());
      Assert.False(Enumerable.Empty<SongsAlbum>().PublishedOn(null, DateTime.MaxValue).Any());

      var firstDate = DateTime.Today;
      var secondDate = DateTime.UtcNow;
      var albums = new[] { null, new SongsAlbum { PublishedOn = firstDate }, null, new SongsAlbum { PublishedOn = secondDate } };
      var filteredAlbums = new[] { new SongsAlbum { PublishedOn = firstDate }, new SongsAlbum { PublishedOn = secondDate } };
      Assert.True(ReferenceEquals(albums.PublishedOn(), albums));
      Assert.True(albums.PublishedOn(DateTime.MinValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(null, DateTime.MaxValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(DateTime.MinValue, DateTime.MaxValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(firstDate, secondDate).SequenceEqual(filteredAlbums));
      Assert.False(albums.PublishedOn(DateTime.MinValue, DateTime.MinValue).Any());
      Assert.False(albums.PublishedOn(DateTime.MaxValue, DateTime.MaxValue).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbumExtensions.OrderByPublishedOn(IEnumerable{SongsAlbum})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPublishedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongsAlbumExtensions.OrderByPublishedOn(null));
      Assert.Throws<NullReferenceException>(() => new SongsAlbum[] { null }.OrderByPublishedOn().Any());

      var albums = new[] { new SongsAlbum { PublishedOn = new DateTime(2000, 1, 2) }, new SongsAlbum { PublishedOn = new DateTime(2000, 1, 1) } };
      Assert.True(albums.OrderByPublishedOn().SequenceEqual(albums.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SongsAlbumExtensions.OrderByPublishedOnDescending(IEnumerable{SongsAlbum})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPublishedOnDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SongsAlbumExtensions.OrderByPublishedOnDescending(null));
      Assert.Throws<NullReferenceException>(() => new SongsAlbum[] { null }.OrderByPublishedOnDescending().Any());

      var albums = new[] { new SongsAlbum { PublishedOn = new DateTime(2000, 1, 1) }, new SongsAlbum { PublishedOn = new DateTime(2000, 1, 2) } };
      Assert.True(albums.OrderByPublishedOnDescending().SequenceEqual(albums.Reverse()));
    }
  }
}