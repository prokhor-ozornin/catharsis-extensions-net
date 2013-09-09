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
    ///   <seealso cref="Rating(string, string, Item, byte)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var rating = new Rating();
      Assert.True(rating.Id == null);
      Assert.True(rating.AuthorId == null);
      Assert.True(rating.DateCreated <= DateTime.UtcNow);
      Assert.True(rating.Item == null);
      Assert.True(rating.LastUpdated <= DateTime.UtcNow);
      Assert.True(rating.Value == 0);

      Assert.Throws<ArgumentNullException>(() => new Rating(null));
      rating = new Rating(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Item", new Item())
        .AddNext("Value", (byte) 1));
      Assert.True(rating.Id == "id");
      Assert.True(rating.AuthorId == "authorId");
      Assert.True(rating.DateCreated <= DateTime.UtcNow);
      Assert.True(rating.Item != null);
      Assert.True(rating.LastUpdated <= DateTime.UtcNow);
      Assert.True(rating.Value == 1);

      Assert.Throws<ArgumentNullException>(() => new Rating(null, "authorId", new Item(), 1));
      Assert.Throws<ArgumentNullException>(() => new Rating("id", null, new Item(), 1));
      Assert.Throws<ArgumentNullException>(() => new Rating("id", "authorId", null, 1));
      Assert.Throws<ArgumentException>(() => new Rating(string.Empty, "authorId", new Item(), 1));
      Assert.Throws<ArgumentException>(() => new Rating("id", string.Empty, new Item(), 1));
      rating = new Rating("id", "authorId", new Item(), 1);
      Assert.True(rating.Id == "id");
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
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Rating"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
        .AddNext("Item", new[] { new Item { Name = "Name" }, new Item { Name = "Name_2" } }));
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
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Item",
          new XElement("Id", "item.id"),
          new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
          new XElement("Language", "item.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
          new XElement("Name", "item.name")),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Value", 1));
      var rating = Rating.Xml(xml);
      Assert.True(rating.Id == "id");
      Assert.True(rating.AuthorId == "authorId");
      Assert.True(rating.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(rating.Item.Id == "item.id");
      Assert.True(rating.Item.Comments.Count == 0);
      Assert.True(rating.Item.AuthorId == null);
      Assert.True(rating.Item.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(rating.Item.Language == "item.language");
      Assert.True(rating.Item.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(rating.Item.Name == "item.name");
      Assert.True(rating.Item.Tags.Count == 0);
      Assert.True(rating.Item.Text == null);
      Assert.True(rating.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(rating.Value == 1);
      Assert.True(new Rating("id", "authorId", new Item("item.id", "item.language", "item.name") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Rating.Xml(rating.Xml()).Equals(rating));
    }
  }
}