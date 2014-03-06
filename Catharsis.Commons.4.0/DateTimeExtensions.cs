using System;
using System.Globalization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for structure <see cref="DateTime"/>.</para>
  /// </summary>
  /// <seealso cref="DateTime"/>
  public static class DateTimeExtensions
  {
    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of day.</para>
    /// </summary>
    /// <param name="dateTime">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the end of day of the specified <paramref name="dateTime"/>.</returns>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the end of the day(hour : 23, minute : 59, second : 59).</remarks>
    /// <seealso cref="StartOfDay(DateTime)"/>
    public static DateTime EndOfDay(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, dateTime.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime EndOfMonth(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month), 23, 59, 59, dateTime.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime EndOfYear(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, dateTime.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool IsSameDate(this DateTime self, DateTime other)
    {
      return self.Day == other.Day && self.Month == other.Month && self.Year == other.Year;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool IsSameTime(this DateTime self, DateTime other)
    {
        return self.Second == other.Second && self.Minute == other.Minute && self.Hour == other.Hour;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime NextDay(this DateTime dateTime)
    {
      return dateTime.AddDays(1);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime NextMonth(this DateTime dateTime)
    {
      return dateTime.AddMonths(1);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime NextYear(this DateTime dateTime)
    {
      return dateTime.AddYears(1);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime PreviousDay(this DateTime dateTime)
    {
      return dateTime.AddDays(-1);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime PreviousMonth(this DateTime dateTime)
    {
      return dateTime.AddMonths(-1);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime PreviousYear(this DateTime dateTime)
    {
      return dateTime.AddYears(-1);
    }

    /// <summary>
    ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
    /// </summary>
    /// <param name="dateTime">Date/time object instance.</param>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <remarks>Returned formatted date/time string represents date in UTC timezone, formatted for invariant culture.</remarks>
    /// <seealso cref="DateTimeFormatInfo"/>
    public static string RFC1123(this DateTime dateTime)
    {
      return dateTime.ToString("r");
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
    /// </summary>
    /// <param name="dateTime">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represent the start of day of the specified <paramref name="dateTime"/>.</returns>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
    /// <seealso cref="EndOfDay(DateTime)"/>
    public static DateTime StartOfDay(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime StartOfMonth(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime StartOfYear(this DateTime dateTime)
    {
      return new DateTime(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Kind);
    }
  }
}