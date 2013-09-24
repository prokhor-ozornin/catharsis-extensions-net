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
  public class Rating : EntityBase, IComparable<Rating>, IEquatable<Rating>, IAuthorable, ITimeable
  {
    private string authorId;
    private DateTime dateCreated = DateTime.UtcNow;
    private Item item;
    private DateTime lastUpdated = DateTime.UtcNow;

    /// <summary>
    ///   <para>Identifier of the user who has rated an item.</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string AuthorId
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
    public virtual DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }

    /// <summary>
    ///   <para>Subject item that was rated.</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public virtual Item Item
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
    public virtual DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual byte Value { get; set; }

    /// <summary>
    ///   <para>Creates new rating.</para>
    /// </summary>
    public Rating()
    {
    }

    /// <summary>
    ///   <para>Creates new rating with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on rating after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Rating(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="authorId">Identifier of rating's maker.</param>
    /// <param name="item"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="authorId"/> or <paramref name="item"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public Rating(string authorId, Item item, byte value)
    {
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

      var rating = new Rating((string) xml.Element("AuthorId"), Item.Xml(xml.Element("Item")), (byte) (int) xml.Element("Value"));
      if (xml.Element("Id") != null)
      {
        rating.Id = (long) xml.Element("Id");
      }
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
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Rating other)
    {
      return base.Equals(other);
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
    public virtual int CompareTo(Rating other)
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
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        this.Item.Xml(),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Value", this.Value));
    }
  }
}