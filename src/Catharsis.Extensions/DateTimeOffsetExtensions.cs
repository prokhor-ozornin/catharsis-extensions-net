using System.Globalization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="DateTimeOffset"/>
public static class DateTimeOffsetExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static bool EqualsByDate(this DateTimeOffset left, DateTimeOffset right) => left.Year == right.Year && left.Month == right.Month && left.Day == right.Day;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static bool EqualsByTime(this DateTimeOffset left, DateTimeOffset right) => left.Hour == right.Hour && left.Minute == right.Minute && left.Second == right.Second && left.Millisecond == right.Millisecond;

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
  public static bool IsFuture(this DateTimeOffset date) => date > DateTimeOffset.UtcNow;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekday(this DateTimeOffset date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekend(this DateTimeOffset date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfYear(this DateTimeOffset date) => new(date.Year, 1, 1, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfMonth(this DateTimeOffset date) => new(date.Year, date.Month, 1, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfDay(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfHour(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfMinute(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtStartOfSecond(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfYear(this DateTimeOffset date) => new(date.Year, 12, 31, 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfMonth(this DateTimeOffset date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfDay(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfHour(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfMinute(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset AtEndOfSecond(this DateTimeOffset date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Offset);

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
  public static string ToIsoString(this DateTimeOffset date) => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  public static string ToRfcString(this DateTimeOffset date) => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly ToDateOnly(this DateTimeOffset date) => DateOnly.FromDateTime(date.DateTime);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static TimeOnly ToTimeOnly(this DateTimeOffset date) => TimeOnly.FromDateTime(date.DateTime);
#endif
}