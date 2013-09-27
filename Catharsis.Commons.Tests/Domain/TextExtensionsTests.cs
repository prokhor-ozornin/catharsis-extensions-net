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
    ///   <para>Performs testing of <see cref="TextExtensions.InCategory(IEnumerable{Text}, TextsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.InCategory(null, new TextsCategory()));

      Assert.False(Enumerable.Empty<Text>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<Text>().InCategory(new TextsCategory()).Any());
      Assert.True(new[] { null, new Text { Category = new TextsCategory { Id = 1 } }, null, new Text { Category = new TextsCategory { Id = 2 } } }.InCategory(new TextsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.OrderByCategoryName(IEnumerable{Text})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.OrderByCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Text[] { null }.OrderByCategoryName().Any());
      var entities = new[] { new Text { Category = new TextsCategory { Name = "Second" } }, new Text { Category = new TextsCategory { Name = "First" } } };
      Assert.True(entities.OrderByCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TextExtensions.OrderByCategoryNameDescending(IEnumerable{Text})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TextExtensions.OrderByCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Text[] { null }.OrderByCategoryNameDescending().Any());
      var entities = new[] { new Text { Category = new TextsCategory { Name = "First" } }, new Text { Category = new TextsCategory { Name = "Second" } } };
      Assert.True(entities.OrderByCategoryNameDescending().SequenceEqual(entities.Reverse()));
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