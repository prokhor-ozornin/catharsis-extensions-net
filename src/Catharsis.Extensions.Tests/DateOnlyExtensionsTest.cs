using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateOnlyExtensions"/>.</para>
/// </summary>
public sealed class DateOnlyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekday(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTime.UtcNow.ToDateOnly();
      var dates = new DateOnly[7].Fill(index => now.AddDays(index));

      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekday().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekend(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    using (new AssertionScope())
    {
      var now = DateTime.UtcNow.ToDateOnly();
      var dates = new DateOnly[7].Fill(index => now.AddDays(index));

      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Monday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday));
      Validate(false, dates.Single(date => date.DayOfWeek == DayOfWeek.Friday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday));
      Validate(true, dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday));
    }

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekend().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Range(DateOnly, DateOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.Now.ToDateOnly());
      Validate(DateTime.UtcNow.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date)
    {
      date.Range(date, default).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().BeAssignableTo<IEnumerable<DateOnly>>().And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.AtStartOfYear(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
      Validate(DateTime.UtcNow.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.AtStartOfYear().Should().HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.AtEndOfYear(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
      Validate(DateTime.UtcNow.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.AtEndOfYear().Should().HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.AtStartOfMonth(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
      Validate(DateTime.UtcNow.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.AtStartOfMonth().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.AtEndOfMonth(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
      Validate(DateTime.UtcNow.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.AtEndOfMonth().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTime(DateOnly, DateTimeKind)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    using (new AssertionScope())
    {
      Enum.GetValues<DateTimeKind>().ForEach(kind =>
      {
        Validate(DateOnly.MinValue, kind);
        Validate(DateOnly.MaxValue, kind);
        Validate(DateTime.Now.ToDateOnly(), kind);
        Validate(DateTime.UtcNow.ToDateOnly(), kind);
      });
    }

    return;
    
    static void Validate(DateOnly date, DateTimeKind kind) => date.ToDateTime(kind).Should().BeIn(kind).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.ToDateTimeOffset(DateOnly, DateTimeKind)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      Enum.GetValues<DateTimeKind>().ForEach(kind =>
      {
        Validate(DateOnly.MaxValue, kind);
        Validate(DateTime.Now.ToDateOnly(), kind);
        Validate(DateTime.UtcNow.ToDateOnly(), kind);
      });
    }

    return;

    static void Validate(DateOnly date, DateTimeKind kind)
    {
      var result = date.ToDateTimeOffset(kind);
      result.Should().HaveOffset(kind != DateTimeKind.Utc ? TimeZoneInfo.Local.GetUtcOffset(result) : default).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(default);
    }
  }
}