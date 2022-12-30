using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RegexExtensions"/>.</para>
/// </summary>
public sealed class RegexExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.IsMatch(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsMatch_Method()
  {
    /*AssertionExtensions.Should(() => RegexExtensions.IsMatch(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.IsMatch(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.IsMatch("anything").Should().BeFalse();
    "ab4Zg95kf".IsMatch("[a-zA-z0-9]").Should().BeTrue();
    "~#$%".IsMatch("[a-zA-z0-9]").Should().BeFalse();*/

    throw new NotImplementedException();

  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.Matches(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Matches_Method()
  {
    /*AssertionExtensions.Should(() => RegexExtensions.Matches(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.Matches(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.Matches("anything").Should().BeEmpty();
    var matches = "ab#1".Matches("[a-zA-z0-9]");
    matches.Should().HaveCount(3);
    matches.ElementAt(0).Value.Should().Be("a");
    matches.ElementAt(1).Value.Should().Be("b");
    matches.ElementAt(2).Value.Should().Be("1");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.ToEnumerable(Regex, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Regex_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => RegexExtensions.ToEnumerable(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new Regex(".*").ToEnumerable(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.ToEnumerable(Match)"/> method.</para>
  /// </summary>
  [Fact]
  public void Match_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => RegexExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}