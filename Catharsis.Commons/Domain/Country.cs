using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("IsoCode")]
  public class Country : EntityBase, IComparable<Country>, IEquatable<Country>, IImageable, INameable
  {
    private string isoCode;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string IsoCode
    {
      get { return this.isoCode; }
      set
      {
        Assertion.NotEmpty(value);

        this.isoCode = value;
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
    ///   <para>Creates new country.</para>
    /// </summary>
    public Country()
    {
    }

    /// <summary>
    ///   <para>Creates new country.</para>
    /// </summary>
    /// <param name="name">Name of country.</param>
    /// <param name="isoCode"></param>
    /// <param name="image"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="name"/> or <paramref name="isoCode"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="name"/> or <paramref name="isoCode"/> is <see cref="string.Empty"/> string.</exception>
    public Country(string name, string isoCode, Image image = null)
    {
      this.Name = name;
      this.IsoCode = isoCode;
      this.Image = image;
    }

    /// <summary>
    ///   <para>Creates new country from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Country"/> type.</param>
    /// <returns>Recreated country object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Country Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var country = new Country((string) xml.Element("Name"), (string) xml.Element("IsoCode"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null);
      if (xml.Element("Id") != null)
      {
        country.Id = (long) xml.Element("Id");
      }
      return country;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Country other)
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
    ///   <para>Compares the current country with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Country"/> to compare with this instance.</param>
    public virtual int CompareTo(Country other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Country"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Image != null ? this.Image.Xml() : null,
        new XElement("IsoCode", this.IsoCode),
        new XElement("Name", this.Name));
    }
  }
}