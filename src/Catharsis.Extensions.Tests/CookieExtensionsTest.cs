using System.Net;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CookieExtensions"/>.</para>
/// </summary>
public sealed class CookieExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="CookieExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((Cookie) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

    new Cookie().IsEmpty().Should().BeTrue();
    new Cookie("name", null).IsEmpty().Should().BeTrue();
    new Cookie("name", string.Empty).IsEmpty().Should().BeTrue();
    new Cookie("name", " \t\r\n ").IsEmpty().Should().BeTrue();
    new Cookie("name", "value").IsEmpty().Should().BeFalse();
  }
}