using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeSpanExtensions"/>.</para>
/// </summary>
public sealed class TimeSpanExtensionsTest : UnitTest
{
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

      Validate(TimeSpan.MinValue, TimeSpan.Zero);
      Validate(TimeSpan.MinValue, TimeSpan.MaxValue);
      Validate(TimeSpan.Zero, TimeSpan.MinValue);
      Validate(TimeSpan.Zero, TimeSpan.Zero);
      Validate(TimeSpan.Zero, TimeSpan.MaxValue);
      Validate(TimeSpan.MaxValue, TimeSpan.MinValue);
      Validate(TimeSpan.MaxValue, TimeSpan.Zero);
    }

    return;

    static void Validate(TimeSpan timespan, TimeSpan offset) => timespan.With(offset).Should().Be(timespan.Add(offset));
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

      Validate(TimeSpan.MinValue, TimeSpan.MinValue);
      Validate(TimeSpan.MinValue, TimeSpan.Zero);
      Validate(TimeSpan.Zero, TimeSpan.Zero);
      Validate(TimeSpan.Zero, TimeSpan.MaxValue);
      Validate(TimeSpan.MaxValue, TimeSpan.Zero);
      Validate(TimeSpan.MaxValue, TimeSpan.MaxValue);
    }

    return;

    static void Validate(TimeSpan timespan, TimeSpan offset) => timespan.Subtract(offset).Should().Be(timespan.Subtract(offset));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.IsEmpty(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      Validate(false, TimeSpan.MinValue);
      Validate(true, TimeSpan.Zero);
      Validate(false, TimeSpan.MaxValue);
      Validate(false, TimeSpan.FromTicks(long.MinValue));
      Validate(false, TimeSpan.FromTicks(long.MaxValue));
      Validate(true, TimeSpan.FromTicks(0));
    }

    return;

    static void Validate(bool result, TimeSpan span) => span.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InThePast(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InThePast_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeSpan.Zero);
      Validate(TimeSpan.FromTicks(1));
      Validate(TimeSpan.FromMicroseconds(1));
      Validate(TimeSpan.FromMilliseconds(1));
      Validate(TimeSpan.FromSeconds(1));
      Validate(TimeSpan.FromMinutes(1));
      Validate(TimeSpan.FromHours(1));
      Validate(TimeSpan.FromDays(1));
    }

    return;

    static void Validate(TimeSpan timespan) => timespan.InThePast().Should().BeCloseTo(DateTimeOffset.UtcNow - timespan, TimeSpan.FromMilliseconds(1)).And.HaveOffset(TimeSpan.Zero).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InTheFuture(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InTheFuture_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeSpan.Zero);
      Validate(TimeSpan.FromTicks(1));
      Validate(TimeSpan.FromMicroseconds(1));
      Validate(TimeSpan.FromMilliseconds(1));
      Validate(TimeSpan.FromSeconds(1));
      Validate(TimeSpan.FromMinutes(1));
      Validate(TimeSpan.FromHours(1));
      Validate(TimeSpan.FromDays(1));
    }

    return;

    static void Validate(TimeSpan timespan) => timespan.InTheFuture().Should().BeCloseTo(DateTimeOffset.UtcNow + timespan, TimeSpan.FromMilliseconds(1)).And.HaveOffset(TimeSpan.Zero).And.BeWithin(TimeSpan.Zero);
  }
}