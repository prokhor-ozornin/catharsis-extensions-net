using System.Security.Cryptography;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for cryptographic functions and types.</para>
/// </summary>
/// <seealso cref="SymmetricAlgorithm"/>
public static class SymmetricAlgorithmExtensions
{
  /// <param name="algorithm"></param>
  extension(SymmetricAlgorithm algorithm)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="EncryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/>
    public byte[] Encrypt(IEnumerable<byte> source)
    {
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
      if (source is null) throw new ArgumentNullException(nameof(source));

      using var encryptor = algorithm.CreateEncryptor();
      using var stream = new MemoryStream();

      foreach (var chunk in source.Chunk(encryptor.InputBlockSize))
      {
        var block = encryptor.TransformFinalBlock(chunk, 0, chunk.Length);
        stream.WriteBytes(block);
      }

      return stream.ToArray();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="EncryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/>
    public byte[] Encrypt(Stream source) => algorithm.Encrypt(source.ToEnumerable());

    /// <summary>
    ///   <para>Encrypts binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="source">Binary data to be encrypted.</param>
    /// <param name="cancellation"></param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, IEnumerable{byte})"/>
    public async Task<byte[]> EncryptAsync(IEnumerable<byte> source, CancellationToken cancellation = default)
    {
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
      if (source is null) throw new ArgumentNullException(nameof(source));

      cancellation.ThrowIfCancellationRequested();

      using var encryptor = algorithm.CreateEncryptor();
      using var stream = new MemoryStream();

      foreach (var chunk in source.Chunk(encryptor.InputBlockSize))
      {
        var block = encryptor.TransformFinalBlock(chunk, 0, chunk.Length);
        await stream.WriteBytesAsync(block, cancellation).ConfigureAwait(false);
      }

      return stream.ToArray();
    }

    /// <summary>
    ///   <para>Encrypts binary data from a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="source">Stream of binary data to be encrypted.</param>
    /// <param name="cancellation"></param>
    /// <returns>Encrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Encrypt(SymmetricAlgorithm, Stream)"/>
    public async Task<byte[]> EncryptAsync(Stream source, CancellationToken cancellation = default) => await algorithm.EncryptAsync(source.ToEnumerable(), cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/>
    public byte[] Decrypt(IEnumerable<byte> source)
    {
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
      if (source is null) throw new ArgumentNullException(nameof(source));

      using var decryptor = algorithm.CreateDecryptor();
      using var stream = new MemoryStream();

      foreach (var chunk in source.Chunk(decryptor.InputBlockSize))
      {
        var block = decryptor.TransformFinalBlock(chunk, 0, chunk.Length);
        stream.WriteBytes(block);
      }

      return stream.ToArray();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/>
    public byte[] Decrypt(Stream source) => algorithm.Decrypt(source.ToEnumerable());

    /// <summary>
    ///   <para>Decrypts encrypted binary data, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="source">Binary data to be decrypted.</param>
    /// <param name="cancellation"></param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Decrypt(SymmetricAlgorithm, IEnumerable{byte})"/>
    public async Task<byte[]> DecryptAsync(IEnumerable<byte> source, CancellationToken cancellation = default)
    {
      if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
      if (source is null) throw new ArgumentNullException(nameof(source));

      cancellation.ThrowIfCancellationRequested();

      using var decryptor = algorithm.CreateDecryptor();
      using var stream = new MemoryStream();

      foreach (var chunk in source.Chunk(decryptor.InputBlockSize))
      {
        var block = decryptor.TransformFinalBlock(chunk, 0, chunk.Length);
        await stream.WriteBytesAsync(block, cancellation).ConfigureAwait(false);
      }

      return stream.ToArray();
    }

    /// <summary>
    ///   <para>Decrypts encrypted contents of a <see cref="Stream"/>, using specified symmetric algorithm.</para>
    /// </summary>
    /// <param name="source">Stream of binary data to be decrypted.</param>
    /// <param name="cancellation"></param>
    /// <returns>Decrypted binary data.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="algorithm"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DecryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/>
    public async Task<byte[]> DecryptAsync(Stream source, CancellationToken cancellation = default) => await algorithm.DecryptAsync(source.ToEnumerable(), cancellation).ConfigureAwait(false);
  }
}