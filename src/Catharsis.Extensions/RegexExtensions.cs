using System.Text.RegularExpressions;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for regular expressions/matches types.</para>
/// </summary>
/// <seealso cref="Regex"/>
/// <seealso cref="Match"/>
public static class RegexExtensions
{
  /// <param name="regex">Regular expression to be cloned.</param>
  extension(Regex regex)
  {
    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="Regex"/> with the same pattern and options as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="regex"/> is <see langword="null"/>.</exception>
    public Regex Clone() => regex is not null ? new Regex(regex.ToString(), regex.Options, regex.MatchTimeout) : throw new ArgumentNullException(nameof(regex));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="regex"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    public IEnumerable<Match> ToEnumerable(string text)
    {
      if (regex is null) throw new ArgumentNullException(nameof(regex));
      if (text is null) throw new ArgumentNullException(nameof(text));

      return regex.Matches(text);
    }
  }
}