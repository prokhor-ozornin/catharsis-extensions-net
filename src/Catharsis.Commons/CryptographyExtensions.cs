using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for cryptographic functions and types.</para>
/// </summary>
/// <seealso cref="SecureString"/>
/// <seealso cref="SymmetricAlgorithm"/>
/// <seealso cref="HashAlgorithm"/>
public static class CryptographyExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static bool IsEmpty(this SecureString secure) => secure.Length <= 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static SecureString Empty(this SecureString secure)
  {
    secure.Clear();
    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static SecureString Min(this SecureString left, SecureString right) => left.Length <= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static SecureString Max(this SecureString left, SecureString right) => left.Length >= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] Bytes(this SecureString secure, Encoding? encoding = null) => secure.Text().Bytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static string Text(this SecureString secure)
  {
    if (secure.Length <= 0)
    {
      return string.Empty;
    }

    var pointer = IntPtr.Zero;

    try
    {
      pointer = Marshal.SecureStringToGlobalAllocUnicode(secure);

      return Marshal.PtrToStringAuto(pointer) ?? string.Empty;
    }
    finally
    {
      if (pointer != IntPtr.Zero)
      {
        Marshal.ZeroFreeGlobalAllocUnicode(pointer);
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static SecureString UseTemporarily(this SecureString secure, Action<SecureString> action) => secure.UseFinally(action, secure => secure.Empty());

  /// <summary>
  ///   <para>Encrypts binary data, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
  /// <param name="bytes">Binary data to be encrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Encrypted binary data.</returns>
  public static async Task<byte[]> Encrypt(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    using var encryptor = algorithm.CreateEncryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(encryptor.InputBlockSize))
    {
      var block = encryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      await stream.Write(block, cancellation);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para>Encrypts binary data from a <see cref="Stream"/>, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
  /// <param name="stream">Stream of binary data to be encrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Encrypted binary data.</returns>
  public static async Task<byte[]> Encrypt(this SymmetricAlgorithm algorithm, Stream stream, CancellationToken cancellation = default) => await algorithm.Encrypt(stream.ToEnumerable(), cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Encrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.Encrypt(bytes, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Encrypt(this Stream stream, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.Encrypt(stream, cancellation);

  /// <summary>
  ///   <para>Decrypts encrypted binary data, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
  /// <param name="bytes">Binary data to be decrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Decrypted binary data.</returns>
  public static async Task<byte[]> Decrypt(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    using var decryptor = algorithm.CreateDecryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(decryptor.InputBlockSize))
    {
      var block = decryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      await stream.Write(block, cancellation);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
  /// <param name="stream">Stream of binary data to be decrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Decrypted binary data.</returns>
  public static async Task<byte[]> Decrypt(this SymmetricAlgorithm algorithm, Stream stream, CancellationToken cancellation = default) => await algorithm.Decrypt(stream.ToEnumerable(), cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Decrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.Decrypt(bytes, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Decrypt(this Stream stream, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.Decrypt(stream, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  public static byte[] Hash(this IEnumerable<byte> bytes, HashAlgorithm algorithm) => algorithm.ComputeHash(bytes.AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Hash(this Stream stream, HashAlgorithm algorithm, CancellationToken cancellation = default)
  {
    #if NET6_0
    return await algorithm.ComputeHashAsync(stream, cancellation);
#else
    return algorithm.ComputeHash(stream);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  public static string Hash(this string text, HashAlgorithm algorithm) => text.Bytes(Encoding.UTF8).Hash(algorithm).Hex();

  /// <summary>
  ///   <para>Computes hash digest for the given sequence of bytes, using <c>MD5</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  public static byte[] HashMd5(this IEnumerable<byte> bytes)
  {
    using var algorithm = HashAlgorithm.Create("MD5");
    return algorithm != null ? bytes.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> HashMd5(this Stream stream, CancellationToken cancellation = default)
  {
    using var algorithm = HashAlgorithm.Create("MD5");
    return algorithm != null ? await stream.Hash(algorithm, cancellation) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string HashMd5(this string text)
  {
    using var algorithm = HashAlgorithm.Create("MD5");
    return algorithm != null ? text.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA1</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  public static byte[] HashSha1(this IEnumerable<byte> bytes)
  {
    using var algorithm = HashAlgorithm.Create("SHA1");
    return algorithm != null ? bytes.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> HashSha1(this Stream stream, CancellationToken cancellation = default)
  {
    using var algorithm = HashAlgorithm.Create("SHA1");
    return algorithm != null ? await stream.Hash(algorithm, cancellation) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string HashSha1(this string text)
  {
    using var algorithm = HashAlgorithm.Create("SHA1");
    return algorithm != null ? text.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA256</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  public static byte[] HashSha256(this IEnumerable<byte> bytes)
  {
    using var algorithm = HashAlgorithm.Create("SHA256");
    return algorithm != null ? bytes.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> HashSha256(this Stream stream, CancellationToken cancellation = default)
  {
    using var algorithm = HashAlgorithm.Create("SHA256");
    return algorithm != null ? await stream.Hash(algorithm, cancellation) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string HashSha256(this string text)
  {
    using var algorithm = HashAlgorithm.Create("SHA256");
    return algorithm != null ? text.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static byte[] HashSha384(this IEnumerable<byte> bytes)
  {
    using var algorithm = HashAlgorithm.Create("SHA384");
    return algorithm != null ? bytes.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> HashSha384(this Stream stream, CancellationToken cancellation = default)
  {
    using var algorithm = HashAlgorithm.Create("SHA384");
    return algorithm != null ? await stream.Hash(algorithm, cancellation) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string HashSha384(this string text)
  {
    using var algorithm = HashAlgorithm.Create("SHA384");
    return algorithm != null ? text.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA512</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  public static byte[] HashSha512(this IEnumerable<byte> bytes)
  {
    using var algorithm = HashAlgorithm.Create("SHA512");
    return algorithm != null ? bytes.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> HashSha512(this Stream stream, CancellationToken cancellation = default)
  {
    using var algorithm = HashAlgorithm.Create("SHA512");
    return algorithm != null ? await stream.Hash(algorithm, cancellation) : throw new InvalidOperationException("Unsupported hash algorithm");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string HashSha512(this string text)
  {
    using var algorithm = HashAlgorithm.Create("SHA512");
    return algorithm != null ? text.Hash(algorithm) : throw new InvalidOperationException("Unsupported hash algorithm");
  }
}