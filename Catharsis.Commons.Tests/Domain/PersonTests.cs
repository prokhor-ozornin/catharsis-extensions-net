using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Person"/>.</para>
  /// </summary>
  public sealed class PersonTests : EntityUnitTests<Person>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Person.BirthDay"/> property.</para>
    /// </summary>
    [Fact]
    public void BirthDay_Property()
    {
      Assert.True(new Person { BirthDay = 1 }.BirthDay == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.BirthMonth"/> property.</para>
    /// </summary>
    [Fact]
    public void BirthMonth_Property()
    {
      Assert.True(new Person { BirthMonth = 1 }.BirthMonth == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.BirthYear"/> property.</para>
    /// </summary>
    [Fact]
    public void BirthYear_Property()
    {
      Assert.True(new Person { BirthYear = 1 }.BirthYear == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.DeathDay"/> property.</para>
    /// </summary>
    [Fact]
    public void DeathDay_Property()
    {
      Assert.True(new Person { DeathDay = 1 }.DeathDay == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.DeathMonth"/> property.</para>
    /// </summary>
    [Fact]
    public void DeathMonth_Property()
    {
      Assert.True(new Person { DeathMonth = 1 }.DeathMonth == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.DeathYear"/> property.</para>
    /// </summary>
    [Fact]
    public void DeathYear_Property()
    {
      Assert.True(new Person { DeathYear = 1 }.DeathYear == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.Description"/> property.</para>
    /// </summary>
    [Fact]
    public void Description_Property()
    {
      Assert.True(new Person { Description = "description" }.Description == "description");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.Image"/> property.</para>
    /// </summary>
    [Fact]
    public void Image_Property()
    {
      var image = new Image();
      Assert.True(ReferenceEquals(new Person { Image = image }.Image, image));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.NameFirst"/> property.</para>
    /// </summary>
    [Fact]
    public void NameFirst_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Person { NameFirst = null });
      Assert.Throws<ArgumentException>(() => new Person { NameFirst = string.Empty });
      Assert.True(new Person { NameFirst = "nameFirst" }.NameFirst == "nameFirst");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.NameLast"/> property.</para>
    /// </summary>
    [Fact]
    public void NameLast_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Person { NameLast = null });
      Assert.Throws<ArgumentException>(() => new Person { NameLast = string.Empty });
      Assert.True(new Person { NameLast = "nameLast" }.NameLast == "nameLast");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.NameMiddle"/> property.</para>
    /// </summary>
    [Fact]
    public void NameMiddle_Property()
    {
      Assert.True(new Person { NameMiddle = "nameMiddle" }.NameMiddle == "nameMiddle");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Person()"/>
    ///   <seealso cref="Person(IDictionary{string, object})"/>
    ///   <seealso cref="Person(string, string, string, string, string, Image, byte?, byte?, short?, byte?, byte?, short?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var person = new Person();
      Assert.True(person.Id == null);
      Assert.False(person.BirthDay.HasValue);
      Assert.False(person.BirthMonth.HasValue);
      Assert.False(person.BirthYear.HasValue);
      Assert.False(person.DeathDay.HasValue);
      Assert.False(person.DeathMonth.HasValue);
      Assert.False(person.DeathYear.HasValue);
      Assert.True(person.Description == null);
      Assert.True(person.Image == null);
      Assert.True(person.NameFirst == null);
      Assert.True(person.NameLast == null);
      Assert.True(person.NameMiddle == null);

      Assert.Throws<ArgumentNullException>(() => new Person(null));
      person = new Person(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("BirthDay", (byte) 1)
        .AddNext("BirthMonth", (byte) 2)
        .AddNext("BirthYear", (short) 2000)
        .AddNext("DeathDay", (byte) 3)
        .AddNext("DeathMonth", (byte) 4)
        .AddNext("DeathYear", (short) 2030)
        .AddNext("Description", "description")
        .AddNext("Image", new Image())
        .AddNext("NameFirst", "nameFirst")
        .AddNext("NameLast", "nameLast")
        .AddNext("NameMiddle", "nameMiddle"));
      Assert.True(person.Id == "id");
      Assert.True(person.BirthDay == 1);
      Assert.True(person.BirthMonth == 2);
      Assert.True(person.BirthYear == 2000);
      Assert.True(person.DeathDay == 3);
      Assert.True(person.DeathMonth == 4);
      Assert.True(person.DeathYear == 2030);
      Assert.True(person.Description == "description");
      Assert.True(person.Image != null);
      Assert.True(person.NameFirst == "nameFirst");
      Assert.True(person.NameLast == "nameLast");
      Assert.True(person.NameMiddle == "nameMiddle");

      Assert.Throws<ArgumentNullException>(() => new Person(null, "nameFirst", "nameLast"));
      Assert.Throws<ArgumentNullException>(() => new Person("id", null, "nameLast"));
      Assert.Throws<ArgumentNullException>(() => new Person("id", "nameFirst", null));
      Assert.Throws<ArgumentException>(() => new Person(string.Empty, "nameFirst", "nameLast"));
      Assert.Throws<ArgumentException>(() => new Person("id", string.Empty, "nameLast"));
      Assert.Throws<ArgumentException>(() => new Person("id", "nameFirst", string.Empty));
      person = new Person("id", "nameFirst", "nameLast", "nameMiddle", "description", new Image(), 1, 2, 2000, 3, 4, 2030);
      Assert.True(person.Id == "id");
      Assert.True(person.BirthDay == 1);
      Assert.True(person.BirthMonth == 2);
      Assert.True(person.BirthYear == 2000);
      Assert.True(person.DeathDay == 3);
      Assert.True(person.DeathMonth == 4);
      Assert.True(person.DeathYear == 2030);
      Assert.True(person.Description == "description");
      Assert.True(person.Image != null);
      Assert.True(person.NameFirst == "nameFirst");
      Assert.True(person.NameLast == "nameLast");
      Assert.True(person.NameMiddle == "nameMiddle");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Person { NameFirst = "NameFirst", NameLast = "NameLast", NameMiddle = "NameMiddle" }.ToString() == "NameLast NameFirst NameMiddle");
      Assert.True(new Person { NameFirst = "NameFirst", NameLast = "NameLast" }.ToString() == "NameLast NameFirst");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Person"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("NameFirst", new[] { "NameFirst", "NameFirst_2" })
        .AddNext("NameLast", new[] { "NameLast", "NameLast_2" })
        .AddNext("NameMiddle", new[] { "NameMiddle", "NameMiddle_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Person.CompareTo(Person)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Person { NameLast = "NameLast" }.CompareTo(new Person { NameLast = "NameLast" }) == 0);
      Assert.True(new Person { NameLast = "a" }.CompareTo(new Person { NameLast = "b" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Person.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Person.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Person.Xml(null));

      var xml = new XElement("Person",
        new XElement("Id", "id"),
        new XElement("NameFirst", "nameFirst"),
        new XElement("NameLast", "nameLast"));
      var person = Person.Xml(xml);
      Assert.True(person.Id == "id");
      Assert.False(person.BirthDay.HasValue);
      Assert.False(person.BirthMonth.HasValue);
      Assert.False(person.BirthYear.HasValue);
      Assert.False(person.DeathDay.HasValue);
      Assert.False(person.DeathMonth.HasValue);
      Assert.False(person.DeathYear.HasValue);
      Assert.True(person.Description == null);
      Assert.True(person.Image == null);
      Assert.True(person.NameFirst == "nameFirst");
      Assert.True(person.NameLast == "nameLast");
      Assert.True(person.NameMiddle == null);
      Assert.True(new Person("id", "nameFirst", "nameLast").Xml().ToString() == xml.ToString());
      Assert.True(Person.Xml(person.Xml()).Equals(person));

      xml = new XElement("Person",
        new XElement("Id", "id"),
        new XElement("BirthDay", 1),
        new XElement("BirthMonth", 1),
        new XElement("BirthYear", 2000),
        new XElement("DeathDay", 31),
        new XElement("DeathMonth", 12),
        new XElement("DeathYear", 2100),
        new XElement("Description", "description"),
        new XElement("Image",
          new XElement("Id", "image.id"),
          new XElement("File",
            new XElement("Id", "image.file.id"),
            new XElement("ContentType", "image.file.contentType"),
            new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
            new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
            new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
            new XElement("Name", "image.file.name"),
            new XElement("OriginalName", "image.file.originalName"),
            new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
          new XElement("Height", 1),
          new XElement("Width", 2)),
        new XElement("NameFirst", "nameFirst"),
        new XElement("NameLast", "nameLast"),
        new XElement("NameMiddle", "nameMiddle"));
      person = Person.Xml(xml);
      Assert.True(person.Id == "id");
      Assert.True(person.BirthDay == 1);
      Assert.True(person.BirthMonth == 1);
      Assert.True(person.BirthYear == 2000);
      Assert.True(person.DeathDay == 31);
      Assert.True(person.DeathMonth == 12);
      Assert.True(person.DeathYear == 2100);
      Assert.True(person.Description == "description");
      Assert.True(person.Image.Id == "image.id");
      Assert.True(person.Image.File.Id == "image.file.id");
      Assert.True(person.Image.File.ContentType == "image.file.contentType");
      Assert.True(person.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(person.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(person.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(person.Image.File.Name == "image.file.name");
      Assert.True(person.Image.File.OriginalName == "image.file.originalName");
      Assert.True(person.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(person.Image.Height == 1);
      Assert.True(person.Image.Width == 2);
      Assert.True(person.NameFirst == "nameFirst");
      Assert.True(person.NameLast == "nameLast");
      Assert.True(person.NameMiddle == "nameMiddle");
      Assert.True(new Person("id", "nameFirst", "nameLast", "nameMiddle", "description", new Image("image.id", new File("image.file.id", "image.file.contentType", "image.file.name", "image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2), 1, 1, 2000, 31, 12, 2100).Xml().ToString() == xml.ToString());
      Assert.True(Person.Xml(person.Xml()).Equals(person));
    }
  }
}