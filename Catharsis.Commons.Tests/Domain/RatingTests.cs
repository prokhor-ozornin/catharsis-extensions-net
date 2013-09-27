using System;
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
      Assert.True(new Rating { AuthorId = 1 }.AuthorId == 1);
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
    ///   <seealso cref="Rating(long, Item, byte)"/>
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

      Assert.Throws<ArgumentNullException>(() => new Rating(1, null, 1));
      rating = new Rating(1, new Item(), 1);
      Assert.True(rating.Id == 0);
      Assert.True(rating.AuthorId == 1);
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
      this.TestEquality("AuthorId", (long) 1, (long) 2);
      this.TestEquality("Item", new Item { Name = "Name" }, new Item { Name = "Name_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Rating.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("AuthorId", (long) 1, (long) 2);
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
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Item",
          new XElement("Id", 3),
          new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
          new XElement("Language", "item.language"),
          new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
          new XElement("Name", "item.name")),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Value", 1));
      var rating = Rating.Xml(xml);
      Assert.True(rating.Id == 1);
      Assert.True(rating.AuthorId == 2);
      Assert.True(rating.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(rating.Item.Id == 3);
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
      Assert.True(new Rating(2, new Item("item.language", "item.name") { Id = 3, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Rating.Xml(rating.Xml()).Equals(rating));
    }
  }
}