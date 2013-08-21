using System;
using System.IO;
using System.Security.Cryptography;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class SymmetricAlgorithmExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, byte[] source)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.CreateDecryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="source"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, Stream source, bool close = false)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.Decrypt(source.Bytes(close));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, byte[] source)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.CreateEncryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="algorithm"></param>
    /// <param name="source"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, Stream source, bool close = false)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.Encrypt(source.Bytes(close));
    }
  }
}