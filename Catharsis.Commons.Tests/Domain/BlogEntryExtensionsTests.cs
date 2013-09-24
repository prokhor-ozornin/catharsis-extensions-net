using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="BlogEntryExtensions"/>.</para>
  /// </summary>
  public sealed class BlogEntryExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="BlogEntryExtensions.InBlog(IEnumerable{BlogEntry}, Blog)"/> method.</para>
    /// </summary>
    [Fact]
    public void InBlog_Method()
    {
      Assert.Throws<ArgumentNullException>(() => BlogEntryExtensions.InBlog(null, new Blog()));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<BlogEntry>().InBlog(null));

      Assert.False(Enumerable.Empty<BlogEntry>().InBlog(new Blog()).Any());
      Assert.True(new[] { null, new BlogEntry { Blog = new Blog { Id = 1 } }, null, new BlogEntry { Blog = new Blog { Id = 2 } } }.InBlog(new Blog { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="BlogEntryExtensions.OrderByBlogName(IEnumerable{BlogEntry})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBlogName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => BlogEntryExtensions.OrderByBlogName(null));
      Assert.Throws<NullReferenceException>(() => new BlogEntry[] { null }.OrderByBlogName().Any());

      var entries = new[] { new BlogEntry { Blog = new Blog { Name = "Second" } }, new BlogEntry { Blog = new Blog { Name = "First" } } };
      Assert.True(entries.OrderByBlogName().SequenceEqual(entries.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="BlogEntryExtensions.OrderByBlogNameDescending(IEnumerable{BlogEntry})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByBlogNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => BlogEntryExtensions.OrderByBlogNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new BlogEntry[] { null }.OrderByBlogNameDescending().Any());

      var entries = new[] { new BlogEntry { Blog = new Blog { Name = "First" } }, new BlogEntry { Blog = new Blog { Name = "Second" } } };
      Assert.True(entries.OrderByBlogNameDescending().SequenceEqual(entries.Reverse()));
    }
  }
}