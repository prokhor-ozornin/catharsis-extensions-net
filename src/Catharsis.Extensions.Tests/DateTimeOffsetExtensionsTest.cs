using System.Globalization;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeOffsetExtensions"/>.</para>
/// </summary>
/// <seealso cref="DateTimeOffsetExtensions"/>
public sealed class DateTimeOffsetExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsPast(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(true, date);
        Test(true, date.AddSeconds(-1));
        Test(false, date.AddSeconds(1));
      });
    }

    return;

    static void Test(bool result, DateTimeOffset date) => date.IsPast.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsFuture(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(false, date);
        Test(false, date.AddSeconds(-1));
        Test(true, date.AddSeconds(1));
      });
    }

    return;

    static void Test(bool result, DateTimeOffset date) => date.IsFuture.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekday(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTimeOffset.UtcNow;
      var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Test(bool result, DateTimeOffset date) => date.IsWeekday.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.IsWeekend(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTimeOffset.UtcNow;
      var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Test(bool result, DateTimeOffset date) => date.IsWeekend.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.Range(DateTimeOffset, DateTimeOffset, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date)
    {
      date.Range(date, TimeSpan.Zero).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.BeEmpty();

      date.Range(date.AddDays(1), 1.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days).Should().BeAssignableTo<IEnumerable<DateTimeOffset>>().And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EqualsByDate(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByDate_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(false, date, date.AddYears(1));
        Test(false, date, date.AddMonths(1));
        Test(false, date, date.AddDays(1));
        Test(true, date, date.AddHours(1));
        Test(true, date, date.AddMinutes(1));
        Test(true, date, date.AddSeconds(1));
        Test(true, date, date.AddMilliseconds(1));
      });
    }

    return;

    static void Test(bool result, DateTimeOffset left, DateTimeOffset right) => left.EqualsByDate(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EqualsByTime(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByTime_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(true, date, date.AddYears(1));
        Test(true, date, date.AddMonths(1));
        Test(true, date, date.AddDays(1));
        Test(false, date, date.AddHours(1));
        Test(false, date, date.AddMinutes(1));
        Test(false, date, date.AddSeconds(1));
        Test(false, date, date.AddMilliseconds(1));
      });
    }

    return;

    static void Test(bool result, DateTimeOffset left, DateTimeOffset right) => left.EqualsByTime(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfYear"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfYear_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfYear.Should().HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfYear"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfYear_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfYear.Should().HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfMonth"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfMonth.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfMonth"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfMonth.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfDay"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfDay_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfDay.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfDay"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfDay_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfDay.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfHour"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfHour_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfHour.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfHour"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfHour_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfHour.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfMinute"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfMinute.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(0).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfMinute"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfMinute.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(59).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.StartOfSecond"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.StartOfSecond.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.EndOfSecond"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.EndOfSecond.Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second).And.BeWithin(date.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateTime(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.ToDateTime().Should().BeSameDateAs(date.ToDateTime()).And.BeSameDateAs(date.UtcDateTime).And.BeIn(DateTimeKind.Utc);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToDateOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.ToDateOnly().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToTimeOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTimeOffset.MinValue);
      Test(DateTimeOffset.MaxValue);
      Test(DateTimeOffset.Now);
      Test(DateTimeOffset.UtcNow);
    }

    return;

    static void Test(DateTimeOffset date) => date.ToTimeOnly().Should().HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToIsoString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    using (new AssertionScope())
    {
      foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
      {
        Test(date);
      }
    }

    return;

    static void Test(DateTimeOffset date) => date.ToIsoString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeOffsetExtensions.ToRfcString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRfcString_Method()
  {
    using (new AssertionScope())
    {
      foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
      {
        Test(date);
      }
    }

    return;

    static void Test(DateTimeOffset date) => date.ToRfcString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture));
  }
}