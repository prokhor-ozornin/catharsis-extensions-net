using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Category of audios.</para>
  /// </summary>
  public class AudiosCategory : Category, IEquatable<AudiosCategory>
  {
    /// <summary>
    ///   <para>Creates new category of audios.</para>
    /// </summary>
    public AudiosCategory()
    {
    }

    /// <summary>
    ///   <para>Creates new category of audios.</para>
    /// </summary>
    /// <param name="language">ISO language code of category's text content.</param>
    /// <param name="name">Name of category.</param>
    /// <param name="parent">Parent of category, or <c>null</c> if there is no parent.</param>
    /// <param name="description">Description of category.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public AudiosCategory(string language, string name, AudiosCategory parent = null, string description = null) : base(language, name, parent, description)
    {
    }

    /// <summary>
    ///   <para>Creates new category of audios from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="AudiosCategory"/> type.</param>
    /// <returns>Recreated category object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static AudiosCategory Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var category = new AudiosCategory((string)xml.Element("Language"), (string)xml.Element("Name"), xml.Element("Parent") != null ? Xml(xml.Element("Parent")) : null, (string)xml.Element("Description"));
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
    public virtual bool Equals(AudiosCategory other)
    {
      return base.Equals(other);
    }
  }
}