using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("AuthorId,Type,Username")]
  public class Profile : EntityBase, IComparable<Profile>, IEquatable<Profile>, IAuthorable, INameable, IUrlAddressable
  {
    private string name;
    private string type;
    private string url;
    private string username;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual long? AuthorId { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Email { get; set; }
    
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
    public virtual string Photo { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Type
    {
      get { return this.type; }
      set
      {
        Assertion.NotEmpty(value);

        this.type = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Url
    {
      get { return this.url; }
      set
      {
        Assertion.NotEmpty(value);

        this.url = value;
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
    ///   <para>Creates new profile.</para>
    /// </summary>
    public Profile()
    {
    }

    /// <summary>
    ///   <para>Creates new profile.</para>
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="name">Name of profile's user.</param>
    /// <param name="username"></param>
    /// <param name="type"></param>
    /// <param name="url"></param>
    /// <param name="email"></param>
    /// <param name="photo"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="name"/>, <paramref name="username"/>, <paramref name="type"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="name"/>, <paramref name="username"/>, <paramref name="type"/> or <paramref name="url"/> is <see cref="string.Empty"/> string.</exception>
    public Profile(long authorId, string name, string username, string type, string url, string email = null, string photo = null)
    {
      this.AuthorId = authorId;
      this.Name = name;
      this.Username = username;
      this.Type = type;
      this.Url = url;
      this.Email = email;
      this.Photo = photo;
    }

    /// <summary>
    ///   <para>Creates new profile from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Profile"/> type.</param>
    /// <returns>Recreated profile object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Profile Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var profile = new Profile((long) xml.Element("AuthorId"), (string) xml.Element("Name"), (string) xml.Element("Username"), (string) xml.Element("Type"), (string) xml.Element("Url"), (string) xml.Element("Email"), (string) xml.Element("Photo"));
      if (xml.Element("Id") != null)
      {
        profile.Id = (long) xml.Element("Id");
      }
      return profile;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Profile other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current profile.</para>
    /// </summary>
    /// <returns>A string that represents the current profile.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current profile with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Profile"/> to compare with this instance.</param>
    public virtual int CompareTo(Profile other)
    {
      return this.Username.Compare(other.Username, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Profile"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        this.Email != null ? new XElement("Email", this.Email) : null,
        new XElement("Name", this.Name),
        this.Photo != null ? new XElement("Photo", this.Photo) : null,
        new XElement("Type", this.Type),
        new XElement("Url", this.Url),
        new XElement("Username", this.Username));
    }
  }
}