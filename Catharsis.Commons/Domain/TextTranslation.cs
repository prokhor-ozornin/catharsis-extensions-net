using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  [EqualsAndHashCode("Language", "Name", "Translator")]
  public class TextTranslation : EntityBase, ILocalizable, INameable, ITextable
  {
    private string language;
    private string name;
    private string text;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Language
    {
      get { return this.language; }
      set
      {
        Assertion.NotEmpty(value);

        this.language = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Name
    {
      get { return this.name; }
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text
    {
      get { return this.text; }
      set
      {
        Assertion.NotEmpty(value);

        this.text = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Translator { get; set; }

    /// <summary>
    ///   <para>Creates new translation.</para>
    /// </summary>
    public TextTranslation()
    {
    }

    /// <summary>
    ///   <para>Creates new translation with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on translation after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public TextTranslation(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="translator"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public TextTranslation(string id, string language, string name, string text, string translator = null) : base(id)
    {
      this.Language = language;
      this.Name = name;
      this.Text = text;
      this.Translator = translator;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static TextTranslation Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new TextTranslation((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("Translator"));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Language", this.Language),
        new XElement("Name", this.Name),
        new XElement("Text", this.Text),
        this.Translator != null ? new XElement("Translator", this.Translator) : null);
    }
  }
}