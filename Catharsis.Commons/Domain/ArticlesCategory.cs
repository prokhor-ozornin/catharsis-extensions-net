using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public sealed class ArticlesCategory : Category
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
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public ArticlesCategory(string id, string language, string name, ArticlesCategory parent = null, string description = null) : base(id, language,name,parent, description)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static ArticlesCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new ArticlesCategory((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), xml.Element("Parent") != null ? ArticlesCategory.Xml(xml.Element("Parent")) : null, (string) xml.Element("Description"));
    }
  }
}