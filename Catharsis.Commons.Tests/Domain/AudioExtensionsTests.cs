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
    ///   <para>Performs testing of <see cref="AudioExtensions.InAudiosCategory(IEnumerable{Audio}, AudiosCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InAudiosCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.InAudiosCategory(null, new AudiosCategory()));

      Assert.False(Enumerable.Empty<Audio>().InAudiosCategory(null).Any());
      Assert.False(Enumerable.Empty<Audio>().InAudiosCategory(new AudiosCategory()).Any());
      Assert.True(new[] { null, new Audio { Category = new AudiosCategory { Id = "1" } }, null, new Audio { Category = new AudiosCategory { Id = "2" } } }.InAudiosCategory(new AudiosCategory { Id = "1" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByAudiosCategoryName(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAudiosCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByAudiosCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByAudiosCategoryName().Any());
      var entities = new[] { new Audio { Category = new AudiosCategory { Name = "Second" } }, new Audio { Category = new AudiosCategory { Name = "First" } } };
      Assert.True(entities.OrderByAudiosCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AudioExtensions.OrderByAudiosCategoryNameDescending(IEnumerable{Audio})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAudiosCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AudioExtensions.OrderByAudiosCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Audio[] { null }.OrderByAudiosCategoryNameDescending().Any());
      var entities = new[] { new Audio { Category = new AudiosCategory { Name = "First" } }, new Audio { Category = new AudiosCategory { Name = "Second" } } };
      Assert.True(entities.OrderByAudiosCategoryNameDescending().SequenceEqual(entities.Reverse()));
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