using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Article"/>.</para>
  ///   <seealso cref="Article"/>
  /// </summary>
  public static class ArticleExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="articles"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="articles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> InArticlesCategory(this IEnumerable<Article> articles, ArticlesCategory category)
    {
      Assertion.NotNull(articles);

      return category != null ? articles.Where(article => article != null && article.Category.Id == category.Id) : articles.Where(article => article != null && article.Category == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="articles"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="article"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> OrderByArticlesCategoryName(this IEnumerable<Article> articles)
    {
      Assertion.NotNull(articles);

      return articles.OrderBy(article => article.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="articles"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="articles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> OrderByArticlesCategoryNameDescending(this IEnumerable<Article> articles)
    {
      Assertion.NotNull(articles);

      return articles.OrderByDescending(article => article.Category.Name);
    }
  }
}