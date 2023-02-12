using System.Text.RegularExpressions;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for regular expressions/matches types.</para>
/// </summary>
/// <seealso cref="Regex"/>
/// <seealso cref="Match"/>
public static class RegexExtensions
{

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="regex"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<Match> ToEnumerable(this Regex regex, string text)
  {
    if (regex is null) throw new ArgumentNullException(nameof(regex));
    if (text is null) throw new ArgumentNullException(nameof(text));

    return regex.Matches(text);
  }
}