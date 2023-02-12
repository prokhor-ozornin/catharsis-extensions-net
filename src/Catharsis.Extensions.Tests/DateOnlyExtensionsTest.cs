using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateOnlyExtensions"/>.</para>
/// </summary>
public sealed class DateOnlyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Min(DateOnly, DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    DateOnly.MinValue.Min(DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    DateOnly.MinValue.Min(DateOnly.MaxValue).Should().Be(DateOnly.MinValue);

    DateOnly.MaxValue.Min(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
    DateOnly.MaxValue.Min(DateOnly.MinValue).Should().Be(DateOnly.MinValue);

    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(1).Min(dateOnly).Should().Be(dateOnly);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Max(DateOnly, DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    DateOnly.MinValue.Max(DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    DateOnly.MinValue.Max(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);

    DateOnly.MaxValue.Max(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
    DateOnly.MaxValue.Max(DateOnly.MinValue).Should().Be(DateOnly.MaxValue);

    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);
      dateOnly.Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Max(dateOnly).Should().Be(dateOnly.AddYears(1));
      dateOnly.AddMonths(1).Max(dateOnly).Should().Be(dateOnly.AddMonths(1));
      dateOnly.AddDays(1).Max(dateOnly).Should().Be(dateOnly.AddDays(1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Range(DateOnly, DateOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Range(dateOnly, TimeSpan.Zero).Should().BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(1)).Should().BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      dateOnly.Range(dateOnly.AddDays(1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(1), 1.Days())).And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-1), 1.Days())).And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(1), 2.Days())).And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-1), 2.Days())).And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(2), 1.Days())).And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(1));
      dateOnly.Range(dateOnly.AddDays(-2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-2), 1.Days())).And.HaveCount(2).And.Equal(dateOnly.AddDays(-2), dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(3), 2.Days())).And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(2));
      dateOnly.Range(dateOnly.AddDays(-3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-3), 2.Days())).And.HaveCount(2).And.Equal(dateOnly.AddDays(-3), dateOnly.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekday(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    var dates = new DateOnly[7].Fill(now.AddDays);

    dates.Count(date => date.IsWeekday()).Should().Be(5);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekday().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekday().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekend(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    var dates = new DateOnly[7].Fill(now.AddDays);

    dates.Count(date => date.IsWeekend()).Should().Be(2);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekend().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekend().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToYearStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToMonthStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToYearEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToMonthEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTime(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.ToDateTime().Should()
              .BeSameDateAs(dateOnly.ToDateTime())
              .And
              .BeIn(DateTimeKind.Unspecified)
              .And
              .HaveYear(dateOnly.Year)
              .And
              .HaveMonth(dateOnly.Month)
              .And
              .HaveDay(dateOnly.Day)
              .And
              .HaveHour(0)
              .And
              .HaveMinute(0)
              .And
              .HaveSecond(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTimeOffset(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.ToDateTimeOffset().Should()
              .BeSameDateAs(dateOnly.ToDateTimeOffset())
              .And
              .HaveYear(dateOnly.Year)
              .And
              .HaveMonth(dateOnly.Month)
              .And
              .HaveDay(dateOnly.Day)
              .And
              .HaveHour(0)
              .And
              .HaveMinute(0)
              .And
              .HaveSecond(0)
              .And
              .BeWithin(TimeSpan.Zero);
    }
  }
}