#if NET10_0_OR_GREATER
namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="TimeOnly"/>
public static class TimeOnlyExtensions
{
  /// <param name="time"></param>
  extension(TimeOnly time)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public IEnumerable<TimeOnly> Range(TimeOnly to, TimeSpan offset)
    {
      if (time == to || offset == TimeSpan.Zero)
      {
        yield break;
      }

      var dateFrom = offset > TimeSpan.Zero ? time.Min(to) : time.Max(to);
      var dateTo = offset > TimeSpan.Zero ? time.Max(to) : time.Min(to);

      for (var date = dateFrom; date < dateTo; date = date.Add(offset))
      {
        yield return date;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfHour"/>
    public TimeOnly StartOfHour => new(time.Hour, 0, 0, 0);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfHour"/>
    public TimeOnly EndOfHour => new(time.Hour, 59, 59, 999);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfMinute"/>
    public TimeOnly StartOfMinute => new(time.Hour, time.Minute, 0, 0);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfMinute"/>
    public TimeOnly EndOfMinute => new(time.Hour, time.Minute, 59, 999);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="EndOfSecond"/>
    public TimeOnly StartOfSecond => new(time.Hour, time.Minute, time.Second, 0);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="StartOfSecond"/>
    public TimeOnly EndOfSecond => new(time.Hour, time.Minute, time.Second, 999);
  }
}
#endif