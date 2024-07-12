#if NET8_0
namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="DateOnly"/>
public static class DateOnlyExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<DateOnly> Range(this DateOnly from, DateOnly to, TimeSpan offset)
  {
    if (from == to || offset == default)
    {
      yield break;
    }

    var dateFrom = offset > TimeSpan.Zero ? from.Min(to) : from.Max(to);
    var dateTo = offset > TimeSpan.Zero ? from.Max(to) : from.Min(to);

    for (var date = dateFrom; date < dateTo; date = date.AddDays((int) offset.TotalDays))
    {
      yield return date;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekend(DateOnly)"/>
  public static bool IsWeekday(this DateOnly date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="IsWeekday(DateOnly)"/>
  public static bool IsWeekend(this DateOnly date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="AtEndOfYear(DateOnly)"/>
  public static DateOnly AtStartOfYear(this DateOnly date) => new(date.Year, 1, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="AtEndOfMonth(DateOnly)"/>
  public static DateOnly AtStartOfMonth(this DateOnly date) => new(date.Year, date.Month, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="AtStartOfYear(DateOnly)"/>
  public static DateOnly AtEndOfYear(this DateOnly date) => new(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  /// <seealso cref="AtStartOfMonth(DateOnly)"/>
  public static DateOnly AtEndOfMonth(this DateOnly date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <param name="kind"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this DateOnly date, DateTimeKind kind = default) => date.ToDateTime(default, kind);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <param name="kind"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this DateOnly date, DateTimeKind kind = default) => new(date.ToDateTime(kind));
}
#endif