using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Dream"/>.</para>
  ///   <seealso cref="Dream"/>
  /// </summary>
  public static class DreamExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of dreams, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="dreams">Source sequence of dreams to filter.</param>
    /// <param name="category">Category of dreams to search for.</param>
    /// <returns>Filtered sequence of dreams with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> InDreamsCategory(this IEnumerable<Dream> dreams, DreamsCategory category)
    {
      Assertion.NotNull(dreams);

      return category != null ? dreams.Where(dream => dream != null && dream.Category.Id == category.Id) : dreams.Where(dream => dream != null && dream.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of dreams by category's name in ascending order.</para>
    /// </summary>
    /// <param name="dreams">Source sequence of dreams for sorting.</param>
    /// <returns>Sorted sequence of dreams.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> OrderByDreamsCategoryName(this IEnumerable<Dream> dreams)
    {
      Assertion.NotNull(dreams);

      return dreams.OrderBy(dream => dream.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of dreams by category's name in descending order.</para>
    /// </summary>
    /// <param name="dreams">Source sequence of dreams for sorting.</param>
    /// <returns>Sorted sequence of dreams.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> OrderByDreamsCategoryNameDescending(this IEnumerable<Dream> dreams)
    {
      Assertion.NotNull(dreams);

      return dreams.OrderByDescending(dream => dream.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dreams"></param>
    /// <param name="inspiredBy"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> InspiredByDream(this IEnumerable<Dream> dreams, Dream inspiredBy)
    {
      Assertion.NotNull(dreams);
 
      return inspiredBy != null ? dreams.Where(dream => dream != null && dream.InspiredBy.Id == inspiredBy.Id) : dreams.Where(dream => dream != null && dream.InspiredBy.Id == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dreams"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> OrderByInspiredByDreamName(this IEnumerable<Dream> dreams)
    {
      Assertion.NotNull(dreams);

      return dreams.OrderBy(dream => dream.InspiredBy.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dreams"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dreams"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Dream> OrderByInspiredByDreamNameDescending(this IEnumerable<Dream> dreams)
    {
      Assertion.NotNull(dreams);

      return dreams.OrderByDescending(dream => dream.InspiredBy.Name);
    }
  }
}