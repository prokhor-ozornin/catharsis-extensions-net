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
  /// <exception cref="ArgumentNullException">If <paramref name="match"/> is <see langword="null"/>.</exception>
  public static IEnumerable<Capture> ToEnumerable(this Match match) => match?.Captures ?? throw new ArgumentNullException(nameof(match));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="match"></param>
  /// <returns></returns>
  public static bool ToBoolean(this Match match) => match is not null && match.Success;
}