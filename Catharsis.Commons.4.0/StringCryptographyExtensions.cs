using System;
using System.Security;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringCryptographyExtensions
  {
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