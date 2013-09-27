using System;
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
      Assert.True(new Comment { AuthorId = 1 }.AuthorId == 1);
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
    ///   <seealso cref="Comment(long, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var comment = new Comment();
      Assert.True(comment.Id == 0);
      Assert.True(comment.AuthorId == null);
      Assert.True(comment.DateCreated <= DateTime.UtcNow);
      Assert.True(comment.LastUpdated <= DateTime.UtcNow);
      Assert.True(comment.Name == null);
      Assert.True(comment.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Comment(1, null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Comment(1, "name", null));
      Assert.Throws<ArgumentException>(() => new Comment(1, string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Comment(1, "name", string.Empty));
      comment = new Comment(1, "name", "text");
      Assert.True(comment.Id == 0);
      Assert.True(comment.AuthorId == 1);
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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Comment.Equals(Comment)"/></description></item>
    ///     <item><description><see cref="Comment.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("AuthorId", (long) 1, (long) 2);
      this.TestEquality("Name", "Name", "Name_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Comment.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("AuthorId", (long) 1, (long) 2);
      this.TestHashCode("Name", "Name", "Name_2");
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
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var comment = Comment.Xml(xml);
      Assert.True(comment.Id == 1);
      Assert.True(comment.AuthorId == 2);
      Assert.True(comment.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(comment.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(comment.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(comment.Name == "name");
      Assert.True(comment.Text == "text");
      Assert.True(new Comment(2, "name", "text") { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Comment.Xml(comment.Xml()).Equals(comment));
    }
  }
}