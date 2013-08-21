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
    ///   <seealso cref="Location(string, City, string, decimal?, decimal?, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var location = new Location();
      Assert.True(location.Id == null);
      Assert.True(location.Address == null);
      Assert.True(location.City == null);
      Assert.False(location.Latitude.HasValue);
      Assert.False(location.Longitude.HasValue);
      Assert.True(location.PostalCode == null);

      Assert.Throws<ArgumentNullException>(() => new Location(null));
      location = new Location(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Address", "address")
        .AddNext("City", new City())
        .AddNext("Country", "country")
        .AddNext("Latitude", (decimal) 1.0)
        .AddNext("Longitude", (decimal) 2.0)
        .AddNext("PostalCode", "postalCode")
        .AddNext("State", "state"));
      Assert.True(location.Id == "id");
      Assert.True(location.Address == "address");
      Assert.True(location.City != null);
      Assert.True(location.Latitude == (decimal) 1.0);
      Assert.True(location.Longitude == (decimal) 2.0);
      Assert.True(location.PostalCode == "postalCode");

      Assert.Throws<ArgumentNullException>(() => new Location(null, new City(), "address"));
      Assert.Throws<ArgumentNullException>(() => new Location("id", null, "address"));
      Assert.Throws<ArgumentException>(() => new Location(string.Empty, new City(), "address"));
      location = new Location("id", new City(), "address", (decimal) 1.0, (decimal) 2.0, "postalCode");
      Assert.True(location.Id == "id");
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
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Location"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Address", new[] { "Address", "Address_2" })
        .AddNext("City", new[] { new City { Name = "City_1" }, new City { Name = "City_2" } }));
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
        new XElement("Id", "id"),
        new XElement("Address", "address"),
        new XElement("City",
          new XElement("Id", "city.id"),
          new XElement("Country",
            new XElement("Id", "city.country.id"),
            new XElement("IsoCode", "city.country.isoCode"),
            new XElement("Name", "city.country.name")),
          new XElement("Name", "city.name")));
      var location = Location.Xml(xml);
      Assert.True(location.Id == "id");
      Assert.True(location.Address == "address");
      Assert.True(location.City.Id == "city.id");
      Assert.True(location.City.Country.Id == "city.country.id");
      Assert.True(location.City.Country.Image == null);
      Assert.True(location.City.Country.IsoCode == "city.country.isoCode");
      Assert.True(location.City.Country.Name == "city.country.name");
      Assert.True(location.City.Name == "city.name");
      Assert.True(location.City.Region == null);
      Assert.False(location.Latitude.HasValue);   
      Assert.False(location.Longitude.HasValue);
      Assert.True(location.PostalCode == null);
      Assert.True(new Location("id", new City("city.id", "city.name", new Country("city.country.id", "city.country.name", "city.country.isoCode")), "address").Xml().ToString() == xml.ToString());
      Assert.True(Location.Xml(location.Xml()).Equals(location));

      xml = new XElement("Location",
        new XElement("Id", "id"),
        new XElement("Address", "address"),
        new XElement("City",
          new XElement("Id", "city.id"),
          new XElement("Country",
            new XElement("Id", "city.country.id"),
            new XElement("Image",
              new XElement("Id", "city.country.image.id"),
              new XElement("File",
                new XElement("Id", "city.country.image.file.id"),
                new XElement("ContentType", "city.country.image.file.contentType"),
                new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
                new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
                new XElement("Hash", Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex()),
                new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
                new XElement("Name", "city.country.image.file.name"),
                new XElement("OriginalName", "city.country.image.file.originalName"),
                new XElement("Size", Guid.Empty.ToByteArray().LongLength)),
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
      Assert.True(location.Id == "id");
      Assert.True(location.Address == "address");
      Assert.True(location.City.Id == "city.id");
      Assert.True(location.City.Country.Id == "city.country.id");
      Assert.True(location.City.Country.Image.Id == "city.country.image.id");
      Assert.True(location.City.Country.Image.File.Id == "city.country.image.file.id");
      Assert.True(location.City.Country.Image.File.ContentType == "city.country.image.file.contentType");
      Assert.True(location.City.Country.Image.File.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(location.City.Country.Image.File.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(location.City.Country.Image.File.Hash == Guid.Empty.ToByteArray().EncodeSHA512().EncodeHex());
      Assert.True(location.City.Country.Image.File.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(location.City.Country.Image.File.Name == "city.country.image.file.name");
      Assert.True(location.City.Country.Image.File.OriginalName == "city.country.image.file.originalName");
      Assert.True(location.City.Country.Image.File.Size == Guid.Empty.ToByteArray().LongLength);
      Assert.True(location.City.Country.Image.Height == 1);
      Assert.True(location.City.Country.Image.Width == 2);
      Assert.True(location.City.Country.IsoCode == "city.country.isoCode");
      Assert.True(location.City.Country.Name == "city.country.name");
      Assert.True(location.City.Name == "city.name");
      Assert.True(location.City.Region == "city.region");
      Assert.True(location.Latitude == 1);
      Assert.True(location.Longitude == 2);
      Assert.True(location.PostalCode == "postalCode");
      Assert.True(new Location("id", new City("city.id", "city.name", new Country("city.country.id", "city.country.name", "city.country.isoCode", new Image("city.country.image.id", new File("city.country.image.file.id", "city.country.image.file.contentType", "city.country.image.file.name", "city.country.image.file.originalName", Guid.Empty.ToByteArray()) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }, 1, 2)), "city.region"), "address", 1, 2, "postalCode").Xml().ToString() == xml.ToString());
      Assert.True(Location.Xml(location.Xml()).Equals(location));
    }
  }
}