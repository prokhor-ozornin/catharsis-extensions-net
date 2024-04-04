#if NET8_0
namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="TimeOnly"/>
public static class TimeOnlyExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<TimeOnly> Range(this TimeOnly from, TimeOnly to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.Add(offset))
    {
      yield return date;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly StartOfHour(this TimeOnly time) => new(time.Hour, 0, 0, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly StartOfMinute(this TimeOnly time) => new(time.Hour, time.Minute, 0, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly StartOfSecond(this TimeOnly time) => new(time.Hour, time.Minute, time.Second, 0);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly EndOfHour(this TimeOnly time) => new(time.Hour, 59, 59, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly EndOfMinute(this TimeOnly time) => new(time.Hour, time.Minute, 59, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static TimeOnly EndOfSecond(this TimeOnly time) => new(time.Hour, time.Minute, time.Second, 999);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this TimeOnly time) => DateTime.UtcNow.TruncateToDayStart().Add(time.ToTimeSpan());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this TimeOnly time) => time.ToDateTime().ToDateTimeOffset();
}
#endif