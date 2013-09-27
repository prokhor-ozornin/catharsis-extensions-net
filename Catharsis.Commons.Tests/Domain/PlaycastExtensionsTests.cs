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
    ///   <para>Performs testing of <see cref="PlaycastExtensions.InCategory(IEnumerable{Playcast}, PlaycastsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.InCategory(null, new PlaycastsCategory()));

      Assert.False(Enumerable.Empty<Playcast>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<Playcast>().InCategory(new PlaycastsCategory()).Any());
      Assert.True(new[] { null, new Playcast { Category = new PlaycastsCategory { Id = 1 } }, null, new Playcast { Category = new PlaycastsCategory { Id = 2 } } }.InCategory(new PlaycastsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PlaycastExtensions.OrderByCategoryName(IEnumerable{Playcast})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.OrderByCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Playcast[] { null }.OrderByCategoryName().Any());
      var entities = new[] { new Playcast { Category = new PlaycastsCategory { Name = "Second" } }, new Playcast { Category = new PlaycastsCategory { Name = "First" } } };
      Assert.True(entities.OrderByCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PlaycastExtensions.OrderByCategoryNameDescending(IEnumerable{Playcast})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PlaycastExtensions.OrderByCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Playcast[] { null }.OrderByCategoryNameDescending().Any());
      var entities = new[] { new Playcast { Category = new PlaycastsCategory { Name = "First" } }, new Playcast { Category = new PlaycastsCategory { Name = "Second" } } };
      Assert.True(entities.OrderByCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }
  }
}