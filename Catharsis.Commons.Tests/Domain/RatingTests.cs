using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Rating"/>.</para>
  /// </summary>
  public sealed class RatingTests : EntityUnitTests<Rating>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Rating { AuthorId = null });
      Assert.Throws<ArgumentException>(() => new Rating { AuthorId = string.Empty });
      
      Assert.True(new Rating { AuthorId = "authorId" }.AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new Rating { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.Item"/> property.</para>
    /// </summary>
    [Fact]
    public void Item_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Rating { Item = null });
      var item = new Item();
      Assert.True(ReferenceEquals(new Rating { Item = item }.Item, item));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new Rating { DateCreated = DateTime.MaxValue }.DateCreated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.Value"/> property.</para>
    /// </summary>
    [Fact]
    public void Value_Property()
    {
      Assert.True(new Rating { Value = 1 }.Value == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Rating()"/>
    ///   <seealso cref="Rating(IDictionary{string, object})"/>
    ///   <seealso cref="Rating(string, Item, byte)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var rating = new Rating();
      Assert.True(rating.Id == 0);
      Assert.True(rating.AuthorId == null);
      Assert.True(rating.DateCreated <= DateTime.UtcNow);
      Assert.True(rating.Item == null);
      Assert.True(rating.LastUpdated <= DateTime.UtcNow);
      Assert.True(rating.Value == 0);

      Assert.Throws<ArgumentNullException>(() => new Rating(null));
      rating = new Rating(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("AuthorId", "authorId")
        .AddNext("Item", new Item())
        .AddNext("Value", (byte) 1));
      Assert.True(rating.Id == 1);
      Assert.True(rating.AuthorId == "authorId");
      Assert.True(rating.DateCreated <= DateTime.UtcNow);
      Assert.True(rating.Item != null);
      Assert.True(rating.LastUpdated <= DateTime.UtcNow);
      Assert.True(rating.Value == 1);

      Assert.Throws<ArgumentNullException>(() => new Rating(null, new Item(), 1));
      Assert.Throws<ArgumentNullException>(() => new Rating("authorId", null, 1));
      Assert.Throws<ArgumentException>(() => new Rating(string.Empty, new Item(), 1));
      rating = new Rating("authorId", new Item(), 1);
      Assert.True(rating.Id == 0);
      Assert.True(rating.AuthorId == "authorId");
      Assert.True(rating.DateCreated <= DateTime.UtcNow);
      Assert.True(rating.Item != null);
      Assert.True(rating.LastUpdated <= DateTime.UtcNow);
      Assert.True(rating.Value == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.CompareTo(Rating)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Rating { Value = 1 }.ToString() == "1");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Rating.Equals(Rating)"/></description></item>
    ///     <item><description><see cref="Rating.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("AuthorId", "AuthorId", "AuthorId_2");
      this.TestEquality("Item", new Item { Name = "Name" }, new Item { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("AuthorId", "AuthorId", "AuthorId_2");
      this.TestHashCode("Item", new Item { Name = "Name" }, new Item { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.CompareTo(Rating)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Rating { Value = 1 }.CompareTo(new Rating { Value = 1 }) == 0);
      Assert.True(new Rating { Value = 1 }.CompareTo(new Rating { Value = 2 }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Rating.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Rating.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Rating.Xml(null));

      var xml = new XElement("Rating",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Item",
          new XElement("Id", 2),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("Language", "item.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "item.name")),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Value", 1));
      var rating = Rating.Xml(xml);
      Assert.True(rating.Id == 1);
      Assert.True(rating.AuthorId == "authorId");
      Assert.True(rating.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(rating.Item.Id == 2);
      Assert.True(rating.Item.Comments.Count == 0);
      Assert.True(rating.Item.AuthorId == null);
      Assert.True(rating.Item.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(rating.Item.Language == "item.language");
      Assert.True(rating.Item.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(rating.Item.Name == "item.name");
      Assert.True(rating.Item.Tags.Count == 0);
      Assert.True(rating.Item.Text == null);
      Assert.True(rating.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(rating.Value == 1);
      Assert.True(new Rating("authorId", new Item("item.language", "item.name") { Id = 2, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Rating.Xml(rating.Xml()).Equals(rating));
    }
  }
}