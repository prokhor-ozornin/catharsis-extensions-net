using System.Net;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="Cookie"/>
public static class CookieExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="cookie"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this Cookie cookie) => cookie is not null ? cookie.Name.IsEmpty() || cookie.Value.IsEmpty() : throw new ArgumentNullException(nameof(cookie));
}