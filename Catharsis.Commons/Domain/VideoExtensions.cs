using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Video"/>.</para>
  ///   <seealso cref="Video"/>
  /// </summary>
  public static class VideoExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of videos, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="videos">Source sequence of videos to filter.</param>
    /// <param name="category">Category of videos to search for.</param>
    /// <returns>Filtered sequence of videos with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> InVideosCategory(this IEnumerable<Video> videos, VideosCategory category)
    {
      Assertion.NotNull(videos);

      return category != null ? videos.Where(video => video != null && video.Category.Id == category.Id) : videos.Where(video => video != null && video.Category == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByVideosCategoryName(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);

      return videos.OrderBy(video => video.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByVideosCategoryNameDescending(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);

      return videos.OrderByDescending(video => video.Category.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of videos, leaving those with specified bitrate.</para>
    /// </summary>
    /// <param name="videos">Source sequence of videos to filter.</param>
    /// <param name="bitrate">Bitrate of videos to search for.</param>
    /// <returns>Filtered sequence of videos with specified bitrate.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> WithBitrate(this IEnumerable<Video> videos, short bitrate)
    {
      Assertion.NotNull(videos);

      return videos.Where(video => video != null && video.Bitrate == bitrate);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByBitrate(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);
      
      return videos.OrderBy(video => video.Bitrate);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByBitrateDescending(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);

      return videos.OrderByDescending(video => video.Bitrate);
    }

    /// <summary>
    ///   <para>Filters sequence of videos, leaving those with duration in specified range.</para>
    /// </summary>
    /// <param name="videos">Source sequence of videos to filter.</param>
    /// <param name="from">Lower border of duration range.</param>
    /// <param name="to">Upper border of duration range.</param>
    /// <returns>Filtered sequence of videos with duration ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> WithDuration(this IEnumerable<Video> videos, long? from = null, long? to = null)
    {
      Assertion.NotNull(videos);

      if (from.HasValue && to.HasValue)
      {
        return videos.Where(video => video != null && video.Duration >= from.Value && video.Duration <= to.Value);
      }

      if (from.HasValue)
      {
        return videos.Where(video => video != null && video.Duration >= from.Value);
      }

      if (to.HasValue)
      {
        return videos.Where(video => video != null && video.Duration <= to.Value);
      }

      return videos;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByDuration(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);

      return videos.OrderBy(video => video.Duration);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="videos"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="videos"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Video> OrderByDurationDescending(this IEnumerable<Video> videos)
    {
      Assertion.NotNull(videos);

      return videos.OrderByDescending(video => video.Duration);
    }
  }
}