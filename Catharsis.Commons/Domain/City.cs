using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Country,Name,Region")]
  public class City : EntityBase, IComparable<City>, INameable
  {
    private Country country;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public Country Country
    {
      get { return this.country; }
      set
      {
        Assertion.NotNull(value);

        this.country = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string Name
    {
      get { return this.name; }
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    ///   <para>Creates new city.</para>
    /// </summary>
    public City()
    {
    }

    /// <summary>
    ///   <para>Creates new city with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on city after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public City(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new city.</para>
    /// </summary>
    /// <param name="id">Unique identifier of city.</param>
    /// <param name="name">Name of city.</param>
    /// <param name="country"></param>
    /// <param name="region"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="name"/> or <paramref name="country"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public City(string id, string name, Country country, string region = null) : base(id)
    {
      this.Name = name;
      this.Country = country;
      this.Region = region;
    }

    /// <summary>
    ///   <para>Creates new city from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="City"/> type.</param>
    /// <returns>Recreated city object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static City Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new City((string) xml.Element("Id"), (string) xml.Element("Name"), Country.Xml(xml.Element("Country")), (string) xml.Element("Region"));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current city with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="City"/> to compare with this instance.</param>
    public int CompareTo(City other)
    {
      return this.Name.CompareTo(other.Name);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="City"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Country.Xml(),
        new XElement("Name", this.Name),
        this.Region != null ? new XElement("Region", this.Region) : null);
    }
  }
}