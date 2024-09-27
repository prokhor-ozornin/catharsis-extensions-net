using System.Net;
using System.Text.RegularExpressions;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="MatchExtensions"/>.</para>
/// </summary>
public sealed class MatchExtensionsTest : UnitTest
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

    static void Validate(IEnumerable<Capture> result, Match match)
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
      Validate(false, null);
      Validate(false, Match.Empty);
      Validate(true, Regex.Match(Attributes.RandomName(), ".*"));
    }

    return;

    static void Validate(bool result, Match match) => match.ToBoolean().Should().Be(result);
  }
}