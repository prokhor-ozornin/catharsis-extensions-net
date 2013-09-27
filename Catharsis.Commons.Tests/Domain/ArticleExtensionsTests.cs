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
    ///   <para>Performs testing of <see cref="ArticleExtensions.InCategory(IEnumerable{Article}, ArticlesCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.InCategory(null, new ArticlesCategory()));

      Assert.False(Enumerable.Empty<Article>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<Article>().InCategory(new ArticlesCategory()).Any());
      Assert.True(new[] { null, new Article { Category = new ArticlesCategory { Id = 1 } }, null, new Article { Category = new ArticlesCategory { Id = 2 } } }.InCategory(new ArticlesCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArticleExtensions.OrderByCategoryName(IEnumerable{Article})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.OrderByCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Article[] { null }.OrderByCategoryName().Any());

      var articles = new[] { new Article { Category = new ArticlesCategory { Name = "Second" } }, new Article { Category = new ArticlesCategory { Name = "First" } } };
      Assert.True(articles.OrderByCategoryName().SequenceEqual(articles.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArticleExtensions.OrderByCategoryName(IEnumerable{Article})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArticleExtensions.OrderByCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Article[] { null }.OrderByCategoryNameDescending().Any());

      var articles = new[] { new Article { Category = new ArticlesCategory { Name = "First" } }, new Article { Category = new ArticlesCategory { Name = "Second" } } };
      Assert.True(articles.OrderByCategoryNameDescending().SequenceEqual(articles.Reverse()));
    }
  }
}