using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Playcast"/>.</para>
  ///   <seealso cref="Playcast"/>
  /// </summary>
  public static class PlaycastExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="playcasts"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> InPlaycastsCategory(this IEnumerable<Playcast> playcasts, PlaycastsCategory category)
    {
      Assertion.NotNull(playcasts);

      return category != null ? playcasts.Where(playcast => playcast != null && playcast.Category.Id == category.Id) : playcasts.Where(playcast => playcast != null && playcast.Category == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="playcasts"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> OrderByPlaycastsCategoryName(this IEnumerable<Playcast> playcasts)
    {
      Assertion.NotNull(playcasts);

      return playcasts.OrderBy(playcast => playcast.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="playcasts"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> OrderByPlaycastsCategoryNameDescending(this IEnumerable<Playcast> playcasts)
    {
      Assertion.NotNull(playcasts);

      return playcasts.OrderByDescending(playcast => playcast.Category.Name);
    }
  }
}