using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RegexExtensions"/>.</para>
/// </summary>
public sealed class RegexExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RegexExtensions.ToEnumerable(Regex, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => RegexExtensions.ToEnumerable(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("regex");
    AssertionExtensions.Should(() => new Regex(".*").ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }
}