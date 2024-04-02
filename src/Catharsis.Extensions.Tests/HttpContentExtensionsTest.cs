using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="HttpContentExtensions"/>.</para>
/// </summary>
public sealed class HttpContentExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToStream(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    AssertionExtensions.Should(() => HttpContentExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    throw new NotImplementedException();

    return;

    static void Validate(HttpContent content)
    {
      using (content)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToStreamAsync(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => HttpContentExtensions.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();
    AssertionExtensions.Should(() => new StringContent(string.Empty).ToStreamAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();

    return;

    static void Validate(HttpContent content)
    {
      using (content)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToBytes(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    new[] { [], Attributes.RandomBytes() }.ForEach(bytes =>
    {
      Validate(bytes, new ByteArrayContent(bytes));
    });

    return;

    static void Validate(IEnumerable<byte> result, HttpContent content)
    {
      using (content)
      {
        content.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToBytesAsync(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();

    new[] { [], Attributes.RandomBytes() }.ForEach(bytes =>
    {
      Validate(bytes, new ByteArrayContent(bytes));
    });

    return;

    static void Validate(IEnumerable<byte> result, HttpContent content)
    {
      using (content)
      {
        var task = content.ToBytesAsync();
        task.Should().BeOfType<IAsyncEnumerable<byte[]>>();
        task.ToArray().Should().BeOfType<byte[]>().And.Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToText(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    new[] { string.Empty, Attributes.RandomString() }.ForEach(text =>
    {
      Validate(text, new StringContent(text));
    });

    return;

    static void Validate(string result, HttpContent content)
    {
      using (content)
      {
        content.ToText().Should().BeOfType<string>().And.Be(content.ReadAsStringAsync().Await()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpContentExtensions.ToTextAsync(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();
    AssertionExtensions.Should(() => new StringContent(string.Empty).ToTextAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    new[] { string.Empty, Attributes.RandomString() }.ForEach(text =>
    {
      Validate(text, new StringContent(text));
    });

    return;

    static void Validate(string result, HttpContent content)
    {
      using (content)
      {
        var task = content.ToTextAsync();
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>().And.Be(content.ReadAsStringAsync().Await()).And.Be(result);
      }
    }
  }
}