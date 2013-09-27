using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Category of announcements.</para>
  /// </summary>
  public class AnnouncementsCategory : Category, IEquatable<AnnouncementsCategory>
  {
    /// <summary>
    ///   <para>Creates new category of announcements.</para>
    /// </summary>
    public AnnouncementsCategory()
    {
    }

    /// <summary>
    ///   <para>Creates new category of announcements.</para>
    /// </summary>
    /// <param name="language">ISO language code of category's text content.</param>
    /// <param name="name">Name of category.</param>
    /// <param name="parent">Parent of category, or <c>null</c> if there is no parent.</param>
    /// <param name="description">Description of category.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public AnnouncementsCategory(string language, string name, AnnouncementsCategory parent = null, string description = null) : base(language, name, parent, description)
    {
    }

    /// <summary>
    ///   <para>Creates new category of announcements from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="AnnouncementsCategory"/> type.</param>
    /// <returns>Recreated category object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static AnnouncementsCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var category = new AnnouncementsCategory((string)xml.Element("Language"), (string)xml.Element("Name"), xml.Element("Parent") != null ? Xml(xml.Element("Parent")) : null, (string)xml.Element("Description"));
      if (xml.Element("Id") != null)
      {
        category.Id = (long) xml.Element("Id");
      }
      return category;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(AnnouncementsCategory other)
    {
      return base.Equals(other);
    }
  }
}