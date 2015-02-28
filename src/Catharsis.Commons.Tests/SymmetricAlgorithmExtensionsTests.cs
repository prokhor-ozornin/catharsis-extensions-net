using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SymmetricAlgorithmExtensions"/>.</para>
  /// </summary>
  public sealed class SymmetricAlgorithmExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, byte[])"/></description></item>
    ///     <item><description><see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, Stream, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Decrypt_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt((byte[])null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt(null, false));

      var bytes = Guid.NewGuid().ToByteArray();
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes);
      Assert.True(algorithm.Decrypt(encrypted).SequenceEqual(bytes));
      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        Assert.True(decryptor.Decrypt(encrypted).SequenceEqual(bytes));
      }
      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        Assert.False(decryptor.Decrypt(encrypted).SequenceEqual(bytes));
      }

      using (var stream = new MemoryStream(encrypted))
      {
        Assert.True(algorithm.Decrypt(stream).SequenceEqual(bytes));
        Assert.Equal(-1, stream.ReadByte());
      }
      using (var stream = new MemoryStream(encrypted))
      {
        Assert.True(algorithm.Decrypt(stream, true).SequenceEqual(bytes));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }
      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        Assert.True(decryptor.Decrypt(new MemoryStream(encrypted), true).SequenceEqual(bytes));
      }
      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        Assert.False(decryptor.Decrypt(new MemoryStream(encrypted), true).SequenceEqual(bytes));
      }
      
      algorithm.Clear();
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, byte[])"/></description></item>
    ///     <item><description><see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, Stream, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Encrypt_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt((byte[]) null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt(null, false));

      var bytes = Guid.NewGuid().ToByteArray();
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes);
      Assert.False(encrypted.SequenceEqual(bytes));
      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        encryptor.IV = algorithm.IV;
        Assert.True(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      }
      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      }
      using (var encryptor = Aes.Create())
      {
        encryptor.IV = algorithm.IV;
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      }
      using (var encryptor = Aes.Create())
      {
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      }

      using (var stream = new MemoryStream(bytes))
      {
        Assert.True(algorithm.Encrypt(stream).SequenceEqual(encrypted));
        Assert.Equal(-1, stream.ReadByte());
      }
      using (var stream = new MemoryStream(bytes))
      {
        Assert.True(algorithm.Encrypt(stream, true).SequenceEqual(encrypted));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      }
      using (var stream = new MemoryStream(bytes))
      {
        Assert.True(algorithm.Encrypt(stream).SequenceEqual(encrypted));
        Assert.Equal(-1, stream.ReadByte());
      }
      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      }
      using (var encryptor = Aes.Create())
      {
        encryptor.IV = algorithm.IV;
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      }
      using (var encryptor = Aes.Create())
      {
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      }

      algorithm.Clear();
    }
  }
}