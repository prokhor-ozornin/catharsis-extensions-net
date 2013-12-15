using System;
using System.Globalization;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DateTimeExtensions"/>.</para>
  /// </summary>
  public sealed class DateTimeExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void EndOfDay_Method()
    {
      var now = DateTime.UtcNow;
      var endOfDay = now.EndOfDay();

      Assert.True(endOfDay >= now);
      Assert.True(endOfDay.Kind == now.Kind);
      Assert.True(endOfDay.Year == now.Year);
      Assert.True(endOfDay.Month == now.Month);
      Assert.True(endOfDay.Day == now.Day);
      Assert.True(endOfDay.Hour == 23);
      Assert.True(endOfDay.Minute == 59);
      Assert.True(endOfDay.Second == 59);
      Assert.True(endOfDay.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void EndOfMonth_Method()
    {
      var now = DateTime.UtcNow;
      var endOfMonth = now.EndOfMonth();

      Assert.True(endOfMonth >= now);
      Assert.True(endOfMonth.Kind == now.Kind);
      Assert.True(endOfMonth.Year == now.Year);
      Assert.True(endOfMonth.Month == now.Month);
      Assert.True(endOfMonth.Day == DateTime.DaysInMonth(now.Year, now.Month));
      Assert.True(endOfMonth.Hour == 23);
      Assert.True(endOfMonth.Minute == 59);
      Assert.True(endOfMonth.Second == 59);
      Assert.True(endOfMonth.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.EndOfYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void EndOfYear_Method()
    {
      var now = DateTime.UtcNow;
      var endOfYear = now.EndOfYear();

      Assert.True(endOfYear >= now);
      Assert.True(endOfYear.Kind == now.Kind);
      Assert.True(endOfYear.Year == now.Year);
      Assert.True(endOfYear.Month == 12);
      Assert.True(endOfYear.Day == 31);
      Assert.True(endOfYear.Hour == 23);
      Assert.True(endOfYear.Minute == 59);
      Assert.True(endOfYear.Second == 59);
      Assert.True(endOfYear.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.IsSameDate(DateTime, DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsSameDate_Method()
    {
      var date = new DateTime(2000, 2, 1);

      Assert.True(date.IsSameDate(new DateTime(2000, 2, 1)));
      Assert.False(date.IsSameDate(new DateTime(2000, 2, 2)));
      Assert.False(date.IsSameDate(new DateTime(2000, 3, 1)));
      Assert.False(date.IsSameDate(new DateTime(2001, 2, 1)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.IsSameTime(DateTime, DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsSameTime_Method()
    {
      var time = new DateTime(2000, 2, 1, 12, 2, 1);

      Assert.True(time.IsSameTime(new DateTime(2000, 1, 1, 12, 2, 1)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 12, 2, 2)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 12, 3, 1)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 13, 2, 1)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.NextDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void NextDay_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.NextDay() == now.AddDays(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.NextMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void NextMonth_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.NextMonth() == now.AddMonths(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.NextYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void NextYear_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.NextYear() == now.AddYears(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousDay_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.PreviousDay() == now.AddDays(-1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousMonth_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.PreviousMonth() == now.AddMonths(-1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousYear_Method()
    {
      var now = DateTime.UtcNow;
      Assert.True(now.PreviousYear() == now.AddYears(-1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void StartOfDay_Method()
    {
      var now = DateTime.UtcNow;
      var startOfDay = now.StartOfDay();

      Assert.True(startOfDay <= now);
      Assert.True(startOfDay.Kind == now.Kind);
      Assert.True(startOfDay.Year == now.Year);
      Assert.True(startOfDay.Month == now.Month);
      Assert.True(startOfDay.Day == now.Day);
      Assert.True(startOfDay.Hour == 0);
      Assert.True(startOfDay.Minute == 0);
      Assert.True(startOfDay.Second == 0);
      Assert.True(startOfDay.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void StartOfMonth_Method()
    {
      var now = DateTime.UtcNow;
      var startOfMonth = now.StartOfMonth();

      Assert.True(startOfMonth <= now);
      Assert.True(startOfMonth.Kind == now.Kind);
      Assert.True(startOfMonth.Year == now.Year);
      Assert.True(startOfMonth.Month == now.Month);
      Assert.True(startOfMonth.Day == 1);
      Assert.True(startOfMonth.Hour == 0);
      Assert.True(startOfMonth.Minute == 0);
      Assert.True(startOfMonth.Second == 0);
      Assert.True(startOfMonth.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void StartOfYear_Method()
    {
      var now = DateTime.UtcNow;
      var startOfYear = now.StartOfYear();

      Assert.True(startOfYear <= now);
      Assert.True(startOfYear.Kind == now.Kind);
      Assert.True(startOfYear.Year == now.Year);
      Assert.True(startOfYear.Month == 1);
      Assert.True(startOfYear.Day == 1);
      Assert.True(startOfYear.Hour == 0);
      Assert.True(startOfYear.Minute == 0);
      Assert.True(startOfYear.Second == 0);
      Assert.True(startOfYear.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfc1123(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToRfc1123_Method()
    {
      var time = DateTime.Today;
      Assert.True(DateTime.ParseExact(time.ToRfc1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Equals(time), "Parsed = " + DateTime.ParseExact(time.ToRfc1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Ticks + ", Exact = " + time.Ticks);
    }
  }
}