using System;
using System.Collections.Generic;
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
      Assert.True(person.BirthDay.Value == 1);
      Assert.True(person.BirthMonth.Value == 2);
      Assert.True(person.BirthYear.Value == 2000);
      Assert.True(person.DeathDay.Value == 3);
      Assert.True(person.DeathMonth.Value == 4);
      Assert.True(person.DeathYear.Value == 2030);
      Assert.True(person.Description == "description");
      Assert.True(person.Image != null);
      Assert.True(person.NameFirst == "nameFirst");
      Assert.True(person.NameLast == "nameLast");
      Assert.True(person.NameMiddle == "nameMiddle");

      person = new Person("id", "nameFirst", "nameLast", "nameMiddle", "description", new Image(), 1, 2, 2000, 3, 4, 2030);
      Assert.True(person.Id == "id");
      Assert.True(person.BirthDay.Value == 1);
      Assert.True(person.BirthMonth.Value == 2);
      Assert.True(person.BirthYear.Value == 2000);
      Assert.True(person.DeathDay.Value == 3);
      Assert.True(person.DeathMonth.Value == 4);
      Assert.True(person.DeathYear.Value == 2030);
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

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("NameFirst", new[] { "NameFirst", "NameLast_2" })
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
  }
}