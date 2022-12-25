using System.Globalization;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for globalization/i18n and culture-related types.</para>
/// </summary>
/// <seealso cref="CultureInfo"/>
public static class GlobalizationExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="culture"></param>
  /// <returns></returns>
  public static CultureInfo ReadOnly(this CultureInfo culture) => CultureInfo.ReadOnly(culture);
}