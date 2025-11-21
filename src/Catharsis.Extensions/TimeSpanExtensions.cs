namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="TimeSpan"/>
public static class TimeSpanExtensions
{
  /// <param name="timespan"></param>
  extension(TimeSpan timespan)
  {
    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time lesser than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <value>Current date/time, decremented by the <paramref name="timespan"/>, expressed as a local time.</value>
    /// <seealso cref="InTheFuture(TimeSpan)"/>
    public DateTimeOffset InThePast => DateTimeOffset.UtcNow - timespan;

    /// <summary>
    ///   <para>Returns a new date/time instance, representing a point in time greater than the current by specified <see cref="TimeSpan"/>.</para>
    /// </summary>
    /// <value>Current date/time, incremented by the <paramref name="timespan"/>, expressed as a local time.</value>
    /// <seealso cref="InThePast(TimeSpan)"/>
    public DateTimeOffset InTheFuture => DateTimeOffset.UtcNow + timespan;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="TimeSpan"/> instance can be considered "empty", meaning that it's equal to <see cref="TimeSpan.Zero"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="timespan"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    public bool IsEmpty => timespan == TimeSpan.Zero;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public TimeSpan With(TimeSpan offset) => timespan.Add(offset);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public TimeSpan Without(TimeSpan offset) => timespan.Subtract(offset);
  }
}