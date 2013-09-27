using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Announcement is a free or paid informational message, created by user.</para>
  /// </summary>
  [EqualsAndHashCode("Category")]
  public class Announcement : Item, IEquatable<Announcement>, IImageable
  {
    /// <summary>
    ///   <para>Category of announcement.</para>
    /// </summary>
    public virtual AnnouncementsCategory Category { get; set; }

    /// <summary>
    ///   <para>Currency of announcement's price (if it's a paid one).</para>
    /// </summary>
    public virtual string Currency { get; set; } 
    
    /// <summary>
    ///   <para>Associated image, representing announcement's nature and contents.</para>
    /// </summary>
    public virtual Image Image { get; set; }
    
    /// <summary>
    ///   <para>Price of announcement's contents (if it's paid one).</para>
    /// </summary>
    public virtual decimal? Price { get; set; }

    /// <summary>
    ///   <para>Creates new announcement.</para>
    /// </summary>
    public Announcement()
    {
    }

    /// <summary>
    ///   <para>Creates new announcement.</para>
    /// </summary>
    /// <param name="language">ISO language code of announcement's text content.</param>
    /// <param name="name">Name of announcement.</param>
    /// <param name="text">Announcement's body text.</param>
    /// <param name="authorId">Identifier of announcement's publisher.</param>
    /// <param name="category">Category of announcement's belongings, or a <c>null</c> reference.</param>
    /// <param name="image">Associated announcement's image, or a <c>null</c> reference.</param>
    /// <param name="currency">Currency of announcement's price, or a <c>null</c> reference.</param>
    /// <param name="price">Price of announcement's content, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    public Announcement(string language, string name, string text, long authorId, AnnouncementsCategory category = null, Image image = null, string currency = null, decimal? price = null) : base(language, name, text, authorId)
    {
      Assertion.NotEmpty(text);

      this.Category = category;
      this.Image = image;
      this.Currency = currency;
      this.Price = price;
    }

    /// <summary>
    ///   <para>Creates new announcement from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Announcement"/> type.</param>
    /// <returns>Recreated announcement object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Announcement Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var announcement = new Announcement((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (long) xml.Element("AuthorId"), xml.Element("AnnouncementsCategory") != null ? AnnouncementsCategory.Xml(xml.Element("AnnouncementsCategory")) : null, xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null, (string) xml.Element("Currency"), (decimal?) xml.Element("Price"));
      if (xml.Element("Id") != null)
      {
        announcement.Id = (long) xml.Element("Id");
      }
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
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Announcement"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        this.Currency != null ? new XElement("Currency", this.Currency) : null,
        this.Image != null ? this.Image.Xml() : null,
        this.Price.HasValue ? new XElement("Price", this.Price.GetValueOrDefault()) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Announcement other)
    {
      return base.Equals(other);
    }
  }
}