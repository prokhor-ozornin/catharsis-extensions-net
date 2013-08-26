using System;
using System.Security.Cryptography;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class ArrayExtendedExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeMD5(this byte[] bytes)
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
    public static byte[] EncodeSHA1(this byte[] bytes)
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
    public static byte[] EncodeSHA256(this byte[] bytes)
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
    public static byte[] EncodeSHA512(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA512"))
      {
        return hash.ComputeHash(bytes);
      }
    }
  }
}