using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Comment"/>.</para>
  /// </summary>
  public sealed class CommentTests : EntityUnitTests<Comment>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Comment()"/>
    ///   <seealso cref="Comment(IDictionary{string, object})"/>
    ///   <seealso cref="Comment(string, string, Item, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var comment = new Comment();
      Assert.True(comment.Id == null);
      Assert.True(comment.AuthorId == null);
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.Item == null);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == null);
      Assert.True(comment.Text == null);

      comment = new Comment(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Item", new Item())
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(comment.Id == "id");
      Assert.True(comment.AuthorId == "authorId");
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.Item != null);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == "name");
      Assert.True(comment.Text == "text");

      comment = new Comment("id", "authorId", new Item(), "name", "text");
      Assert.True(comment.Id == "id");
      Assert.True(comment.AuthorId == "authorId");
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.Item != null);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == "name");
      Assert.True(comment.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Comment { Name = "name" }.ToString() == "name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
        .AddNext("Item", new[] { new Item { Name = "Name" }, new Item { Name = "Name_2" }})
        .AddNext("Name", new[] { "Name", "Name_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.CompareTo(Comment)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Comment { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Comment { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new Comment { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Comment { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }
  }
}