using System;
using System.IO;
using System.Security.Cryptography;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="SymmetricAlgorithm"/>.</para>
  /// </summary>
  /// <seealso cref="SymmetricAlgorithm"/>
  public static class SymmetricAlgorithmExtensions
  {
    /// <summary>
    ///   <para>Decrypts encrypted binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
    /// <param name="source">Binary data to be decrypted.</param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Decrypt(SymmetricAlgorithm, Stream, bool)"/>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, byte[] source)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.CreateDecryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
    /// <param name="source">Stream of binary data to be decrypted.</param>
    /// <param name="close">Whether to close <paramref name="source"/> stream when all data from it has been read and decrypted.</param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Decrypt(SymmetricAlgorithm, byte[])"/>
    public static byte[] Decrypt(this SymmetricAlgorithm algorithm, Stream source, bool close = false)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.Decrypt(source.Bytes(close));
    }

    /// <summary>
    ///   <para>Encrypts binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
    /// <param name="source">Binary data to be encrypted.</param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, Stream, bool)"/>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, byte[] source)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.CreateEncryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para>Encrypts binary data from a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
    /// <param name="source">Stream of binary data to be encrypted.</param>
    /// <param name="close">Whether to close <paramref name="source"/> stream when all data from it has been read and encrypted.</param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, byte[])"/>
    public static byte[] Encrypt(this SymmetricAlgorithm algorithm, Stream source, bool close = false)
    {
      Assertion.NotNull(algorithm);
      Assertion.NotNull(source);

      return algorithm.Encrypt(source.Bytes(close));
    }
  }
}