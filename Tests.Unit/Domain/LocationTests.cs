using System;
using System.Collections.Generic;
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
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Location()"/>
    ///   <seealso cref="Location(IDictionary{string, object})"/>
    ///   <seealso cref="Location(string, string, string, string, string, decimal?, decimal?, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var location = new Location();
      Assert.True(location.Id == null);
      Assert.True(location.Address == null);
      Assert.True(location.City == null);
      Assert.True(location.Country == null);
      Assert.False(location.Latitude.HasValue);
      Assert.False(location.Longitude.HasValue);
      Assert.True(location.PostalCode == null);
      Assert.True(location.State == null);

      location = new Location(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Address", "address")
        .AddNext("City", "city")
        .AddNext("Country", "country")
        .AddNext("Latitude", (decimal) 1.0)
        .AddNext("Longitude", (decimal) 2.0)
        .AddNext("PostalCode", "postalCode")
        .AddNext("State", "state"));
      Assert.True(location.Id == "id");
      Assert.True(location.Address == "address");
      Assert.True(location.City == "city");
      Assert.True(location.Country == "country");
      Assert.True(location.Latitude.Value == (decimal) 1.0);
      Assert.True(location.Longitude.Value == (decimal) 2.0);
      Assert.True(location.PostalCode == "postalCode");
      Assert.True(location.State == "state");

      location = new Location("id", "country", "state", "city", "address", (decimal) 1.0, (decimal) 2.0, "postalCode");
      Assert.True(location.Id == "id");
      Assert.True(location.Address == "address");
      Assert.True(location.City == "city");
      Assert.True(location.Country == "country");
      Assert.True(location.Latitude.Value == (decimal) 1.0);
      Assert.True(location.Longitude.Value == (decimal) 2.0);
      Assert.True(location.PostalCode == "postalCode");
      Assert.True(location.State == "state");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Location.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Location { Country = "Country", City = "City", Address = "Address" }.ToString() == "Country,City,Address");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Address", new[] { "Address", "Address_2" })
        .AddNext("City", new[] { "City", "City_2" })
        .AddNext("Country", new[] { "Country", "Country_2" })
        .AddNext("State", new[] { "State", "State_2" }));
    }
  }
}