using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="AudioExtensions"/>.</para>
  /// </summary>
  public sealed class AudioExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.InCategory(IEnumerable{Audio}, AudiosCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.InCategory(null, new AudiosCategory()));

      Assert.False(Enumerable.Empty<Audio>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<Audio>().InCategory(new AudiosCategory()).Any());
      Assert.True(new[] { null, new Audio { Category = new AudiosCategory { Id = 1 } }, null, new Audio { Category = new AudiosCategory { Id = 2 } } }.InCategory(new AudiosCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByCategoryName(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByCategoryName().Any());
      var entities = new[] { new Audio { Category = new AudiosCategory { Name = "Second" } }, new Audio { Category = new AudiosCategory { Name = "First" } } };
      Assert.True(entities.OrderByCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByCategoryNameDescending(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByCategoryNameDescending().Any());
      var entities = new[] { new Audio { Category = new AudiosCategory { Name = "First" } }, new Audio { Category = new AudiosCategory { Name = "Second" } } };
      Assert.True(entities.OrderByCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.WithBitrate(IEnumerable{Audio}, short)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithBitrate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.WithBitrate(null, 0));

      Assert.False(Enumerable.Empty<Audio>().WithBitrate(0).Any());
      Assert.True(new[] { null, new Audio { Bitrate = 1 }, null, new Audio { Bitrate = 2 } }.WithBitrate(1).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByBitrate(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBitrate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByBitrate(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByBitrate().Any());
      var entities = new[] { new Audio { Bitrate = 2 }, new Audio { Bitrate = 1 } };
      Assert.True(entities.OrderByBitrate().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByBitrateDescending(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBitrateDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByBitrateDescending(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByBitrateDescending().Any());
      var entities = new[] { new Audio { Bitrate = 1 }, new Audio { Bitrate = 2 } };
      Assert.True(entities.OrderByBitrateDescending().SequenceEqual(entities.Reverse()));
    }
  }
}