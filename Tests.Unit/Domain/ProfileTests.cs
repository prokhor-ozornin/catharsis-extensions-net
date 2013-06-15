using System;
using System.Collections.Generic;
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
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Profile()"/>
    ///   <seealso cref="Profile(IDictionary{string, object})"/>
    ///   <seealso cref="Profile(string, string, string, string, string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var profile = new Profile();
      Assert.True(profile.Id == null);
      Assert.True(profile.AuthorId == null);
      Assert.True(profile.Email == null);
      Assert.True(profile.Name == null);
      Assert.True(profile.Photo == null);
      Assert.True(profile.Type == null);
      Assert.True(profile.Url == null);
      Assert.True(profile.Username == null);

      profile = new Profile(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Email", "email@mail.ru")
        .AddNext("Name", "name")
        .AddNext("Photo", "photo")
        .AddNext("Type", "type")
        .AddNext("Url", "url")
        .AddNext("Username", "username"));
      Assert.True(profile.Id == "id");
      Assert.True(profile.AuthorId == "authorId");
      Assert.True(profile.Email == "email@mail.ru");
      Assert.True(profile.Name == "name");
      Assert.True(profile.Photo == "photo");
      Assert.True(profile.Type == "type");
      Assert.True(profile.Url == "url");
      Assert.True(profile.Username == "username");

      profile = new Profile("id", "authorId", "name", "username", "type", "url", "email@mail.ru", "photo");
      Assert.True(profile.Id == "id");
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

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("AuthorId", new[] { "AuthorId", "AuthorId_2" })
        .AddNext("Type", new[] { "Type", "Type_2" })
        .AddNext("Username", new[] { "Username", "Username_2" }));
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
  }
}