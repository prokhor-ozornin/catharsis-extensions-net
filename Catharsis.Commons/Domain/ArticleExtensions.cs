using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Article"/>.</para>
  ///   <seealso cref="Article"/>
  /// </summary>
  public static class ArticleExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of articles, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="articles">Source sequence of articles to filter.</param>
    /// <param name="category">Category of articles to search for.</param>
    /// <returns>Filtered sequence of articles with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="articles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> InArticlesCategory(this IEnumerable<Article> articles, ArticlesCategory category)
    {
      Assertion.NotNull(articles);

      return category != null ? articles.Where(article => article != null && article.Category.Id == category.Id) : articles.Where(article => article != null && article.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of articles by category's name in ascending order.</para>
    /// </summary>
    /// <param name="articles">Source sequence of articles for sorting.</param>
    /// <returns>Sorted sequence of articles.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="articles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> OrderByArticlesCategoryName(this IEnumerable<Article> articles)
    {
      Assertion.NotNull(articles);

      return articles.OrderBy(article => article.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of articles by category's name in descending order.</para>
    /// </summary>
    /// <param name="articles">Source sequence of articles for sorting.</param>
    /// <returns>Sorted sequence of articles.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="articles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Article> OrderByArticlesCategoryNameDescending(this IEnumerable<Article> articles)
    {
      Assertion.NotNull(articles);

      return articles.OrderByDescending(article => article.Category.Name);
    }
  }
}