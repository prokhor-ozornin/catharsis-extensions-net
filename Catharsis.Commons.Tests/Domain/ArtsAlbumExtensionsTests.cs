using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArtsAlbumExtensions"/>.</para>
  /// </summary>
  public sealed class ArtsAlbumExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArtsAlbumExtensions.PublishedOn(IEnumerable{ArtsAlbum}, DateTime?, DateTime?)"/> method.</para>
    /// </summary>
    [Fact]
    public void PublishedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtsAlbumExtensions.PublishedOn(null));

      Assert.False(Enumerable.Empty<ArtsAlbum>().PublishedOn().Any());
      Assert.False(Enumerable.Empty<ArtsAlbum>().PublishedOn(DateTime.MinValue).Any());
      Assert.False(Enumerable.Empty<ArtsAlbum>().PublishedOn(null, DateTime.MaxValue).Any());

      var firstDate = DateTime.Today;
      var secondDate = DateTime.UtcNow;
      var albums = new[] { null, new ArtsAlbum { PublishedOn = firstDate }, null, new ArtsAlbum { PublishedOn = secondDate } };
      var filteredAlbums = new[] { new ArtsAlbum { PublishedOn = firstDate }, new ArtsAlbum { PublishedOn = secondDate } };
      Assert.True(ReferenceEquals(albums.PublishedOn(), albums));
      Assert.True(albums.PublishedOn(DateTime.MinValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(null, DateTime.MaxValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(DateTime.MinValue, DateTime.MaxValue).SequenceEqual(filteredAlbums));
      Assert.True(albums.PublishedOn(firstDate, secondDate).SequenceEqual(filteredAlbums));
      Assert.False(albums.PublishedOn(DateTime.MinValue, DateTime.MinValue).Any());
      Assert.False(albums.PublishedOn(DateTime.MaxValue, DateTime.MaxValue).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtsAlbumExtensions.OrderByPublishedOn(IEnumerable{ArtsAlbum})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPublishedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtsAlbumExtensions.OrderByPublishedOn(null));
      Assert.Throws<NullReferenceException>(() => new ArtsAlbum[] { null }.OrderByPublishedOn().Any());

      var albums = new[] { new ArtsAlbum { PublishedOn = new DateTime(2000, 1, 2) }, new ArtsAlbum { PublishedOn = new DateTime(2000, 1, 1) } };
      Assert.True(albums.OrderByPublishedOn().SequenceEqual(albums.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtsAlbumExtensions.OrderByPublishedOnDescending(IEnumerable{ArtsAlbum})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPublishedOnDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtsAlbumExtensions.OrderByPublishedOnDescending(null));
      Assert.Throws<NullReferenceException>(() => new ArtsAlbum[] { null }.OrderByPublishedOnDescending().Any());

      var albums = new[] { new ArtsAlbum { PublishedOn = new DateTime(2000, 1, 1) }, new ArtsAlbum { PublishedOn = new DateTime(2000, 1, 2) } };
      Assert.True(albums.OrderByPublishedOnDescending().SequenceEqual(albums.Reverse()));
    }
  }
}