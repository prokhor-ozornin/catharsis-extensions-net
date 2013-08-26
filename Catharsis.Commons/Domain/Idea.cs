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
  public class Idea : Item, IDescriptable, IImageable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public IdeasCategory Category { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Image Image { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Idea InspiredBy { get; set; }

    /// <summary>
    ///   <para>Creates new idea.</para>
    /// </summary>
    public Idea()
    {
    }

    /// <summary>
    ///   <para>Creates new idea with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on idea after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Idea(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="authorId"></param>
    /// <param name="text"></param>
    /// <param name="category"></param>
    /// <param name="inspiredBy"></param>
    /// <param name="description"></param>
    /// <param name="image"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="authorId"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="authorId"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Idea(string id, string language, string name, string authorId, string text, IdeasCategory category = null, Idea inspiredBy = null, string description = null, Image image = null) : base(id, language, name, text, authorId)
    {
      Assertion.NotEmpty(authorId);
      Assertion.NotEmpty(text);

      this.Category = category;
      this.Description = description;
      this.InspiredBy = inspiredBy;
      this.Image = image;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Idea Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var idea = new Idea((string)xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("AuthorId"), (string) xml.Element("Text"), xml.Element("IdeasCategory") != null ? IdeasCategory.Xml(xml.Element("IdeasCategory")) : null, xml.Element("InspiredBy") != null ? Idea.Xml(xml.Element("InspiredBy").Element("Idea")) : null, (string) xml.Element("Description"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null);
      if (xml.Element("DateCreated") != null)
      {
        idea.DateCreated = (DateTime)xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        idea.LastUpdated = (DateTime)xml.Element("LastUpdated");
      }
      return idea;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml () : null,
        this.Description != null ? new XElement("Description", this.Description) : null,
        this.Image != null ? this.Image.Xml() : null,
        this.InspiredBy != null ? new XElement("InspiredBy", this.InspiredBy.Xml()) : null);
    }
  }
}