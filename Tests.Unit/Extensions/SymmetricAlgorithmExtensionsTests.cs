using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Catharsis.Commons.Extensions
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
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(Aes.Create(), (byte[])null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(Aes.Create(), null, false));

      var bytes = Guid.NewGuid().ToByteArray();
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes);
      Assert.True(algorithm.Decrypt(encrypted).SequenceEqual(bytes));
      Aes.Create().With(decryptor =>
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        Assert.True(decryptor.Decrypt(encrypted).SequenceEqual(bytes));
      });
      Aes.Create().With(decryptor =>
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        Assert.False(decryptor.Decrypt(encrypted).SequenceEqual(bytes));
      });

      new MemoryStream(encrypted).With(stream =>
      {
        Assert.True(algorithm.Decrypt(stream).SequenceEqual(bytes));
        Assert.True(stream.ReadByte() == -1);
      });
      new MemoryStream(encrypted).With(stream =>
      {
        Assert.True(algorithm.Decrypt(stream, true).SequenceEqual(bytes));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
      Aes.Create().With(decryptor =>
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        Assert.True(decryptor.Decrypt(new MemoryStream(encrypted), true).SequenceEqual(bytes));
      });
      Aes.Create().With(decryptor =>
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        Assert.False(decryptor.Decrypt(new MemoryStream(encrypted), true).SequenceEqual(bytes));
      });
      
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
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(Aes.Create(), (byte[]) null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => SymmetricAlgorithmExtensions.Encrypt(Aes.Create(), null, false));

      var bytes = Guid.NewGuid().ToByteArray();
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes);
      Assert.False(encrypted.SequenceEqual(bytes));
      Aes.Create().With(encryptor => 
      {
        encryptor.Key = algorithm.Key;
        encryptor.IV = algorithm.IV;
        Assert.True(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      });
      Aes.Create().With(encryptor =>
      {
        encryptor.Key = algorithm.Key;
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      });
      Aes.Create().With(encryptor =>
      {
        encryptor.IV = algorithm.IV;
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      });
      Aes.Create().With(encryptor =>
      {
        Assert.False(encryptor.Encrypt(bytes).SequenceEqual(encrypted));
      });

      new MemoryStream(bytes).With(stream =>
      {
        Assert.True(algorithm.Encrypt(stream).SequenceEqual(encrypted));
        Assert.True(stream.ReadByte() == -1);
      });
      new MemoryStream(bytes).With(stream =>
      {
        Assert.True(algorithm.Encrypt(stream, true).SequenceEqual(encrypted));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
      new MemoryStream(bytes).With(stream =>
      {
        Assert.True(algorithm.Encrypt(stream).SequenceEqual(encrypted));
        Assert.True(stream.ReadByte() == -1);
      });
      Aes.Create().With(encryptor =>
      {
        encryptor.Key = algorithm.Key;
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      });
      Aes.Create().With(encryptor =>
      {
        encryptor.IV = algorithm.IV;
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      });
      Aes.Create().With(encryptor =>
      {
        Assert.False(encryptor.Encrypt(new MemoryStream(bytes), true).SequenceEqual(encrypted));
      });

      algorithm.Clear();
    }
  }
}