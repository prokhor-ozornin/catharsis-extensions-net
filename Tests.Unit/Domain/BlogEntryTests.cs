using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="BlogEntry"/>.</para>
  /// </summary>
  public sealed class BlogEntryTests : EntityUnitTests<BlogEntry>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="BlogEntry()"/>
    ///   <seealso cref="BlogEntry(IDictionary{string, object})"/>
    ///   <seealso cref="BlogEntry(string, string, string, string, Blog)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var entry = new BlogEntry();
      Assert.True(entry.Id == null);
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.Comments.Count == 0);
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == null);
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == null);
      Assert.True(entry.Tags.Count == 0);
      Assert.True(entry.Text == null);
      Assert.True(entry.Blog == null);

      entry = new BlogEntry(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Blog", new Blog()));
      Assert.True(entry.Id == "id");
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.Comments.Count == 0);
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == "language");
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == "name");
      Assert.True(entry.Tags.Count == 0);
      Assert.True(entry.Text == "text");
      Assert.True(entry.Blog != null);

      entry = new BlogEntry("id", "language", "name", "text", new Blog());
      Assert.True(entry.Id == "id");
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.Comments.Count == 0);
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == "language");
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == "name");
      Assert.True(entry.Tags.Count == 0);
      Assert.True(entry.Text == "text");
      Assert.True(entry.Blog != null);
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Blog", new[] { new Blog { Name = "Name" }, new Blog { Name = "Name_2" } }));
    }
  }
}