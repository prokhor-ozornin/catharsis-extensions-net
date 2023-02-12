using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeOffsetExtensions"/>.</para>
/// </summary>
public sealed class DateTimeOffsetExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EqualsByDate(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByDate_Method()
  {
    var dates = new[] { DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByDate(date).Should().BeTrue();
      date.EqualsByDate(startOfDay).Should().BeTrue();

      startOfDay.AddYears(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddMonths(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddDays(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddHours(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMinutes(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddSeconds(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMilliseconds(1).EqualsByDate(startOfDay).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EqualsByTime(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByTime_Method()
  {
    var dates = new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByTime(date).Should().BeTrue();
      date.EqualsByTime(startOfDay).Should().BeFalse();

      startOfDay.AddYears(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddMonths(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddDays(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddHours(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMinutes(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddSeconds(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMilliseconds(1).EqualsByTime(startOfDay).Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.Min(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    DateTimeOffset.MinValue.Min(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);
    DateTimeOffset.MinValue.Min(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MinValue);

    DateTimeOffset.MaxValue.Min(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);
    DateTimeOffset.MaxValue.Min(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);

    foreach (var date in new[] {DateTimeOffset.Now, DateTimeOffset.UtcNow})
    {
      date.Min(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromHours(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMinutes(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromSeconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMilliseconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromTicks(1)).Min(date).Should().Be(date);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.Max(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    DateTimeOffset.MinValue.Max(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);
    DateTimeOffset.MinValue.Max(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);

    DateTimeOffset.MaxValue.Max(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);
    DateTimeOffset.MaxValue.Max(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MaxValue);

    foreach (var date in new[] {DateTimeOffset.Now, DateTimeOffset.UtcNow})
    {
      date.Max(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Max(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Max(date).Should().Be(date.Add(TimeSpan.FromDays(1)));
      date.Add(TimeSpan.FromHours(1)).Max(date).Should().Be(date.Add(TimeSpan.FromHours(1)));
      date.Add(TimeSpan.FromMinutes(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMinutes(1)));
      date.Add(TimeSpan.FromSeconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromSeconds(1)));
      date.Add(TimeSpan.FromMilliseconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMilliseconds(1)));
      date.Add(TimeSpan.FromTicks(1)).Max(date).Should().Be(date.Add(TimeSpan.FromTicks(1)));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.Range(DateTimeOffset, DateTimeOffset, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.Range(date, TimeSpan.Zero).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 1.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 1.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 2.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 2.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(2), 1.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-2), 1.Days())).And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(3), 2.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-3), 2.Days())).And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsPast(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.IsPast().Should().BeTrue();
      date.AddSeconds(-1).IsPast().Should().BeTrue();
      date.AddSeconds(1).IsPast().Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsFuture(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.IsFuture().Should().BeFalse();
      date.AddSeconds(-1).IsFuture().Should().BeFalse();
      date.AddSeconds(1).IsFuture().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekday(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

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
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekend(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

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
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToYearStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMonthStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToDayStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToDayStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToHourStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMinuteStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToSecondStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToYearEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMonthEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToDayEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToDayEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToHourEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMinuteEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToSecondEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateTime(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToDateTime().Should().BeSameDateAs(date.ToDateTime()).And.BeSameDateAs(date.UtcDateTime).And.BeIn(DateTimeKind.Utc);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToIsoString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    var now = DateTimeOffset.Now;
    now.ToIsoString().Should().Be(now.ToUniversalTime().ToString("o"));
    DateTimeOffset.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture).Should().Be(now);

    now = DateTimeOffset.UtcNow;
    now.ToIsoString().Should().Be(now.ToString("o"));
    DateTimeOffset.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToRfcString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRfcString_Method()
  {
    var now = DateTimeOffset.Now;
    now.ToRfcString().Should().Be(now.ToUniversalTime().ToString("r"));
    DateTimeOffset.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.ToUniversalTime().TruncateToSecondStart());

    now = DateTimeOffset.UtcNow;
    now.ToRfcString().Should().Be(now.ToString("r"));
    DateTimeOffset.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.TruncateToSecondStart());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToTimeOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    }
  }
}