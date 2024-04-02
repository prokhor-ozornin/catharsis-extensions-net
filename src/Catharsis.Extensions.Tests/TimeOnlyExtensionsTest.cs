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
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      var timeOnly = TimeOnly.FromDateTime(date);

      timeOnly.Range(timeOnly, TimeSpan.Zero).Should().BeOfType<IEnumerable<TimeOnly>>().And.BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(1)).Should().BeOfType<IEnumerable<TimeOnly>>().And.BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(-1)).Should().BeOfType<IEnumerable<TimeOnly>>().And.BeEmpty();

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 1.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 1.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 2.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 2.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(2.Milliseconds()), 1.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(1.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-2.Milliseconds()), 1.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(timeOnly.Add(-2.Milliseconds()), timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(3.Milliseconds()), 2.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(2.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-3.Milliseconds()), 2.Milliseconds()).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(timeOnly.Add(-3.Milliseconds()), timeOnly.Add(-1.Milliseconds()));
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