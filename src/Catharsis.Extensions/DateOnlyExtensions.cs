#if NET10_0_OR_GREATER
namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for date and time types.</para>
/// </summary>
/// <seealso cref="DateOnly"/>
public static class DateOnlyExtensions
{
  /// <param name="date"></param>
  extension(DateOnly date)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekend(DateOnly)"/>
    public bool IsWeekday => !date.IsWeekend;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsWeekday(DateOnly)"/>
    public bool IsWeekend => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public IEnumerable<DateOnly> Range(DateOnly to, TimeSpan offset)
    {
      if (date == to || offset == TimeSpan.Zero)
      {
        yield break;
      }

      var dateFrom = offset > TimeSpan.Zero ? date.Min(to) : date.Max(to);
      var dateTo = offset > TimeSpan.Zero ? date.Max(to) : date.Min(to);

      for (var dateOnly = dateFrom; dateOnly < dateTo; dateOnly = dateOnly.AddDays((int) offset.TotalDays))
      {
        yield return dateOnly;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateOnlyExtensions.EndOfYear"/>
    public DateOnly StartOfYear => new(date.Year, 1, 1);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateOnlyExtensions.StartOfYear"/>
    public DateOnly EndOfYear => new(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateOnlyExtensions.EndOfMonth"/>
    public DateOnly StartOfMonth => new(date.Year, date.Month, 1);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="DateOnlyExtensions.StartOfMonth"/>
    public DateOnly EndOfMonth => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    public DateTime ToDateTime(DateTimeKind kind = default) => date.ToDateTime(default, kind);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset(DateTimeKind kind = default) => new(date.ToDateTime(kind));
  }
}
#endif