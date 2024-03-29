using System.Globalization;
using Catharsis.Commons;
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
    Validate(DateTime.MinValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);
    
    return;

    static void Validate(DateTime date)
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
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date)
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

    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.Min(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromHours(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMinutes(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromSeconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMilliseconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromTicks(1)).Min(date).Should().Be(date);
    });

    return;

    static void Validate(DateTime result, DateTime left, DateTime right) => left.Min(right).Should().Be(result);
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

    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.Max(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Max(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Max(date).Should().Be(date.Add(TimeSpan.FromDays(1)));
      date.Add(TimeSpan.FromHours(1)).Max(date).Should().Be(date.Add(TimeSpan.FromHours(1)));
      date.Add(TimeSpan.FromMinutes(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMinutes(1)));
      date.Add(TimeSpan.FromSeconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromSeconds(1)));
      date.Add(TimeSpan.FromMilliseconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMilliseconds(1)));
      date.Add(TimeSpan.FromTicks(1)).Max(date).Should().Be(date.Add(TimeSpan.FromTicks(1)));
    });

    return;

    static void Validate(DateTime result, DateTime left, DateTime right) => left.Max(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTime, DateTime, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.Range(date, TimeSpan.Zero).Should().BeOfType<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeOfType<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeOfType<IEnumerable<DateTime>>().And.BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    });

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsPast(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      Validate(true, date);
      Validate(true, date.AddSeconds(-1));
      Validate(false, date.AddSeconds(1));  
    });

    return;

    static void Validate(bool result, DateTime date) => date.IsPast().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsFuture(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      Validate(false, date);
      Validate(false, date.AddSeconds(-1));
      Validate(true, date.AddSeconds(1));
    });

    return;

    static void Validate(bool result, DateTime date) => date.IsFuture().Should().Be(result);
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

    return;

    static void Validate(bool result, DateTime date) => date.IsWeekday().Should().Be(result);
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

    return;

    static void Validate(bool result, DateTime date) => date.IsWeekend().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToYearStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToMonthStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToDayStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToHourStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToMinuteStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToSecondStart().Should().BeOnOrBefore(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToYearEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToMonthEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToDayEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToHourEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToMinuteEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTime date) => date.TruncateToSecondEnd().Should().BeOnOrAfter(date).And.BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.ToDateTimeOffset().Should().BeSameDateAs(date.ToDateTimeOffset()).And.BeSameDateAs(new DateTimeOffset(date.ToUniversalTime())).And.BeWithin(TimeSpan.Zero);
    });

    return;

    static void Validate(DateTimeOffset result, DateTime date) => date.ToDateTimeOffset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToIsoString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    var now = DateTime.Now;
    now.ToIsoString().Should().BeOfType<string>().And.Be(now.ToUniversalTime().ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture).Should().Be(now);

    now = DateTime.UtcNow;
    now.ToIsoString().Should().BeOfType<string>().And.Be(now.ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now);

    return;

    static void Validate(string result, DateTime date) => date.ToIsoString().Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfcString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRfcString_Method()
  {
    var now = DateTime.Now;
    now.ToRfcString().Should().BeOfType<string>().And.Be(now.ToUniversalTime().ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.ToUniversalTime().TruncateToSecondStart());

    now = DateTime.UtcNow;
    now.ToRfcString().Should().BeOfType<string>().And.Be(now.ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now.TruncateToSecondStart());

    return;

    static void Validate(string result, DateTime date) => date.ToRfcString().Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
    });

    return;

    static void Validate(DateOnly result, DateTime date) => date.ToDateOnly().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    });

    return;

    static void Validate(TimeOnly result, DateTime date) => date.ToTimeOnly().Should().Be(result);
  }
}