using System.Security.Cryptography;
using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for cryptographic functions and types.</para>
/// </summary>
/// <seealso cref="SymmetricAlgorithm"/>
/// <seealso cref="HashAlgorithm"/>
public static class CryptographyExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes)
  {
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var encryptor = algorithm.CreateEncryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(encryptor.InputBlockSize))
    {
      var block = encryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      stream.WriteBytes(block);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para>Encrypts binary data, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
  /// <param name="bytes">Binary data to be encrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Encrypted binary data.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> EncryptAsync(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    using var encryptor = algorithm.CreateEncryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(encryptor.InputBlockSize))
    {
      var block = encryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      await stream.WriteBytesAsync(block, cancellation).ConfigureAwait(false);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this SymmetricAlgorithm algorithm, Stream stream) => algorithm.Encrypt(stream.ToEnumerable());

  /// <summary>
  ///   <para>Encrypts binary data from a <see cref="Stream"/>, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm to be used for encryption.</param>
  /// <param name="stream">Stream of binary data to be encrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Encrypted binary data.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> EncryptAsync(this SymmetricAlgorithm algorithm, Stream stream, CancellationToken cancellation = default) => await algorithm.EncryptAsync(stream.ToEnumerable(), cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Encrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> EncryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this Stream stream, SymmetricAlgorithm algorithm) => algorithm.Encrypt(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> EncryptAsync(this Stream stream, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.EncryptAsync(stream, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes)
  {
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var decryptor = algorithm.CreateDecryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(decryptor.InputBlockSize))
    {
      var block = decryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      stream.WriteBytes(block);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para>Decrypts encrypted binary data, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
  /// <param name="bytes">Binary data to be decrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Decrypted binary data.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this SymmetricAlgorithm algorithm, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    using var decryptor = algorithm.CreateDecryptor();
    using var stream = new MemoryStream();

    foreach (var chunk in bytes.Chunk(decryptor.InputBlockSize))
    {
      var block = decryptor.TransformFinalBlock(chunk, 0, chunk.Length);
      await stream.WriteBytesAsync(block, cancellation).ConfigureAwait(false);
    }

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this SymmetricAlgorithm algorithm, Stream stream) => algorithm.Decrypt(stream.ToEnumerable());

  /// <summary>
  ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
  /// <param name="stream">Stream of binary data to be decrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Decrypted binary data.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this SymmetricAlgorithm algorithm, Stream stream, CancellationToken cancellation = default) => await algorithm.DecryptAsync(stream.ToEnumerable(), cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm) => algorithm.Decrypt(bytes);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this IEnumerable<byte> bytes, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(bytes, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this Stream stream, SymmetricAlgorithm algorithm) => algorithm.Decrypt(stream);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this Stream stream, SymmetricAlgorithm algorithm, CancellationToken cancellation = default) => await algorithm.DecryptAsync(stream, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Hash(this IEnumerable<byte> bytes, HashAlgorithm algorithm)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    return algorithm.ComputeHash(bytes.AsArray());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string Hash(this string text, HashAlgorithm algorithm) => text.ToBytes(Encoding.UTF8).Hash(algorithm).ToHex();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Hash(this Stream stream, HashAlgorithm algorithm)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    return algorithm.ComputeHash(stream);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="algorithm"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> HashAsync(this Stream stream, HashAlgorithm algorithm, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));
    if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));

    #if NET7_0_OR_GREATER
      return await algorithm.ComputeHashAsync(stream, cancellation).ConfigureAwait(false);
    #else
      return algorithm.ComputeHash(stream);
    #endif
  }

  /// <summary>
  ///   <para>Computes hash digest for the given sequence of bytes, using <c>MD5</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashMd5(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = HashAlgorithm.Create("MD5");

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashMd5(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = HashAlgorithm.Create("MD5");

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashMd5(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var algorithm = HashAlgorithm.Create("MD5");

    return stream.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task<byte[]> HashMd5Async(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var algorithm = HashAlgorithm.Create("MD5");

    return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA1</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha1(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = HashAlgorithm.Create("SHA1");

    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha1(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = HashAlgorithm.Create("SHA1");

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha1(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var algorithm = HashAlgorithm.Create("SHA1");

    return stream.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task<byte[]> HashSha1Async(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var algorithm = HashAlgorithm.Create("SHA1");

    return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA256</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha256(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = HashAlgorithm.Create("SHA256");
    
    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha256(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = HashAlgorithm.Create("SHA256");
    
    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha256(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var algorithm = HashAlgorithm.Create("SHA256");
    
    return stream.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task<byte[]> HashSha256Async(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var algorithm = HashAlgorithm.Create("SHA256");
    
    return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha384(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = HashAlgorithm.Create("SHA384");
    
    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha384(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = HashAlgorithm.Create("SHA384");
    
    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha384(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var algorithm = HashAlgorithm.Create("SHA384");
    
    return stream.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task<byte[]> HashSha384Async(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var algorithm = HashAlgorithm.Create("SHA384");
    
    return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para>Computes hash digest for the given array of bytes, using <c>SHA512</c> algorithm.</para>
  /// </summary>
  /// <param name="bytes">Source bytes sequence.</param>
  /// <returns>Hash digest value.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha512(this IEnumerable<byte> bytes)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    using var algorithm = HashAlgorithm.Create("SHA512");
    
    return bytes.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha512(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = HashAlgorithm.Create("SHA512");
    
    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static byte[] HashSha512(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var algorithm = HashAlgorithm.Create("SHA512");
    
    return stream.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task<byte[]> HashSha512Async(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var algorithm = HashAlgorithm.Create("SHA512");
    
    return await stream.HashAsync(algorithm, cancellation).ConfigureAwait(false);
  }
}