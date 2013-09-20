using System;
using System.Security.Cryptography;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for <see cref="Array"/> or <see cref="byte"/>.</para>
  /// </summary>
  public static class ArrayExtendedExtensions
  {
    /// <summary>
    ///   <para>Computes hash for the given sequence of bytes, using MD5 algorithm.</para>
    /// </summary>
    /// <param name="bytes">Source sequence for hash computation.</param>
    /// <returns>Hashed value </returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeMd5(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("MD5"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSha1(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA1"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSha256(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA256"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSha512(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA512"))
      {
        return hash.ComputeHash(bytes);
      }
    }
  }
}