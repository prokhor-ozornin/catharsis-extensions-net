using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Blog"/>.</para>
  /// </summary>
  public sealed class BlogTests : EntityUnitTests<Blog>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Blog()"/>
    ///   <seealso cref="Blog(IDictionary{string, object})"/>
    ///   <seealso cref="Blog(string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var blog = new Blog();
      Assert.True(blog.Id == null);
      Assert.True(blog.AuthorId == null);
      Assert.True(blog.Comments.Count == 0);
      Assert.True(blog.DateCreated <= DateTime.UtcNow);
      Assert.True(blog.Language == null);
      Assert.True(blog.LastUpdated <= DateTime.UtcNow);
      Assert.True(blog.Name == null);
      Assert.True(blog.Tags.Count == 0);
      Assert.True(blog.Text == null);

      blog = new Blog(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name"));
      Assert.True(blog.Id == "id");
      Assert.True(blog.AuthorId == "authorId");
      Assert.True(blog.Comments.Count == 0);
      Assert.True(blog.DateCreated <= DateTime.UtcNow);
      Assert.True(blog.Language == "language");
      Assert.True(blog.LastUpdated <= DateTime.UtcNow);
      Assert.True(blog.Name == "name");
      Assert.True(blog.Tags.Count == 0);
      Assert.True(blog.Text == null);

      blog = new Blog("id", "language", "name", "authorId");
      Assert.True(blog.Id == "id");
      Assert.True(blog.AuthorId == "authorId");
      Assert.True(blog.Comments.Count == 0);
      Assert.True(blog.DateCreated <= DateTime.UtcNow);
      Assert.True(blog.Language == "language");
      Assert.True(blog.LastUpdated <= DateTime.UtcNow);
      Assert.True(blog.Name == "name");
      Assert.True(blog.Tags.Count == 0);
      Assert.True(blog.Text == null);
    }
  }
}