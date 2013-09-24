using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="VideoExtensions"/>.</para>
  /// </summary>
  public sealed class VideoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.InVideosCategory(IEnumerable{Video}, VideosCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InVideosCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.InVideosCategory(null, new VideosCategory()));

      Assert.False(Enumerable.Empty<Video>().InVideosCategory(null).Any());
      Assert.False(Enumerable.Empty<Video>().InVideosCategory(new VideosCategory()).Any());
      Assert.True(new[] { null, new Video { Category = new VideosCategory { Id = 1 } }, null, new Video { Category = new VideosCategory { Id = 2 } } }.InVideosCategory(new VideosCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByVideosCategoryName(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByVideosCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByVideosCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByVideosCategoryName().Any());
      var entities = new[] { new Video { Category = new VideosCategory { Name = "Second" } }, new Video { Category = new VideosCategory { Name = "First" } } };
      Assert.True(entities.OrderByVideosCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByVideosCategoryNameDescending(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByVideosCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByVideosCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByVideosCategoryNameDescending().Any());
      var entities = new[] { new Video { Category = new VideosCategory { Name = "First" } }, new Video { Category = new VideosCategory { Name = "Second" } } };
      Assert.True(entities.OrderByVideosCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.WithBitrate(IEnumerable{Video}, short)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithBitrate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.WithBitrate(null, 0));

      Assert.False(Enumerable.Empty<Video>().WithBitrate(0).Any());
      Assert.True(new[] { null, new Video { Bitrate = 1 }, null, new Video { Bitrate = 2 } }.WithBitrate(1).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByBitrate(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBitrate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByBitrate(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByBitrate().Any());
      var entities = new[] { new Video { Bitrate = 2 }, new Video { Bitrate = 1 } };
      Assert.True(entities.OrderByBitrate().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByBitrateDescending(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBitrateDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByBitrateDescending(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByBitrateDescending().Any());
      var entities = new[] { new Video { Bitrate = 1 }, new Video { Bitrate = 2 } };
      Assert.True(entities.OrderByBitrateDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.WithDuration(IEnumerable{Video}, long?, long?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithDuration_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.WithDuration(null));

      Assert.False(Enumerable.Empty<Video>().WithDuration(0, 0).Any());

      var videos = new[] { null, new Video { Duration = 1 }, null, new Video { Duration = 2 } };
      Assert.False(videos.WithDuration(0, 0).Any());
      Assert.True(videos.WithDuration(0, 1).Count() == 1);
      Assert.True(videos.WithDuration(1, 1).Count() == 1);
      Assert.True(videos.WithDuration(1, 2).Count() == 2);
      Assert.True(videos.WithDuration(2, 3).Count() == 1);
      Assert.False(videos.WithDuration(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByDuration(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDuration_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByDuration(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByDuration().Any());
      var entities = new[] { new Video { Duration = 2 }, new Video { Duration = 1 } };
      Assert.True(entities.OrderByDuration().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="VideoExtensions.OrderByDurationDescending(IEnumerable{Video})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDurationDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideoExtensions.OrderByDurationDescending(null));

      Assert.Throws<NullReferenceException>(() => new Video[] { null }.OrderByDurationDescending().Any());
      var entities = new[] { new Video { Duration = 1 }, new Video { Duration = 2 } };
      Assert.True(entities.OrderByDurationDescending().SequenceEqual(entities.Reverse()));
    }
  }
}