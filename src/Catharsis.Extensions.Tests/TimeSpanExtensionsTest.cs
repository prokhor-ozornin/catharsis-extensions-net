using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeSpanExtensions"/>.</para>
/// </summary>
public sealed class TimeSpanExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.IsEmpty(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    TimeSpan.MinValue.IsEmpty().Should().BeFalse();
    TimeSpan.MaxValue.IsEmpty().Should().BeFalse();

    TimeSpan.FromTicks(long.MinValue).IsEmpty().Should().BeFalse();
    TimeSpan.FromTicks(long.MaxValue).IsEmpty().Should().BeFalse();

    TimeSpan.Zero.IsEmpty().Should().BeTrue();
    TimeSpan.FromTicks(0).IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InThePast(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InThePast_Method()
  {
    var now = DateTime.UtcNow;

    TimeSpan.Zero.InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(1).InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(-1).InThePast().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InTheFuture(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InTheFuture_Method()
  {
    var now = DateTime.UtcNow;

    TimeSpan.Zero.InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(1).InTheFuture().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(-1).InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
  }
}