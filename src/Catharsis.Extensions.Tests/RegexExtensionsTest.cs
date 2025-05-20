using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RegexExtensions"/>.</para>
/// </summary>
public sealed class RegexExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.Clone(Regex)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RegexExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("regex");

      Test(string.Empty.ToRegex());
      Test("anything".ToRegex());
    }

    return;

    static void Test(Regex original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<Regex>().And.NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.MatchTimeout.Should().Be(original.MatchTimeout);
      clone.Options.Should().Be(original.Options);
      clone.RightToLeft.Should().Be(original.RightToLeft);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.ToEnumerable(Regex, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RegexExtensions.ToEnumerable(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("regex");
      AssertionExtensions.Should(() => RegexExtensions.ToEnumerable(new Regex(".*"), null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<Match> result, Regex regex, string text)
    {
    }
  }
}