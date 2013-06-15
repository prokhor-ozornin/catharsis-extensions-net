using System;
using System.Collections.Generic;
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
  }
}