using System;
using System.Net;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  ///   <seealso cref="string"/>
  /// </summary>
  public static class StringHtmlExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static string DecodeHtml(this string value)
    {
      Assertion.NotNull(value);

      return value.Length > 0 ? WebUtility.HtmlDecode(value) : value;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static string EncodeHtml(this string value)
    {
      Assertion.NotNull(value);

      return value.Length > 0 ? WebUtility.HtmlEncode(value) : value;
    }
  }
}