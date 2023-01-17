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
  ///   <para>Determines whether a string matches specified regular expression.</para>
  /// </summary>
  /// <param name="text">The string to search for a match.</param>
  /// <param name="pattern">The regular expression pattern to match.</param>
  /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
  /// <returns><c>true</c> if <paramref name="text"/> matches <paramref name="pattern"/> regular expression, <c>false</c> if not.</returns>
  /// <seealso cref="Regex.IsMatch(string, string)"/>
  public static bool IsMatch(this string text, string pattern, RegexOptions? options = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (pattern is null) throw new ArgumentNullException(nameof(pattern));

    return options is not null ? Regex.IsMatch(text, pattern, options.Value) : Regex.IsMatch(text, pattern);
  }

  /// <summary>
  ///   <para>Searches source input string for all occurrences of a specified regular expression, using the specified matching options.</para>
  /// </summary>
  /// <param name="text">The string to search for a match.</param>
  /// <param name="pattern">The regular expression pattern to match.</param>
  /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
  /// <returns>A collection of the <see cref="Match"/> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
  public static IEnumerable<Match> Matches(this string text, string pattern, RegexOptions? options = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (pattern is null) throw new ArgumentNullException(nameof(pattern));

    return options is not null ? Regex.Matches(text, pattern, options.Value) : Regex.Matches(text, pattern);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="regex"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static IEnumerable<Match> ToEnumerable(this Regex regex, string text)
  {
    if (regex is null) throw new ArgumentNullException(nameof(regex));
    if (text is null) throw new ArgumentNullException(nameof(text));

    return regex.Matches(text);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="match"></param>
  /// <returns></returns>
  public static IEnumerable<Capture> ToEnumerable(this Match match) => match?.Captures ?? throw new ArgumentNullException(nameof(match));
}