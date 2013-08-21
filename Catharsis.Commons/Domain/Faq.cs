using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  public class Faq : Item, IComparable<Faq>
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
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Faq(string id, string language, string name, string text) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
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
    ///   <para></para>
    /// </summary>
    /// <param name="faq"></param>
    /// <returns></returns>
    public int CompareTo(Faq faq)
    {
      return base.CompareTo(faq);
    }
  }
}