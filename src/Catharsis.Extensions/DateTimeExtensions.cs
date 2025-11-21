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
  /// <param name="date"></param>
  extension(DateTime date)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsFuture(DateTime)"/>
    public bool IsPast => date.ToUniversalTime() < DateTime.UtcNow;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsPast(DateTime)"/>
    public bool IsFuture => date.ToUniversalTime() > DateTime.UtcNow;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekend(DateTime)"/>
    public bool IsWeekday => !date.IsWeekend;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekday(DateTime)"/>
    public bool IsWeekend => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public IEnumerable<DateTime> Range(DateTime to, TimeSpan offset)
    {
      if (date == to || offset == TimeSpan.Zero)
      {
        yield break;
      }

      var dateFrom = offset > TimeSpan.Zero ? date.Min(to) : date.Max(to);
      var dateTo = offset > TimeSpan.Zero ? date.Max(to) : date.Min(to);

      for (var dateTime = dateFrom; dateTime < dateTo; dateTime = dateTime.Add(offset))
      {
        yield return dateTime;
      }
    }

    /// <summary>
    ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same date (have same year/month/day values).</para>
    /// </summary>
    /// <param name="right">Second date to compare with the current.</param>
    /// <returns><c>true</c> if both <paramref name="date"/> and <paramref name="right"/> have equals date component.</returns>
    /// <seealso cref="EqualsByTime(DateTime, DateTime)"/>
    public bool EqualsByDate(DateTime right) => date.Year == right.Year && date.Month == right.Month && date.Day == right.Day;

    /// <summary>
    ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same time (have same hour/minute/second values).</para>
    /// </summary>
    /// <param name="right">Second date to compare with the current.</param>
    /// <returns><c>true</c> if both <paramref name="date"/> and <paramref name="right"/> have equal time component.</returns>
    /// <seealso cref="EqualsByDate(DateTime, DateTime)"/>
    public bool EqualsByTime(DateTime right) => date.Hour == right.Hour && date.Minute == right.Minute && date.Second == right.Second && date.Millisecond == right.Millisecond;

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of year.</para>
    /// </summary>
    /// <value>New date/time object instance that represents the start of year of the specified <paramref name="date"/>.</value>
    /// <seealso cref="DateTimeExtensions.EndOfYear"/>
    public DateTime StartOfYear => new(date.Year, 1, 1, 0, 0, 0, date.Kind);

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current year.</para>
    /// </summary>
    /// <value>New date/time object instance that represents the end of year of the specified <paramref name="date"/>.</value>
    /// <seealso cref="DateTimeExtensions.StartOfYear"/>
    public DateTime EndOfYear => new(date.Year, 12, 31, 23, 59, 59, 999, date.Kind);

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of month.</para>
    /// </summary>
    /// <value>New date/time object instance that represents the start of month of the specified <paramref name="date"/>.</value>
    /// <seealso cref="DateTimeExtensions.EndOfMonth"/>
    public DateTime StartOfMonth => new(date.Year, date.Month, 1, 0, 0, 0, date.Kind);

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current month.</para>
    /// </summary>
    /// <value>New date/time object instance that represents the end of month of the specified <paramref name="date"/>.</value>
    /// <seealso cref="DateTimeExtensions.StartOfMonth"/>
    public DateTime EndOfMonth => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
    /// </summary>
    /// <value>New date/time object instance that represent the start of day of the specified <paramref name="date"/>.</value>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
    /// <seealso cref="DateTimeExtensions.EndOfDay"/>
    public DateTime StartOfDay => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current day.</para>
    /// </summary>
    /// <value>New date/time object instance that represents the end of day of the specified <paramref name="date"/>.</value>
    /// <seealso cref="DateTimeExtensions.StartOfDay"/>
    public DateTime EndOfDay => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.EndOfHour"/>
    public DateTime StartOfHour => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.StartOfHour"/>
    public DateTime EndOfHour => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.EndOfMinute"/>
    public DateTime StartOfMinute => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.StartOfMinute"/>
    public DateTime EndOfMinute => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.EndOfSecond"/>
    public DateTime StartOfSecond => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateTimeExtensions.StartOfSecond"/>
    public DateTime EndOfSecond => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset() => new(date.ToUniversalTime());

  #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public DateOnly ToDateOnly() => DateOnly.FromDateTime(date);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public TimeOnly ToTimeOnly() => TimeOnly.FromDateTime(date);
  #endif

    /// <summary>
    ///   <para>Formats given date/time instance according to ISO 8601 specification and returns formatted date as a string.</para>
    /// </summary>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <seealso cref="ToRfcString(DateTime)"/>
    public string ToIsoString() => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

    /// <summary>
    ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
    /// </summary>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <seealso cref="ToIsoString(DateTime)"/>
    public string ToRfcString() => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
  }
}