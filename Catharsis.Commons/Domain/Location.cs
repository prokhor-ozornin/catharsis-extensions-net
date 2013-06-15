using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Address", "City", "Country", "State")]
  public class Location : EntityBase
  {
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }

    public Location()
    {
    }

    public Location(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Location(string id, string country, string state, string city, string address, decimal? latitude = null, decimal? longitude = null, string postalCode = null)
    {
      this.Id = id;
      this.Country = country;
      this.State = state;
      this.City = city;
      this.Address = address;
      this.Latitude = latitude;
      this.Longitude = longitude;
      this.PostalCode = postalCode;
    }

    public override string ToString()
    {
      return string.Format("{0},{1},{2}", this.Country, this.City, this.Address);
    }
  }
}