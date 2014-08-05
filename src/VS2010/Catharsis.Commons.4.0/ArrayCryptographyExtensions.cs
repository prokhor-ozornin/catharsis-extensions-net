using System;
using System.Security.Cryptography;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for <see cref="Array"/> or <see cref="byte"/>.</para>
  /// </summary>
  public static class ArrayCryptographyExtensions
  {
    /// <summary>
    ///   <para>Computes hash digest for the given array of bytes, using <c>MD5</c> algorithm.</para>
    /// </summary>
    /// <param name="self">Source bytes sequence.</param>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="HashAlgorithm"/>
    public static byte[] MD5(this byte[] self)
    {
      Assertion.NotNull(self);

      using (var hash = HashAlgorithm.Create("MD5"))
      {
        return hash.ComputeHash(self);
      }
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of bytes, using <c>SHA1</c> algorithm.</para>
    /// </summary>
    /// <param name="self">Source bytes sequence.</param>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="HashAlgorithm"/>
    /// <seealso cref="SHA256(byte[])"/>
    /// <seealso cref="SHA512(byte[])"/>
    public static byte[] SHA1(this byte[] self)
    {
      Assertion.NotNull(self);

      using (var hash = HashAlgorithm.Create("SHA1"))
      {
        return hash.ComputeHash(self);
      }
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of bytes, using <c>SHA256</c> algorithm.</para>
    /// </summary>
    /// <param name="self">Source bytes sequence.</param>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="HashAlgorithm"/>
    /// <seealso cref="SHA1(byte[])"/>
    /// <seealso cref="SHA512(byte[])"/>
    public static byte[] SHA256(this byte[] self)
    {
      Assertion.NotNull(self);

      using (var hash = HashAlgorithm.Create("SHA256"))
      {
        return hash.ComputeHash(self);
      }
    }

    /// <summary>
    ///   <para>Computes hash digest for the given array of bytes, using <c>SHA512</c> algorithm.</para>
    /// </summary>
    /// <param name="self">Source bytes sequence.</param>
    /// <returns>Hash digest value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="HashAlgorithm"/>
    /// <seealso cref="SHA1(byte[])"/>
    /// <seealso cref="SHA256(byte[])"/>
    public static byte[] SHA512(this byte[] self)
    {
      Assertion.NotNull(self);

      using (var hash = HashAlgorithm.Create("SHA512"))
      {
        return hash.ComputeHash(self);
      }
    }
  }
}