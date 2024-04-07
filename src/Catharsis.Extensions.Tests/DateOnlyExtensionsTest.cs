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
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.Range(DateOnly, DateOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.Now.ToDateOnly());
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
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekday(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekday_Method()
  {
    using (new AssertionScope())
    {
      var monday = new DateOnly(2024, 1, 1);

      Validate(true, monday);
      Validate(true, monday.AddDays(1));
      Validate(true, monday.AddDays(2));
      Validate(true, monday.AddDays(3));
      Validate(true, monday.AddDays(4));
      Validate(false, monday.AddDays(5));
      Validate(false, monday.AddDays(6));
    }

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekday().Should().Be(date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday or DayOfWeek.Friday).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.IsWeekend(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWeekend_Method()
  {
    using (new AssertionScope())
    {
      var monday = new DateOnly(2024, 1, 1);

      Validate(false, monday);
      Validate(false, monday.AddDays(1));
      Validate(false, monday.AddDays(2));
      Validate(false, monday.AddDays(3));
      Validate(false, monday.AddDays(4));
      Validate(true, monday.AddDays(5));
      Validate(true, monday.AddDays(6));
    }

    return;

    static void Validate(bool result, DateOnly date) => date.IsWeekend().Should().Be(date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.StartOfYear(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.StartOfYear().Should().HaveYear(date.Year).And.HaveMonth(1).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.StartOfMonth(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.StartOfMonth().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.EndOfYear(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfYear_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.EndOfYear().Should().HaveYear(date.Year).And.HaveMonth(12).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateOnlyExtensions.EndOfMonth(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfMonth_Method()
  {
    using (new AssertionScope())
    {
      Validate(DateOnly.MinValue);
      Validate(DateOnly.MaxValue);
      Validate(DateTime.Now.ToDateOnly());
    }

    return;

    static void Validate(DateOnly date) => date.EndOfMonth().Should().HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(DateTime.DaysInMonth(date.Year, date.Month));
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