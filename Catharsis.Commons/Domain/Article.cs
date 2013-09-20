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
  public class Article : Item, IEquatable<Article>, IImageable
  {
    /// <summary>
    ///   <para>Short summary description of article.</para>
    /// </summary>
    public string Annotation { get; set; }
    
    /// <summary>
    ///   <para>Category of article.</para>
    /// </summary>
    public ArticlesCategory Category { get; set; }
    
    /// <summary>
    ///   <para>Associated image, representing article's text contents.</para>
    /// </summary>
    public Image Image { get; set; }

    /// <summary>
    ///   <para>Creates new article.</para>
    /// </summary>
    public Article()
    {
    }

    /// <summary>
    ///   <para>Creates new article with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on article after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Article(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new article.</para>
    /// </summary>
    /// <param name="id">Unique identifier of article.</param>
    /// <param name="language">ISO language code of article's text content.</param>
    /// <param name="name">Title of article.</param>
    /// <param name="category">Category of article's belongings, or a <c>null</c> reference.</param>
    /// <param name="annotation"></param>
    /// <param name="text">Article's body text.</param>
    /// <param name="authorId">Identifier of article's publisher.</param>
    /// <param name="image"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public Article(string id, string language, string name, ArticlesCategory category = null, string annotation = null, string text = null, string authorId = null, Image image = null) : base(id, language, name, text, authorId)
    {
      this.Category = category;
      this.Annotation = annotation;
      this.Image = image;
    }

    /// <summary>
    ///   <para>Creates new article from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Article"/> type.</param>
    /// <returns>Recreated article object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Article Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var article = new Article((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), xml.Element("ArticlesCategory") != null ? ArticlesCategory.Xml(xml.Element("ArticlesCategory")) : null, (string) xml.Element("Annotation"), (string) xml.Element("Text"), (string) xml.Element("AuthorId"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null);
      if (xml.Element("DateCreated") != null)
      {
        article.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        article.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return article;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Article"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Annotation != null ? new XElement("Annotation", this.Annotation) : null,
        this.Category != null ? this.Category.Xml() : null,
        this.Image != null ? this.Image.Xml() : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Article other)
    {
      return base.Equals(other);
    }
  }
}