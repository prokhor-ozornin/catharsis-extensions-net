using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RangeExtensions"/>.</para>
/// </summary>
public sealed class RangeExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RangeExtensions.ToEnumerable(Range)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    foreach (var range in new[] {Range.All, .., ..0, ^0..0, ^0.., 1..1, ^1..^1, int.MaxValue..int.MaxValue})
    {
      range.ToEnumerable().Should().NotBeNull().And.BeSameAs(range.ToEnumerable()).And.BeEmpty();
    }

    foreach (var range in new[] { ..1, ..^1, 1..0, ^1..0, ^1.. })
    {
      range.ToEnumerable().Should().NotBeNull().And.NotBeSameAs(range.ToEnumerable()).And.AllBeEquivalentTo(0);
    }

    var totalRange = ..int.MaxValue;
    totalRange.ToEnumerable().Should().NotBeNull().And.NotBeSameAs(totalRange.ToEnumerable()).And.HaveCount(int.MaxValue);

    return;

    static void Validate()
    {
    }
  }
}