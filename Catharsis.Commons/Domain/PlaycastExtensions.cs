using System;
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
    ///   <para>Filters sequence of playcasts, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="playcasts">Source sequence of playcasts to filter.</param>
    /// <param name="category">Category of playcasts to search for.</param>
    /// <returns>Filtered sequence of playcasts with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> InPlaycastsCategory(this IEnumerable<Playcast> playcasts, PlaycastsCategory category)
    {
      Assertion.NotNull(playcasts);

      return category != null ? playcasts.Where(playcast => playcast != null && playcast.Category.Id == category.Id) : playcasts.Where(playcast => playcast != null && playcast.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of playcasts by category's name in ascending order.</para>
    /// </summary>
    /// <param name="playcasts">Source sequence of playcasts for sorting.</param>
    /// <returns>Sorted sequence of playcasts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> OrderByPlaycastsCategoryName(this IEnumerable<Playcast> playcasts)
    {
      Assertion.NotNull(playcasts);

      return playcasts.OrderBy(playcast => playcast.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of playcasts by category's name in descending order.</para>
    /// </summary>
    /// <param name="playcasts">Source sequence of playcasts for sorting.</param>
    /// <returns>Sorted sequence of playcasts.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="playcasts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Playcast> OrderByPlaycastsCategoryNameDescending(this IEnumerable<Playcast> playcasts)
    {
      Assertion.NotNull(playcasts);

      return playcasts.OrderByDescending(playcast => playcast.Category.Name);
    }
  }
}