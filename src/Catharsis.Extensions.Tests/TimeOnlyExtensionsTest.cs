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
    TimeOnly.MinValue.Min(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    TimeOnly.MinValue.Min(TimeOnly.MaxValue).Should().Be(TimeOnly.MinValue);

    TimeOnly.MaxValue.Min(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
    TimeOnly.MaxValue.Min(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);

    var timeOnly = TimeOnly.MinValue;
    timeOnly.Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.Zero).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromDays(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromHours(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromMinutes(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromSeconds(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromMilliseconds(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromTicks(1)).Min(timeOnly).Should().Be(timeOnly);
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
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
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
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToHourStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToMinuteStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToSecondStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(now.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToHourEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToMinuteEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.TruncateToSecondEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(now.Second).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTime(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var timeOnly = TimeOnly.FromDateTime(date);
      var now = DateTime.UtcNow;

      timeOnly.ToDateTime().Should()
              .BeSameDateAs(timeOnly.ToDateTime())
              .And
              .BeIn(DateTimeKind.Utc)
              .And
              .HaveYear(now.Year)
              .And
              .HaveMonth(now.Month)
              .And
              .HaveDay(now.Day)
              .And
              .HaveHour(timeOnly.Hour)
              .And
              .HaveMinute(timeOnly.Minute)
              .And
              .HaveSecond(timeOnly.Second);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTimeOffset(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var timeOnly = TimeOnly.FromDateTime(date);
      var now = DateTimeOffset.UtcNow;

      timeOnly.ToDateTimeOffset().Should()
              .BeSameDateAs(timeOnly.ToDateTime())
              .And
              .HaveYear(now.Year)
              .And
              .HaveMonth(now.Month)
              .And
              .HaveDay(now.Day)
              .And
              .HaveHour(timeOnly.Hour)
              .And
              .HaveMinute(timeOnly.Minute)
              .And
              .HaveSecond(timeOnly.Second)
              .And
              .BeWithin(TimeSpan.Zero);
    }
  }
}