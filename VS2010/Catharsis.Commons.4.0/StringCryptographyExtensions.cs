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
    ///   <para>Creates a secure version of string (text that should be kept confidential) from a normal one.</para>
    /// </summary>
    /// <param name="unsecure">Normal string to convert to secure version.</param>
    /// <returns>Secure version of <paramref name="unsecure"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="unsecure"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="SecureString"/>
    public static SecureString Secure(this string unsecure)
    {
      Assertion.NotNull(unsecure);

      var secure = new SecureString();
      unsecure.Each(secure.AppendChar);
      return secure;
    }
  }
}