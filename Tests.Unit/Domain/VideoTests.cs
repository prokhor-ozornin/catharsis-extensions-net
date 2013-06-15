using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Video"/>.</para>
  /// </summary>
  public sealed class VideoTests : EntityUnitTests<Video>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Video()"/>
    ///   <seealso cref="Video(IDictionary{string, object})"/>
    ///   <seealso cref="Video(string, File, short, long, short, short, VideosCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var video = new Video();
      Assert.True(video.Id == null);
      Assert.True(video.Bitrate == 0);
      Assert.True(video.Category == null);
      Assert.True(video.Duration == 0);
      Assert.True(video.File == null);
      Assert.True(video.Height == 0);
      Assert.True(video.Width == 0);

      video = new Video(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Bitrate", (short) 1)
        .AddNext("Category", new VideosCategory())
        .AddNext("Duration", 2)
        .AddNext("File", new File())
        .AddNext("Height", (short) 3)
        .AddNext("Width", (short) 4));
      Assert.True(video.Id == "id");
      Assert.True(video.Bitrate == 1);
      Assert.True(video.Category != null);
      Assert.True(video.Duration == 2);
      Assert.True(video.File != null);
      Assert.True(video.Height == 3);
      Assert.True(video.Width == 4);

      video = new Video("id", new File(), 1, 2, 3, 4, new VideosCategory());
      Assert.True(video.Id == "id");
      Assert.True(video.Bitrate == 1);
      Assert.True(video.Category != null);
      Assert.True(video.Duration == 2);
      Assert.True(video.File != null);
      Assert.True(video.Height == 3);
      Assert.True(video.Width == 4);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Video { File = new File { Name = "Name" } }.ToString() == "Name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new VideosCategory { Name = "Name" }, new VideosCategory { Name = "Name_2" } })
        .AddNext("File", new[] { new File { Hash = "Hash" }, new File { Hash = "Hash_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.CompareTo(Video)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Video { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Video { File = new File { DateCreated = new DateTime(2000, 1, 1) } }) == 0);
      Assert.True(new Video { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Video { File = new File { DateCreated = new DateTime(2000, 1, 2) } }) < 0);
    }
  }
}