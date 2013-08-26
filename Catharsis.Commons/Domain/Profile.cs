using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("AuthorId", "Type", "Username")]
  public class Profile : EntityBase, IComparable<Profile>, IAuthorable, INameable, IUrlAddressable
  {
    private string authorId;
    private string name;
    private string type;
    private string url;
    private string username;

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
    public string Email { get; set; }
    
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
    public string Photo { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Type
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
    public string Url
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
    public string Username
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
    ///   <para>Creates new profile with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on profile after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Profile(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authorId"></param>
    /// <param name="name"></param>
    /// <param name="username"></param>
    /// <param name="type"></param>
    /// <param name="url"></param>
    /// <param name="email"></param>
    /// <param name="photo"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="name"/>, <paramref name="username"/>, <paramref name="type"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="name"/>, <paramref name="username"/>, <paramref name="type"/> or <paramref name="url"/> is <see cref="string.Empty"/> string.</exception>
    public Profile(string id, string authorId, string name, string username, string type, string url, string email = null, string photo = null) : base(id)
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
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Profile Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new Profile((string) xml.Element("Id"), (string) xml.Element("AuthorId"), (string) xml.Element("Name"), (string) xml.Element("Username"), (string) xml.Element("Type"), (string) xml.Element("Url"), (string) xml.Element("Email"), (string) xml.Element("Photo"));
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
    ///   <para></para>
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    public int CompareTo(Profile profile)
    {
      return this.Username.CompareTo(profile.Username);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
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