using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="City"/>.</para>
  /// </summary>
  public sealed class CityTests : EntityUnitTests<City>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="City.Country"/> property.</para>
    /// </summary>
    [Fact]
    public void Country_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new City { Country = null });
      var country = new Country();
      Assert.True(ReferenceEquals(new City { Country = country }.Country, country));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new City { Name = null });
      Assert.Throws<ArgumentException>(() => new City { Name = string.Empty });
      Assert.True(new City { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.Region"/> property.</para>
    /// </summary>
    [Fact]
    public void Region_Property()
    {
      Assert.True(new City { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="City()"/>
    ///   <seealso cref="City(IDictionary{string, object)"/>
    ///   <seealso cref="City(string, string, Country, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var city = new City();
      Assert.True(city.Id == null);
      Assert.True(city.Country == null);
      Assert.True(city.Name == null);
      Assert.True(city.Region == null);

      Assert.Throws<ArgumentNullException>(() => new City(null));
      city = new City(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Country", new Country())
        .AddNext("Name", "name")
        .AddNext("Region", "region"));
      Assert.True(city.Id == "id");
      Assert.True(city.Country != null);
      Assert.True(city.Name == "name");
      Assert.True(city.Region == "region");

      Assert.Throws<ArgumentNullException>(() => new City(null, "name", new Country()));
      Assert.Throws<ArgumentNullException>(() => new City("id", null, new Country()));
      Assert.Throws<ArgumentNullException>(() => new City("id", "name", null));
      Assert.Throws<ArgumentException>(() => new City(string.Empty, "name", new Country()));
      Assert.Throws<ArgumentException>(() => new City("id", string.Empty, new Country()));
      city = new City("id", "name", new Country(), "region");
      Assert.True(city.Id == "id");
      Assert.True(city.Country != null);
      Assert.True(city.Name == "name");
      Assert.True(city.Region == "region");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new City { Name = "name" }.ToString() == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.Equals(object)"/> and <see cref="City.GetHashCode()"/> methods.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Country", new[] { new Country { IsoCode = "IsoCode" }, new Country { IsoCode = "IsoCode_2" } })
        .AddNext("Name", new[] { "Name", "Name_2" })
        .AddNext("Region", new[] { "Region", "Region_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.CompareTo(City)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new City { Name = "Name" }.CompareTo(new City { Name = "Name" }) == 0);
      Assert.True(new City { Name = "First" }.CompareTo(new City { Name = "Second" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="City.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="City.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => City.Xml(null));

      var xml = new XElement("City",
        new XElement("Id", "id"),
        new XElement("Country",
          new XElement("Id", "country.id"),
          new XElement("IsoCode", "country.isoCode"),
          new XElement("Name", "country.name")),
        new XElement("Name", "name"));
      var city = City.Xml(xml);
      Assert.True(city.Id == "id");
      Assert.True(city.Country.Id == "country.id");
      Assert.True(city.Country.IsoCode == "country.isoCode");
      Assert.True(city.Country.Name == "country.name");
      Assert.True(city.Name == "name");
      Assert.True(city.Region == null);
      Assert.True(new City("id", "name", new Country("country.id", "country.name", "country.isoCode")).Xml().ToString() == xml.ToString());
      Assert.True(City.Xml(city.Xml()).Equals(city));

      xml = new XElement("City",
        new XElement("Id", "id"),
        new XElement("Country",
          new XElement("Id", "country.id"),
          new XElement("IsoCode", "country.isoCode"),
          new XElement("Name", "country.name")),
        new XElement("Name", "name"),
        new XElement("Region", "region"));
      city = City.Xml(xml);
      Assert.True(city.Id == "id");
      Assert.True(city.Country.Id == "country.id");
      Assert.True(city.Country.IsoCode == "country.isoCode");
      Assert.True(city.Country.Name == "country.name");
      Assert.True(city.Name == "name");
      Assert.True(city.Region == "region");
      Assert.True(new City("id", "name", new Country("country.id", "country.name", "country.isoCode"), "region").Xml().ToString() == xml.ToString());
      Assert.True(City.Xml(city.Xml()).Equals(city));
    }
  }
}