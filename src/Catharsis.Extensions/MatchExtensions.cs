using System.Text.RegularExpressions;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for regular expressions/matches types.</para>
/// </summary>
/// <seealso cref="Match"/>
public static class MatchExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="match"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<Capture> ToEnumerable(this Match match) => match?.Captures ?? throw new ArgumentNullException(nameof(match));
}