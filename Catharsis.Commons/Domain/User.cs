using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Username")]
  public class User : EntityBase, IComparable<User>, IEquatable<User>, IEmailable, INameable, ITimeable
  {
    private DateTime dateCreated = DateTime.UtcNow;
    private string email;
    private DateTime lastUpdated = DateTime.UtcNow;
    private string name;
    private string username;

    /// <summary>
    ///   <para>Date and time of user's creation.</para>
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
    ///   <para>Date and time of user's last modification.</para>
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
    public virtual string Name
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
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Username
    {
      get { return this.username; }
      set
      {
        Assertion.NotEmpty(value);

        this.username = value;
      }
    }

    /// <summary>
    ///   <para>Creates new user.</para>
    /// </summary>
    public User()
    {
    }

    /// <summary>
    ///   <para>Creates new user with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on user after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public User(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new user.</para>
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="name">Name of user.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="username"/>, <paramref name="email"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="username"/>, <paramref name="email"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public User(string username, string email, string name)
    {
      this.Username = username;
      this.Email = email;
      this.Name = name;
    }

    /// <summary>
    ///   <para>Creates new user from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="User"/> type.</param>
    /// <returns>Recreated user object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static User Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var user = new User((string) xml.Element("Username"), (string) xml.Element("Email"), (string) xml.Element("Name"));
      if (xml.Element("Id") != null)
      {
        user.Id = (long) xml.Element("Id");
      }
      if (xml.Element("DateCreated") != null)
      {
        user.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        user.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return user;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(User other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current user.</para>
    /// </summary>
    /// <returns>A string that represents the current user.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current user with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="User"/> to compare with this instance.</param>
    public virtual int CompareTo(User other)
    {
      return this.Username.Compare(other.Username, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="User"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("Email", this.Email),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Name", this.Name),
        new XElement("Username", this.Username));
    }
  }
}