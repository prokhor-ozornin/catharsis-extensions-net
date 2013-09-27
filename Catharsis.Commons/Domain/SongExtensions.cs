using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Song"/>.</para>
  ///   <seealso cref="Song"/>
  /// </summary>
  public static class SongExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of songs, leaving those belonging to specified album.</para>
    /// </summary>
    /// <param name="songs">Source sequence of songs to filter.</param>
    /// <param name="album">Album of songs to search for.</param>
    /// <returns>Filtered sequence of songs in specified album.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="songs"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Song> InAlbum(this IEnumerable<Song> songs, SongsAlbum album)
    {
      Assertion.NotNull(songs);

      return album != null ? songs.Where(song => song != null && song.Album.Id == album.Id) : songs.Where(song => song != null && song.Album == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of songs by album's name in ascending order.</para>
    /// </summary>
    /// <param name="songs">Source sequence of songs for sorting.</param>
    /// <returns>Sorted sequence of songs.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="songs"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Song> OrderByAlbumName(this IEnumerable<Song> songs)
    {
      Assertion.NotNull(songs);

      return songs.OrderBy(song => song.Album.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of songs by album's name in descending order.</para>
    /// </summary>
    /// <param name="songs">Source sequence of songs for sorting.</param>
    /// <returns>Sorted sequence of songs.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="songs"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Song> OrderByAlbumNameDescending(this IEnumerable<Song> songs)
    {
      Assertion.NotNull(songs);

      return songs.OrderByDescending(article => article.Album.Name);
    }
  }
}