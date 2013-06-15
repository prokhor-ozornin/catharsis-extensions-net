using System;
using System.Globalization;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for structure <see cref="DateTime"/>.</para>
  ///   <seealso cref="DateTime"/>
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of day.</para>
    ///   <seealso cref="StartOfDay(DateTime)"/>
    /// </summary>
    /// <param name="date">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the end of day of the specified <paramref name="date"/>.</returns>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the end of the day(hour : 23, minute : 59, second : 59).</remarks>
    public static DateTime EndOfDay(this DateTime date)
    {
      return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, date.Kind);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool EqualsDate(this DateTime self, DateTime other)
    {
      return self.Day == other.Day && self.Month == other.Month && self.Year == other.Year;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool EqualsTime(this DateTime self, DateTime other)
    {
      return self.Second == other.Second && self.Minute == other.Minute && self.Hour == other.Hour;
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
    ///   <seealso cref="EndOfDay(DateTime)"/>
    /// </summary>
    /// <param name="date">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represent the start of day of the specified <paramref name="date"/>.</returns>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
    public static DateTime StartOfDay(this DateTime date)
    {
      return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);
    }

    /// <summary>
    ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
    ///   <seealso cref="DateTimeFormatInfo"/>
    /// </summary>
    /// <param name="date">Date/time object instance.</param>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <remarks>Returned formatted date/time string represents date in UTC timezone, formatted for invariant culture.</remarks>
    public static string ToRFC1123(this DateTime date)
    {
      return date.ToString("r");
    }
  }
}