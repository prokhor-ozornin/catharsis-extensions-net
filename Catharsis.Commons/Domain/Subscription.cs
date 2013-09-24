using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Email,Type")]
  public class Subscription : EntityBase, IComparable<Subscription>, IEquatable<Subscription>, IAuthorable, IEmailable, ITimeable, ITypeable
  {
    private string authorId;
    private DateTime dateCreated = DateTime.UtcNow;
    private string email;
    private DateTime lastUpdated = DateTime.UtcNow;
    private string token;

    /// <summary>
    ///   <para></para>
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
    ///   <para></para>
    /// </summary>
    public virtual bool Active { get; set; }
    
    /// <summary>
    ///   <para>Date and time of subscription's creation.</para>
    /// </summary>
    public virtual DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Email
    {
      get { return this.email; }
      set
      {
        Assertion.NotEmpty(value);

        this.email = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual DateTime? ExpiredOn { get; set; }
    
    /// <summary>
    ///   <para>Date and time of subscription's last modification.</para>
    /// </summary>
    public virtual DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Token
    {
      get { return this.token; }
      set
      {
        Assertion.NotEmpty(value);

        this.token = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual int Type { get; set; }

    /// <summary>
    ///   <para>Creates new subscription.</para>
    /// </summary>
    public Subscription()
    {
      this.Active = true;
      this.Token = Guid.NewGuid().ToString();
    }

    /// <summary>
    ///   <para>Creates new subscription with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on subscription after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Subscription(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new subscription.</para>
    /// </summary>
    /// <param name="authorId">Identifier of subscription's </param>
    /// <param name="email"></param>
    /// <param name="type"></param>
    /// <param name="expiredOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="authorId"/> or <paramref name="email"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="authorId"/> or <paramref name="email"/> is <see cref="string.Empty"/> string.</exception>
    public Subscription(string authorId, string email, int type = 0, DateTime? expiredOn = null)
    {
      this.Active = true;
      this.AuthorId = authorId;
      this.Email = email;
      this.Token = Guid.NewGuid().ToString();
      this.Type = type;
      this.ExpiredOn = expiredOn;
    }

    /// <summary>
    ///   <para>Creates new subscription from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Subscription"/> type.</param>
    /// <returns>Recreated subscription object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Subscription Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var subscription = new Subscription((string) xml.Element("AuthorId"), (string) xml.Element("Email"), (int) xml.Element("Type"), (DateTime?) xml.Element("ExpiredOn"));
      if (xml.Element("Id") != null)
      {
        subscription.Id = (long) xml.Element("Id");
      }
      if (xml.Element("DateCreated") != null)
      {
        subscription.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        subscription.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Token") != null)
      {
        subscription.Token = (string) xml.Element("Token");
      }
      return subscription;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public virtual bool Equals(Subscription other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current subscription.</para>
    /// </summary>
    /// <returns>A string that represents the current subscription.</returns>
    public override string ToString()
    {
      return this.Email;
    }

    /// <summary>
    ///   <para>Compares the current subscription with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Subscription"/> to compare with this instance.</param>
    public virtual int CompareTo(Subscription other)
    {
      return this.DateCreated.CompareTo(other.DateCreated);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Subscription"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        new XElement("Active", this.Active),
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("Email", this.Email),
        this.ExpiredOn.HasValue ? new XElement("ExpiredOn", this.ExpiredOn.GetValueOrDefault().ToRfc1123()) : null,
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Token", this.Token),
        new XElement("Type", this.Type));
    }
  }
}