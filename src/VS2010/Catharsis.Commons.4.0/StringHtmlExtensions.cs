using System;
using System.Net;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringHtmlExtensions
  {
    /// <summary>
    ///   <para>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</para>
    /// </summary>
    /// <param name="self">HTML-encoded version of string.</param>
    /// <returns>HTML-decoded version of <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="WebUtility.HtmlDecode(string)"/>
    /// <seealso cref="HtmlEncode(string)"/>
    public static string HtmlDecode(this string self)
    {
      Assertion.NotNull(self);

      return WebUtility.HtmlDecode(self);
    }

    /// <summary>
    ///   <para>Converts a string to an HTML-encoded string.</para>
    /// </summary>
    /// <param name="self">String to convert to HTML-encoded version.</param>
    /// <returns>HTML-encoded version of <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="WebUtility.HtmlEncode(string)"/>
    /// <seealso cref="HtmlDecode(string)"/>
    public static string HtmlEncode(this string self)
    {
      Assertion.NotNull(self);

      return WebUtility.HtmlEncode(self);
    }
  }
}