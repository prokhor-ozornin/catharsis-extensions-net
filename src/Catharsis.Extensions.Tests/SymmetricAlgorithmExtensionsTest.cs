using System.Security.Cryptography;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SymmetricAlgorithmExtensions"/>.</para>
/// </summary>
public sealed class SymmetricAlgorithmExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Encrypt_Bytes_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Encrypt(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Encrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Encrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Encrypt_Stream_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Encrypt(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Encrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.EncryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void EncryptAsync_Bytes_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.EncryptAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.EncryptAsync(RandomBytes, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.EncryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void EncryptAsync_Stream_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.EncryptAsync(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
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
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Decrypt_Bytes_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Decrypt(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Decrypt((IEnumerable<byte>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.Decrypt(SymmetricAlgorithm, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decrypt_Stream_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.Decrypt(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    AssertionExtensions.Should(() => Algorithm.Decrypt((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.DecryptAsync(SymmetricAlgorithm, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecryptAsync_Bytes_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.DecryptAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync((IEnumerable<byte>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync(RandomBytes, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SymmetricAlgorithmExtensions.DecryptAsync(SymmetricAlgorithm, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecryptAsync_Stream_Method()
  {
    AssertionExtensions.Should(() => SymmetricAlgorithmExtensions.DecryptAsync(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
    AssertionExtensions.Should(() => Algorithm.DecryptAsync((Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }
}