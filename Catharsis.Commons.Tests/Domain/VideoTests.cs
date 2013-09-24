using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
    ///   <para>Performs testing of <see cref="Video.Bitrate"/> property.</para>
    /// </summary>
    [Fact]
    public void Bitrate_Property()
    {
      Assert.True(new Video { Bitrate = 1 }.Bitrate == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new VideosCategory();
      Assert.True(ReferenceEquals(new Video { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.Duration"/> property.</para>
    /// </summary>
    [Fact]
    public void Duration_Property()
    {
      Assert.True(new Video { Duration = 1 }.Duration == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.File"/> property.</para>
    /// </summary>
    [Fact]
    public void File_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Video { File = null });
      var file = new File();
      Assert.True(ReferenceEquals(new Video { File = file }.File, file));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.Height"/> property.</para>
    /// </summary>
    [Fact]
    public void Height_Property()
    {
      Assert.True(new Video { Height = 1 }.Height == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.Width"/> property.</para>
    /// </summary>
    [Fact]
    public void Width_Property()
    {
      Assert.True(new Video { Width = 1 }.Width == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Video()"/>
    ///   <seealso cref="Video(IDictionary{string, object})"/>
    ///   <seealso cref="Video(File, short, long, short, short, VideosCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var video = new Video();
      Assert.True(video.Id == 0);
      Assert.True(video.Bitrate == 0);
      Assert.True(video.Category == null);
      Assert.True(video.Duration == 0);
      Assert.True(video.File == null);
      Assert.True(video.Height == 0);
      Assert.True(video.Width == 0);

      Assert.Throws<ArgumentNullException>(() => new Video(null));
      video = new Video(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("Bitrate", (short) 1)
        .AddNext("Category", new VideosCategory())
        .AddNext("Duration", 2)
        .AddNext("File", new File())
        .AddNext("Height", (short) 3)
        .AddNext("Width", (short) 4));
      Assert.True(video.Id == 1);
      Assert.True(video.Bitrate == 1);
      Assert.True(video.Category != null);
      Assert.True(video.Duration == 2);
      Assert.True(video.File != null);
      Assert.True(video.Height == 3);
      Assert.True(video.Width == 4);

      Assert.Throws<ArgumentNullException>(() => new Video(null, 1, 2, 3, 4));
      video = new Video(new File(), 1, 2, 3, 4, new VideosCategory());
      Assert.True(video.Id == 0);
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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Video.Equals(Video)"/></description></item>
    ///     <item><description><see cref="Video.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new VideosCategory { Name = "Name" }, new VideosCategory { Name = "Name_2" });
      this.TestEquality("File", new File { Name = "Name" }, new File { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Video.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new VideosCategory { Name = "Name" }, new VideosCategory { Name = "Name_2" });
      this.TestHashCode("File", new File { Name = "Name" }, new File { Name = "Name_2" });
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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Video.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Video.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Video.Xml(null));

      var xml = new XElement("Video",
        new XElement("Id", 1),
        new XElement("Bitrate", 1),
        new XElement("Duration", 2),
        new XElement("File",
          new XElement("Id", 2),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().Length)),
        new XElement("Height", 10),
        new XElement("Width", 20));
      var video = Video.Xml(xml);
      Assert.True(video.Id == 1);
      Assert.True(video.Bitrate == 1);
      Assert.True(video.Category == null);
      Assert.True(video.Duration == 2);
      Assert.True(video.File.Id == 2);
      Assert.True(video.File.ContentType == "file.contentType");
      Assert.True(video.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(video.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(video.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(video.File.Name == "file.name");
      Assert.True(video.File.OriginalName == "file.originalName");
      Assert.True(video.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(video.Height == 10);
      Assert.True(video.Width == 20);
      Assert.True(new Video(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 2, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, 10, 20) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Video.Xml(video.Xml()).Equals(video));

      xml = new XElement("Video",
        new XElement("Id", 1),
        new XElement("Bitrate", 1),
        new XElement("VideosCategory",
          new XElement("Id", 2),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Duration", 2),
        new XElement("File",
          new XElement("Id", 3),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().Length)),
        new XElement("Height", 10),
        new XElement("Width", 20));
      video = Video.Xml(xml);
      Assert.True(video.Id == 1);
      Assert.True(video.Bitrate == 1);
      Assert.True(video.Category.Id == 2);
      Assert.True(video.Category.Language == "category.language");
      Assert.True(video.Category.Name == "category.name");
      Assert.True(video.Duration == 2);
      Assert.True(video.File.Id == 3);
      Assert.True(video.File.ContentType == "file.contentType");
      Assert.True(video.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(video.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(video.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(video.File.Name == "file.name");
      Assert.True(video.File.OriginalName == "file.originalName");
      Assert.True(video.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(video.Height == 10);
      Assert.True(video.Width == 20);
      Assert.True(new Video(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, 10, 20, new VideosCategory("category.language", "category.name") { Id = 2 }) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Video.Xml(video.Xml()).Equals(video));
    }
  }
}