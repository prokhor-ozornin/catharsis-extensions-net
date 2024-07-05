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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="regex"/> is <see langword="null"/>.</exception>
  public static Regex Clone(this Regex regex) => regex is not null ? new Regex(regex.ToString(), regex.Options, regex.MatchTimeout) : throw new ArgumentNullException(nameof(regex));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="regex"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="regex"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  public static IEnumerable<Match> ToEnumerable(this Regex regex, string text)
  {
    if (regex is null) throw new ArgumentNullException(nameof(regex));
    if (text is null) throw new ArgumentNullException(nameof(text));

    return regex.Matches(text);
  }
}