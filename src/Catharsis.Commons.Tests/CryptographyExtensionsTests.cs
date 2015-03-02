using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="CryptographyExtensions"/>.</para>
  /// </summary>
  public sealed class CryptographyExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, byte[])"/></description></item>
    ///     <item><description><see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, Stream, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Decrypt_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.Encrypt(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt((byte[])null));
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.Encrypt(null, Stream.Null));
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
    ///     <item><description><see cref="CryptographyExtensions.Encrypt(SymmetricAlgorithm, byte[])"/></description></item>
    ///     <item><description><see cref="CryptographyExtensions.Encrypt(SymmetricAlgorithm, Stream, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Encrypt_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.Encrypt(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Aes.Create().Encrypt((byte[]) null));
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.Encrypt(null, Stream.Null));
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
  
    /// <summary>
    ///   <para>Performs testing of <see cref="CryptographyExtensions.MD5(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void MD5_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.MD5(null));

      Assert.Equal(16, Enumerable.Empty<byte>().ToArray().MD5().Length);
      Assert.Equal(16, Guid.NewGuid().ToByteArray().MD5().Length);
      Assert.False(Guid.NewGuid().ToByteArray().MD5().SequenceEqual(Guid.NewGuid().ToByteArray().MD5()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CryptographyExtensions.Secure(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Secure_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.Secure(null));

      using (var value = string.Empty.Secure())
      {
        Assert.Equal(0, value.Length);
      }
      var text = Guid.NewGuid().ToString();
      using (var value = text.Secure())
      {
        Assert.Equal(text.Length, value.Length);
        Assert.False(ReferenceEquals(value.ToString(), text));
      }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CryptographyExtensions.SHA1(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA1_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.SHA1(null));

      Assert.Equal(20, Enumerable.Empty<byte>().ToArray().SHA1().Length);
      Assert.Equal(20, Guid.NewGuid().ToByteArray().SHA1().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA1().SequenceEqual(Guid.NewGuid().ToByteArray().SHA1()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CryptographyExtensions.SHA256(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA256_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.SHA256(null));

      Assert.Equal(32, Enumerable.Empty<byte>().ToArray().SHA256().Length);
      Assert.Equal(32, Guid.NewGuid().ToByteArray().SHA256().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA256().SequenceEqual(Guid.NewGuid().ToByteArray().SHA256()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CryptographyExtensions.SHA512(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA512_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CryptographyExtensions.SHA512(null));

      Assert.Equal(64, Enumerable.Empty<byte>().ToArray().SHA512().Length);
      Assert.Equal(64, Guid.NewGuid().ToByteArray().SHA512().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA512().SequenceEqual(Guid.NewGuid().ToByteArray().SHA512()));
    }
  }
}