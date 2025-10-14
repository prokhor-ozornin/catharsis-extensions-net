using AutoFixture;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="MatchExtensions"/>.</para>
/// </summary>
/// <seealso cref="MatchExtensions"/>
public sealed class MatchExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="MatchExtensions.ToEnumerable(Match)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MatchExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("match");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<Capture> result, Match match)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MatchExtensions.ToBoolean(Match)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, Match.Empty);
      Test(true, Regex.Match(Fixture.Create<string>(), ".*"));
    }

    return;

    static void Test(bool result, Match match) => match.ToBoolean().Should().Be(result);
  }
}