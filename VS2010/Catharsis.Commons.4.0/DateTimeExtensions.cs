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
    ///   <para>For a given range of dates decrements starting date by one day down to final date, calling specified action with each iteration.</para>
    /// </summary>
    /// <param name="self">Starting date to decrement.</param>
    /// <param name="to">Final date to decrement to.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <returns>Back reference to <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static DateTime DownTo(this DateTime self, DateTime to, Action action)
    {
      Assertion.NotNull(action);

      self.Subtract(to).Days.Times(action);
      return self;
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current day.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the end of day of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="StartOfDay(DateTime)"/>
    public static DateTime EndOfDay(this DateTime self)
    {
      return new DateTime(self.Year, self.Month, self.Day, 23, 59, 59, self.Kind);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current month.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the end of month of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="StartOfDay(DateTime)"/>
    /// <seealso cref="StartOfMonth(DateTime)"/>
    public static DateTime EndOfMonth(this DateTime self)
    {
      return new DateTime(self.Year, self.Month, DateTime.DaysInMonth(self.Year, self.Month), 23, 59, 59, self.Kind);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the end of current year.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the end of year of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="StartOfYear(DateTime)"/>
    public static DateTime EndOfYear(this DateTime self)
    {
      return new DateTime(self.Year, 12, 31, 23, 59, 59, self.Kind);
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Friday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Friday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Friday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Friday;
    }

    /// <summary>
    ///   <para>Formats given date/time instance according to ISO 8601 specification and returns formatted date as a string.</para>
    /// </summary>
    /// <param name="self">Date/time object instance.</param>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <seealso cref="DateTimeFormatInfo"/>
    public static string ISO8601(this DateTime self)
    {
      return self.ToString("o");
    }

    /// <summary>
    ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same date (have same year/month/day values).</para>
    /// </summary>
    /// <param name="self">Curent date to compare with the second.</param>
    /// <param name="other">Second date to compare with the current.</param>
    /// <returns><c>true</c> if both <paramref name="self"/> and <paramref name="other"/> have equals date component.</returns>
    /// <seealso cref="IsSameTime(DateTime, DateTime)"/>
    public static bool IsSameDate(this DateTime self, DateTime other)
    {
      return self.Day == other.Day && self.Month == other.Month && self.Year == other.Year;
    }

    /// <summary>
    ///   <para>Determines whether two <see cref="DateTime"/> object instances represent the same time (have same hour/minute/second values).</para>
    /// </summary>
    /// <param name="self">Current date to compare with the second.</param>
    /// <param name="other">Second date to compare with the current.</param>
    /// <returns><c>true</c> if both <paramref name="self"/> and <paramref name="other"/> have equal time component.</returns>
    /// <seealso cref="IsSameDate(DateTime, DateTime)"/>
    public static bool IsSameTime(this DateTime self, DateTime other)
    {
      return self.Second == other.Second && self.Minute == other.Minute && self.Hour == other.Hour;
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Monday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Monday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Monday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Monday;
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the next day from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the day after the day of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddDays(double)"/>
    /// <seealso cref="PreviousDay(DateTime)"/>
    public static DateTime NextDay(this DateTime self)
    {
      return self.AddDays(1);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the next month from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the month after the month of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddMonths(int)"/>
    /// <seealso cref="PreviousMonth(DateTime)"/>
    public static DateTime NextMonth(this DateTime self)
    {
      return self.AddMonths(1);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the next year from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the year after the year of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddYears(int)"/>
    /// <seealso cref="PreviousYear(DateTime)"/>
    public static DateTime NextYear(this DateTime self)
    {
      return self.AddYears(1);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the previous day from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the day before the day of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddDays(double)"/>
    /// <seealso cref="NextDay(DateTime)"/>
    public static DateTime PreviousDay(this DateTime self)
    {
      return self.AddDays(-1);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the previous month from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the month before the month of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddMonths(int)"/>
    /// <seealso cref="NextMonth(DateTime)"/>
    public static DateTime PreviousMonth(this DateTime self)
    {
      return self.AddMonths(-1);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the previous year from the current date.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the year before the year of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="DateTime.AddYears(int)"/>
    /// <seealso cref="NextYear(DateTime)"/>
    public static DateTime PreviousYear(this DateTime self)
    {
      return self.AddYears(-1);
    }

    /// <summary>
    ///   <para>Formats given date/time instance according to RFC 1123 specification and returns formatted date as a string.</para>
    /// </summary>
    /// <param name="self">Date/time object instance.</param>
    /// <returns>Formatted date/time value as a string.</returns>
    /// <seealso cref="DateTimeFormatInfo"/>
    public static string RFC1121(this DateTime self)
    {
      return self.ToString("r");
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Saturday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Saturday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Saturday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Saturday;
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of day.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represent the start of day of the specified <paramref name="self"/>.</returns>
    /// <remarks>Date component (year, month, day) remains the same, while time component (hour/minute/second) is changed to represent the beginning of the day (hour : 0, minute : 0, second : 0).</remarks>
    /// <seealso cref="EndOfDay(DateTime)"/>
    public static DateTime StartOfDay(this DateTime self)
    {
      return new DateTime(self.Year, self.Month, self.Day, 0, 0, 0, self.Kind);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of month.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the start of month of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="EndOfMonth(DateTime)"/>
    public static DateTime StartOfMonth(this DateTime self)
    {
      return new DateTime(self.Year, self.Month, 1, 0, 0, 0, self.Kind);
    }

    /// <summary>
    ///   <para>For a given date/time instance returns a new date/time, representing the start of year.</para>
    /// </summary>
    /// <param name="self">Original date/time object instance.</param>
    /// <returns>New date/time object instance that represents the start of year of the specified <paramref name="self"/>.</returns>
    /// <seealso cref="EndOfYear(DateTime)"/>
    public static DateTime StartOfYear(this DateTime self)
    {
      return new DateTime(self.Year, 1, 1, 0, 0, 0, self.Kind);
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Sunday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Sunday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Sunday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Thursday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Thursday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Thursday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Thursday;
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Tuesday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Tuesday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Tuesday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Tuesday;
    }

    /// <summary>
    ///   <para>For a given range of dates increments starting date by one day up to final date, calling specified action with each iteration.</para>
    /// </summary>
    /// <param name="self">Starting date to increment.</param>
    /// <param name="to">Final date to increment to.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <returns>Back reference to <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static DateTime UpTo(this DateTime self, DateTime to, Action action)
    {
      Assertion.NotNull(action);

      to.Subtract(self).Days.Times(action);
      return self;
    }

    /// <summary>
    ///   <para>Determines whether specified <see cref="DateTime"/> object represents a Wednesday day of week.</para>
    /// </summary>
    /// <param name="self">Date instance.</param>
    /// <returns><c>true</c> of <paramref name="self"/>'s day of week is Wednesday, false if not.</returns>
    /// <seealso cref="DateTime.DayOfWeek"/>
    public static bool Wednesday(this DateTime self)
    {
      return self.DayOfWeek == DayOfWeek.Wednesday;
    }
  }
}