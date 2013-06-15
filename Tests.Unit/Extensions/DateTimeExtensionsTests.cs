using System;
using System.Globalization;
using Xunit;


namespace Catharsis.Commons.Extensions
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
      var current = DateTime.UtcNow;
      var endOfDay = current.EndOfDay();
      
      Assert.True(endOfDay >= current);
      Assert.True(endOfDay.Kind == current.Kind);
      Assert.True(endOfDay.Year == current.Year);
      Assert.True(endOfDay.Month == current.Month);
      Assert.True(endOfDay.Day == current.Day);
      Assert.True(endOfDay.Hour == 23);
      Assert.True(endOfDay.Minute == 59);
      Assert.True(endOfDay.Second == 59);
      Assert.True(endOfDay.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsDate(DateTime, DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void EqualsDate_Method()
    {
      var date = new DateTime(2000, 2, 1);

      Assert.True(date.EqualsDate(new DateTime(2000, 2, 1)));
      Assert.False(date.EqualsDate(new DateTime(2000, 2, 2)));
      Assert.False(date.EqualsDate(new DateTime(2000, 3, 1)));
      Assert.False(date.EqualsDate(new DateTime(2001, 2, 1)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsTime(DateTime, DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void EqualsTime_Method()
    {
      var time = new DateTime(2000, 2, 1, 12, 2, 1);

      Assert.True(time.EqualsTime(new DateTime(2000, 1, 1, 12, 2, 1)));
      Assert.False(time.EqualsTime(new DateTime(2000, 1, 1, 12, 2, 2)));
      Assert.False(time.EqualsTime(new DateTime(2000, 1, 1, 12, 3, 1)));
      Assert.False(time.EqualsTime(new DateTime(2000, 1, 1, 13, 2, 1)));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.StartOfDay(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void StartOfDay_Method()
    {
      var current = DateTime.UtcNow;
      var startOfDay = current.StartOfDay();

      Assert.True(startOfDay <= current);
      Assert.True(startOfDay.Kind == current.Kind);
      Assert.True(startOfDay.Year == current.Year);
      Assert.True(startOfDay.Month == current.Month);
      Assert.True(startOfDay.Day == current.Day);
      Assert.True(startOfDay.Hour == 0);
      Assert.True(startOfDay.Minute == 0);
      Assert.True(startOfDay.Second == 0);
      Assert.True(startOfDay.Millisecond == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRFC1123(DateTime)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToRFC1123_Method()
    {
      var time = DateTime.Today;
      Assert.True(DateTime.ParseExact(time.ToRFC1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Equals(time), "Parsed = " + DateTime.ParseExact(time.ToRFC1123(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Ticks + ", Exact = " + time.Ticks);
    }
  }
}