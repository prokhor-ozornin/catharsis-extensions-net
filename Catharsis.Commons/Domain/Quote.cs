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
  [EqualsAndHashCode("Type")]
  public class Quote : Item, IComparable<Quote>, ITypeable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    ///   <para>Creates new quote.</para>
    /// </summary>
    public Quote()
    {
    }

    /// <summary>
    ///   <para>Creates new quote with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on quote after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Quote(IDictionary<string, object> properties) : base(properties)
    {
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Quote(string id, string language, string name, string text, int type = 0) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);

      this.Type = type;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Quote Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var quote = new Quote((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (int) xml.Element("Type"));
      if (xml.Element("DateCreated") != null)
      {
        quote.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        quote.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return quote;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="quote"></param>
    /// <returns></returns>
    public int CompareTo(Quote quote)
    {
      return base.CompareTo(quote);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(new XElement("Type", this.Type));
    }
  }
}