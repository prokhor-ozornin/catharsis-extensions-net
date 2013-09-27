using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Audio"/>.</para>
  /// </summary>
  public sealed class AudioTests : EntityUnitTests<Audio>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.Bitrate"/> property.</para>
    /// </summary>
    [Fact]
    public void Bitrate_Property()
    {
      Assert.True(new Audio { Bitrate = 1 }.Bitrate == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new AudiosCategory();
      Assert.True(ReferenceEquals(new Audio { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.Duration"/> property.</para>
    /// </summary>
    [Fact]
    public void Duration_Property()
    {
      Assert.True(new Audio { Duration = 1 }.Duration == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.File"/> property.</para>
    /// </summary>
    [Fact]
    public void File_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Audio { File = null });
      var file = new File();
      Assert.True(ReferenceEquals(new Audio { File = file }.File, file));
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Audio()"/>
    ///   <seealso cref="Audio(File, short, short, AudiosCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var audio = new Audio();
      Assert.True(audio.Id == 0);
      Assert.True(audio.Bitrate == 0);
      Assert.True(audio.Category == null);
      Assert.True(audio.Duration == 0);
      Assert.True(audio.File == null);

      Assert.Throws<ArgumentNullException>(() => new Audio(null, 1, 2));
      audio = new Audio(new File(), 1, 2, new AudiosCategory());
      Assert.True(audio.Id == 0);
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category != null);
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Audio { File = new File { Name = "Name" } }.ToString() == "Name");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Audio.Equals(Audio)"/></description></item>
    ///     <item><description><see cref="Audio.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new AudiosCategory { Name = "Name" }, new AudiosCategory { Name = "Name_2" });
      this.TestEquality("File", new File { Name = "Name" }, new File { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new AudiosCategory { Name = "Name" }, new AudiosCategory { Name = "Name_2" });
      this.TestHashCode("File", new File { Name = "Name" }, new File { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Audio.CompareTo(Audio)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Audio { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Audio { File = new File { DateCreated = new DateTime(2000, 1, 1) } }) == 0);
      Assert.True(new Audio { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Audio { File = new File { DateCreated = new DateTime(2000, 1, 2) } }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Audio.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Audio.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Audio.Xml(null));

      var xml = new XElement("Audio",
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
          new XElement("Size", Guid.Empty.ToByteArray().Length)));
      var audio = Audio.Xml(xml);
      Assert.True(audio.Id == 1);
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category == null);
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File.Id == 2);
      Assert.True(audio.File.ContentType == "file.contentType");
      Assert.True(audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(audio.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(audio.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(audio.File.Name == "file.name");
      Assert.True(audio.File.OriginalName == "file.originalName");
      Assert.True(audio.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(new Audio(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 2, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Audio.Xml(audio.Xml()).Equals(audio));

      xml = new XElement("Audio",
        new XElement("Id", 1),
        new XElement("Bitrate", 1),
        new XElement("AudiosCategory",
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
          new XElement("Size", Guid.Empty.ToByteArray().Length)));
      audio = Audio.Xml(xml);
      Assert.True(audio.Id == 1);
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category.Id == 2);
      Assert.True(audio.Category.Language == "category.language");
      Assert.True(audio.Category.Name == "category.name");
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File.Id == 3);
      Assert.True(audio.File.ContentType == "file.contentType");
      Assert.True(audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(audio.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(audio.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(audio.File.Name == "file.name");
      Assert.True(audio.File.OriginalName == "file.originalName");
      Assert.True(audio.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(new Audio(new File("file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, new AudiosCategory("category.language", "category.name") { Id = 2 }) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Audio.Xml(audio.Xml()).Equals(audio));
    }
  }
}