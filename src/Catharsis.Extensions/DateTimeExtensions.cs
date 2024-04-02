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
  public static bool EqualsByDate(this DateTime left, DateTime right) => left.Year == right.Year && left.Month == right.Month && left.Day == right.Day;

  /// <summary>
  ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same time (have same hour/minute/second values).</para>
  /// </summary>
  /// <param name="left">Current date to compare with the second.</param>
  /// <param name="right">Second date to compare with the current.</param>
  /// <returns><c>true</c> if both <paramref name="left"/> and <paramref name="right"/> have equal time component.</returns>
  public static bool EqualsByTime(this DateTime left, DateTime right) => left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second && left.Millisecond == right.Millisecond;

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
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsPast(this DateTime date) => date.ToUniversalTime() < DateTime.UtcNow;

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
  public static bool IsWeekday(this DateTime date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekend(this DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of year.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the start of year of the specified <paramref name="date"/>.</returns>
  public static DateTime TruncateToYearStart(this DateTime date) => new(date.Year, 1, 1, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of month.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the start of month of the specified <paramref name="date"/>.</returns>
  public static DateTime TruncateToMonthStart(this DateTime date) => new(date.Year, date.Month, 1, 0, 0, 0, date.Kind);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represent the start of day of the specified <paramref name="date"/>.</returns>
  /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
  public static DateTime TruncateToDayStart(this DateTime date) => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);

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
  public static DateTime TruncateToMinuteStart(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime TruncateToSecondStart(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Kind);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current year.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of year of the specified <paramref name="date"/>.</returns>
  public static DateTime TruncateToYearEnd(this DateTime date) => new(date.Year, 12, 31, 23, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current month.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of month of the specified <paramref name="date"/>.</returns>
  public static DateTime TruncateToMonthEnd(this DateTime date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);

  /// <summary>
  ///   <para>For a given date/time instance returns a new date/time, representing the end of current day.</para>
  /// </summary>
  /// <param name="date">Original date/time object instance.</param>
  /// <returns>New date/time object instance that represents the end of day of the specified <paramref name="date"/>.</returns>
  public static DateTime TruncateToDayEnd(this DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);

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
  public static DateTime TruncateToMinuteEnd(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Kind);

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
  public static DateTimeOffset ToDateTimeOffset(this DateTime date) => new(date.ToUniversalTime());

  /// <summary>
  ///   <para>Formats given date/time instance according to ISO 8601 specification and returns formatted date as a string.</para>
  /// </summary>
  /// <param name="date">Date/time object instance.</param>
  /// <returns>Formatted date/time value as a string.</returns>
  public static string ToIsoString(this DateTime date) => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
  /// </summary>
  /// <param name="date">Date/time object instance.</param>
  /// <returns>Formatted date/time value as a string.</returns>
  public static string ToRfcString(this DateTime date) => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static TimeOnly ToTimeOnly(this DateTime date) => TimeOnly.FromDateTime(date);
#endif
}