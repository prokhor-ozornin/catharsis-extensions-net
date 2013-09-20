using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
    ///   <para>Performs testing of <see cref="BlogEntry.Blog"/> property.</para>
    /// </summary>
    [Fact]
    public void Blog_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new BlogEntry { Blog = null });
      var blog = new Blog();
      Assert.True(ReferenceEquals(new BlogEntry { Blog = blog }.Blog, blog));
    }

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
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == null);
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == null);
      Assert.True(entry.Text == null);
      Assert.True(entry.Blog == null);

      Assert.Throws<ArgumentNullException>(() => new BlogEntry(null));
      entry = new BlogEntry(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Blog", new Blog()));
      Assert.True(entry.Id == "id");
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == "language");
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == "name");
      Assert.True(entry.Text == "text");
      Assert.True(entry.Blog != null);

      Assert.Throws<ArgumentNullException>(() => new BlogEntry(null, "language", "name", "text", new Blog()));
      Assert.Throws<ArgumentNullException>(() => new BlogEntry("id", null, "name", "text", new Blog()));
      Assert.Throws<ArgumentNullException>(() => new BlogEntry("id", "language", null, "text", new Blog()));
      Assert.Throws<ArgumentNullException>(() => new BlogEntry("id", "language", "name", null, new Blog()));
      Assert.Throws<ArgumentNullException>(() => new BlogEntry("id", "language", "name", "text", null));
      Assert.Throws<ArgumentException>(() => new BlogEntry(string.Empty, "language", "name", "text", new Blog()));
      Assert.Throws<ArgumentException>(() => new BlogEntry("id", string.Empty, "name", "text", new Blog()));
      Assert.Throws<ArgumentException>(() => new BlogEntry("id", "language", string.Empty, "text", new Blog()));
      Assert.Throws<ArgumentException>(() => new BlogEntry("id", "language", "name", string.Empty, new Blog()));
      entry = new BlogEntry("id", "language", "name", "text", new Blog());
      Assert.True(entry.Id == "id");
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.DateCreated <= DateTime.UtcNow);
      Assert.True(entry.Language == "language");
      Assert.True(entry.LastUpdated <= DateTime.UtcNow);
      Assert.True(entry.Name == "name");
      Assert.True(entry.Text == "text");
      Assert.True(entry.Blog != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="BlogEntry"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Blog", new[] { new Blog { Name = "Name" }, new Blog { Name = "Name_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="BlogEntry.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="BlogEntry.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => BlogEntry.Xml(null));

      var xml = new XElement("BlogEntry",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
          new XElement("Blog",
            new XElement("Id", "blog.id"),
            new XElement("AuthorId", "blog.authorId"),
            new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
            new XElement("Language", "blog.language"),
            new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
            new XElement("Name", "blog.name")));
      var entry = BlogEntry.Xml(xml);
      Assert.True(entry.Id == "id");
      Assert.True(entry.AuthorId == null);
      Assert.True(entry.Comments.Count == 0);
      Assert.True(entry.Blog.Id == "blog.id");
      Assert.True(entry.Blog.AuthorId == "blog.authorId");
      Assert.True(entry.Blog.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(entry.Blog.Language == "blog.language");
      Assert.True(entry.Blog.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(entry.Blog.Name == "blog.name");
      Assert.True(entry.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(entry.Language == "language");
      Assert.True(entry.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(entry.Name == "name");
      Assert.True(entry.Tags.Count == 0);
      Assert.True(entry.Text == "text");
      Assert.True(new BlogEntry("id", "language", "name", "text", new Blog("blog.id", "blog.language", "blog.name", "blog.authorId") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue} ) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(BlogEntry.Xml(entry.Xml()).Equals(entry));
    }
  }
}