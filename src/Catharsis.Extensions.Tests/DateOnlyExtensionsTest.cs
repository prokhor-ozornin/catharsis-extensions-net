using Catharsis.Commons;
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

    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(1).Min(dateOnly).Should().Be(dateOnly);
    });

    return;

    static void Validate(DateOnly result, DateOnly left, DateOnly right) => left.Min(right).Should().Be(result);
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

    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Max(dateOnly).Should().Be(dateOnly.AddYears(1));
      dateOnly.AddMonths(1).Max(dateOnly).Should().Be(dateOnly.AddMonths(1));
      dateOnly.AddDays(1).Max(dateOnly).Should().Be(dateOnly.AddDays(1));
    });

    return;

    static void Validate(DateOnly result, DateOnly left, DateOnly right) => left.Max(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Range(DateOnly, DateOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    new[] { DateTime.Now, DateTime.UtcNow }.ForEach(date =>
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Range(dateOnly, TimeSpan.Zero).Should().BeOfType<IEnumerable<DateOnly>>().And.BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(1)).Should().BeOfType<IEnumerable<DateOnly>>().And.BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(-1)).Should().BeOfType<IEnumerable<DateOnly>>().And.BeEmpty();

      dateOnly.Range(dateOnly.AddDays(1), 1.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 1.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(1), 2.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 2.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(2), 1.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(1));
      dateOnly.Range(dateOnly.AddDays(-2), 1.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(dateOnly.AddDays(-2), dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(3), 2.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(2));
      dateOnly.Range(dateOnly.AddDays(-3), 2.Days()).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(dateOnly.AddDays(-3), dateOnly.AddDays(-1));
    });

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekday(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    var dates = new DateOnly[7].Fill(DateTime.UtcNow.ToDateOnly().AddDays);

    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekday().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekend(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    var dates = new DateOnly[7].Fill(DateTime.UtcNow.ToDateOnly().AddDays);

    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
    Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
    Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekend().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToYearStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearStart_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;

    static void Validate(DateOnly date) => date.TruncateToYearStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToMonthStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthStart_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;

    static void Validate(DateOnly date) => date.TruncateToMonthStart().Should().BeOnOrBefore(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToYearEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToYearEnd_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;

    static void Validate(DateOnly date) => date.TruncateToYearEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.TruncateToMonthEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TruncateToMonthEnd_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;

    static void Validate(DateOnly date) => date.TruncateToMonthEnd().Should().BeOnOrAfter(date).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTime(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;
    
    static void Validate(DateOnly date) => date.ToDateTime().Should().BeSameDateAs(date.ToDateTime()).And.BeIn(DateTimeKind.Unspecified).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTimeOffset(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    Validate(DateOnly.MinValue);
    Validate(DateOnly.MaxValue);
    Validate(DateTime.Now.ToDateOnly());
    Validate(DateTime.UtcNow.ToDateOnly());

    return;

    static void Validate(DateOnly date) => date.ToDateTimeOffset().Should().BeSameDateAs(date.ToDateTimeOffset()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(TimeSpan.Zero);
  }
}