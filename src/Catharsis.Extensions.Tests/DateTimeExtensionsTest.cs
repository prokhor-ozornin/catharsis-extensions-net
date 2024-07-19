using System.Globalization;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeExtensions"/>.</para>
/// </summary>
public sealed class DateTimeExtensionsTest : UnitTest
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
        Validate(true, date);
        Validate(true, date.AddSeconds(-1));
        Validate(false, date.AddSeconds(1));
      });
    }

    return;

    static void Validate(bool result, DateTime date) => date.IsPast().Should().Be(result);
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
        Validate(false, date);
        Validate(false, date.AddSeconds(-1));
        Validate(true, date.AddSeconds(1));
      });
    }

    return;

    static void Validate(bool result, DateTime date) => date.IsFuture().Should().Be(result);
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

      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Validate(bool result, DateTime date) => date.IsWeekday().Should().Be(result);
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

      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Validate(bool result, DateTime date) => date.IsWeekend().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTime, DateTime, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date)
    {
      date.Range(date, default).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<DateTime>>().And.BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().BeAssignableTo<IEnumerable<DateTime>>().And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
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
        Validate(false, date, date.AddYears(1));
        Validate(false, date, date.AddMonths(1));
        Validate(false, date, date.AddDays(1));
        Validate(true, date, date.AddHours(1));
        Validate(true, date, date.AddMinutes(1));
        Validate(true, date, date.AddSeconds(1));
        Validate(true, date, date.AddMilliseconds(1));
      });
    }

    return;

    static void Validate(bool result, DateTime left, DateTime right) => left.EqualsByDate(right).Should().Be(result);
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
        Validate(true, date, date.AddYears(1));
        Validate(true, date, date.AddMonths(1));
        Validate(true, date, date.AddDays(1));
        Validate(false, date, date.AddHours(1));
        Validate(false, date, date.AddMinutes(1));
        Validate(false, date, date.AddSeconds(1));
        Validate(false, date, date.AddMilliseconds(1));
      });
    }

    return;

    static void Validate(bool result, DateTime left, DateTime right) => left.EqualsByTime(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfYear(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfYear().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfYear(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfYear().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfMonth(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfMonth().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfMonth(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfMonth().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfDay(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfDay_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfDay().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfDay(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfDay_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfDay().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfHour(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfHour().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfHour(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfHour().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfMinute(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfMinute().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfMinute(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfMinute().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(59);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtStartOfSecond(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtStartOfSecond().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.AtEndOfSecond(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.AtEndOfSecond().Should().BeIn(date.Kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(date.Hour).And.HaveMinute(date.Minute).And.HaveSecond(date.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.ToDateTimeOffset().Should().BeSameDateAs(new DateTimeOffset(date.ToUniversalTime())).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateOnly_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.ToDateOnly().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.MinValue);
      Validate(DateTime.MaxValue);
      Validate(DateTime.Now);
      Validate(DateTime.UtcNow);
    }

    return;

    static void Validate(DateTime date) => date.ToTimeOnly().Should().HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
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
        Validate(date);
      }
    }

    return;

    static void Validate(DateTime date) => date.ToIsoString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
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
        Validate(date);
      }
    }

    return;

    static void Validate(DateTime date) => date.ToRfcString().Should().BeOfType<string>().And.Be(date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture));
  }
}