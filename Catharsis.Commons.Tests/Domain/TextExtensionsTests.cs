using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TextExtensions"/>.</para>
  /// </summary>
  public sealed class TextExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.InTextsCategory(IEnumerable{Text}, TextsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InTextsCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.InTextsCategory(null, new TextsCategory()));

      Assert.False(Enumerable.Empty<Text>().InTextsCategory(null).Any());
      Assert.False(Enumerable.Empty<Text>().InTextsCategory(new TextsCategory()).Any());
      Assert.True(new[] { null, new Text { Category = new TextsCategory { Id = 1 } }, null, new Text { Category = new TextsCategory { Id = 2 } } }.InTextsCategory(new TextsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.OrderByTextsCategoryName(IEnumerable{Text})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByTextsCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.OrderByTextsCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Text[] { null }.OrderByTextsCategoryName().Any());
      var entities = new[] { new Text { Category = new TextsCategory { Name = "Second" } }, new Text { Category = new TextsCategory { Name = "First" } } };
      Assert.True(entities.OrderByTextsCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.OrderByTextsCategoryNameDescending(IEnumerable{Text})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByTextsCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.OrderByTextsCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Text[] { null }.OrderByTextsCategoryNameDescending().Any());
      var entities = new[] { new Text { Category = new TextsCategory { Name = "First" } }, new Text { Category = new TextsCategory { Name = "Second" } } };
      Assert.True(entities.OrderByTextsCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.WithPerson(IEnumerable{Text}, Person)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithPerson_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.WithPerson(null, new Person()));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<Text>().WithPerson(null));

      Assert.False(Enumerable.Empty<Text>().WithPerson(new Person()).Any());
      Assert.True(new[] { null, new Text { Person = new Person { Id = 1 } }, null, new Text { Person = new Person { Id = 2 } } }.WithPerson(new Person { Id = 1 }).Count() == 1);
    }
  }
}