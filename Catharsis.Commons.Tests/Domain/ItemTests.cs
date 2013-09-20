using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Item"/>.</para>
  /// </summary>
  public sealed class ItemTests : EntityUnitTests<Item>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Item.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.True(new Item { AuthorId = "authorId" }.AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.Comments"/> property.</para>
    /// </summary>
    [Fact]
    public void Comments_Property()
    {
      var comment = new Comment();
      var item = new Item();
      Assert.True(item.Comments.Count == 0);
      item.Comments.Add(comment);
      Assert.True(item.Comments.Count == 1);
      Assert.True(ReferenceEquals(item.Comments.Single(), comment));
      item.Comments.Add(comment);
      Assert.True(item.Comments.Count == 2);
      item.Comments.Add(new Comment());
      Assert.True(item.Comments.Count == 3);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new Item { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.Language"/> property.</para>
    /// </summary>
    [Fact]
    public void Language_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Item { Language = null });
      Assert.Throws<ArgumentException>(() => new Item { Language = string.Empty });
      Assert.True(new Item { Language = "language" }.Language == "language");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new Item { LastUpdated = DateTime.MaxValue }.LastUpdated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Item { Name = null });
      Assert.Throws<ArgumentException>(() => new Item { Name = string.Empty });
      Assert.True(new Item { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.Tags"/> property.</para>
    /// </summary>
    [Fact]
    public void Tags_Property()
    {
      var item = new Item();
      Assert.True(item.Tags.Count == 0);
      item.Tags.Add("tag");
      Assert.True(item.Tags.Count == 1);
      Assert.True(item.Tags.Single() == "tag");
      item.Tags.Add("tag");
      Assert.True(item.Tags.Count == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.Text"/> property.</para>
    /// </summary>
    [Fact]
    public void Text_Property()
    {
      Assert.True(new Item { Text = "text" }.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Item()"/>
    ///   <seealso cref="Item(IDictionary{string, object})"/>
    ///   <seealso cref="Item(string, string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var item = new Item();
      Assert.True(item.Id == null);
      Assert.True(item.AuthorId == null);
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == null);
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == null);
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Item(null));
      item = new Item(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(item.Id == "id");
      Assert.True(item.AuthorId == "authorId");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Item(null, "language", "name"));
      Assert.Throws<ArgumentNullException>(() => new Item("id", null, "name"));
      Assert.Throws<ArgumentNullException>(() => new Item("id", "language", null));
      Assert.Throws<ArgumentException>(() => new Item(string.Empty, "language", "name"));
      Assert.Throws<ArgumentException>(() => new Item("id", string.Empty, "name"));
      Assert.Throws<ArgumentException>(() => new Item("id", "language", string.Empty));
      item = new Item("id", "language", "name", "text", "authorId");
      Assert.True(item.Id == "id");
      Assert.True(item.AuthorId == "authorId");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated <= DateTime.UtcNow);
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated <= DateTime.UtcNow);
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Item { Name = "Name" }.ToString() == "Name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Item"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
        .AddNext("Language", new[] { "Language", "Language_2" })
        .AddNext("Name", new[] { "Name", "Name_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Item.CompareTo(Item)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Item { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Item { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new Item { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Item { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Item.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Item.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Item.Xml(null));

      var xml = new XElement("Item",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"));
      var item = Item.Xml(xml);
      Assert.True(item.Id == "id");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.AuthorId == null);
      Assert.True(item.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == null);
      Assert.True(new Item("id", "language", "name") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Item.Xml(item.Xml()).Equals(item));

      xml = new XElement("Item",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      item = Item.Xml(xml);
      Assert.True(item.Id == "id");
      Assert.True(item.AuthorId == "authorId");
      Assert.True(item.Comments.Count == 0);
      Assert.True(item.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(item.Language == "language");
      Assert.True(item.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(item.Name == "name");
      Assert.True(item.Tags.Count == 0);
      Assert.True(item.Text == "text");
      Assert.True(new Item("id", "language", "name", "text", "authorId") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Item.Xml(item.Xml()).Equals(item));
    }
  }
}