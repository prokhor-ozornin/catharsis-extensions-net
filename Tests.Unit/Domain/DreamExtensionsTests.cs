using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DreamExtensions"/>.</para>
  /// </summary>
  public sealed class DreamExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.InDreamsCategory(IEnumerable{Dream}, DreamsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InDreamsCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.InDreamsCategory(null, new DreamsCategory()));

      Assert.False(Enumerable.Empty<Dream>().InDreamsCategory(null).Any());
      Assert.False(Enumerable.Empty<Dream>().InDreamsCategory(new DreamsCategory()).Any());
      Assert.True(new[] { null, new Dream { Category = new DreamsCategory { Id = "Id" } }, null, new Dream { Category = new DreamsCategory { Id = "Id_2" } } }.InDreamsCategory(new DreamsCategory { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.OrderByDreamsCategoryName(IEnumerable{Dream})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDreamsCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.OrderByDreamsCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Dream[] { null }.OrderByDreamsCategoryName().Any());

      var dreams = new[] { new Dream { Category = new DreamsCategory { Name = "Second" } }, new Dream { Category = new DreamsCategory { Name = "First" } } };
      Assert.True(dreams.OrderByDreamsCategoryName().SequenceEqual(dreams.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.OrderByDreamsCategoryNameDescending(IEnumerable{Dream})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDreamsCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.OrderByDreamsCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Dream[] { null }.OrderByDreamsCategoryNameDescending().Any());

      var dreams = new[] { new Dream { Category = new DreamsCategory { Name = "First" } }, new Dream { Category = new DreamsCategory { Name = "Second" } } };
      Assert.True(dreams.OrderByDreamsCategoryNameDescending().SequenceEqual(dreams.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.InspiredByDream(IEnumerable{Dream}, Dream)"/> method.</para>
    /// </summary>
    [Fact]
    public void InspiredByDream_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.InspiredByDream(null, new Dream()));

      Assert.False(Enumerable.Empty<Dream>().InspiredByDream(null).Any());
      Assert.False(Enumerable.Empty<Dream>().InspiredByDream(new Dream()).Any());
      Assert.True(new[] { null, new Dream { InspiredBy = new Dream { Id = "Id" } }, null, new Dream { InspiredBy = new Dream { Id = "Id_2" } } }.InspiredByDream(new Dream { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.OrderByInspiredByDreamName(IEnumerable{Dream})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByInspiredByDreamName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.OrderByInspiredByDreamName(null));
      Assert.Throws<NullReferenceException>(() => new Dream[] { null }.OrderByInspiredByDreamName().Any());

      var dreams = new[] { new Dream { InspiredBy = new Dream { Name = "Second" } }, new Dream { InspiredBy = new Dream { Name = "First" } } };
      Assert.True(dreams.OrderByInspiredByDreamName().SequenceEqual(dreams.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DreamExtensions.OrderByInspiredByDreamNameDescending(IEnumerable{Dream})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByInspiredByDreamNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DreamExtensions.OrderByInspiredByDreamNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Dream[] { null }.OrderByInspiredByDreamNameDescending().Any());

      var dreams = new[] { new Dream { InspiredBy = new Dream { Name = "First" } }, new Dream { InspiredBy = new Dream { Name = "Second" } } };
      Assert.True(dreams.OrderByInspiredByDreamNameDescending().SequenceEqual(dreams.Reverse()));
    }
  }
}