using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category,Url")]
  public class WebLink : Item, IEquatable<WebLink>, IUrlAddressable
  {
    private string url;

    /// <summary>
    ///   <para>Category of web link.</para>
    /// </summary>
    public virtual WebLinksCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Url
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
    ///   <para>Creates new web link.</para>
    /// </summary>
    /// <param name="language">ISO language code of web link's text content.</param>
    /// <param name="name">Name of web link.</param>
    /// <param name="text">Web link's description text.</param>
    /// <param name="url"></param>
    /// <param name="category">Category of web link's belongings, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    public WebLink(string language, string name, string text, string url, WebLinksCategory category = null) : base(language, name, text)
    {
      Assertion.NotEmpty(text);

      this.Url = url;
      this.Category = category;
    }

    /// <summary>
    ///   <para>Creates new web link from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="WebLink"/> type.</param>
    /// <returns>Recreated web link object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static WebLink Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var weblink = new WebLink((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("Url"), xml.Element("WebLinksCategory") != null ? WebLinksCategory.Xml(xml.Element("WebLinksCategory")) : null);
      if (xml.Element("Id") != null)
      {
        weblink.Id = (long) xml.Element("Id");
      }
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
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(WebLink other)
    {
      return base.Equals(other);
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
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="WebLink"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Url", this.Url));
    }
  }
}