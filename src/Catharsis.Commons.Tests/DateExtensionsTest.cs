using System;
using System.Globalization;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class DateExtensionsTest
  {
    [Fact]
    public void days()
    {
      Assert.Equal(1, ((byte)1).Days().TotalDays);
      Assert.Equal(1, ((short)1).Days().TotalDays);
      Assert.Equal(1, 1.Days().TotalDays);
      Assert.Equal(0, ((byte)0).Days().TotalMilliseconds);
      Assert.Equal(-1, ((short)-1).Days().TotalDays);
      Assert.Equal(-1, -1.Days().TotalDays);
    }

    [Fact]
    public void end_of_day()
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

    [Fact]
    public void end_of_month()
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

    [Fact]
    public void end_of_year()
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

    [Fact]
    public void hours()
    {
      Assert.Equal(1, ((byte)1).Hours().TotalHours);
      Assert.Equal(1, ((short)1).Hours().TotalHours);
      Assert.Equal(1, 1.Hours().TotalHours);
      Assert.Equal(0, ((byte)0).Hours().TotalMilliseconds);
      Assert.Equal(-1, ((short)-1).Hours().TotalHours);
      Assert.Equal(-1, -1.Hours().TotalHours);
    }

    [Fact]
    public void iso8601()
    {
      var time = DateTime.Today;
      Assert.True(DateTime.ParseExact(time.ISO8601(), "o", CultureInfo.InvariantCulture).Equals(time));
    }

    [Fact]
    public void is_same_date()
    {
      var date = new DateTime(2000, 2, 1);

      Assert.True(date.IsSameDate(new DateTime(2000, 2, 1)));
      Assert.False(date.IsSameDate(new DateTime(2000, 2, 2)));
      Assert.False(date.IsSameDate(new DateTime(2000, 3, 1)));
      Assert.False(date.IsSameDate(new DateTime(2001, 2, 1)));
    }

    [Fact]
    public void is_same_time()
    {
      var time = new DateTime(2000, 2, 1, 12, 2, 1);

      Assert.True(time.IsSameTime(new DateTime(2000, 1, 1, 12, 2, 1)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 12, 2, 2)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 12, 3, 1)));
      Assert.False(time.IsSameTime(new DateTime(2000, 1, 1, 13, 2, 1)));
    }

    [Fact]
    public void minutes()
    {
      Assert.Equal(1, ((byte)1).Minutes().TotalMinutes);
      Assert.Equal(1, ((short)1).Minutes().TotalMinutes);
      Assert.Equal(1, 1.Minutes().TotalMinutes);
      Assert.Equal(0, ((byte)0).Minutes().TotalMilliseconds);
      Assert.Equal(-1, ((short)-1).Minutes().TotalMinutes);
      Assert.Equal(-1, -1.Minutes().TotalMinutes);
    }

    [Fact]
    public void milliseconds()
    {
      Assert.Equal(1, ((byte)1).Milliseconds().TotalMilliseconds);
      Assert.Equal(1, ((short)1).Milliseconds().TotalMilliseconds);
      Assert.Equal(1, 1.Milliseconds().TotalMilliseconds);
      Assert.Equal(0, ((byte)0).Milliseconds().TotalMilliseconds);
      Assert.Equal(-1, ((short)-1).Milliseconds().TotalMilliseconds);
      Assert.Equal(-1, -1.Milliseconds().TotalMilliseconds);
    }

    [Fact]
    public void next_day()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddDays(1), now.NextDay());
    }

    [Fact]
    public void next_month()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddMonths(1), now.NextMonth());
    }

    [Fact]
    public void next_year()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddYears(1), now.NextYear());
    }

    [Fact]
    public void previous_day()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddDays(-1), now.PreviousDay());
    }

    [Fact]
    public void previous_month()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddMonths(-1), now.PreviousMonth());
    }

    [Fact]
    public void previous_year()
    {
      var now = DateTime.UtcNow;
      Assert.Equal(now.AddYears(-1), now.PreviousYear());
    }

    [Fact]
    public void rfc()
    {
      var time = DateTime.Today;
      Assert.True(DateTime.ParseExact(time.RFC1121(), new DateTimeFormatInfo().RFC1123Pattern, CultureInfo.InvariantCulture).Equals(time));
    }

    [Fact]
    public void seconds()
    {
      Assert.Equal(1, ((byte)1).Seconds().TotalSeconds);
      Assert.Equal(1, ((short)1).Seconds().TotalSeconds);
      Assert.Equal(1, 1.Seconds().TotalSeconds);
      Assert.Equal(0, ((byte)0).Seconds().TotalMilliseconds);
      Assert.Equal(-1, ((short)-1).Seconds().TotalSeconds);
      Assert.Equal(-1, -1.Seconds().TotalSeconds);
    }


    [Fact]
    public void start_of_day()
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

    [Fact]
    public void start_of_month()
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

    [Fact]
    public void start_of_year()
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

    [Fact]
    public void friday()
    {
      Assert.True(new DateTime(2014, 1, 3).Friday());
      Assert.False(new DateTime(2014, 1, 4).Friday());
    }

    [Fact]
    public void monday()
    {
      Assert.True(new DateTime(2013, 12, 30).Monday());
      Assert.False(new DateTime(2013, 12, 31).Monday());
    }

    [Fact]
    public void saturday()
    {
      Assert.True(new DateTime(2014, 1, 4).Saturday());
      Assert.False(new DateTime(2014, 1, 5).Saturday());
    }

    [Fact]
    public void sunday()
    {
      Assert.True(new DateTime(2014, 1, 5).Sunday());
      Assert.False(new DateTime(2014, 1, 6).Sunday());
    }

    [Fact]
    public void thursday()
    {
      Assert.True(new DateTime(2014, 1, 2).Thursday());
      Assert.False(new DateTime(2014, 1, 3).Thursday());
    }

    [Fact]
    public void tuesday()
    {
      Assert.True(new DateTime(2013, 12, 31).Tuesday());
      Assert.False(new DateTime(2014, 1, 1).Tuesday());
    }

    [Fact]
    public void wednesday()
    {
      Assert.True(new DateTime(2014, 1, 1).Wednesday());
      Assert.False(new DateTime(2014, 1, 2).Wednesday());
    }

    [Fact]
    public void up_to()
    {
      var counter = 0;
      new DateTime(1999, 1, 1).UpTo(new DateTime(1998, 12, 31), () => counter++);
      Assert.Equal(0, counter);
      new DateTime(1999, 1, 1).UpTo(new DateTime(1999, 1, 1), () => counter++);
      Assert.Equal(0, counter);
      new DateTime(1999, 1, 1).UpTo(new DateTime(1999, 1, 3), () => counter++);
      Assert.Equal(2, counter);
    }

    [Fact]
    public void down_to()
    {
      var counter = 0;
      new DateTime(1999, 1, 1).DownTo(new DateTime(1999, 1, 2), () => counter++);
      Assert.Equal(0, counter);
      new DateTime(1999, 1, 1).DownTo(new DateTime(1999, 1, 1), () => counter++);
      Assert.Equal(0, counter);
      new DateTime(1999, 1, 1).DownTo(new DateTime(1998, 12, 30), () => counter++);
      Assert.Equal(2, counter);
    }

    [Fact]
    public void from_now()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.FromNow().IsSameDate(DateTime.Now.Add(timespan)));
    }

    [Fact]
    public void from_now_utc()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.FromNowUtc().IsSameDate(DateTime.UtcNow.Add(timespan)));
    }

    [Fact]
    public void before_now()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.BeforeNow().IsSameDate(DateTime.Now.Subtract(timespan)));
    }

    [Fact]
    public void before_now_utc()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.BeforeNowUtc().IsSameDate(DateTime.UtcNow.Subtract(timespan)));
    }
  }
}