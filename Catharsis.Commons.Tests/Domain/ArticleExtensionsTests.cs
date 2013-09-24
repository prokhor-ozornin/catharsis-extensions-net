using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArticleExtensions"/>.</para>
  /// </summary>
  public sealed class ArticleExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArticleExtensions.InArticlesCategory(IEnumerable{Article}, ArticlesCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InArticlesCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.InArticlesCategory(null, new ArticlesCategory()));

      Assert.False(Enumerable.Empty<Article>().InArticlesCategory(null).Any());
      Assert.False(Enumerable.Empty<Article>().InArticlesCategory(new ArticlesCategory()).Any());
      Assert.True(new[] { null, new Article { Category = new ArticlesCategory { Id = 1 } }, null, new Article { Category = new ArticlesCategory { Id = 2 } } }.InArticlesCategory(new ArticlesCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArticleExtensions.OrderByArticlesCategoryName"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.OrderByArticlesCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Article[] { null }.OrderByArticlesCategoryName().Any());

      var articles = new[] { new Article { Category = new ArticlesCategory { Name = "Second" } }, new Article { Category = new ArticlesCategory { Name = "First" } } };
      Assert.True(articles.OrderByArticlesCategoryName().SequenceEqual(articles.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArticleExtensions.OrderByArticlesCategoryName(IEnumerable{Article})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByArticlesCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.OrderByArticlesCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Article[] { null }.OrderByArticlesCategoryNameDescending().Any());

      var articles = new[] { new Article { Category = new ArticlesCategory { Name = "First" } }, new Article { Category = new ArticlesCategory { Name = "Second" } } };
      Assert.True(articles.OrderByArticlesCategoryNameDescending().SequenceEqual(articles.Reverse()));
    }
  }
}