using System.Net;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="Cookie"/>
public static class CookieExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="Cookie"/> with the same properties as the original.</para>
  /// </summary>
  /// <param name="cookie">Cookie to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="cookie"/> is <see langword="null"/>.</exception>
  public static Cookie Clone(this Cookie cookie) => cookie is not null ? new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain) 
  {
    Secure = cookie.Secure,
    HttpOnly = cookie.HttpOnly,
    Port = cookie.Port,
    Expires = cookie.Expires,
    Version = cookie.Version,
    Comment = cookie.Comment,
    CommentUri = cookie.CommentUri,
    Discard = cookie.Discard
  } : throw new ArgumentNullException(nameof(cookie));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="cookie"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(Cookie)"/>
  public static bool IsUnset(this Cookie cookie) => cookie is null || cookie.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="Cookie"/> instance can be considered "empty", meaning it has an "empty" name or value.</para>
  /// </summary>
  /// <param name="cookie">Cookie instance for evaluation.</param>
  /// <returns>If the specified <paramref name="cookie"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="cookie"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(Cookie)"/>
  public static bool IsEmpty(this Cookie cookie) => cookie is not null ? cookie.Name.IsUnset() || cookie.Value.IsUnset() : throw new ArgumentNullException(nameof(cookie));
}