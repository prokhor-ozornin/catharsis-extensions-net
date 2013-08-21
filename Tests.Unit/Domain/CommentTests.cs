using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
    ///   <para>Performs testing of <see cref="Comment.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Comment { AuthorId = null });
      Assert.Throws<ArgumentException>(() => new Comment { AuthorId = string.Empty });
      Assert.True(new Comment { AuthorId = "authorId" }.AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new Comment { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new Comment { LastUpdated = DateTime.MinValue }.LastUpdated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Comment { Name = null });
      Assert.Throws<ArgumentException>(() => new Comment { Name = string.Empty });
      Assert.True(new Comment { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.Text"/> property.</para>
    /// </summary>
    [Fact]
    public void Text_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Comment { Text = null });
      Assert.Throws<ArgumentException>(() => new Comment { Text = string.Empty });
      Assert.True(new Comment { Text = "text" }.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Comment()"/>
    ///   <seealso cref="Comment(IDictionary{string, object})"/>
    ///   <seealso cref="Comment(string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var comment = new Comment();
      Assert.True(comment.Id == null);
      Assert.True(comment.AuthorId == null);
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == null);
      Assert.True(comment.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Comment(null));
      comment = new Comment(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Item", new Item())
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(comment.Id == "id");
      Assert.True(comment.AuthorId == "authorId");
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == "name");
      Assert.True(comment.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Comment(null, "authorId", "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Comment("id", null, "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Comment("id", "authorId", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Comment("id", "authorId", "name", null));
      Assert.Throws<ArgumentException>(() => new Comment(string.Empty, "authorId", "name", "text"));
      Assert.Throws<ArgumentException>(() => new Comment("id", string.Empty, "name", "text"));
      Assert.Throws<ArgumentException>(() => new Comment("id", "authorId", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Comment("id", "authorId", "name", string.Empty));
      comment = new Comment("id", "authorId", "name", "text");
      Assert.True(comment.Id == "id");
      Assert.True(comment.AuthorId == "authorId");
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
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

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.Equals(object)"/> and <see cref="Comment.GetHashCode()"/> methods.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Comment.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Comment.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Comment.Xml(null));

      var xml = new XElement("Comment",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var comment = Comment.Xml(xml);
      Assert.True(comment.Id == "id");
      Assert.True(comment.AuthorId == "authorId");
      Assert.True(comment.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(comment.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(comment.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(comment.Name == "name");
      Assert.True(comment.Text == "text");
      Assert.True(new Comment("id", "authorId", "name", "text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Comment.Xml(comment.Xml()).Equals(comment));
    }
  }
}