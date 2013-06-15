using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Article"/>.</para>
  /// </summary>
  public sealed class ArticleTests : EntityUnitTests<Article>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Article()"/>
    ///   <seealso cref="Article(IDictionary{string, object)"/>
    ///   <seealso cref="Article(string, string, string, ArticlesCategory, string, string, string, Image)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var article = new Article();
      Assert.True(article.Id == null);
      Assert.True(article.AuthorId == null);
      Assert.True(article.Comments.Count == 0);
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == null);
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == null);
      Assert.True(article.Tags.Count == 0);
      Assert.True(article.Text == null);
      Assert.True(article.Annotation == null);
      Assert.True(article.Category == null);
      Assert.True(article.Image == null);

      article = new Article(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Annotation", "annotation")
        .AddNext("Category", new ArticlesCategory())
        .AddNext("Image", new Image()));
      Assert.True(article.Id == "id");
      Assert.True(article.AuthorId == "authorId");
      Assert.True(article.Comments.Count == 0);
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == "name");
      Assert.True(article.Tags.Count == 0);
      Assert.True(article.Text == "text");
      Assert.True(article.Annotation == "annotation");
      Assert.True(article.Category != null);
      Assert.True(article.Image != null);

      article = new Article("id", "language", "name", new ArticlesCategory(), "annotation", "text", "authorId", new Image());
      Assert.True(article.Id == "id");
      Assert.True(article.AuthorId == "authorId");
      Assert.True(article.Comments.Count == 0);
      Assert.True(article.DateCreated <= DateTime.UtcNow);
      Assert.True(article.Language == "language");
      Assert.True(article.LastUpdated <= DateTime.UtcNow);
      Assert.True(article.Name == "name");
      Assert.True(article.Tags.Count == 0);
      Assert.True(article.Text == "text");
      Assert.True(article.Annotation == "annotation");
      Assert.True(article.Category != null);
      Assert.True(article.Image != null);
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new [] { new ArticlesCategory { Name = "Name" }, new ArticlesCategory { Name = "Name_2" } }));
    }
  }
}