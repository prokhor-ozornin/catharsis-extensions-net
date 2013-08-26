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
  public class Announcement : Item, IImageable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public AnnouncementsCategory Category { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Currency { get; set; } 
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    ///   <para>Creates new announcement.</para>
    /// </summary>
    public Announcement()
    {
    }

    /// <summary>
    ///   <para>Creates new announcement with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on announcement after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Announcement(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="authorId"></param>
    /// <param name="category"></param>
    /// <param name="image"></param>
    /// <param name="currency"></param>
    /// <param name="price"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="authorId"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="authorId"/> is a <c>null</c> reference.</exception>
    public Announcement(string id, string language, string name, string text, string authorId, AnnouncementsCategory category = null, Image image = null, string currency = null, decimal? price = null) : base(id, language, name, text, authorId)
    {
      Assertion.NotEmpty(text);
      Assertion.NotEmpty(authorId);

      this.Category = category;
      this.Image = image;
      this.Currency = currency;
      this.Price = price;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Announcement Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var announcement = new Announcement((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("AuthorId"), xml.Element("AnnouncementsCategory") != null ? AnnouncementsCategory.Xml(xml.Element("AnnouncementsCategory")) : null, xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null, (string) xml.Element("Currency"), (decimal?) xml.Element("Price"));
      if (xml.Element("DateCreated") != null)
      {
        announcement.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        announcement.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return announcement;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        this.Currency != null ? new XElement("Currency", this.Currency) : null,
        this.Image != null ? this.Image.Xml() : null,
        this.Price.HasValue ? new XElement("Price", this.Price.GetValueOrDefault()) : null);
    }
  }
}