using System.Globalization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="DateTime"/>
/// <seealso cref="DateTimeOffset"/>
/// <seealso cref="TimeSpan"/>
/// <seealso cref="DateOnly"/>
/// <seealso cref="TimeOnly"/>
public static class DateTimeExtensions
{
  /// <summary>
  ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same date (have same year/month/day values).</para>
  /// </summary>
  /// <param name="left">Current date to compare with the second.</param>
  /// <param name="right">Second date to compare with the current.</param>
  /// <returns><c>true</c> if both <paramref name="left"/> and <paramref name="right"/> have equals date component.</returns>
  /// <seealso cref="EqualsByDate(DateTimeOffset, DateTimeOffset)"/>
  public static bool EqualsByDate(this DateTime left, DateTime right) => left.Year == right.Year && left.Month == right.Month && left.Day == right.Day;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <seealso cref="EqualsByDate(DateTime, DateTime)"/>
  public static bool EqualsByDate(this DateTimeOffset left, DateTimeOffset right) => left.Year == right.Year && left.Month == right.Month && left.Day == right.Day;

  /// <summary>
  ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same time (have same hour/minute/second values).</para>
  /// </summary>
  /// <param name="left">Current date to compare with the second.</param>
  /// <param name="right">Second date to compare with the current.</param>
  /// <returns><c>true</c> if both <paramref name="left"/> and <paramref name="right"/> have equal time component.</returns>
  /// <seealso cref="EqualsByTime(DateTimeOffset, DateTimeOffset)"/>
  public static bool EqualsByTime(this DateTime left, DateTime right) => left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second && left.Millisecond == right.Millisecond;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <seealso cref="EqualsByTime(DateTime, DateTime)"/>
  public static bool EqualsByTime(this DateTimeOffset left, DateTimeOffset right) => left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second && left.Millisecond == right.Millisecond;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateTime Min(this DateTime left, DateTime right) => left <= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateTimeOffset Min(this DateTimeOffset left, DateTimeOffset right) => left <= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateTime Max(this DateTime left, DateTime right) => left >= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateTimeOffset Max(this DateTimeOffset left, DateTimeOffset right) => left >= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<DateTime> Range(this DateTime from, DateTime to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.Add(offset))
    {
      yield return date;
    }
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<DateTimeOffset> Range(this DateTimeOffset from, DateTimeOffset to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.Add(offset))
    {
      yield return date;
    }
  }

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of days.</para>
  /// </summary>
  /// <param name="count">Number of days.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Days(this int count) => new(count, 0, 0, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of hours.</para>
  /// </summary>
  /// <param name="count">Number of hours.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Hours(this int count) => new(count, 0, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of minutes.</para>
  /// </summary>
  /// <param name="count">Number of minutes.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Minutes(this int count) => new(0, count, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of seconds.</para>
  /// </summary>
  /// <param name="count">Number of seconds.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Seconds(this int count) => new(0, 0, count);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
  /// </summary>
  /// <param name="count">Number of milliseconds.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Milliseconds(this int count) => new(0, 0, 0, 0, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="count"></param>
  /// <returns></returns>
  public static TimeSpan Ticks(this long count) => new(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="timespan"></param>
  /// <returns></returns>
  public static bool IsEmpty(this TimeSpan timespan) => timespan == TimeSpan.Zero;

  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to subtract from current date/time.</param>
  /// <returns>Current date/time, decremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  public static DateTimeOffset InThePast(this TimeSpan offset) => DateTimeOffset.UtcNow - offset;

  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to add to current date/time.</param>
  /// <returns>Current date/time, incremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  public static DateTimeOffset InTheFuture(this TimeSpan offset) => DateTimeOffset.UtcNow + offset;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsPast(this DateTime date) => date.ToUniversalTime() < DateTime.UtcNow;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsPast(this DateTimeOffset date) => date < DateTimeOffset.UtcNow;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsFuture(this DateTime date) => date.ToUniversalTime() > DateTime.UtcNow;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsFuture(this DateTimeOffset date) => date > DateTimeOffset.UtcNow;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekday(DateTimeOffset)"/>
  public static bool IsWeekday(this DateTime date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekday(DateTime)"/>
  public static bool IsWeekday(this DateTimeOffset date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekend(DateTimeOffset)"/>
  public static bool IsWeekend(this DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekend(DateTime)"/>
  public static bool IsWeekend(this DateTimeOffset date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of year.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the start of year of the specified <paramref name="date"/>.</returns>
  /// <seealso cref="TruncateToYearStart(DateTimeOffset)"/>
  public static DateTime TruncateToYearStart(this DateTime date) => new(date.Year, 1, 1, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToYearStart(DateTime)"/>
  public static DateTimeOffset TruncateToYearStart(this DateTimeOffset date) => new(date.Year, 1, 1, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of month.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the start of month of the specified <paramref name="date"/>.</returns>
  /// <seealso cref="TruncateToMonthStart(DateTimeOffset)"/>
  public static DateTime TruncateToMonthStart(this DateTime date) => new(date.Year, date.Month, 1, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToMonthStart(DateTime)"/>
  public static DateTimeOffset TruncateToMonthStart(this DateTimeOffset date) => new(date.Year, date.Month, 1, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represent the start of day of the specified <paramref name="date"/>.</returns>
  /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
  /// <seealso cref="TruncateToDayStart(DateTimeOffset)"/>
  public static DateTime TruncateToDayStart(this DateTime date) => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToDayStart(DateTime)"/>
  public static DateTimeOffset TruncateToDayStart(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToHourStart(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToHourStart(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToMinuteStart(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToMinuteStart(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToSecondStart(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToSecondStart(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Offset);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current year.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of year of the specified <paramref name="date"/>.</returns>
  /// <seealso cref="TruncateToYearEnd(DateTimeOffset)"/>
  public static DateTime TruncateToYearEnd(this DateTime date) => new(date.Year, 12, 31, 23, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToYearEnd(DateTime)"/>
  public static DateTimeOffset TruncateToYearEnd(this DateTimeOffset date) => new(date.Year, 12, 31, 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current month.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of month of the specified <paramref name="date"/>.</returns>
  /// <seealso cref="TruncateToMonthEnd(DateTimeOffset)"/>
  public static DateTime TruncateToMonthEnd(this DateTime date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToMonthEnd(DateTime)"/>
  public static DateTimeOffset TruncateToMonthEnd(this DateTimeOffset date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current day.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of day of the specified <paramref name="date"/>.</returns>
  /// <seealso cref="TruncateToDayEnd(DateTimeOffset)"/>
  public static DateTime TruncateToDayEnd(this DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="TruncateToDayEnd(DateTime)"/>
  public static DateTimeOffset TruncateToDayEnd(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToHourEnd(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToHourEnd(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToMinuteEnd(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToMinuteEnd(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToSecondEnd(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset TruncateToSecondEnd(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this DateTimeOffset date) => date.UtcDateTime;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this DateTime date) => new(date.ToUniversalTime());

  /// <summary>
  ///   <para>Formats given date/time instance according to ISO 8601 specification and returns formatted date as a string.</para>
  /// </summary>
  /// <param name="date">Date/time object instance.</param>
  /// <returns>Formatted date/time value as a string.</returns>
  public static string ToIsoString(this DateTime date) => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static string ToIsoString(this DateTimeOffset date) => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
  /// </summary>
  /// <param name="date">Date/time object instance.</param>
  /// <returns>Formatted date/time value as a string.</returns>
  public static string ToRfcString(this DateTime date) => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  public static string ToRfcString(this DateTimeOffset date) => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);

  #if NET7_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateOnly Min(this DateOnly left, DateOnly right) => left <= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static TimeOnly Min(this TimeOnly left, TimeOnly right) => left <= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateOnly Max(this DateOnly left, DateOnly right) => left >= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static TimeOnly Max(this TimeOnly left, TimeOnly right) => left >= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<DateOnly> Range(this DateOnly from, DateOnly to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.AddDays((int) offset.TotalDays))
    {
      yield return date;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<TimeOnly> Range(this TimeOnly from, TimeOnly to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.Add(offset))
    {
      yield return date;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekday(this DateOnly date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekend(this DateOnly date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToYearStart(this DateOnly date) => new(date.Year, 1, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToMonthStart(this DateOnly date) => new(date.Year, date.Month, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToHourStart(this TimeOnly time) => new(time.Hour, 0, 0, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToMinuteStart(this TimeOnly time) => new(time.Hour, time.Minute, 0, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToSecondStart(this TimeOnly time) => new(time.Hour, time.Minute, time.Second, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToYearEnd(this DateOnly date) => new(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToMonthEnd(this DateOnly date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToHourEnd(this TimeOnly time) => new(time.Hour, 59, 59, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToMinuteEnd(this TimeOnly time) => new(time.Hour, time.Minute, 59, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly TruncateToSecondEnd(this TimeOnly time) => new(time.Hour, time.Minute, time.Second, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this DateOnly date) => date.ToDateTime(TimeOnly.MinValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this TimeOnly time) => DateTime.UtcNow.TruncateToDayStart().Add(time.ToTimeSpan());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this DateOnly date) => new(date.ToDateTime(), TimeSpan.Zero);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this TimeOnly time) => time.ToDateTime().ToDateTimeOffset();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="ToDateOnly(DateTimeOffset)"/>
  public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="ToDateOnly(DateTime)"/>
  public static DateOnly ToDateOnly(this DateTimeOffset date) => DateOnly.FromDateTime(date.DateTime);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="ToTimeOnly(DateTimeOffset)"/>
  public static TimeOnly ToTimeOnly(this DateTime date) => TimeOnly.FromDateTime(date);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <see cref="ToTimeOnly(DateTime)"/>
  public static TimeOnly ToTimeOnly(this DateTimeOffset date) => TimeOnly.FromDateTime(date.DateTime);
  #endif
}