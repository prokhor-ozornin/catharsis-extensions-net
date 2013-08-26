using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category", "Url")]
  public class WebLink : Item, IUrlAddressable
  {
    private string url;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public WebLinksCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Url
    {
      get { return this.url; }
      set
      {
        Assertion.NotEmpty(value);

        this.url = value;
      }
    }

    /// <summary>
    ///   <para>Creates new web link.</para>
    /// </summary>
    public WebLink()
    {
    }

    /// <summary>
    ///   <para>Create new web link with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on web link after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public WebLink(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="url"></param>
    /// <param name="category"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    public WebLink(string id, string language, string name, string text, string url, WebLinksCategory category = null) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);

      this.Url = url;
      this.Category = category;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static WebLink Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var weblink = new WebLink((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("Url"), xml.Element("WebLinksCategory") != null ? WebLinksCategory.Xml(xml.Element("WebLinksCategory")) : null);
      if (xml.Element("DateCreated") != null)
      {
        weblink.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        weblink.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return weblink;
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current web link.</para>
    /// </summary>
    /// <returns>A string that represents the current web link.</returns>
    public override string ToString()
    {
      return this.Url;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Url", this.Url));
    }
  }
}