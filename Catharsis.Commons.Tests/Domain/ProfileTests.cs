using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Profile"/>.</para>
  /// </summary>
  public sealed class ProfileTests : EntityUnitTests<Profile>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Profile { AuthorId = null });
      Assert.Throws<ArgumentException>(() => new Profile { AuthorId = string.Empty });
      Assert.True(new Profile { AuthorId = "authorId" } .AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Email"/> property.</para>
    /// </summary>
    [Fact]
    public void Email_Property()
    {
      Assert.True(new Profile { Email = "email" }.Email == "email");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Profile { Name = null });
      Assert.Throws<ArgumentException>(() => new Profile { Name = string.Empty });
      Assert.True(new Profile { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Photo"/> property.</para>
    /// </summary>
    [Fact]
    public void Photo_Property()
    {
      Assert.True(new Profile { Photo = "photo" }.Photo == "photo");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Type"/> property.</para>
    /// </summary>
    [Fact]
    public void Type_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Profile { Type = null });
      Assert.Throws<ArgumentException>(() => new Profile { Type = string.Empty });
      Assert.True(new Profile { Type = "type" }.Type == "type");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Url"/> property.</para>
    /// </summary>
    [Fact]
    public void Url_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Profile { Url = null });
      Assert.Throws<ArgumentException>(() => new Profile { Url = string.Empty });
      Assert.True(new Profile { Url = "url" }.Url == "url");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.Username"/> property.</para>
    /// </summary>
    [Fact]
    public void Username_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Profile { Username = null });
      Assert.Throws<ArgumentException>(() => new Profile { Username = string.Empty });
      Assert.True(new Profile { Username = "username" }.Username == "username");
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Profile()"/>
    ///   <seealso cref="Profile(IDictionary{string, object})"/>
    ///   <seealso cref="Profile(string, string, string, string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var profile = new Profile();
      Assert.True(profile.Id == 0);
      Assert.True(profile.AuthorId == null);
      Assert.True(profile.Email == null);
      Assert.True(profile.Name == null);
      Assert.True(profile.Photo == null);
      Assert.True(profile.Type == null);
      Assert.True(profile.Url == null);
      Assert.True(profile.Username == null);

      Assert.Throws<ArgumentNullException>(() => new Profile(null));
      profile = new Profile(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("AuthorId", "authorId")
        .AddNext("Email", "email@mail.ru")
        .AddNext("Name", "name")
        .AddNext("Photo", "photo")
        .AddNext("Type", "type")
        .AddNext("Url", "url")
        .AddNext("Username", "username"));
      Assert.True(profile.Id == 1);
      Assert.True(profile.AuthorId == "authorId");
      Assert.True(profile.Email == "email@mail.ru");
      Assert.True(profile.Name == "name");
      Assert.True(profile.Photo == "photo");
      Assert.True(profile.Type == "type");
      Assert.True(profile.Url == "url");
      Assert.True(profile.Username == "username");

      Assert.Throws<ArgumentNullException>(() => new Profile(null, "name", "username", "type", "url"));
      Assert.Throws<ArgumentNullException>(() => new Profile("authorId", null, "username", "type", "url"));
      Assert.Throws<ArgumentNullException>(() => new Profile("authorId", "name", null, "type", "url"));
      Assert.Throws<ArgumentNullException>(() => new Profile("authorId", "name", "username", null, "url"));
      Assert.Throws<ArgumentNullException>(() => new Profile("authorId", "name", "username", "type", null));
      Assert.Throws<ArgumentException>(() => new Profile(string.Empty, "name", "username", "type", "url"));
      Assert.Throws<ArgumentException>(() => new Profile("authorId", string.Empty, "username", "type", "url"));
      Assert.Throws<ArgumentException>(() => new Profile("authorId", "name", string.Empty, "type", "url"));
      Assert.Throws<ArgumentException>(() => new Profile("authorId", "name", "username", string.Empty, "url"));
      Assert.Throws<ArgumentException>(() => new Profile("authorId", "name", "username", "type", string.Empty));
      profile = new Profile("authorId", "name", "username", "type", "url", "email@mail.ru", "photo");
      Assert.True(profile.Id == 0);
      Assert.True(profile.AuthorId == "authorId");
      Assert.True(profile.Email == "email@mail.ru");
      Assert.True(profile.Name == "name");
      Assert.True(profile.Photo == "photo");
      Assert.True(profile.Type == "type");
      Assert.True(profile.Url == "url");
      Assert.True(profile.Username == "username");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Profile { Name = "name" }.ToString() == "name");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Profile.Equals(Profile)"/></description></item>
    ///     <item><description><see cref="Profile.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("AuthorId", "AuthorId", "AuthorId_2");
      this.TestEquality("Type", "Type", "Type_2");
      this.TestEquality("Username", "Username", "Username_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("AuthorId", "AuthorId", "AuthorId_2");
      this.TestHashCode("Type", "Type", "Type_2");
      this.TestHashCode("Username", "Username", "Username_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Profile.CompareTo(Profile)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Profile { Username = "Username" }.CompareTo(new Profile { Username = "Username" }) == 0);
      Assert.True(new Profile { Username = "a" }.CompareTo(new Profile { Username = "b" }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Profile.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Profile.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Profile.Xml(null));

      var xml = new XElement("Profile",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("Name", "name"),
        new XElement("Type", "type"),
        new XElement("Url", "url"),
        new XElement("Username", "username"));
      var profile = Profile.Xml(xml);
      Assert.True(profile.Id == 1);
      Assert.True(profile.AuthorId == "authorId");
      Assert.True(profile.Email == null);
      Assert.True(profile.Name == "name");
      Assert.True(profile.Photo == null);
      Assert.True(profile.Type == "type");
      Assert.True(profile.Url == "url");
      Assert.True(profile.Username == "username");
      Assert.True(new Profile("authorId", "name", "username", "type", "url") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Profile.Xml(profile.Xml()).Equals(profile));

      xml = new XElement("Profile",
        new XElement("Id", 1),
        new XElement("AuthorId", "authorId"),
        new XElement("Email", "email"),
        new XElement("Name", "name"),
        new XElement("Photo", "photo"),
        new XElement("Type", "type"),
        new XElement("Url", "url"),
        new XElement("Username", "username"));
      profile = Profile.Xml(xml);
      Assert.True(profile.Id == 1);
      Assert.True(profile.AuthorId == "authorId");
      Assert.True(profile.Email == "email");
      Assert.True(profile.Name == "name");
      Assert.True(profile.Photo == "photo");
      Assert.True(profile.Type == "type");
      Assert.True(profile.Url == "url");
      Assert.True(profile.Username == "username");
      Assert.True(new Profile("authorId", "name", "username", "type", "url", "email", "photo") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(Profile.Xml(profile.Xml()).Equals(profile));
    }
  }
}