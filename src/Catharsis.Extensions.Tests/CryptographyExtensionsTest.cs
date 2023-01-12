﻿using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CryptographyExtensions"/>.</para>
/// </summary>
public sealed class CryptographyExtensionsTest : UnitTest
{
  private SymmetricAlgorithm Algorithm { get; } = Aes.Create();

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Encrypt_Bytes_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.Encrypt(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Encrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.EncryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_EncryptAsync_Bytes_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.EncryptAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync(RandomBytes, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Encrypt_Stream_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.Encrypt(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Encrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.EncryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_EncryptAsync_Stream_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.EncryptAsync(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync((Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync(RandomStream, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    /*var bytes = RandomBytes;
    var algorithm = Algorithm;

    var encrypted = algorithm.EncryptAsync(bytes).Await();
    encrypted.Should().NotEqual(bytes);

    using (var encryptor = Algorithm)
    {
      encryptor.Key = algorithm.Key;
      encryptor.IV = algorithm.IV;
      encryptor.EncryptAsync(bytes).Await().Should().Equal(encrypted);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.Key = algorithm.Key;
      encryptor.EncryptAsync(bytes).Await().Should().NotEqual(encrypted);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.IV = algorithm.IV;
      encryptor.EncryptAsync(bytes).Await().Should().NotEqual(encrypted);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.EncryptAsync(bytes).Await().Should().NotEqual(encrypted);
    }

    using (var stream = new MemoryStream(bytes))
    {
      algorithm.EncryptAsync(stream).Await().Should().Equal(encrypted);
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(bytes))
    {
      algorithm.EncryptAsync(stream).Await().Should().Equal(encrypted);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (var stream = new MemoryStream(bytes))
    {
      algorithm.EncryptAsync(stream).Await().Should().Equal(encrypted);
      stream.ReadByte().Should().Be(-1);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.Key = algorithm.Key;
      encryptor.EncryptAsync(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.IV = algorithm.IV;
      encryptor.EncryptAsync(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
    }

    using (var encryptor = Algorithm)
    {
      encryptor.EncryptAsync(new MemoryStream(bytes)).Await().Should().NotEqual(encrypted);
    }

    algorithm.Clear();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Encrypt_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Encrypt(Algorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().Encrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.EncryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_EncryptAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).EncryptAsync(Algorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().EncryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().EncryptAsync(Algorithm, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Encrypt(Stream, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Encrypt_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).Encrypt(Algorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.Encrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.EncryptAsync(Stream, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_EncryptAsync_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).EncryptAsync(Algorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Stream.Null.EncryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Stream.Null.EncryptAsync(Algorithm, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Decrypt_Bytes_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.Decrypt(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Decrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.DecryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_DecryptAsync_Bytes_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.DecryptAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync(RandomBytes, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_Decrypt_Stream_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.Decrypt(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Decrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.DecryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void SymmetricAlgorithm_DecryptAsync_Stream_Method()
  {
    AssertionExtensions.Should(() => CryptographyExtensions.DecryptAsync(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync((Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(IEnumerable{byte}, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Decrypt_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Decrypt(Algorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().Decrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.DecryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_DecryptAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).DecryptAsync(Algorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Stream.Null.DecryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Stream.Null.DecryptAsync(Algorithm, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Decrypt(Stream, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Decrypt_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).Decrypt(Algorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.Decrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.DecryptAsync(Stream, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecryptAsync_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).DecryptAsync(Algorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Stream.Null.DecryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Stream.Null.DecryptAsync(Algorithm, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(IEnumerable{byte}, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Hash_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    Enumerable.Empty<byte>().Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(Enumerable.Empty<byte>().Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(Array.Empty<byte>()));

    var bytes = RandomBytes;
    bytes.Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(bytes.Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(bytes));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(string, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Hash_Method()
  {
    AssertionExtensions.Should(() => ((string) null).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.Hash(algorithm).Should().NotBeNull().And.NotBeSameAs(text.Hash(algorithm)).And.HaveLength(algorithm.HashSize / 4).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.Hash(Stream, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Hash_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).Hash(HashAlgorithm.Create("MD5")!)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.Hash(algorithm);
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashAsync(Stream, HashAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashAsync_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashAsync(HashAlgorithm.Create("MD5")!)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Stream.Null.HashAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashAsync(algorithm, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashAsync(algorithm);
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashAsync(algorithm));
        hash.Await().Should().HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashMd5().Should().NotBeNull().And.NotBeSameAs(sequence.HashMd5()).And.HaveCount(16).And.Equal(algorithm.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashMd5().Should().NotBeNull().And.NotBeSameAs(text.HashMd5()).And.HaveLength(32).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashMd5();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashMd5()).And.HaveCount(16).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashMd5Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashMd5Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = HashAlgorithm.Create("MD5")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashMd5Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashMd5Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashMd5Async());
        hash.Await().Should().HaveCount(16).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    using var algorithm = HashAlgorithm.Create("SHA1")!;
    algorithm.Should().NotBeNull();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };
    
    foreach (var sequence in sequences)
    {
      sequence.HashSha1().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha1()).And.HaveCount(20).And.Equal(algorithm.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = HashAlgorithm.Create("SHA1")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha1().Should().NotBeNull().And.NotBeSameAs(text.HashSha1()).And.HaveLength(40).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = HashAlgorithm.Create("SHA1")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha1();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha1()).And.HaveCount(20).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha1Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha1Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = HashAlgorithm.Create("SHA1")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha1Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha1Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha1Async());
        hash.Await().Should().HaveCount(20).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    using var algorithm = HashAlgorithm.Create("SHA256")!;
    algorithm.Should().NotBeNull();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha256().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha256()).And.HaveCount(32).And.Equal(algorithm.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = HashAlgorithm.Create("SHA256")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha256().Should().NotBeNull().And.NotBeSameAs(text.HashSha256()).And.HaveLength(64).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = HashAlgorithm.Create("SHA256")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha256();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha256()).And.HaveCount(32).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha256Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha256Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = HashAlgorithm.Create("SHA256")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha256Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha256Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha256Async());
        hash.Await().Should().HaveCount(32).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    using var algorithm = HashAlgorithm.Create("SHA384")!;
    algorithm.Should().NotBeNull();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha384().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha384()).And.HaveCount(48).And.Equal(algorithm.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = HashAlgorithm.Create("SHA384")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha384().Should().NotBeNull().And.NotBeSameAs(text.HashSha384()).And.HaveLength(96).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = HashAlgorithm.Create("SHA384")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha384();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha384()).And.HaveCount(48).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha384Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha384Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = HashAlgorithm.Create("SHA384")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha384Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha384Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha384Async());
        hash.Await().Should().HaveCount(48).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    using var algorithm = HashAlgorithm.Create("SHA512")!;
    algorithm.Should().NotBeNull();

    var sequences = new[] { Enumerable.Empty<byte>().ToArray(), RandomBytes };

    foreach (var sequence in sequences)
    {
      sequence.HashSha512().Should().NotBeNull().And.NotBeSameAs(sequence.HashSha512()).And.HaveCount(64).And.Equal(algorithm.ComputeHash(sequence));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = HashAlgorithm.Create("SHA512")!;
    algorithm.Should().NotBeNull();

    var texts = new[] { string.Empty, RandomString };

    foreach (var text in texts)
    {
      text.HashSha512().Should().NotBeNull().And.NotBeSameAs(text.HashSha512()).And.HaveLength(128).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = HashAlgorithm.Create("SHA512")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha512();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha512()).And.HaveCount(64).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CryptographyExtensions.HashSha512Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha512Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = HashAlgorithm.Create("SHA512")!;
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] {Stream.Null, RandomStream})
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha512Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha512Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha512Async());
        hash.Await().Should().HaveCount(64).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    Algorithm.Dispose();
  }
}