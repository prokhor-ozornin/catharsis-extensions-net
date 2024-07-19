namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="TimeSpan"/>
public static class TimeSpanExtensions
{
  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to subtract from current date/time.</param>
  /// <returns>Current date/time, decremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  /// <seealso cref="InTheFuture(TimeSpan)"/>
  public static DateTimeOffset InThePast(this TimeSpan offset) => DateTimeOffset.UtcNow - offset;

  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to add to current date/time.</param>
  /// <returns>Current date/time, incremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  /// <seealso cref="InThePast(TimeSpan)"/>
  public static DateTimeOffset InTheFuture(this TimeSpan offset) => DateTimeOffset.UtcNow + offset;

  /// <summary>
  ///   <para>Determines whether the specified <see cref="TimeSpan"/> instance can be considered "empty", meaning that it's equal to <see cref="TimeSpan.Zero"/>.</para>
  /// </summary>
  /// <param name="timespan">Time span instance for evaluation.</param>
  /// <returns>If the specified <paramref name="timespan"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  public static bool IsEmpty(this TimeSpan timespan) => timespan == TimeSpan.Zero;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="timespan"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static TimeSpan With(this TimeSpan timespan, TimeSpan offset) => timespan.Add(offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="timespan"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static TimeSpan Without(this TimeSpan timespan, TimeSpan offset) => timespan.Subtract(offset);
}