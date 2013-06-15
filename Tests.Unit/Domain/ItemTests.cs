using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Item"/>.</para>
  /// </summary>
  public sealed class ItemTests : EntityUnitTests<Item>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Item()"/>
    ///   <seealso cref="Item(IDictionary{string, object})"/>
    ///   <seealso cref="Item(string, string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var item = new Item();
      Assert.True(item.Id == null);
      Assert.True(item.AuthorId == null);
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == null);
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == null);
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == null);

      item = new Blog(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(item.Id == "id");
      Assert.True(item.AuthorId == "authorId");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);

      item = new Item("id", "language", "name", "text", "authorId");
      Assert.True(item.Id == "id");
      Assert.True(item.AuthorId == "authorId");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);
    }

    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Item { Name = "Name" }.ToString() == "Name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
        .AddNext("Language", new[] { "Language", "Language_2" })
        .AddNext("Name", new[] { "Name", "Name_2" }));
    }

    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Item { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Item { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new Item { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Item { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }
  }
}