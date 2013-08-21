using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="SongsAlbum"/>.</para>
  ///   <seealso cref="SongsAlbum"/>
  /// </summary>
  public static class SongsAlbumExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="albums"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<SongsAlbum> PublishedOn(this IEnumerable<SongsAlbum> albums, DateTime? from = null, DateTime? to = null)
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
    ///   <para>Sorts sequence of songs albums by creation date in ascending order.</para>
    /// </summary>
    /// <param name="albums">Source sequence of albums for sorting.</param>
    /// <returns>Sorted sequence of albums.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<SongsAlbum> OrderByPublishedOn(this IEnumerable<SongsAlbum> albums)
    {
      Assertion.NotNull(albums);

      return albums.OrderBy(album => album.PublishedOn);
    }

    /// <summary>
    ///   <para>Sorts sequence of songs albums by creation date in descending order.</para>
    /// </summary>
    /// <param name="albums">Source sequence of albums for sorting.</param>
    /// <returns>Sorted sequence of albums.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="albums"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<SongsAlbum> OrderByPublishedOnDescending(this IEnumerable<SongsAlbum> albums)
    {
      Assertion.NotNull(albums);

      return albums.OrderByDescending(album => album.PublishedOn);
    }
  }
}