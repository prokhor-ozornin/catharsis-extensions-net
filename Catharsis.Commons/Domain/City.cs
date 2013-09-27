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
  public class City : EntityBase, IComparable<City>, IEquatable<City>, INameable
  {
    private Country country;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public virtual Country Country
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
    public virtual string Name
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
    public virtual string Region { get; set; }

    /// <summary>
    ///   <para>Creates new city.</para>
    /// </summary>
    public City()
    {
    }

    /// <summary>
    ///   <para>Creates new city.</para>
    /// </summary>
    /// <param name="name">Name of city.</param>
    /// <param name="country"></param>
    /// <param name="region"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="name"/> or <paramref name="country"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public City(string name, Country country, string region = null)
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

      var city = new City((string) xml.Element("Name"), Country.Xml(xml.Element("Country")), (string) xml.Element("Region"));
      if (xml.Element("Id") != null)
      {
        city.Id = (long) xml.Element("Id");
      }
      return city;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(City other)
    {
      return base.Equals(other);
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
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
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