using System.Net;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="Cookie"/>
public static class CookieExtensions
{
  /// <param name="cookie"></param>
  extension(Cookie cookie)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => cookie is null || cookie.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="Cookie"/> instance can be considered "empty", meaning it has an "empty" name or value.</para>
    /// </summary>
    /// <value>If the specified <paramref name="cookie"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="cookie"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => cookie is not null ? cookie.Name.IsUnset || cookie.Value.IsUnset : throw new ArgumentNullException(nameof(cookie));

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="Cookie"/> with the same properties as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cookie"/> is <see langword="null"/>.</exception>
    public Cookie Clone() => cookie is not null ? new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
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
  }
}