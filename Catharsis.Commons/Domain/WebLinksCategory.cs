using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  public class WebLinksCategory : Category
  {
    /// <summary>
    ///   <para>Creates new category of web links.</para>
    /// </summary>
    public WebLinksCategory()
    {
    }

    /// <summary>
    ///   <para>Creates new category of web links with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on category after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public WebLinksCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public WebLinksCategory(string id, string language, string name, WebLinksCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static WebLinksCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new WebLinksCategory((string)xml.Element("Id"), (string)xml.Element("Language"), (string)xml.Element("Name"), xml.Element("Parent") != null ? WebLinksCategory.Xml(xml.Element("Parent")) : null, (string)xml.Element("Description"));
    }
  }
}