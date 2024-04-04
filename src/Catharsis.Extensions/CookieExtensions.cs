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
  public static Cookie Clone(this Cookie cookie) => cookie is not null ? new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain) 
  {
    Secure = cookie.Secure,
    HttpOnly = cookie.HttpOnly,
    Port = cookie.Port,
    Expires = cookie.Expires,
    Version = cookie.Version,
    Comment = cookie.Comment,
    CommentUri = cookie.CommentUri,
  } : throw new ArgumentNullException(nameof(cookie));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="cookie"></param>
  /// <returns></returns>
  public static bool IsUnset(this Cookie cookie) => cookie is null || cookie.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="cookie"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this Cookie cookie) => cookie is not null ? cookie.Name.IsUnset() || cookie.Value.IsUnset() : throw new ArgumentNullException(nameof(cookie));
}