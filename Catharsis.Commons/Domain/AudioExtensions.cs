using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Audio"/>.</para>
  ///   <seealso cref="Audio"/>
  /// </summary>
  public static class AudioExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of audios, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios to filter.</param>
    /// <param name="category">Category of audios to search for.</param>
    /// <returns>Filtered sequence of audios with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> InCategory(this IEnumerable<Audio> audios, AudiosCategory category)
    {
      Assertion.NotNull(audios);

      return category != null ? audios.Where(audio => audio != null && audio.Category.Id == category.Id) : audios.Where(audio => audio != null && audio.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of audios by category's name in ascending order.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios for sorting.</param>
    /// <returns>Sorted sequence of audios.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByCategoryName(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderBy(audio => audio.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of audios by category's name in descending order.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios for sorting.</param>
    /// <returns>Sorted sequence of audios.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByCategoryNameDescending(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderByDescending(audio => audio.Category.Name);
    }
    
    /// <summary>
    ///   <para>Filters sequence of audios, leaving those with specified bitrate.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios to filter.</param>
    /// <param name="bitrate">Bitrate of audios to search for.</param>
    /// <returns>Filtered sequence of audios with specified bitrate.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> WithBitrate(this IEnumerable<Audio> audios, short bitrate)
    {
      Assertion.NotNull(audios);

      return audios.Where(audio => audio != null && audio.Bitrate == bitrate);
    }

    /// <summary>
    ///   <para>Sorts sequence of audios by bitrate in ascending order.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios for sorting.</param>
    /// <returns>Sorted sequence of audios.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByBitrate(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderBy(audio => audio.Bitrate);
    }

    /// <summary>
    ///   <para>Sorts sequence of audios by bitrate in descending order.</para>
    /// </summary>
    /// <param name="audios">Source sequence of audios for sorting.</param>
    /// <returns>Sorted sequence of audios.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByBitrateDescending(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderByDescending(audio => audio.Bitrate);
    }
  }
}