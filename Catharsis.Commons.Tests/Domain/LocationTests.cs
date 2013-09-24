using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Location"/>.</para>
  /// </summary>
  public sealed class LocationTests : EntityUnitTests<Location>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Location.Address"/> property.</para>
    /// </summary>
    [Fact]
    public void Address_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Location { Address = null });
      Assert.Throws<ArgumentException>(() => new Location { Address = string.Empty });
      Assert.True(new Location { Address = "address" }.Address == "address");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.City"/> property.</para>
    /// </summary>
    [Fact]
    public void City_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Location { City = null });
      var city = new City();
      Assert.True(ReferenceEquals(new Location { City = city }.City, city));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.Latitude"/> property.</para>
    /// </summary>
    [Fact]
    public void Latitude_Property()
    {
      Assert.True(new Location { Latitude = 1 }.Latitude == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.Longitude"/> property.</para>
    /// </summary>
    [Fact]
    public void Longitude_Property()
    {
      Assert.True(new Location { Longitude = 1 }.Longitude == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.PostalCode"/> property.</para>
    /// </summary>
    [Fact]
    public void PostalCode_Property()
    {
      Assert.True(new Location { PostalCode = "postalCode" }.PostalCode == "postalCode");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Location()"/>
    ///   <seealso cref="Location(IDictionary{string, object})"/>
    ///   <seealso cref="Location(City, string, decimal?, decimal?, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var location = new Location();
      Assert.True(location.Id == 0);
      Assert.True(location.Address == null);
      Assert.True(location.City == null);
      Assert.False(location.Latitude.HasValue);
      Assert.False(location.Longitude.HasValue);
      Assert.True(location.PostalCode == null);

      Assert.Throws<ArgumentNullException>(() => new Location(null));
      location = new Location(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("Address", "address")
        .AddNext("City", new City())
        .AddNext("Country", "country")
        .AddNext("Latitude", (decimal) 1.0)
        .AddNext("Longitude", (decimal) 2.0)
        .AddNext("PostalCode", "postalCode")
        .AddNext("State", "state"));
      Assert.True(location.Id == 1);
      Assert.True(location.Address == "address");
      Assert.True(location.City != null);
      Assert.True(location.Latitude == (decimal) 1.0);
      Assert.True(location.Longitude == (decimal) 2.0);
      Assert.True(location.PostalCode == "postalCode");

      Assert.Throws<ArgumentNullException>(() => new Location(null, "address"));
      location = new Location(new City(), "address", (decimal) 1.0, (decimal) 2.0, "postalCode");
      Assert.True(location.Id == 0);
      Assert.True(location.Address == "address");
      Assert.True(location.City != null);
      Assert.True(location.Latitude == (decimal) 1.0);
      Assert.True(location.Longitude == (decimal) 2.0);
      Assert.True(location.PostalCode == "postalCode");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Location { City = new City { Name = "City", Country = new Country { Name = "Country" } }, Address = "Address" }.ToString() == "Country,City,Address");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Location.Equals(Location)"/></description></item>
    ///     <item><description><see cref="Location.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Address", "Address", "Address_2");
      this.TestEquality("City", new City { Name = "City_1" }, new City { Name = "City_2" });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Address", "Address", "Address_2");
      this.TestHashCode("City", new City { Name = "City_1" }, new City { Name = "City_2" });
    }

   /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Location.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Location.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Location.Xml(null));

      var xml = new XElement("Location",
        new XElement("Id", 1),
        new XElement("Address", "address"),
        new XElement("City",
          new XElement("Id", 2),
          new XElement("Country",
            new XElement("Id", 3),
            new XElement("IsoCode", "city.country.isoCode"),
            new XElement("Name", "city.country.name")),
          new XElement("Name", "city.name")));
      var location = Location.Xml(xml);
      Assert.True(location.Id == 1);
      Assert.True(location.Address == "address");
      Assert.True(location.City.Id == 2);
      Assert.True(location.City.Country.Id == 3);
      Assert.True(location.City.Country.Image == null);
      Assert.True(location.City.Country.IsoCode == "city.country.isoCode");
      Assert.True(location.City.Country.Name == "city.country.name");
      Assert.True(location.City.Name == "city.name");
      Assert.True(location.City.Region == null);
      Assert.False(location.Latitude.HasValue);   
      Assert.False(location.Longitude.HasValue);
      Assert.True(location.PostalCode == null);
      Assert.True(new Location(new City("city.name", new Country("city.country.name", "city.country.isoCode") { Id = 3 }) { Id = 2 }, "address") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Location.Xml(location.Xml()).Equals(location));

      xml = new XElement("Location",
        new XElement("Id", 1),
        new XElement("Address", "address"),
        new XElement("City",
          new XElement("Id", 2),
          new XElement("Country",
            new XElement("Id", 3),
            new XElement("Image",
              new XElement("Id", 4),
              new XElement("File",
                new XElement("Id", 5),
                new XElement("ContentType", "city.country.image.file.contentType"),
                new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
                new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
                new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
                new XElement("Name", "city.country.image.file.name"),
                new XElement("OriginalName", "city.country.image.file.originalName"),
                new XElement("Size", Guid.Empty.ToByteArray().Length)),
              new XElement("Height", 1),
              new XElement("Width", 2)),
            new XElement("IsoCode", "city.country.isoCode"),
            new XElement("Name", "city.country.name")),
          new XElement("Name", "city.name"),
          new XElement("Region", "city.region")),
        new XElement("Latitude", 1),
        new XElement("Longitude", 2),
        new XElement("PostalCode", "postalCode"));
      location = Location.Xml(xml);
      Assert.True(location.Id == 1);
      Assert.True(location.Address == "address");
      Assert.True(location.City.Id == 2);
      Assert.True(location.City.Country.Id == 3);
      Assert.True(location.City.Country.Image.Id == 4);
      Assert.True(location.City.Country.Image.File.Id == 5);
      Assert.True(location.City.Country.Image.File.ContentType == "city.country.image.file.contentType");
      Assert.True(location.City.Country.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(location.City.Country.Image.File.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(location.City.Country.Image.File.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(location.City.Country.Image.File.Name == "city.country.image.file.name");
      Assert.True(location.City.Country.Image.File.OriginalName == "city.country.image.file.originalName");
      Assert.True(location.City.Country.Image.File.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(location.City.Country.Image.Height == 1);
      Assert.True(location.City.Country.Image.Width == 2);
      Assert.True(location.City.Country.IsoCode == "city.country.isoCode");
      Assert.True(location.City.Country.Name == "city.country.name");
      Assert.True(location.City.Name == "city.name");
      Assert.True(location.City.Region == "city.region");
      Assert.True(location.Latitude == 1);
      Assert.True(location.Longitude == 2);
      Assert.True(location.PostalCode == "postalCode");
      Assert.True(new Location(new City("city.name", new Country("city.country.name", "city.country.isoCode", new Image(new File("city.country.image.file.contentType", "city.country.image.file.name", "city.country.image.file.originalName", Guid.Empty.ToByteArray()) { Id = 5, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2) { Id = 4 }) { Id = 3 }, "city.region") { Id = 2 }, "address", 1, 2, "postalCode") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Location.Xml(location.Xml()).Equals(location));
    }
  }
}