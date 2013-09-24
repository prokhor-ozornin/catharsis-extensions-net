using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="PlaycastExtensions"/>.</para>
  /// </summary>
  public sealed class PlaycastExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="PlaycastExtensions.InPlaycastsCategory(IEnumerable{Playcast}, PlaycastsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InPlaycastsCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.InPlaycastsCategory(null, new PlaycastsCategory()));

      Assert.False(Enumerable.Empty<Playcast>().InPlaycastsCategory(null).Any());
      Assert.False(Enumerable.Empty<Playcast>().InPlaycastsCategory(new PlaycastsCategory()).Any());
      Assert.True(new[] { null, new Playcast { Category = new PlaycastsCategory { Id = 1 } }, null, new Playcast { Category = new PlaycastsCategory { Id = 2 } } }.InPlaycastsCategory(new PlaycastsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PlaycastExtensions.OrderByPlaycastsCategoryName(IEnumerable{Playcast})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPlaycastsCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.OrderByPlaycastsCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Playcast[] { null }.OrderByPlaycastsCategoryName().Any());
      var entities = new[] { new Playcast { Category = new PlaycastsCategory { Name = "Second" } }, new Playcast { Category = new PlaycastsCategory { Name = "First" } } };
      Assert.True(entities.OrderByPlaycastsCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PlaycastExtensions.OrderByPlaycastsCategoryNameDescending(IEnumerable{Playcast})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPlaycastsCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.OrderByPlaycastsCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Playcast[] { null }.OrderByPlaycastsCategoryNameDescending().Any());
      var entities = new[] { new Playcast { Category = new PlaycastsCategory { Name = "First" } }, new Playcast { Category = new PlaycastsCategory { Name = "Second" } } };
      Assert.True(entities.OrderByPlaycastsCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }
  }
}