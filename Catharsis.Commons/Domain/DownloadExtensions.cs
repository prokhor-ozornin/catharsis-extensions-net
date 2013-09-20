using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Download"/>.</para>
  ///   <seealso cref="Download"/>
  /// </summary>
  public static class DownloadExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of downloads, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads to filter.</param>
    /// <param name="category">Category of downloads to search for.</param>
    /// <returns>Filtered sequence of downloads with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> InDownloadsCategory(this IEnumerable<Download> downloads, DownloadsCategory category)
    {
      Assertion.NotNull(downloads);

      return category != null ? downloads.Where(download => download != null && download.Category.Id == category.Id) : downloads.Where(download => download != null && download.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of downloads by category's name in ascending order.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads for sorting.</param>
    /// <returns>Sorted sequence of downloads.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> OrderByDownloadsCategoryName(this IEnumerable<Download> downloads)
    {
      Assertion.NotNull(downloads);

      return downloads.OrderBy(download => download.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of downloads by category's name in descending order.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads for sorting.</param>
    /// <returns>Sorted sequence of download.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> OrderByDownloadsCategoryNameDescending(this IEnumerable<Download> downloads)
    {
      Assertion.NotNull(downloads);

      return downloads.OrderByDescending(download => download.Category.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of downloads, leaving those with count of downloads in specified range.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads to filter.</param>
    /// <param name="from">Lower bound of downloads range.</param>
    /// <param name="to">Upper bound of downloads range.</param>
    /// <returns>Filtered sequence of downloads with count of downloads ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> WithDownloads(this IEnumerable<Download> downloads, long? from = null, long? to = null)
    {
      Assertion.NotNull(downloads);

      if (from.HasValue && to.HasValue)
      {
        return downloads.Where(download => download != null && download.Downloads >= from.Value && download.Downloads <= to.Value);
      }

      if (from.HasValue)
      {
        return downloads.Where(download => download != null && download.Downloads >= from.Value);
      }

      return to.HasValue ? downloads.Where(download => download != null && download.Downloads <= to.Value) : downloads;
    }

    /// <summary>
    ///   <para>Sorts sequence of downloads by downloads count in ascending order.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads for sorting.</param>
    /// <returns>Sorted sequence of downloads.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> OrderByDownloads(this IEnumerable<Download> downloads)
    {
      Assertion.NotNull(downloads);

      return downloads.OrderBy(download => download.Downloads);
    }

    /// <summary>
    ///   <para>Sorts sequence of downloads by downloads count in descending order.</para>
    /// </summary>
    /// <param name="downloads">Source sequence of downloads for sorting.</param>
    /// <returns>Sorted sequence of downloads.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="downloads"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Download> OrderByDownloadsDescending(this IEnumerable<Download> downloads)
    {
      Assertion.NotNull(downloads);

      return downloads.OrderByDescending(download => download.Downloads);
    }
  }
}