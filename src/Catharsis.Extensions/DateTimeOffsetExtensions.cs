using System.Globalization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="DateTimeOffset"/>
public static class DateTimeOffsetExtensions
{
  /// <param name="date"></param>
  extension(DateTimeOffset date)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsFuture"/>
    public bool IsPast => date < DateTimeOffset.UtcNow;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsPast"/>
    public bool IsFuture => date > DateTimeOffset.UtcNow;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekend"/>
    public bool IsWeekday => !date.IsWeekend;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekday"/>
    public bool IsWeekend => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfYear"/>
    public DateTimeOffset StartOfYear => new(date.Year, 1, 1, 0, 0, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfYear"/>
    public DateTimeOffset EndOfYear => new(date.Year, 12, 31, 23, 59, 59, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfMonth"/>
    public DateTimeOffset StartOfMonth => new(date.Year, date.Month, 1, 0, 0, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfMonth"/>
    public DateTimeOffset EndOfMonth => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfDay"/>
    public DateTimeOffset StartOfDay => new(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfDay"/>
    public DateTimeOffset EndOfDay => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfHour"/>
    public DateTimeOffset StartOfHour => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfHour"/>
    public DateTimeOffset EndOfHour => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfMinute"/>
    public DateTimeOffset StartOfMinute => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfMinute"/>
    public DateTimeOffset EndOfMinute => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfSecond"/>
    public DateTimeOffset StartOfSecond => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfSecond"/>
    public DateTimeOffset EndOfSecond => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public IEnumerable<DateTimeOffset> Range(DateTimeOffset to, TimeSpan offset)
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
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <seealso cref="EqualsByTime(DateTimeOffset, DateTimeOffset)"/>
    public bool EqualsByDate(DateTimeOffset right) => date.Year == right.Year && date.Month == right.Month && date.Day == right.Day;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <seealso cref="EqualsByDate(DateTimeOffset, DateTimeOffset)"/>
    public bool EqualsByTime(DateTimeOffset right) => date.Hour == right.Hour && date.Minute == right.Minute && date.Second == right.Second && date.Millisecond == right.Millisecond;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public DateTime ToDateTime() => date.UtcDateTime;

  #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public DateOnly ToDateOnly() => DateOnly.FromDateTime(date.DateTime);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public TimeOnly ToTimeOnly() => TimeOnly.FromDateTime(date.DateTime);
  #endif

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <seealso cref="ToRfcString(DateTimeOffset)"/>
    public string ToIsoString() => date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <seealso cref="ToIsoString(DateTimeOffset)"/>
    public string ToRfcString() => date.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
  }
}