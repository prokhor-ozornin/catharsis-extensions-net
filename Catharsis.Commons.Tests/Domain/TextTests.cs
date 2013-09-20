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
    ///   <seealso cref="Text(IDictionary{string, object})"/>
    ///   <seealso cref="Text(string, string, string, string, string, Person, TextsCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var text = new Text();
      Assert.True(text.Id == null);
      Assert.True(text.AuthorId == null);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == null);
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == null);
      Assert.True(text.Text == null);
      Assert.True(text.Category == null);
      Assert.True(text.Person == null);

      Assert.Throws<ArgumentNullException>(() => new Text(null));
      text = new Text(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new TextsCategory())
        .AddNext("Person", new Person()));
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == "name");
      Assert.True(text.Text == "text");
      Assert.True(text.Category != null);
      Assert.True(text.Person != null);

      Assert.Throws<ArgumentNullException>(() => new Text(null, "authorId", "language", "name", "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text("id", null, "language", "name", "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text("id", "authorId", null, "name", "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text("id", "authorId", "language", null, "text", new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text("id", "authorId", "language", "name", null, new Person()));
      Assert.Throws<ArgumentNullException>(() => new Text("id", "authorId", "language", "name", "text", null));
      Assert.Throws<ArgumentException>(() => new Text(string.Empty, "authorId", "language", "name", "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text("id", string.Empty, "language", "name", "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text("id", "authorId", string.Empty, "name", "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text("id", "authorId", "language", string.Empty, "text", new Person()));
      Assert.Throws<ArgumentException>(() => new Text("id", "authorId", "language", "name", string.Empty, new Person()));
      text = new Text("id", "authorId", "language", "name", "text", new Person(), new TextsCategory());
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == "name");
      Assert.True(text.Text == "text");
      Assert.True(text.Category != null);
      Assert.True(text.Person != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Text"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new TextsCategory { Name = "Name" }, new TextsCategory { Name = "Name_2" } })
        .AddNext("Person", new[] { new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" } }));
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
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Person",
          new XElement("Id", "person.id"),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")));
      var text = Text.Xml(xml);
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.Category == null);
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(text.Name == "name");
      Assert.True(text.Person.Id == "person.id");
      Assert.True(text.Person.NameFirst == "person.nameFirst");
      Assert.True(text.Person.NameLast == "person.nameLast");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(new Text("id", "authorId", "language", "name", "text", new Person("person.id", "person.nameFirst", "person.nameLast")) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Text.Xml(text.Xml()).Equals(text));

      xml = new XElement("Text",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("TextsCategory",
          new XElement("Id", "category.id"),
          new XElement("Language", "category.language"),
          new XElement("Name", "category.name")),
        new XElement("Person",
          new XElement("Id", "person.id"),
          new XElement("NameFirst", "person.nameFirst"),
          new XElement("NameLast", "person.nameLast")));
      text = Text.Xml(xml);
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.Category.Id == "category.id");
      Assert.True(text.Category.Language == "category.language");
      Assert.True(text.Category.Name == "category.name");
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(text.Name == "name");
      Assert.True(text.Person.Id == "person.id");
      Assert.True(text.Person.NameFirst == "person.nameFirst");
      Assert.True(text.Person.NameLast == "person.nameLast");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(new Text("id", "authorId", "language", "name", "text", new Person("person.id", "person.nameFirst", "person.nameLast"), new TextsCategory("category.id", "category.language", "category.name")) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Text.Xml(text.Xml()).Equals(text));
    }
  }
}