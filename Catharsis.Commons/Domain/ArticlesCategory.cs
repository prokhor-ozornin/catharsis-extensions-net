using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Category of articles.</para>
  /// </summary>
  public class ArticlesCategory : Category
  {
    /// <summary>
    ///   <para>Creates new category of articles.</para>
    /// </summary>
    public ArticlesCategory()
    {
    }

    /// <summary>
    ///   <para>Creates new category of articles with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on category after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public ArticlesCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new category of articles.</para>
    /// </summary>
    /// <param name="id">Unique identifier of category.</param>
    /// <param name="language">ISO language code of category's text content.</param>
    /// <param name="name">Name of category.</param>
    /// <param name="parent">Parent of category, or <c>null</c> if there is no parent.</param>
    /// <param name="description">Description of category.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public ArticlesCategory(string id, string language, string name, ArticlesCategory parent = null, string description = null) : base(id, language,name,parent, description)
    {
    }

    /// <summary>
    ///   <para>Creates new category of articles from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="ArticlesCategory"/> type.</param>
    /// <returns>Recreated category object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static ArticlesCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new ArticlesCategory((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), xml.Element("Parent") != null ? ArticlesCategory.Xml(xml.Element("Parent")) : null, (string) xml.Element("Description"));
    }
  }
}