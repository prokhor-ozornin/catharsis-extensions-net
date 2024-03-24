using System.Net;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CookieExtensions"/>.</para>
/// </summary>
public sealed class CookieExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.Clone(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => CookieExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

    Validate(new Cookie());
    //Validate(new Cookie());
    throw new NotImplementedException();

    return;

    static void Validate(Cookie original)
    {
      var clone = original.Clone();

      clone.Should().NotBeSameAs(original).And.NotBe(original);
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
      clone.TimeStamp.Should().Be(original.TimeStamp);
      clone.Version.Should().Be(original.Version);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((Cookie) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

    Validate(true, new Cookie());
    Validate(true, new Cookie("name", null));
    Validate(true, new Cookie("name", string.Empty));
    Validate(true, new Cookie("name", " \t\r\n "));
    Validate(false, new Cookie("name", "value"));

    return;

    static void Validate(bool result, Cookie cookie) => cookie.IsEmpty().Should().Be(result);
  }
}