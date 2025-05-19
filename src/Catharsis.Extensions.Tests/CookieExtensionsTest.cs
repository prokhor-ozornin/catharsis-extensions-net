using AutoFixture;
using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CookieExtensions"/>.</para>
/// </summary>
public sealed class CookieExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.IsUnset(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Test(true, null);
      Test(true, new Cookie());
      Test(true, new Cookie("name", null));
      Test(true, new Cookie("name", string.Empty));
      Test(true, new Cookie("name", " \t\r\n "));
      Test(false, new Cookie("name", "value"));
    }

    return;

    static void Test(bool result, Cookie cookie) => cookie.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Cookie) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

      Test(true, new Cookie());
      Test(true, new Cookie("name", null));
      Test(true, new Cookie("name", string.Empty));
      Test(true, new Cookie("name", " \t\r\n "));
      Test(false, new Cookie("name", "value"));
    }

    return;

    static void Test(bool result, Cookie cookie) => cookie.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.Clone(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CookieExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

      Test(new Cookie("id", string.Empty, "/", "localhost"));
      
      Test(new Cookie
      {
        Name = "id",
        Value = string.Empty,
        Comment = "comment",
        CommentUri = "localhost".ToUri(),
        Domain = "localhost",
        Expires = DateTime.Now,
        Expired = true,
        Discard = true,
        HttpOnly = true,
        Path = "/",
        Port = string.Empty,
        Secure = true,
        Version = 1
      });
    }

    return;

    static void Test(Cookie original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<Cookie>().And.NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.Name.Should().Be(original.Name);
      clone.Value.Should().Be(original.Value);
      clone.Comment.Should().Be(original.Comment);
      clone.CommentUri.Should().Be(original.CommentUri);
      clone.Domain.Should().Be(original.Domain);
      clone.Expires.Should().Be(original.Expires);
      clone.Expired.Should().Be(original.Expired);
      clone.Discard.Should().Be(original.Discard);
      clone.HttpOnly.Should().Be(original.HttpOnly);
      clone.Path.Should().Be(original.Path);
      clone.Port.Should().Be(original.Port);
      clone.Secure.Should().Be(original.Secure);
      clone.TimeStamp.Should().BeOnOrAfter(original.TimeStamp);
      clone.Version.Should().Be(original.Version);
    }
  }
}