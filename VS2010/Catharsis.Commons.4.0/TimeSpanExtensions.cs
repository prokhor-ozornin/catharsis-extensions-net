using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for structure <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <seealso cref="TimeSpan"/>
  public static class TimeSpanExtensions
  {
    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="value">Time span to add to current date/time.</param>
    /// <returns>Current date/time, incremented by the <paramref name="value"/>, expressed as a local time.</returns>
    public static DateTime FromNow(this TimeSpan value)
    {
      return DateTime.Now.Add(value);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="value">Time span to add to current date/time.</param>
    /// <returns>Current date/time, incremented by the <paramref name="value"/>, expressed as Coordinated Universal Time (UTC).</returns>
    public static DateTime FromNowUtc(this TimeSpan value)
    {
      return DateTime.UtcNow.Add(value);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="value">Time span to substract from current date/time.</param>
    /// <returns>Current date/time, decremented by the <paramref name="value"/>, expressed as a local time.</returns>
    public static DateTime BeforeNow(this TimeSpan value)
    {
      return DateTime.Now.Subtract(value);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="value">Time span to substract from current date/time.</param>
    /// <returns>Current date/time, decremented by the <paramref name="value"/>, expressed as Coordinated Universal Time (UTC).</returns>
    public static DateTime BeforeNowUtc(this TimeSpan value)
    {
      return DateTime.UtcNow.Subtract(value);
    }
  }
}