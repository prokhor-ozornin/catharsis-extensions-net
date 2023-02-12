namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="TimeSpan"/>
public static class TimeSpanExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="timespan"></param>
  /// <returns></returns>
  public static bool IsEmpty(this TimeSpan timespan) => timespan == TimeSpan.Zero;

  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to subtract from current date/time.</param>
  /// <returns>Current date/time, decremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  public static DateTimeOffset InThePast(this TimeSpan offset) => DateTimeOffset.UtcNow - offset;

  /// <summary>
  ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
  /// </summary>
  /// <param name="offset">Time span to add to current date/time.</param>
  /// <returns>Current date/time, incremented by the <paramref name="offset"/>, expressed as a local time.</returns>
  public static DateTimeOffset InTheFuture(this TimeSpan offset) => DateTimeOffset.UtcNow + offset;
}