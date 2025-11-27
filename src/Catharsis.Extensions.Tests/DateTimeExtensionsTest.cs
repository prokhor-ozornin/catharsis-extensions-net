using System.Globalization;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeExtensions"/>.</para>
/// </summary>
/// <seealso cref="DateTimeExtensions"/>
public sealed class DateTimeExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsPast(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPast_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
      {
        Test(true, date);
        Test(true, date.AddSeconds(-1));
        Test(false, date.AddSeconds(1));
      });
    }

    return;

    static void Test(bool result, DateTime date) => date.IsPast.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsFuture(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFuture_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
      {
        Test(false, date);
        Test(false, date.AddSeconds(-1));
        Test(true, date.AddSeconds(1));
      });
    }

    return;

    static void Test(bool result, DateTime date) => date.IsFuture.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekday(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTime.UtcNow;
      var dates = new DateTime[7].Fill(index => now.AddDays(index));

      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Test(bool result, DateTime date) => date.IsWeekday.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekend(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTime.UtcNow;
      var dates = new DateTime[7].Fill(index => now.AddDays(index));

      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Test(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Test(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Test(bool result, DateTime date) => date.IsWeekend.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTime, DateTime, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date)
    {
      date.Range(date, TimeSpan.Zero).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();

      date.Range(date.AddDays(1), 1.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByDate(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByDate_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTime.MinValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
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

    static void Test(bool result, DateTime left, DateTime right) => left.EqualsByDate(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByTime(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void EqualsByTime_Method()
  {
    using (new AssertionScope())
    {
      new[] { DateTime.MinValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
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

    static void Test(bool result, DateTime left, DateTime right) => left.EqualsByTime(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfYear"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfYear_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfYear.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfYear"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfYear_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfYear.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfMonth"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfMonth.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfMonth"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfMonth.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfDay"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfDay_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfDay.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfDay"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfDay_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfDay.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfHour"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfHour_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfHour.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfHour"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfHour_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfHour.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfMinute"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfMinute.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfMinute"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfMinute.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(59);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfSecond"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.StartOfSecond.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfSecond"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.EndOfSecond.Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.ToDateTimeOffset().Should().BeSameDateAs(new DateTimeOffset(date.ToUniversalTime())).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.ToDateOnly().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    using (new AssertionScope())
    {
      Test(DateTime.MinValue);
      Test(DateTime.MaxValue);
      Test(DateTime.Now);
      Test(DateTime.UtcNow);
    }

    return;

    static void Test(DateTime date) => date.ToTimeOnly().Should().HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToIsoString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIsoString_Method()
  {
    using (new AssertionScope())
    {
      foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
      {
        Test(date);
      }
    }

    return;

    static void Test(DateTime date) => date.ToIsoString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfcString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRfcString_Method()
  {
    using (new AssertionScope())
    {
      foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
      {
        Test(date);
      }
    }

    return;

    static void Test(DateTime date) => date.ToRfcString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture));
  }
}