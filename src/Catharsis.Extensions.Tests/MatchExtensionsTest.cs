using System.Text.RegularExpressions;
using FluentAssertions;
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
    AssertionExtensions.Should(() => MatchExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("match");

    throw new NotImplementedException();
  }
}