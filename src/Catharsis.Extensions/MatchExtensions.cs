using System.Text.RegularExpressions;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for regular expressions/matches types.</para>
/// </summary>
/// <seealso cref="Match"/>
public static class MatchExtensions
{
  /// <param name="match"></param>
  extension(Match match)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="match"/> is <see langword="null"/>.</exception>
    public IEnumerable<Capture> ToEnumerable() => match?.Captures ?? throw new ArgumentNullException(nameof(match));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => match is not null && match.Success;
  }
}