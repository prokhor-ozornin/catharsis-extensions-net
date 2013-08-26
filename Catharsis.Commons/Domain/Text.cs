using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category", "Person")]
  public class Text : Item
  {
    private Person person;
    private readonly ICollection<TextTranslation> translations = new HashSet<TextTranslation>();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public TextsCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public Person Person
    {
      get { return this.person; }
      set
      {
        Assertion.NotNull(value);

        this.person = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ICollection<TextTranslation> Translations
    {
      get { return this.translations; }
    }

    /// <summary>
    ///   <para>Creates new text.</para>
    /// </summary>
    public Text()
    {
    }

    /// <summary>
    ///   <para>Creates new text with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on text after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Text(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authorId"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="category"></param>
    /// <param name="person"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="person"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Text(string id, string authorId, string language, string name, string text, Person person, TextsCategory category = null) : base(id, language, name, text, authorId)
    {
      Assertion.NotEmpty(authorId);
      Assertion.NotEmpty(text);

      this.Category = category;
      this.Person = person;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Text Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var text = new Text((string)xml.Element("Id"), (string)xml.Element("AuthorId"), (string)xml.Element("Language"), (string)xml.Element("Name"), (string)xml.Element("Text"), Person.Xml(xml.Element("Person")), xml.Element("TextsCategory") != null ? TextsCategory.Xml(xml.Element("TextsCategory")) : null);
      if (xml.Element("DateCreated") != null)
      {
        text.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        text.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Translations") != null)
      {
        text.Translations.AddAll(xml.Element("Translations").Descendants().Select(translation => TextTranslation.Xml(translation)));
      }
      return text;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        this.Person.Xml(),
        this.Translations.Count > 0 ? new XElement("Translations", this.Translations.Select(translation => translation.Xml())) : null);
    }
  }
}