using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Category of texts.</para>
  /// </summary>
  public class TextsCategory : Category, IEquatable<TextsCategory>
  {
    /// <summary>
    ///   <para>Creates new category of texts.</para>
    /// </summary>
    public TextsCategory()
    {
    }

    /// <summary>
    ///   <para>Creates new category of texts.</para>
    /// </summary>
    /// <param name="language">ISO language code of category's text content.</param>
    /// <param name="name">Name of category.</param>
    /// <param name="parent">Parent of category, or <c>null</c> if there is no parent.</param>
    /// <param name="description">Description of category.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public TextsCategory(string language, string name, TextsCategory parent = null, string description = null) : base(language, name, parent, description)
    {
    }

    /// <summary>
    ///   <para>Creates new category of texts from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="TextsCategory"/> type.</param>
    /// <returns>Recreated category object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static TextsCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var category = new TextsCategory((string)xml.Element("Language"), (string)xml.Element("Name"), xml.Element("Parent") != null ? Xml(xml.Element("Parent")) : null, (string)xml.Element("Description"));
      if (xml.Element("Id") != null)
      {
        category.Id = (long)xml.Element("Id");
      }
      return category;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(TextsCategory other)
    {
      return base.Equals(other);
    }
  }
}