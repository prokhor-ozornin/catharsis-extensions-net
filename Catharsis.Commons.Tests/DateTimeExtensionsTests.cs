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
      Assert.Equal(now.Kind, endOfDay.Kind);
      Assert.Equal(now.Year, endOfDay.Year);
      Assert.Equal(now.Month, endOfDay.Month);
      Assert.Equal(now.Day, endOfDay.Day);
      Assert.Equal(23, endOfDay.Hour);
      Assert.Equal(59, endOfDay.Minute);
      Assert.Equal(59, endOfDay.Second);
      Assert.Equal(0, endOfDay.Millisecond);
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
      Assert.Equal(now.Kind, endOfMonth.Kind);
      Assert.Equal(now.Year, endOfMonth.Year);
      Assert.Equal(now.Month, endOfMonth.Month);
      Assert.Equal(DateTime.DaysInMonth(now.Year, now.Month), endOfMonth.Day);
      Assert.Equal(23, endOfMonth.Hour);
      Assert.Equal(59, endOfMonth.Minute);
      Assert.Equal(59, endOfMonth.Second);
      Assert.Equal(0, endOfMonth.Millisecond);
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
      Assert.Equal(now.Kind, endOfYear.Kind);
      Assert.Equal(now.Year, endOfYear.Year);
      Assert.Equal(12, endOfYear.Month);
      Assert.Equal(31, endOfYear.Day);
      Assert.Equal(23, endOfYear.Hour);
      Assert.Equal(59, endOfYear.Minute);
      Assert.Equal(59, endOfYear.Second);
      Assert.Equal(0, endOfYear.Millisecond);
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
      Assert.Equal(now.AddDays(1), now.NextDay());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.NextMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void NextMonth_Method()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddMonths(1), now.NextMonth());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.NextYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void NextYear_Method()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddYears(1), now.NextYear());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousDay_Method()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddDays(-1), now.PreviousDay());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousMonth(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousMonth_Method()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddMonths(-1), now.PreviousMonth());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.PreviousYear(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void PreviousYear_Method()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddYears(-1), now.PreviousYear());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.RFC1123(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void RFC1123_Method()
    {
      var time = DateTime.Today;
      Assert.True(DateTime.ParseExact(time.RFC1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Equals(time), "Parsed = " + DateTime.ParseExact(time.RFC1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Ticks + ", Exact = " + time.Ticks);
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
      Assert.Equal(now.Kind, startOfDay.Kind);
      Assert.Equal(now.Year, startOfDay.Year);
      Assert.Equal(now.Month, startOfDay.Month);
      Assert.Equal(now.Day, startOfDay.Day);
      Assert.Equal(0, startOfDay.Hour);
      Assert.Equal(0, startOfDay.Minute);
      Assert.Equal(0, startOfDay.Second);
      Assert.Equal(0, startOfDay.Millisecond);
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
      Assert.Equal(now.Kind, startOfMonth.Kind);
      Assert.Equal(now.Year, startOfMonth.Year);
      Assert.Equal(now.Month, startOfMonth.Month);
      Assert.Equal(1, startOfMonth.Day);
      Assert.Equal(0, startOfMonth.Hour);
      Assert.Equal(0, startOfMonth.Minute);
      Assert.Equal(0, startOfMonth.Second);
      Assert.Equal(0, startOfMonth.Millisecond);
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
      Assert.Equal(now.Kind, startOfYear.Kind);
      Assert.Equal(now.Year, startOfYear.Year);
      Assert.Equal(1, startOfYear.Month);
      Assert.Equal(1, startOfYear.Day);
      Assert.Equal(0, startOfYear.Hour);
      Assert.Equal(0, startOfYear.Minute);
      Assert.Equal(0, startOfYear.Second);
      Assert.Equal(0, startOfYear.Millisecond);
    }
  }
}