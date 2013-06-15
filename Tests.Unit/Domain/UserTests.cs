using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="User"/>.</para>
  /// </summary>
  public sealed class UserTests : EntityUnitTests<User>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="User()"/>
    ///   <seealso cref="User(IDictionary{string, object})"/>
    ///   <seealso cref="User(string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var user = new User();
      Assert.True(user.Id == null);
      Assert.True(user.DateCreated <= DateTime.UtcNow);
      Assert.True(user.Email == null);
      Assert.True(user.LastUpdated <= DateTime.UtcNow);
      Assert.True(user.Name == null);
      Assert.True(user.Username == null);

      user = new User(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Email", "email@mail.ru")
        .AddNext("Name", "name")
        .AddNext("Username", "username"));
      Assert.True(user.Id == "id");
      Assert.True(user.DateCreated <= DateTime.UtcNow);
      Assert.True(user.Email == "email@mail.ru");
      Assert.True(user.LastUpdated <= DateTime.UtcNow);
      Assert.True(user.Name == "name");
      Assert.True(user.Username == "username");

      user = new User("id", "username", "email@mail.ru", "name");
      Assert.True(user.Id == "id");
      Assert.True(user.DateCreated <= DateTime.UtcNow);
      Assert.True(user.Email == "email@mail.ru");
      Assert.True(user.LastUpdated <= DateTime.UtcNow);
      Assert.True(user.Name == "name");
      Assert.True(user.Username == "username");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new User { Name = "Name" }.ToString() == "Name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Username", new[] { "Username", "Username_2" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="User.CompareTo(User)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new User { Username = "Username" }.CompareTo(new User { Username = "Username"}) == 0);
      Assert.True(new User { Username = "a" }.CompareTo(new User { Username = "b" }) < 0);
    }
  }
}