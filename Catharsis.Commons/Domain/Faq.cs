using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class Faq : Item, IComparable<Faq>, IEquatable<Faq>
  {
    /// <summary>
    ///   <para>Creates new F.A.Q.</para>
    /// </summary>
    public Faq()
    {
    }

    /// <summary>
    ///   <para>Creates new F.A.Q. with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on F.A.Q. after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Faq(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new F.A.Q.</para>
    /// </summary>
    /// <param name="id">Unique identifier of F.A.Q.</param>
    /// <param name="language">ISO language code of F.A.Q.'s text content.</param>
    /// <param name="name">Name of F.A.Q.</param>
    /// <param name="text">F.A.Q.'s question text.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Faq(string id, string language, string name, string text) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);
    }

    /// <summary>
    ///   <para>Creates new F.A.Q. from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Faq"/> type.</param>
    /// <returns>Recreated F.A.Q. object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Faq Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var faq = new Faq((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"));
      if (xml.Element("DateCreated") != null)
      {
        faq.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        faq.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return faq;
    }

    /// <summary>
    ///   <para>Compares the current F.A.Q. with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Faq"/> to compare with this instance.</param>
    public int CompareTo(Faq other)
    {
      return base.CompareTo(other);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Faq other)
    {
      return base.Equals(other);
    }
  }
}