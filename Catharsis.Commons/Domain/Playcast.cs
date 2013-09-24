using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category")]
  public class Playcast : Item, IEquatable<Playcast>, IImageable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Audio Audio { get; set; }

    /// <summary>
    ///   <para>Category of playcast.</para>
    /// </summary>
    public virtual PlaycastsCategory Category { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Image Image { get; set; }

    /// <summary>
    ///   <para>Creates new playcast.</para>
    /// </summary>
    public Playcast()
    {
    }

    /// <summary>
    ///   <para>Creates new playcast with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on playcast after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Playcast(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new playcast.</para>
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="language">ISO language code of playcast's text content.</param>
    /// <param name="name">Title of playcast.</param>
    /// <param name="text">Playcast's body text.</param>
    /// <param name="category">Category of playcast's belongings, or a <c>null</c> reference.</param>
    /// <param name="audio"></param>
    /// <param name="image"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="authorId"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="authorId"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Playcast(string authorId, string language, string name, string text, PlaycastsCategory category = null, Audio audio = null, Image image = null) : base(language, name, text, authorId)
    {
      Assertion.NotEmpty(authorId);
      Assertion.NotEmpty(text);

      this.Category = category;
      this.Audio = audio;
      this.Image = image;
    }

    /// <summary>
    ///   <para>Creates new playcast from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Playcast"/> type.</param>
    /// <returns>Recreated playcast object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Playcast Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var playcast = new Playcast((string)xml.Element("AuthorId"), (string)xml.Element("Language"), (string)xml.Element("Name"), (string)xml.Element("Text"), xml.Element("PlaycastsCategory") != null ? PlaycastsCategory.Xml(xml.Element("PlaycastsCategory")) : null, xml.Element("Audio") != null ? Audio.Xml(xml.Element("Audio")) : null, xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null);
      if (xml.Element("Id") != null)
      {
        playcast.Id = (long) xml.Element("Id");
      }
      if (xml.Element("DateCreated") != null)
      {
        playcast.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        playcast.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return playcast;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Playcast"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Audio != null ? this.Audio.Xml() : null,
        this.Category != null ? this.Category.Xml() : null,
        this.Image != null ? this.Image.Xml() : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Playcast other)
    {
      return base.Equals(other);
    }
  }
}