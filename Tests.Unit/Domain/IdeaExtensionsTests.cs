using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IdeaExtensions"/>.</para>
  /// </summary>
  public sealed class IdeaExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.InIdeasCategory(IEnumerable{Idea}, IdeasCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InIdeasCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.InIdeasCategory(null, new IdeasCategory()));

      Assert.False(Enumerable.Empty<Idea>().InIdeasCategory(null).Any());
      Assert.False(Enumerable.Empty<Idea>().InIdeasCategory(new IdeasCategory()).Any());
      Assert.True(new[] { null, new Idea { Category = new IdeasCategory { Id = "Id" } }, null, new Idea { Category = new IdeasCategory { Id = "Id_2" } } }.InIdeasCategory(new IdeasCategory { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.OrderByIdeasCategoryName(IEnumerable{Idea})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByIdeasCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.OrderByIdeasCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Idea[] { null }.OrderByIdeasCategoryName().Any());

      var ideas = new[] { new Idea { Category = new IdeasCategory { Name = "Second" } }, new Idea { Category = new IdeasCategory { Name = "First" } } };
      Assert.True(ideas.OrderByIdeasCategoryName().SequenceEqual(ideas.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.OrderByIdeasCategoryNameDescending(IEnumerable{Idea})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByIdeasCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.OrderByIdeasCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Idea[] { null }.OrderByIdeasCategoryNameDescending().Any());

      var ideas = new[] { new Idea { Category = new IdeasCategory { Name = "First" } }, new Idea { Category = new IdeasCategory { Name = "Second" } } };
      Assert.True(ideas.OrderByIdeasCategoryNameDescending().SequenceEqual(ideas.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.InspiredByIdea(IEnumerable{Idea}, Idea)"/> method.</para>
    /// </summary>
    [Fact]
    public void InspiredByIdea_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.InspiredByIdea(null, new Idea()));

      Assert.False(Enumerable.Empty<Idea>().InspiredByIdea(null).Any());
      Assert.False(Enumerable.Empty<Idea>().InspiredByIdea(new Idea()).Any());
      Assert.True(new[] { null, new Idea { InspiredBy = new Idea { Id = "Id" } }, null, new Idea { InspiredBy = new Idea { Id = "Id_2" } } }.InspiredByIdea(new Idea { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.OrderByInspiredByIdeaName(IEnumerable{Idea})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByInspiredByIdeaName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.OrderByInspiredByIdeaName(null));
      Assert.Throws<NullReferenceException>(() => new Idea[] { null }.OrderByInspiredByIdeaName().Any());

      var ideas = new[] { new Idea { InspiredBy = new Idea { Name = "Second" } }, new Idea { InspiredBy = new Idea { Name = "First" } } };
      Assert.True(ideas.OrderByInspiredByIdeaName().SequenceEqual(ideas.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IdeaExtensions.OrderByInspiredByIdeaNameDescending(IEnumerable{Idea})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByInspiredByIdeaNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IdeaExtensions.OrderByInspiredByIdeaNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Idea[] { null }.OrderByInspiredByIdeaNameDescending().Any());

      var ideas = new[] { new Idea { InspiredBy = new Idea { Name = "First" } }, new Idea { InspiredBy = new Idea { Name = "Second" } } };
      Assert.True(ideas.OrderByInspiredByIdeaNameDescending().SequenceEqual(ideas.Reverse()));
    }
  }
}