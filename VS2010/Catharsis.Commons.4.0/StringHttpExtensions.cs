using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringHttpExtensions
  {
    /// <summary>
    ///   <para>Decodes URL-encoded string.</para>
    /// </summary>
    /// <param name="value">String to decode.</param>
    /// <returns>Decoded string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static string UrlDecode(this string value)
    {
      Assertion.NotNull(value);

      return Uri.UnescapeDataString(value);
    }

    /// <summary>
    ///   <para>URL-encodes string.</para>
    /// </summary>
    /// <param name="value">String to encode.</param>
    /// <returns>Encoded string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public static string UrlEncode(this string value)
    {
      Assertion.NotNull(value);

      return Uri.EscapeDataString(value);
    }
  }
}