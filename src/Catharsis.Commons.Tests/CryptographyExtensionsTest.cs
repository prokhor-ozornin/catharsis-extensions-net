using System.Security;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CryptographyExtensions"/>.</para>
/// </summary>
public sealed class CryptographyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_IsEmpty_Method()
  {
    //AssertionExtensions.Should(() => CryptographyExtensions.IsEmpty(null!)).ThrowExactly<ArgumentNullException>();

    using var secure = EmptySecureString;

    secure.IsEmpty().Should().BeTrue();

    secure.AppendChar(char.MinValue);
    secure.IsEmpty().Should().BeFalse();

    secure.RemoveAt(0);
    secure.IsEmpty().Should().BeTrue();

    secure.AppendChar(char.MinValue);
    secure.Clear();
    secure.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Empty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Empty_Method()
  {
    void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.Empty().Should().NotBeNull().And.BeSameAs(secure);
        secure.Length.Should().Be(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => CryptographyExtensions.Empty(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString);
      Validate(RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Min(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Min_Method()
  {
    void Validate(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Min(min).Should().NotBeNull().And.BeSameAs(min);
          max.Min(max).Should().NotBeNull().And.BeSameAs(max);
          min.Min(max).Should().NotBeNull().And.BeSameAs(min);
        }
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => CryptographyExtensions.Min(null!, new SecureString())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => new SecureString().Min(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString, EmptySecureString);
      Validate(EmptySecureString, RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Max(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Max_Method()
  {
    void Validate(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Max(min).Should().NotBeNull().And.BeSameAs(min);
          max.Max(max).Should().NotBeNull().And.BeSameAs(max);
          max.Max(min).Should().NotBeNull().And.BeSameAs(max);
        }
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => CryptographyExtensions.Max(null!, new SecureString())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => new SecureString().Max(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString, EmptySecureString);
      Validate(EmptySecureString, RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Bytes(SecureString, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Bytes_Method()
  {
    void Validate(Encoding? encoding = null)
    {
      using (var secure = EmptySecureString)
      {
        secure.Bytes(encoding).Should().BeSameAs(secure.Bytes(encoding)).And.BeEmpty();
      }

      using (var secure = RandomSecureString)
      {
        var text = secure.Text();
        secure.Bytes(encoding).Should().NotBeNull().And.NotBeSameAs(secure.Bytes(encoding)).And.Equal(text.Bytes(encoding));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CryptographyExtensions.Text(null!));

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Text(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Text_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.Text(null!));

    using (var secure = new SecureString())
    {
      secure.Text().Should().BeSameAs(secure.Text()).And.BeEmpty();
    }

    using (var secure = new SecureString())
    {
      var text = RandomString;

      text.ForEach(secure.AppendChar);
      secure.Text().Should().NotBeNull().And.NotBeSameAs(secure.Text()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.UseTemporarily(SecureString, Action{SecureString})"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => CryptographyExtensions.UseTemporarily(null!, _ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new SecureString().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    using var secure = new SecureString();

    secure.UseTemporarily(secure => secure.AppendChar(char.MinValue)).Should().NotBeNull().And.BeSameAs(secure);
    secure.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CryptographyExtensions.Encrypt(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="CryptographyExtensions.Encrypt(SymmetricAlgorithm, Stream, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Encrypt_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CryptographyExtensions.Encrypt(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Aes.Create().Encrypt((IEnumerable<byte>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

      var bytes = RandomBytes;
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes).Await();
      encrypted.Should().NotEqual(bytes);

      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        encryptor.IV = algorithm.IV;
        encryptor.Encrypt(bytes).Await().Should().Equal(encrypted);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        encryptor.Encrypt(bytes).Await().Should().NotEqual(encrypted);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.IV = algorithm.IV;
        encryptor.Encrypt(bytes).Await().Should().NotEqual(encrypted);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.Encrypt(bytes).Await().Should().NotEqual(encrypted);
      }

      using (var stream = new MemoryStream(bytes))
      {
        algorithm.Encrypt(stream).Await().Should().Equal(encrypted);
        stream.ReadByte().Should().Be(-1);
      }

      using (var stream = new MemoryStream(bytes))
      {
        algorithm.Encrypt(stream).Await().Should().Equal(encrypted);
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }

      using (var stream = new MemoryStream(bytes))
      {
        algorithm.Encrypt(stream).Await().Should().Equal(encrypted);
        stream.ReadByte().Should().Be(-1);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.Key = algorithm.Key;
        encryptor.Encrypt(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.IV = algorithm.IV;
        encryptor.Encrypt(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
      }

      using (var encryptor = Aes.Create())
      {
        encryptor.Encrypt(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
      }

      algorithm.Clear();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CryptographyExtensions.Encrypt(null!, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Aes.Create().Encrypt((Stream) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Encrypt_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).Encrypt(Aes.Create())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().Encrypt(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(Stream, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Encrypt_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Encrypt(Aes.Create())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Encrypt(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public async void IEnumerable_Decrypt_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).Decrypt(Aes.Create())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Decrypt(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(Stream, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Decrypt_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Decrypt(Aes.Create())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Decrypt(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, Stream, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Decrypt_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CryptographyExtensions.Decrypt(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Aes.Create().Decrypt((IEnumerable<byte>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

      /*var bytes = RandomBytes;
      var algorithm = Aes.Create();

      var encrypted = algorithm.Encrypt(bytes);
      algorithm.Decrypt(encrypted).Should().Equal(bytes);

      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        decryptor.Decrypt(encrypted).Should().Equal(bytes);
      }

      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        decryptor.Decrypt(encrypted).Should().NotEqual(bytes);
      }

      using (var stream = new MemoryStream(encrypted))
      {
        algorithm.Decrypt(stream).Should().Equal(bytes);
        stream.ReadByte().Should().Be(-1);
      }

      using (var stream = new MemoryStream(encrypted))
      {
        algorithm.Decrypt(stream).Should().Equal(bytes);
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }

      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.IV = algorithm.IV;
        decryptor.Decrypt(new MemoryStream(encrypted)).Should().Equal(bytes);
      }

      using (var decryptor = Aes.Create())
      {
        decryptor.Key = algorithm.Key;
        decryptor.GenerateIV();
        decryptor.Decrypt(new MemoryStream(encrypted)).Should().NotEqual(bytes);
      }

      algorithm.Clear();*/
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CryptographyExtensions.Decrypt(null!, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Aes.Create().Decrypt((Stream) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(IEnumerable{byte}, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Hash_Method()
  {
    //AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Enumerable.Empty<byte>().Hash(null!)).ThrowExactly<ArgumentNullException>();

    var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    Enumerable.Empty<byte>().Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(Enumerable.Empty<byte>().Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(Array.Empty<byte>()));

    var bytes = RandomBytes;
    bytes.Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(bytes.Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(bytes));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(Stream, HashAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Hash_Method()
  {
    //AssertionExtensions.Should(() => ((Stream) null!).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Stream.Null.Hash(null!)).ThrowExactly<ArgumentNullException>();

    var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.Hash(algorithm, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.Hash(algorithm);
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().Hash(algorithm));
        hash.Await().Should().HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(string, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Hash_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => string.Empty.Hash(null!)).ThrowExactly<ArgumentNullException>();

    var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(text.Hash(algorithm)).And.HaveLength(algorithm.HashSize / 4).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).HashMd5()).ThrowExactly<ArgumentNullException>();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashMd5().Should().NotBeNull().And.NotBeSameAs(sequence.HashMd5()).And.HaveCount(16).And.Equal(HashAlgorithm.Create("MD5")!.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).HashMd5()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashMd5(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashMd5();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashMd5());
        hash.Await().Should().HaveCount(16).And.Equal(HashAlgorithm.Create("MD5")!.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashMd5_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).HashMd5()).ThrowExactly<ArgumentNullException>();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashMd5().Should().NotBeNull().And.NotBeSameAs(text.HashMd5()).And.HaveLength(32).And.Be(System.Convert.ToHexString(HashAlgorithm.Create("MD5")!.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).HashSha1()).ThrowExactly<ArgumentNullException>();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };
    
    foreach (var sequence in sequences)
    {
      sequence.HashSha1().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha1()).And.HaveCount(20).And.Equal(HashAlgorithm.Create("SHA1")!.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).HashSha1()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha1(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashSha1();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha1());
        hash.Await().Should().HaveCount(20).And.Equal(HashAlgorithm.Create("SHA1")!.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha1_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).HashSha1()).ThrowExactly<ArgumentNullException>();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha1().Should().NotBeNull().And.NotBeSameAs(text.HashSha1()).And.HaveLength(40).And.Be(System.Convert.ToHexString(HashAlgorithm.Create("SHA1")!.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).HashSha256()).ThrowExactly<ArgumentNullException>();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha256().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha256()).And.HaveCount(32).And.Equal(HashAlgorithm.Create("SHA256")!.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).HashSha256()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha256(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashSha256();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha256());
        hash.Await().Should().HaveCount(32).And.Equal(HashAlgorithm.Create("SHA256")!.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha256_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).HashSha256()).ThrowExactly<ArgumentNullException>();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha256().Should().NotBeNull().And.NotBeSameAs(text.HashSha256()).And.HaveLength(64).And.Be(System.Convert.ToHexString(HashAlgorithm.Create("SHA256")!.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).HashSha384()).ThrowExactly<ArgumentNullException>();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha384().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha384()).And.HaveCount(48).And.Equal(HashAlgorithm.Create("SHA384")!.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).HashSha384()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha384(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashSha384();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha384());
        hash.Await().Should().HaveCount(48).And.Equal(HashAlgorithm.Create("SHA384")!.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha384_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).HashSha384()).ThrowExactly<ArgumentNullException>();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha384().Should().NotBeNull().And.NotBeSameAs(text.HashSha384()).And.HaveLength(96).And.Be(System.Convert.ToHexString(HashAlgorithm.Create("SHA384")!.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).HashSha512()).ThrowExactly<ArgumentNullException>();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha512().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha512()).And.HaveCount(64).And.Equal(HashAlgorithm.Create("SHA512")!.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512_Method()
  {
    //AssertionExtensions.Should(() => ((Stream) null!).HashSha512()).ThrowExactly<ArgumentNullException>();

    foreach (var stream in new[] {Stream.Null, RandomStream})
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha512(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashSha512();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha512());
        hash.Await().Should().HaveCount(64).And.Equal(HashAlgorithm.Create("SHA512")!.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha512_Method()
  {
    //AssertionExtensions.Should(() => ((string) null!).HashSha512()).ThrowExactly<ArgumentNullException>();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha512().Should().NotBeNull().And.NotBeSameAs(text.HashSha512()).And.HaveLength(128).And.Be(System.Convert.ToHexString(HashAlgorithm.Create("SHA512")!.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }
}