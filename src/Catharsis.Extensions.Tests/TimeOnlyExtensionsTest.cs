using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeOnlyExtensions"/>.</para>
/// </summary>
public sealed class TimeOnlyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Min(TimeOnly, TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    Validate(TimeOnly.MinValue, TimeOnly.MinValue, TimeOnly.MinValue);
    Validate(TimeOnly.MinValue, TimeOnly.MinValue, TimeOnly.MaxValue);
    Validate(TimeOnly.MinValue, TimeOnly.MaxValue, TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue, TimeOnly.MaxValue, TimeOnly.MaxValue);

    var time = TimeOnly.MinValue;

    //Validate(time.Add(TimeSpan.Zero), time);

    //time.Min(time).Should().Be(time);
    //time.Add(TimeSpan.Zero).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromDays(1)).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromHours(1)).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromMinutes(1)).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromSeconds(1)).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromMilliseconds(1)).Min(time).Should().Be(time);
    //time.Add(TimeSpan.FromTicks(1)).Min(time).Should().Be(time);

    return;

    static void Validate(TimeOnly result, TimeOnly min, TimeOnly max) => min.Min(max).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Max(TimeOnly, TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    TimeOnly.MinValue.Max(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    TimeOnly.MinValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);

    TimeOnly.MaxValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
    TimeOnly.MinValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);

    var timeOnly = TimeOnly.MinValue;
    timeOnly.Max(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.Zero).Max(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromDays(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromDays(1)));
    timeOnly.Add(TimeSpan.FromHours(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromHours(1)));
    timeOnly.Add(TimeSpan.FromMinutes(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromMinutes(1)));
    timeOnly.Add(TimeSpan.FromSeconds(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromSeconds(1)));
    timeOnly.Add(TimeSpan.FromMilliseconds(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromMilliseconds(1)));
    timeOnly.Add(TimeSpan.FromTicks(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromTicks(1)));

    return;

    static void Validate(TimeOnly result, TimeOnly min, TimeOnly max) => min.Max(max).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      var timeOnly = TimeOnly.FromDateTime(date);

      timeOnly.Range(timeOnly, TimeSpan.Zero).Should().BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(1)).Should().BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(1.Milliseconds()), 1.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 1.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(1.Milliseconds()), 2.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 2.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(2.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(2.Milliseconds()), 1.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(1.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-2.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-2.Milliseconds()), 1.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly.Add(-2.Milliseconds()), timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(3.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(3.Milliseconds()), 2.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(2.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-3.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-3.Milliseconds()), 2.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly.Add(-3.Milliseconds()), timeOnly.Add(-1.Milliseconds()));
    });

    return;

    static void Validate(TimeOnly from, TimeOnly to, TimeSpan offset, params TimeOnly[] ranges)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToHourStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToHourStart().Should().BeOnOrBefore(time).And.HaveHours(time.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToMinuteStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToMinuteStart().Should().BeOnOrBefore(time).And.HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToSecondStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToSecondStart().Should().BeOnOrBefore(time).And.HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToHourEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToHourEnd().Should().BeOnOrAfter(time).And.HaveHours(time.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToMinuteEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToMinuteEnd().Should().BeOnOrAfter(time).And.HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToSecondEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time) => time.TruncateToSecondEnd().Should().BeOnOrAfter(time).And.HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTime(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time)
    {
      var now = DateTimeOffset.UtcNow;
      time.ToDateTime().Should().BeSameDateAs(time.ToDateTime()).And.BeIn(DateTimeKind.Utc).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(time.Hour).And.HaveMinute(time.Minute).And.HaveSecond(time.Second);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTimeOffset(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    Validate(TimeOnly.MinValue);
    Validate(TimeOnly.MaxValue);
    Validate(DateTime.Now.ToTimeOnly());
    Validate(DateTime.UtcNow.ToTimeOnly());

    return;

    static void Validate(TimeOnly time)
    {
      var now = DateTimeOffset.UtcNow;
      time.ToDateTimeOffset().Should().BeSameDateAs(time.ToDateTime()).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(time.Hour).And.HaveMinute(time.Minute).And.HaveSecond(time.Second).And.BeWithin(TimeSpan.Zero);
    }
  }
}