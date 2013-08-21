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
    ///   <seealso cref="Audio(IDictionary{string, object})"/>
    ///   <seealso cref="Audio(string, File, short, short, AudiosCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var audio = new Audio();
      Assert.True(audio.Id == null);
      Assert.True(audio.Bitrate == 0);
      Assert.True(audio.Category == null);
      Assert.True(audio.Duration == 0);
      Assert.True(audio.File == null);

      Assert.Throws<ArgumentNullException>(() => new Audio(null));
      audio = new Audio(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Bitrate", (short) 1)
        .AddNext("Category", new AudiosCategory())
        .AddNext("Duration", (short) 2)
        .AddNext("File", new File()));
      Assert.True(audio.Id == "id");
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category != null);
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File != null);
      
      Assert.Throws<ArgumentNullException>(() => new Audio(null, new File(), 1, 2));
      Assert.Throws<ArgumentNullException>(() => new Audio("id", null, 1, 2));
      Assert.Throws<ArgumentException>(() => new Audio(string.Empty, new File(), 1, 2));
      audio = new Audio("id", new File(), 1, 2, new AudiosCategory());
      Assert.True(audio.Id == "id");
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
    ///   <para>Performs testing of <see cref="Audio.Equals(object)"/> and <see cref="Audio.GetHashCode()"/> methods.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new AudiosCategory { Name = "Name" }, new AudiosCategory { Name = "Name_2" } })
        .AddNext("File", new[] { new File { Hash = "Hash" }, new File { Hash = "Hash_2" } }));
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
        new XElement("Id", "id"),
        new XElement("Bitrate", 1),
        new XElement("Duration", 2),
        new XElement("File",
          new XElement("Id", "file.id"),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().LongLength)));
      var audio = Audio.Xml(xml);
      Assert.True(audio.Id == "id");
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category == null);
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File.Id == "file.id");
      Assert.True(audio.File.ContentType == "file.contentType");
      Assert.True(audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(audio.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(audio.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(audio.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(audio.File.Name == "file.name");
      Assert.True(audio.File.OriginalName == "file.originalName");
      Assert.True(audio.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(new Audio("id", new File("file.id", "file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2).Xml().ToString() == xml.ToString());
      Assert.True(Audio.Xml(audio.Xml()).Equals(audio));

      xml = new XElement("Audio",
        new XElement("Id", "id"),
        new XElement("Bitrate", 1),
        new XElement("AudiosCategory",
          new XElement("Id", "category.id"),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Duration", 2),
        new XElement("File",
          new XElement("Id", "file.id"),
          new XElement("ContentType", "file.contentType"),
          new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "file.name"),
          new XElement("OriginalName", "file.originalName"),
          new XElement("Size", Guid.Empty.ToByteArray().LongLength)));
      audio = Audio.Xml(xml);
      Assert.True(audio.Id == "id");
      Assert.True(audio.Bitrate == 1);
      Assert.True(audio.Category.Id == "category.id");
      Assert.True(audio.Category.Language == "category.language");
      Assert.True(audio.Category.Name == "category.name");
      Assert.True(audio.Duration == 2);
      Assert.True(audio.File.Id == "file.id");
      Assert.True(audio.File.ContentType == "file.contentType");
      Assert.True(audio.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(audio.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(audio.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(audio.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(audio.File.Name == "file.name");
      Assert.True(audio.File.OriginalName == "file.originalName");
      Assert.True(audio.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(new Audio("id", new File("file.id", "file.contentType", "file.name", "file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2, new AudiosCategory("category.id", "category.language", "category.name")).Xml().ToString() == xml.ToString());
      Assert.True(Audio.Xml(audio.Xml()).Equals(audio));
    }
  }
}