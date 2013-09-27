using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="WebLink"/>.</para>
  ///   <seealso cref="WebLink"/>
  /// </summary>
  public static class WebLinkExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of web links, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="weblinks">Source sequence of web links to filter.</param>
    /// <param name="category">Category of web links to search for.</param>
    /// <returns>Filtered sequence of web links with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> InCategory(this IEnumerable<WebLink> weblinks, WebLinksCategory category)
    {
      Assertion.NotNull(weblinks);

      return category != null ? weblinks.Where(weblink => weblink != null && weblink.Category.Id == category.Id) : weblinks.Where(weblink => weblink != null && weblink.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of web links by category's name in ascending order.</para>
    /// </summary>
    /// <param name="weblinks">Source sequence of web links for sorting.</param>
    /// <returns>Sorted sequence of web links.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> OrderByCategoryName(this IEnumerable<WebLink> weblinks)
    {
      Assertion.NotNull(weblinks);

      return weblinks.OrderBy(weblink => weblink.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of web links by category's name in descending order.</para>
    /// </summary>
    /// <param name="weblinks">Source sequence of web links for sorting.</param>
    /// <returns>Sorted sequence of web links.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> OrderByCategoryNameDescending(this IEnumerable<WebLink> weblinks)
    {
      Assertion.NotNull(weblinks);

      return weblinks.OrderByDescending(weblink => weblink.Category.Name);
    }
  }
}