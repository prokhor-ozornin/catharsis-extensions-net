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
    ///   <para></para>
    /// </summary>
    /// <param name="weblinks"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> InWebLinksCategory(this IEnumerable<WebLink> weblinks, WebLinksCategory category)
    {
      Assertion.NotNull(weblinks);

      return category != null ? weblinks.Where(weblink => weblink != null && weblink.Category.Id == category.Id) : weblinks.Where(weblink => weblink != null && weblink.Category == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="weblinks"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> OrderByWebLinksCategoryName(this IEnumerable<WebLink> weblinks)
    {
      Assertion.NotNull(weblinks);

      return weblinks.OrderBy(weblink => weblink.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="weblinks"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="weblinks"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<WebLink> OrderByWebLinksCategoryNameDescending(this IEnumerable<WebLink> weblinks)
    {
      Assertion.NotNull(weblinks);

      return weblinks.OrderByDescending(weblink => weblink.Category.Name);
    }
  }
}