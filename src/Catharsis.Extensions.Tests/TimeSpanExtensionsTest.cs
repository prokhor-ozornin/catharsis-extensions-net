using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeSpanExtensions"/>.</para>
/// </summary>
public sealed class TimeSpanExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InThePast(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InThePast_Method()
  {
    using (new AssertionScope())
    {
      Test(TimeSpan.Zero);
      Test(TimeSpan.FromTicks(1));
      Test(TimeSpan.FromMicroseconds(1));
      Test(TimeSpan.FromMilliseconds(1));
      Test(TimeSpan.FromSeconds(1));
      Test(TimeSpan.FromMinutes(1));
      Test(TimeSpan.FromHours(1));
      Test(TimeSpan.FromDays(1));
    }

    return;

    static void Test(TimeSpan timespan) => timespan.InThePast().Should().BeCloseTo(DateTimeOffset.UtcNow - timespan, TimeSpan.FromMilliseconds(1)).And.HaveOffset(TimeSpan.Zero).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InTheFuture(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InTheFuture_Method()
  {
    using (new AssertionScope())
    {
      Test(TimeSpan.Zero);
      Test(TimeSpan.FromTicks(1));
      Test(TimeSpan.FromMicroseconds(1));
      Test(TimeSpan.FromMilliseconds(1));
      Test(TimeSpan.FromSeconds(1));
      Test(TimeSpan.FromMinutes(1));
      Test(TimeSpan.FromHours(1));
      Test(TimeSpan.FromDays(1));
    }

    return;

    static void Test(TimeSpan timespan) => timespan.InTheFuture().Should().BeCloseTo(DateTimeOffset.UtcNow + timespan, TimeSpan.FromMilliseconds(1)).And.HaveOffset(TimeSpan.Zero).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.IsEmpty(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      Test(false, TimeSpan.MinValue);
      Test(true, TimeSpan.Zero);
      Test(false, TimeSpan.MaxValue);
      Test(false, TimeSpan.FromTicks(long.MinValue));
      Test(false, TimeSpan.FromTicks(long.MaxValue));
      Test(true, TimeSpan.FromTicks(0));
    }

    return;

    static void Test(bool result, TimeSpan span) => span.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.With(TimeSpan, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void With_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TimeSpan.MinValue.With(TimeSpan.MinValue)).ThrowExactly<OverflowException>();
      AssertionExtensions.Should(() => TimeSpan.MaxValue.With(TimeSpan.MaxValue)).ThrowExactly<OverflowException>();

      Test(TimeSpan.MinValue, TimeSpan.Zero);
      Test(TimeSpan.MinValue, TimeSpan.MaxValue);
      Test(TimeSpan.Zero, TimeSpan.MinValue);
      Test(TimeSpan.Zero, TimeSpan.Zero);
      Test(TimeSpan.Zero, TimeSpan.MaxValue);
      Test(TimeSpan.MaxValue, TimeSpan.MinValue);
      Test(TimeSpan.MaxValue, TimeSpan.Zero);
    }

    return;

    static void Test(TimeSpan timespan, TimeSpan offset) => timespan.With(offset).Should().Be(timespan.Add(offset));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.Without(TimeSpan, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Without_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TimeSpan.MinValue.Without(TimeSpan.MaxValue)).ThrowExactly<OverflowException>();
      AssertionExtensions.Should(() => TimeSpan.Zero.Without(TimeSpan.MinValue)).ThrowExactly<OverflowException>();
      AssertionExtensions.Should(() => TimeSpan.MaxValue.Without(TimeSpan.MinValue)).ThrowExactly<OverflowException>();

      Test(TimeSpan.MinValue, TimeSpan.MinValue);
      Test(TimeSpan.MinValue, TimeSpan.Zero);
      Test(TimeSpan.Zero, TimeSpan.Zero);
      Test(TimeSpan.Zero, TimeSpan.MaxValue);
      Test(TimeSpan.MaxValue, TimeSpan.Zero);
      Test(TimeSpan.MaxValue, TimeSpan.MaxValue);
    }

    return;

    static void Test(TimeSpan timespan, TimeSpan offset) => timespan.Subtract(offset).Should().Be(timespan.Subtract(offset));
  }
}