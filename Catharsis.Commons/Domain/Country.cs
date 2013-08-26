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
  public class Country : EntityBase, IComparable<Country>, IImageable, INameable
  {
    private string isoCode;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string IsoCode
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
    ///   <para>Creates new country.</para>
    /// </summary>
    public Country()
    {
    }

    /// <summary>
    ///   <para>Creates new country with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on country after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Country(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isoCode"></param>
    /// <param name="image"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="name"/> or <paramref name="isoCode"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="name"/> or <paramref name="isoCode"/> is <see cref="string.Empty"/> string.</exception>
    public Country(string id, string name, string isoCode, Image image = null) : base(id)
    {
      this.Name = name;
      this.IsoCode = isoCode;
      this.Image = image;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Country Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new Country((string) xml.Element("Id"), (string) xml.Element("Name"), (string) xml.Element("IsoCode"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Image != null ? this.Image.Xml() : null,
        new XElement("IsoCode", this.IsoCode),
        new XElement("Name", this.Name));
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
    ///   <para></para>
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    public int CompareTo(Country country)
    {
      return this.Name.CompareTo(country.Name);
    }
  }
}