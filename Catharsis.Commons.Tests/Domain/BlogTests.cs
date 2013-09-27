using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
    ///   <seealso cref="Blog(string, string, long)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var blog = new Blog();
      Assert.True(blog.Id == 0);
      Assert.True(blog.AuthorId == null);
      Assert.True(blog.DateCreated <= DateTime.UtcNow);
      Assert.True(blog.Language == null);
      Assert.True(blog.LastUpdated <= DateTime.UtcNow);
      Assert.True(blog.Name == null);
      Assert.True(blog.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Blog(null, "name", 1));
      Assert.Throws<ArgumentNullException>(() => new Blog("language", null, 1));
      Assert.Throws<ArgumentException>(() => new Blog(string.Empty, "name", 1));
      Assert.Throws<ArgumentException>(() => new Blog("language", string.Empty, 1));
      blog = new Blog("language", "name", 1);
      Assert.True(blog.Id == 0);
      Assert.True(blog.AuthorId == 1);
      Assert.True(blog.DateCreated <= DateTime.UtcNow);
      Assert.True(blog.Language == "language");
      Assert.True(blog.LastUpdated <= DateTime.UtcNow);
      Assert.True(blog.Name == "name");
      Assert.True(blog.Text == null);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Blog.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Blog.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Blog.Xml(null));

      var xml = new XElement("Blog",
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"));
      var blog = Blog.Xml(xml);
      Assert.True(blog.Id == 1);
      Assert.True(blog.AuthorId == 2);
      Assert.True(blog.Comments.Count == 0);
      Assert.True(blog.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(blog.Language == "language");
      Assert.True(blog.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(blog.Name == "name");
      Assert.True(blog.Tags.Count == 0);
      Assert.True(blog.Text == null);
      Assert.True(new Blog("language", "name", 2) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Item.Xml(blog.Xml()).Equals(blog));
    }
  }
}