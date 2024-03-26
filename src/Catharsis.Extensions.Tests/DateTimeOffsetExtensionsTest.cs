using System.Globalization;
using Catharsis.Commons;
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
    Validate(DateTimeOffset.MinValue);
    Validate(DateTimeOffset.MaxValue);
    Validate(DateTimeOffset.Now);
    Validate(DateTimeOffset.UtcNow);
    
    return;

    static void Validate(DateTimeOffset date)
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
    Validate(DateTimeOffset.MinValue);
    Validate(DateTimeOffset.MaxValue);
    Validate(DateTimeOffset.Now);
    Validate(DateTimeOffset.UtcNow);

    return;

    static void Validate(DateTimeOffset date)
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
    //Validate(DateTimeOffset.MinValue, DateTimeOffset.MinValue);
    //Validate(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue);
    //Validate(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);

    new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
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

    static void Validate(DateTimeOffset result, DateTimeOffset left, DateTimeOffset right) => left.Min(right).Should().Be(result);
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

    new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
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

    static void Validate(DateTimeOffset result, DateTimeOffset left, DateTimeOffset right) => left.Max(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.Range(DateTimeOffset, DateTimeOffset, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
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
    });

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsPast(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
    {
      Validate(true, date);
      Validate(true, date.AddSeconds(-1));
      Validate(false, date.AddSeconds(1));
    });

    return;

    static void Validate(bool result, DateTimeOffset date) => date.IsPast().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsFuture(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
    {
      Validate(false, date);
      Validate(false, date.AddSeconds(-1));
      Validate(true, date.AddSeconds(1));
    });

    return;

    static void Validate(bool result, DateTimeOffset date) => date.IsFuture().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekday(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));

    return;

    static void Validate(bool result, DateTimeOffset date) => date.IsWeekday().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekend(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));

    return;

    static void Validate(bool result, DateTimeOffset date) => date.IsWeekend().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToYearStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToYearStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMonthStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToMonthStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToDayStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToDayStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToHourStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToHourStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMinuteStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToMinuteStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToSecondStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondStart_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToSecondStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToYearEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToYearEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMonthEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToMonthEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToDayEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToDayEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToDayEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToHourEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToHourEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToHourEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToMinuteEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMinuteEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToMinuteEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.TruncateToSecondEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToSecondEnd_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.TruncateToSecondEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateTime(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.ToDateTime().Should().BeSameDateAs(date.ToDateTime()).And.BeSameDateAs(date.UtcDateTime).And.BeIn(DateTimeKind.Utc);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToIsoString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    //Validate(DateTimeOffset.Now);
    //Validate(DateTimeOffset.UtcNow);
    
    return;

    static void Validate(string result, DateTimeOffset date)
    {
      var iso = date.ToIsoString();
      iso.Should().NotBeNull().And.NotBeSameAs(date.ToIsoString()).And.Be(date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)).And.Be(result);
      DateTimeOffset.ParseExact(iso, "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(date.ToUniversalTime());
    }
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

    return;

    static void Validate(string result, DateTimeOffset date) => date.ToRfcString().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    Validate(DateTime.MinValue);
    Validate(DateTime.MaxValue);
    Validate(DateTime.Now);
    Validate(DateTime.UtcNow);

    return;

    static void Validate(DateTimeOffset date) => date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToTimeOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    });

    return;

    static void Validate(DateTimeOffset date) => date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
  }
}