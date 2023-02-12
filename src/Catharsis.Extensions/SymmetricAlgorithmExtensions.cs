using System.Security.Cryptography;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for cryptographic functions and types.</para>
/// </summary>
/// <seealso cref="SymmetricAlgorithm"/>
public static class SymmetricAlgorithmExtensions
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
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Encrypt(this SymmetricAlgorithm algorithm, Stream stream) => algorithm.Encrypt(stream.ToEnumerable());

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
  ///   <para></para>
  /// </summary>
  /// <param name="algorithm"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] Decrypt(this SymmetricAlgorithm algorithm, Stream stream) => algorithm.Decrypt(stream.ToEnumerable());

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
  ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
  /// </summary>
  /// <param name="algorithm">Symmetric algorithm which have been used for encryption previously and should be used for decryption now.</param>
  /// <param name="stream">Stream of binary data to be decrypted.</param>
  /// <param name="cancellation"></param>
  /// <returns>Decrypted binary data.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> DecryptAsync(this SymmetricAlgorithm algorithm, Stream stream, CancellationToken cancellation = default) => await algorithm.DecryptAsync(stream.ToEnumerable(), cancellation).ConfigureAwait(false);
}