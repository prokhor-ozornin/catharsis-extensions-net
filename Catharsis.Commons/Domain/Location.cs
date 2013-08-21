using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  [EqualsAndHashCode("Address", "City")]
  public class Location : EntityBase
  {
    private string address;
    private City city;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Address
    {
      get { return this.address; }
      set
      {
        Assertion.NotEmpty(value);

        this.address = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public City City
    {
      get { return this.city; }
      set
      {
        Assertion.NotNull(value);

        this.city = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public decimal? Latitude { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public decimal? Longitude { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string PostalCode { get; set; }
    
    /// <summary>
    ///   <para>Creates new location.</para>
    /// </summary>
    public Location()
    {
    }

    /// <summary>
    ///   <para>Creates new location with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on location after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Location(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="city"></param>
    /// <param name="address"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="postalCode"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="city"/> or <paramref name="address"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="address"/> is <see cref="string.Empty"/> string.</exception>
    public Location(string id, City city, string address, decimal? latitude = null, decimal? longitude = null, string postalCode = null) : base(id)
    {
      this.City = city;
      this.Address = address;
      this.Latitude = latitude;
      this.Longitude = longitude;
      this.PostalCode = postalCode;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Location Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new Location((string) xml.Element("Id"), City.Xml(xml.Element("City")), (string) xml.Element("Address"), (decimal?) xml.Element("Latitude"), (decimal?) xml.Element("Longitude"), (string) xml.Element("PostalCode"));
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current location.</para>
    /// </summary>
    /// <returns>A string that represents the current location.</returns>
    public override string ToString()
    {
      return string.Format("{0},{1},{2}", this.City.Country, this.City, this.Address);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Address", this.Address),
        this.City.Xml(),
        this.Latitude.HasValue ? new XElement("Latitude", this.Latitude.GetValueOrDefault()) : null,
        this.Longitude.HasValue ? new XElement("Longitude", this.Longitude.GetValueOrDefault()) : null,
        this.PostalCode != null ? new XElement("PostalCode", this.PostalCode) : null);
    }
  }
}