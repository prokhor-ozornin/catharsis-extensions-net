#if NET7_0_OR_GREATER
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
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateOnly Min(this DateOnly left, DateOnly right) => left <= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static DateOnly Max(this DateOnly left, DateOnly right) => left >= right ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static IEnumerable<DateOnly> Range(this DateOnly from, DateOnly to, TimeSpan offset)
  {
    if (from == to || offset == TimeSpan.Zero)
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
  public static bool IsWeekday(this DateOnly date) => !date.IsWeekend();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static bool IsWeekend(this DateOnly date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToYearStart(this DateOnly date) => new(date.Year, 1, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToMonthStart(this DateOnly date) => new(date.Year, date.Month, 1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToYearEnd(this DateOnly date) => new(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateOnly TruncateToMonthEnd(this DateOnly date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTime ToDateTime(this DateOnly date) => date.ToDateTime(TimeOnly.MinValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this DateOnly date) => new(date.ToDateTime(), TimeSpan.Zero);
}
#endif