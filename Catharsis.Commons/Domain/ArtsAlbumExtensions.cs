using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="ArtsAlbum"/>.</para>
  ///   <seealso cref="ArtsAlbum"/>
  /// </summary>
  public static class ArtsAlbumExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of arts albums, leaving those published in specified time period.</para>
    /// </summary>
    /// <param name="albums">Source sequence of arts albums to filter.</param>
    /// <param name="from">Lower bound of date range.</param>
    /// <param name="to">Upper bound of date range.</param>
    /// <returns>Filtered sequence of arts albums with a publishing date between <paramref name="from"/> and <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<ArtsAlbum> PublishedOn(this IEnumerable<ArtsAlbum> albums, DateTime? from = null, DateTime? to = null)
    {
      Assertion.NotNull(albums);

      if (from.HasValue && to.HasValue)
      {
        return albums.Where(album => album != null && album.PublishedOn >= from.Value && album.PublishedOn <= to.Value);
      }

      if (from.HasValue)
      {
        return albums.Where(album => album != null && album.PublishedOn >= from.Value);
      }

      if (to.HasValue)
      {
        return albums.Where(album => album != null && album.PublishedOn <= to.Value);
      }

      return albums;
    }

    /// <summary>
    ///   <para>Sorts sequence of arts albums by creation date in ascending order.</para>
    /// </summary>
    /// <param name="albums">Source sequence of albums for sorting.</param>
    /// <returns>Sorted sequence of albums.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<ArtsAlbum> OrderByPublishedOn(this IEnumerable<ArtsAlbum> albums)
    {
      Assertion.NotNull(albums);

      return albums.OrderBy(album => album.PublishedOn);
    }

    /// <summary>
    ///   <para>Sorts sequence of arts albums by creation date in descending order.</para>
    /// </summary>
    /// <param name="albums">Source sequence of albums for sorting.</param>
    /// <returns>Sorted sequence of albums.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<ArtsAlbum> OrderByPublishedOnDescending(this IEnumerable<ArtsAlbum> albums)
    {
      Assertion.NotNull(albums);

      return albums.OrderByDescending(album => album.PublishedOn);
    }
  }
}