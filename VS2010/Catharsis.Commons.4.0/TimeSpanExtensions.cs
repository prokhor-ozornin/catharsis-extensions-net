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
    ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="self">Time span to substract from current date/time.</param>
    /// <returns>Current date/time, decremented by the <paramref name="self"/>, expressed as a local time.</returns>
    public static DateTime BeforeNow(this TimeSpan self)
    {
      return DateTime.Now.Subtract(self);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="self">Time span to substract from current date/time.</param>
    /// <returns>Current date/time, decremented by the <paramref name="self"/>, expressed as Coordinated Universal Time (UTC).</returns>
    public static DateTime BeforeNowUtc(this TimeSpan self)
    {
      return DateTime.UtcNow.Subtract(self);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="self">Time span to add to current date/time.</param>
    /// <returns>Current date/time, incremented by the <paramref name="self"/>, expressed as a local time.</returns>
    public static DateTime FromNow(this TimeSpan self)
    {
      return DateTime.Now.Add(self);
    }

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <param name="self">Time span to add to current date/time.</param>
    /// <returns>Current date/time, incremented by the <paramref name="self"/>, expressed as Coordinated Universal Time (UTC).</returns>
    public static DateTime FromNowUtc(this TimeSpan self)
    {
      return DateTime.UtcNow.Add(self);
    }
  }
}