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
    ///   <seealso cref="City(string, Country, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var city = new City();
      Assert.True(city.Id == 0);
      Assert.True(city.Country == null);
      Assert.True(city.Name == null);
      Assert.True(city.Region == null);

      Assert.Throws<ArgumentNullException>(() => new City(null, new Country()));
      Assert.Throws<ArgumentNullException>(() => new City("name", null));
      Assert.Throws<ArgumentException>(() => new City(string.Empty, new Country()));
      city = new City("name", new Country(), "region");
      Assert.True(city.Id == 0);
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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="City.Equals(City)"/></description></item>
    ///     <item><description><see cref="City.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Country", new Country { IsoCode = "IsoCode" }, new Country { IsoCode = "IsoCode_2" });
      this.TestEquality("Name", "Name", "Name_2");
      this.TestEquality("Region", "Region", "Region_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="City.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Country", new Country { IsoCode = "IsoCode" }, new Country { IsoCode = "IsoCode_2" });
      this.TestHashCode("Name", "Name", "Name_2");
      this.TestHashCode("Region", "Region", "Region_2");
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
        new XElement("Id", 1),
        new XElement("Country",
          new XElement("Id", 2),
          new XElement("IsoCode", "country.isoCode"),
          new XElement("Name", "country.name")),
        new XElement("Name", "name"));
      var city = City.Xml(xml);
      Assert.True(city.Id == 1);
      Assert.True(city.Country.Id == 2);
      Assert.True(city.Country.IsoCode == "country.isoCode");
      Assert.True(city.Country.Name == "country.name");
      Assert.True(city.Name == "name");
      Assert.True(city.Region == null);
      Assert.True(new City("name", new Country("country.name", "country.isoCode") { Id = 2 }) { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(City.Xml(city.Xml()).Equals(city));

      xml = new XElement("City",
        new XElement("Id", 1),
        new XElement("Country",
          new XElement("Id", 2),
          new XElement("IsoCode", "country.isoCode"),
          new XElement("Name", "country.name")),
        new XElement("Name", "name"),
        new XElement("Region", "region"));
      city = City.Xml(xml);
      Assert.True(city.Id == 1);
      Assert.True(city.Country.Id == 2);
      Assert.True(city.Country.IsoCode == "country.isoCode");
      Assert.True(city.Country.Name == "country.name");
      Assert.True(city.Name == "name");
      Assert.True(city.Region == "region");
      Assert.True(new City("name", new Country("country.name", "country.isoCode") { Id = 2 }, "region") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(City.Xml(city.Xml()).Equals(city));
    }
  }
}