namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for ranges.</para>
/// </summary>
/// <seealso cref="Range"/>
public static class RangeExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="range"></param>
  /// <returns></returns>
  public static IEnumerable<int> ToEnumerable(this Range range) => range.Start.Value.To(range.End.Value);
}