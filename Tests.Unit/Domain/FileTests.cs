using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="File"/>.</para>
  /// </summary>
  public sealed class FileTests : EntityUnitTests<File>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="File()"/>
    ///   <seealso cref="File(IDictionary{string, object})"/>
    ///   <seealso cref="File(string, string, string, string, byte[], string, long)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var file = new File();
      Assert.True(file.Id == null);
      Assert.True(file.ContentType == null);
      Assert.True(file.Data == null);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.Hash == null);
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == null);
      Assert.True(file.OriginalName == null);
      Assert.True(file.Size == 0);
      Assert.True(file.Tags.Count == 0);

      var data = new byte[] { 1, 2, 3 };
      file = new File(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("ContentType", "contentType")
        .AddNext("Data", data)
        .AddNext("Hash", "hash")
        .AddNext("Name", "name")
        .AddNext("OriginalName", "originalName")
        .AddNext("Size", 1));
      Assert.True(file.Id == "id");
      Assert.True(file.ContentType == "contentType");
      Assert.True(file.Data == data);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.Hash == "hash");
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == "name");
      Assert.True(file.OriginalName == "originalName");
      Assert.True(file.Size == 1);
      Assert.True(file.Tags.Count == 0);

      file = new File("id", "contentType", "name", "originalName", data, "hash", 1);
      Assert.True(file.Id == "id");
      Assert.True(file.ContentType == "contentType");
      Assert.True(file.Data == data);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.Hash == "hash");
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == "name");
      Assert.True(file.OriginalName == "originalName");
      Assert.True(file.Size == 1);
      Assert.True(file.Tags.Count == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new File { Name = "name" }.ToString() == "name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Hash", new[] { "Hash", "Hash_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.CompareTo(File)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new File { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new File { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new File { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new File { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }
  }
}