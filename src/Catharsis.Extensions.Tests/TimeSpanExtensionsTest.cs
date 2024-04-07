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
    }

    throw new NotImplementedException();

    return;

    static void Validate(TimeSpan result, TimeSpan timespan, TimeSpan add) => timespan.With(add).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.Without(TimeSpan, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Without_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate(TimeSpan result, TimeSpan timespan, TimeSpan subtract) => timespan.Subtract(subtract).Should().Be(result);
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
      Validate(false, TimeSpan.MaxValue);
      Validate(false, TimeSpan.FromTicks(long.MinValue));
      Validate(false, TimeSpan.FromTicks(long.MaxValue));

      Validate(true, TimeSpan.Zero);
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
      var now = DateTime.UtcNow;

      TimeSpan.Zero.InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
      TimeSpan.FromMinutes(1).InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
      TimeSpan.FromMinutes(-1).InThePast().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    }

    return;

    static void Validate(bool result, DateTimeOffset date, TimeSpan offset)
    {
      //TimeSpan.Zero.InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
      //TimeSpan.FromMinutes(1).InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
      //TimeSpan.FromMinutes(-1).InThePast().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeSpanExtensions.InTheFuture(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void InTheFuture_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTime.UtcNow;

      //Validate();

      //TimeSpan.Zero.InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
      //TimeSpan.FromMinutes(1).InTheFuture().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
      //TimeSpan.FromMinutes(-1).InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
    }

    return;

    static void Validate(bool result, TimeSpan offset) => offset.InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow + offset).And.BeCloseTo(DateTimeOffset.UtcNow, offset);
//    {
//
//      TimeSpan.Zero.InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(date).And.BeWithin(TimeSpan.Zero);
//      TimeSpan.FromMinutes(1).InTheFuture().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(date).And.BeWithin(TimeSpan.Zero);
//      TimeSpan.FromMinutes(-1).InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(date).And.BeWithin(TimeSpan.Zero);
//    }
  }
}