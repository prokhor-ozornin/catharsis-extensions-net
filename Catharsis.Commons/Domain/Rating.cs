using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("AuthorId,Item")]
  public class Rating : EntityBase, IComparable<Rating>, IAuthorable, ITimeable
  {
    private string authorId;
    private Item item;

    /// <summary>
    ///   <para>Identifier of the user who has rated an item.</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string AuthorId
    {
      get { return this.authorId; }
      set
      {
        Assertion.NotEmpty(value);

        this.authorId = value;
      }
    }

    /// <summary>
    ///   <para>Date and time of rating's creation.</para>
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///   <para>Subject item that was rated.</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public Item Item
    {
      get { return this.item; }
      set
      {
        Assertion.NotNull(value);

        this.item = value;
      }
    }

    /// <summary>
    ///   <para>Date and time of rating's last modification.</para>
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public byte Value { get; set; }

    /// <summary>
    ///   <para>Creates new rating.</para>
    /// </summary>
    public Rating()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new rating with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on rating after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Rating(IDictionary<string, object> properties) : base(properties)
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id">Unique identifier of rating.</param>
    /// <param name="authorId">Identifier of rating's maker.</param>
    /// <param name="item"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/> or <paramref name="item"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public Rating(string id, string authorId, Item item, byte value) : base(id)
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;

      this.AuthorId = authorId;
      this.Item = item;
      this.Value = value;
    }

    /// <summary>
    ///   <para>Creates new rating from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Rating"/> type.</param>
    /// <returns>Recreated rating object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Rating Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var rating = new Rating((string) xml.Element("Id"), (string) xml.Element("AuthorId"), Item.Xml(xml.Element("Item")), (byte) (int) xml.Element("Value"));
      if (xml.Element("DateCreated") != null)
      {
        rating.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        rating.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return rating;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.Value.ToString();
    }

    /// <summary>
    ///   <para>Compares the current rating with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Rating"/> to compare with this instance.</param>
    public int CompareTo(Rating other)
    {
      return this.Value.CompareTo(other.Value);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Rating"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        this.Item.Xml(),
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        new XElement("Value", this.Value));
    }
  }
}