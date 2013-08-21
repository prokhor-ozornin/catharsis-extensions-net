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
  [EqualsAndHashCode("Email", "Type")]
  public class Subscription : EntityBase, IComparable<Subscription>, IAuthorable, IEmailable, ITimeable, ITypeable
  {
    private string authorId;
    private string email;
    private string token;

    /// <summary>
    ///   <para></para>
    /// </summary>
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
    ///   <para></para>
    /// </summary>
    public bool Active { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Email
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
    public DateTime? ExpiredOn { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Token
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
    public int Type { get; set; }

    /// <summary>
    ///   <para>Creates new subscription.</para>
    /// </summary>
    public Subscription()
    {
      this.Active = true;
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
      this.Token = Guid.NewGuid().ToString();
    }

    /// <summary>
    ///   <para>Creates new subscription with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on subscription after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Subscription(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authorId"></param>
    /// <param name="email"></param>
    /// <param name="type"></param>
    /// <param name="expiredOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/> or <paramref name="email"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="authorId"/> or <paramref name="email"/> is <see cref="string.Empty"/> string.</exception>
    public Subscription(string id, string authorId, string email, int type = 0, DateTime? expiredOn = null) : this()
    {
      this.Id = id;
      this.AuthorId = authorId;
      this.Email = email;
      this.Type = type;
      this.ExpiredOn = expiredOn;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Subscription Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var subscription = new Subscription((string) xml.Element("Id"), (string) xml.Element("AuthorId"), (string) xml.Element("Email"), (int) xml.Element("Type"), (DateTime?) xml.Element("ExpiredOn"));
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
    ///   <para>Returns a <see cref="string"/> that represents the current subscription.</para>
    /// </summary>
    /// <returns>A string that represents the current subscription.</returns>
    public override string ToString()
    {
      return this.Email;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subscription"></param>
    /// <returns></returns>
    public int CompareTo(Subscription subscription)
    {
      return this.DateCreated.CompareTo(subscription.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        new XElement("Active", this.Active),
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        new XElement("Email", this.Email),
        this.ExpiredOn.HasValue ? new XElement("ExpiredOn", this.ExpiredOn.GetValueOrDefault().ToRFC1123()) : null,
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        new XElement("Token", this.Token),
        new XElement("Type", this.Type));
    }
  }
}