using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeExtensions"/>.</para>
/// </summary>
public sealed class DateTimeExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByDate(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByDate_Method()
  {
    DateTime[] dates = [DateTime.MinValue, DateTime.Now, DateTime.UtcNow];

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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByTime(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByTime_Method()
  {
    DateTime[] dates = [DateTime.Now, DateTime.UtcNow];

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByTime(date).Should().BeTrue();
      date.EqualsByTime(startOfDay).Should().BeFalse();

      startOfDay.AddYears(1).EqualsByTime(startOfDay).Should().BeTrue();
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Min(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    DateTime.MinValue.Min(DateTime.MinValue).Should().Be(DateTime.MinValue);
    DateTime.MinValue.Min(DateTime.MaxValue).Should().Be(DateTime.MinValue);

    DateTime.MaxValue.Min(DateTime.MaxValue).Should().Be(DateTime.MaxValue);
    DateTime.MaxValue.Min(DateTime.MinValue).Should().Be(DateTime.MinValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Max(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    DateTime.MinValue.Max(DateTime.MinValue).Should().Be(DateTime.MinValue);
    DateTime.MinValue.Max(DateTime.MaxValue).Should().Be(DateTime.MaxValue);

    DateTime.MaxValue.Max(DateTime.MaxValue).Should().Be(DateTime.MaxValue);
    DateTime.MaxValue.Max(DateTime.MinValue).Should().Be(DateTime.MaxValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTime, DateTime, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsPast(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      date.IsPast().Should().BeTrue();
      date.AddSeconds(-1).IsPast().Should().BeTrue();
      date.AddSeconds(1).IsPast().Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsFuture(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      date.IsFuture().Should().BeFalse();
      date.AddSeconds(-1).IsFuture().Should().BeFalse();
      date.AddSeconds(1).IsFuture().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekday(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    var now = DateTime.UtcNow;
    var dates = new DateTime[7].Fill(index => now.AddDays(index));

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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekend(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    var now = DateTime.UtcNow;
    var dates = new DateTime[7].Fill(index => now.AddDays(index));

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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToDayStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToDayEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToDateTimeOffset().Should().BeSameDateAs(date.ToDateTimeOffset()).And.BeSameDateAs(new DateTimeOffset(date.ToUniversalTime())).And.BeWithin(TimeSpan.Zero);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToIsoString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    var now = DateTime.Now;
    now.ToIsoString().Should().Be(now.ToUniversalTime().ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture).Should().Be(now);

    now = DateTime.UtcNow;
    now.ToIsoString().Should().Be(now.ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfcString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRfcString_Method()
  {
    var now = DateTime.Now;
    now.ToRfcString().Should().Be(now.ToUniversalTime().ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.ToUniversalTime().TruncateToSecondStart());

    now = DateTime.UtcNow;
    now.ToRfcString().Should().Be(now.ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now.TruncateToSecondStart());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    }
  }
}