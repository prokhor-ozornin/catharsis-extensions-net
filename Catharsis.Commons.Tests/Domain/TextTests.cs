using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Text"/>.</para>
  /// </summary>
  public sealed class TextTests : EntityUnitTests<Text>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Text.Category"/> property.</para>
    /// </summary>
    [Fact]
    public void Category_Property()
    {
      var category = new TextsCategory();
      Assert.True(ReferenceEquals(new Text { Category = category }.Category, category));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Text.Person"/> property.</para>
    /// </summary>
    [Fact]
    public void Person_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Text { Person = null });
      var person = new Person();
      Assert.True(ReferenceEquals(new Text { Person = person }.Person, person));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Text.Translations"/> property.</para>
    /// </summary>
    [Fact]
    public void Translations_Property()
    {
      var translation = new TextTranslation();
      var text = new Text();
      Assert.True(text.Translations.Count == 0);
      text.Translations.Add(translation);
      Assert.True(text.Translations.Count == 1);
      Assert.True(ReferenceEquals(text.Translations.Single(), translation));
      text.Translations.Add(translation);
      Assert.True(text.Translations.Count == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Text()"/>
    ///   <seealso cref="Text(long, string, string, string, Person, TextsCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var text = new Text();
      Assert.True(text.Id == 0);
      Assert.True(text.AuthorId == null);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == null);
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == null);
      Assert.True(text.Text == null);
      Assert.True(text.Category == null);
      Assert.True(text.Person == null);

      Assert.Throws<ArgumentNullException>(() => new Text(1, null, "name", "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text(1, "language", null, "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text(1, "language", "name", null, new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text(1, "language", "name", "text", null));
      Assert.Throws<ArgumentException>(() => new Text(1, string.Empty, "name", "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text(1, "language", string.Empty, "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text(1, "language", "name", string.Empty, new Person()));
      text = new Text(1, "language", "name", "text", new Person(), new TextsCategory());
      Assert.True(text.Id == 0);
      Assert.True(text.AuthorId == 1);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == "name");
      Assert.True(text.Text == "text");
      Assert.True(text.Category != null);
      Assert.True(text.Person != null);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Text.Equals(Text)"/></description></item>
    ///     <item><description><see cref="Text.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Category", new TextsCategory { Name = "Name" }, new TextsCategory { Name = "Name_2" });
      this.TestEquality("Person", new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Text.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Category", new TextsCategory { Name = "Name" }, new TextsCategory { Name = "Name_2" });
      this.TestHashCode("Person", new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" });
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Text.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Text.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Text.Xml(null));

      var xml = new XElement("Text",
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Person",
          new XElement("Id", 3),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")));
      var text = Text.Xml(xml);
      Assert.True(text.Id == 1);
      Assert.True(text.AuthorId == 2);
      Assert.True(text.Category == null);
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(text.Name == "name");
      Assert.True(text.Person.Id == 3);
      Assert.True(text.Person.NameFirst == "person.nameFirst");
      Assert.True(text.Person.NameLast == "person.nameLast");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(new Text(2, "language", "name", "text", new Person("person.nameFirst", "person.nameLast") { Id = 3 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Text.Xml(text.Xml()).Equals(text));

      xml = new XElement("Text",
        new XElement("Id", 1),
        new XElement("AuthorId", 2),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("TextsCategory",
          new XElement("Id", 3),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Person",
          new XElement("Id", 4),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")));
      text = Text.Xml(xml);
      Assert.True(text.Id == 1);
      Assert.True(text.AuthorId == 2);
      Assert.True(text.Category.Id == 3);
      Assert.True(text.Category.Language == "category.language");
      Assert.True(text.Category.Name == "category.name");
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(text.Name == "name");
      Assert.True(text.Person.Id == 4);
      Assert.True(text.Person.NameFirst == "person.nameFirst");
      Assert.True(text.Person.NameLast == "person.nameLast");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(new Text(2, "language", "name", "text", new Person("person.nameFirst", "person.nameLast") { Id = 4 }, new TextsCategory("category.language", "category.name") { Id = 3 }) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Text.Xml(text.Xml()).Equals(text));
    }
  }
}