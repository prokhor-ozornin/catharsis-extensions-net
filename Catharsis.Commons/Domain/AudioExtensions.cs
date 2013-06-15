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
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> InAudiosCategory(this IEnumerable<Audio> audios, AudiosCategory category)
    {
      Assertion.NotNull(audios);

      return category != null ? audios.Where(audio => audio != null && audio.Category.Id == category.Id) : audios.Where(audio => audio != null && audio.Category == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByAudiosCategoryName(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderBy(audio => audio.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByAudiosCategoryNameDescending(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderByDescending(audio => audio.Category.Name);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <param name="bitrate"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> WithBitrate(this IEnumerable<Audio> audios, short bitrate)
    {
      Assertion.NotNull(audios);

      return audios.Where(audio => audio != null && audio.Bitrate == bitrate);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByBitrate(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderBy(audio => audio.Bitrate);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="audios"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="audios"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Audio> OrderByBitrateDescending(this IEnumerable<Audio> audios)
    {
      Assertion.NotNull(audios);

      return audios.OrderByDescending(audio => audio.Bitrate);
    }
  }
}