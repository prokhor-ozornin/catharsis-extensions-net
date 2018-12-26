namespace Catharsis.Commons
{
  using System;
  using System.IO;
  using System.Security;

  #if NET_40
  using System.Security.Cryptography;
  #endif

  /// <summary>
  ///   <para>Set of cryptography and security-related extensions methods.</para>
  /// </summary>
  public static class SecurityExtensions
  {
#if NET_40
    /// <summary>
    ///   <para>Decrypts encrypted binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="self">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
    /// <param name="source">Binary data to be decrypted.</param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Decrypt(SymmetricAlgorithm, Stream, bool)"/>
    public static byte[] Decrypt(this SymmetricAlgorithm self, byte[] source)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(source);

      return self.CreateDecryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="self">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
    /// <param name="source">Stream of binary data to be decrypted.</param>
    /// <param name="close">Whether to close <paramref name="source"/> stream when all data from it has been read and decrypted.</param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Decrypt(SymmetricAlgorithm, byte[])"/>
    public static byte[] Decrypt(this SymmetricAlgorithm self, Stream source, bool close = false)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(source);

      return self.Decrypt(source.Bytes(close));
    }

    /// <summary>
    ///   <para>Encrypts binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="self">Symmetric algorithm to be used for encryption.</param>
    /// <param name="source">Binary data to be encrypted.</param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, Stream, bool)"/>
    public static byte[] Encrypt(this SymmetricAlgorithm self, byte[] source)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(source);

      return self.CreateEncryptor().TransformFinalBlock(source, 0, source.Length);
    }

    /// <summary>
    ///   <para>Encrypts binary data from a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="self">Symmetric algorithm to be used for encryption.</param>
    /// <param name="source">Stream of binary data to be encrypted.</param>
    /// <param name="close">Whether to close <paramref name="source"/> stream when all data from it has been read and encrypted.</param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, byte[])"/>
    public static byte[] Encrypt(this SymmetricAlgorithm self, Stream source, bool close = false)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(source);

      return self.Encrypt(source.Bytes(close));
    }

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
#endif
  }
}