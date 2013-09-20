using System;
using System.Net;
using System.Security;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  ///   <seealso cref="string"/>
  /// </summary>
  public static class StringExtendedExtensions
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="unsecure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="unsecure"/> is a <c>null</c> reference.</exception>
    public static SecureString Secure(this string unsecure)
    {
      Assertion.NotNull(unsecure);

      var secure = new SecureString();
      unsecure.Each(secure.AppendChar);
      return secure;
    }
  }
}