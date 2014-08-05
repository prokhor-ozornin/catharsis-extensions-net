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
    /// <param name="self">Normal string to convert to secure version.</param>
    /// <returns>Secure version of <paramref name="self"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="SecureString"/>
    public static SecureString Secure(this string self)
    {
      Assertion.NotNull(self);

      var secure = new SecureString();
      self.Each(secure.AppendChar);
      return secure;
    }
  }
}