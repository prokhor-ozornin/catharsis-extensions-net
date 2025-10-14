using System.Security.Cryptography;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SymmetricAlgorithmExtensions"/>.</para>
/// </summary>
/// <seealso cref="SymmetricAlgorithmExtensions"/>
public sealed class SymmetricAlgorithmExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Encrypt_Bytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Encrypt(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
      AssertionExtensions.Should(() => SymmetricAlgorithm.Encrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, byte[] bytes)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Encrypt_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Encrypt(null, System.IO.Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
      AssertionExtensions.Should(() => SymmetricAlgorithm.Encrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, Stream stream)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.EncryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void EncryptAsync_Bytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.EncryptAsync(null, [])).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.EncryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.EncryptAsync(Bytes)).ThrowExactlyAsync<TaskCanceledException>().Await();

      Test(SymmetricAlgorithm, Bytes);
    }

    return;

    static void Test(SymmetricAlgorithm algorithm, byte[] bytes)
    {
      using (algorithm)
      {
        var encrypted = algorithm.EncryptAsync(bytes).Await();
        encrypted.Should().BeOfType<byte[]>().And.NotBeSameAs(bytes).And.NotEqual(bytes);

        algorithm.Key = algorithm.Key;
        algorithm.IV = algorithm.IV;
        var task = algorithm.EncryptAsync(bytes);
        task.Should().BeAssignableTo<Task<byte[]>>();
        task.Await().Should().BeOfType<byte[]>().And.Equal(encrypted);

        algorithm.Key = algorithm.Key;
        task = algorithm.EncryptAsync(bytes);
        task.Await().Should().BeOfType<byte[]>().And.NotEqual(encrypted);

        algorithm.IV = algorithm.IV;
        task = algorithm.EncryptAsync(bytes);
        task.Should().BeAssignableTo<Task<byte>>();
        task.Await().Should().BeOfType<byte[]>().And.NotEqual(encrypted);

        task = algorithm.EncryptAsync(bytes);
        task.Should().BeAssignableTo<Task<byte>>();
        task.Await().Should().BeOfType<byte[]>().And.NotEqual(encrypted);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.EncryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void EncryptAsync_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.EncryptAsync(null, System.IO.Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.EncryptAsync((Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.EncryptAsync(Stream)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, Stream stream)
    {
      /*using (var stream = new MemoryStream(bytes))
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
      }*/
    }
  }


  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Decrypt_Bytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Decrypt(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
      AssertionExtensions.Should(() => SymmetricAlgorithm.Decrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, byte[] bytes)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decrypt_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Decrypt(null, System.IO.Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
      AssertionExtensions.Should(() => SymmetricAlgorithm.Decrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, Stream stream)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.DecryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecryptAsync_Bytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.DecryptAsync(null, [])).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.DecryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.DecryptAsync(Bytes)).ThrowExactlyAsync<TaskCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, byte[] bytes)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.DecryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecryptAsync_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.DecryptAsync(null, System.IO.Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
      AssertionExtensions.Should(() => SymmetricAlgorithm.DecryptAsync((Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Test(SymmetricAlgorithm algorithm, Stream stream)
    {
      using (algorithm)
      {

      }
    }
  }
}