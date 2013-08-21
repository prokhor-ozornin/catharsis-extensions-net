using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Art"/>.</para>
  ///   <seealso cref="Art"/>
  /// </summary>
  public static class ArtExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of arts, leaving those belonging to specified album.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts to filter.</param>
    /// <param name="album">Arts album to search for.</param>
    /// <returns>Filtered sequence of arts in specified album.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> InArtsAlbum(this IEnumerable<Art> arts, ArtsAlbum album)
    {
      Assertion.NotNull(arts);

      return album != null ? arts.Where(art => art != null && art.Album.Id == album.Id) : arts.Where(art => art != null && art.Album == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of arts by album's name in ascending order.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts for sorting.</param>
    /// <returns>Sorted sequence of arts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> OrderByArtsAlbumName(this IEnumerable<Art> arts)
    {
      Assertion.NotNull(arts);

      return arts.OrderBy(art => art.Album.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of arts by album's name in descending order.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts for sorting.</param>
    /// <returns>Sorted sequence of arts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> OrderByArtsAlbumNameDescending(this IEnumerable<Art> arts)
    {
      Assertion.NotNull(arts);

      return arts.OrderByDescending(art => art.Album.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of arts, leaving those belonging to specified album.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts to filter.</param>
    /// <param name="material">Material to search for.</param>
    /// <returns>Filtered sequence of arts with specified material.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> WithMaterial(this IEnumerable<Art> arts, string material)
    {
      Assertion.NotNull(arts);

      return arts.Where(art => art != null && art.Material == material);
    }

    /// <summary>
    ///   <para>Sorts sequence of arts by material in ascending order.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts for sorting.</param>
    /// <returns>Sorted sequence of arts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> OrderByMaterial(this IEnumerable<Art> arts)
    {
      Assertion.NotNull(arts);

      return arts.OrderBy(art => art.Material);
    }

    /// <summary>
    ///   <para>Sorts sequence of arts by material in descending order.</para>
    /// </summary>
    /// <param name="arts">Source sequence of arts for sorting.</param>
    /// <returns>Sorted sequence of arts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="arts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Art> OrderByMaterialDescending(this IEnumerable<Art> arts)
    {
      Assertion.NotNull(arts);

      return arts.OrderByDescending(art => art.Material);
    }
  }
}