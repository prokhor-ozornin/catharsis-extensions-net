﻿using Catharsis.Commons;
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

    foreach (var bytes in new[] { [], Attributes.RandomBytes() })
    {
      using var content = new ByteArrayContent(bytes);

      content.ToBytes().Should().NotBeNull().And.NotBeSameAs(content.ToBytes()).And.Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }

    return;

    static void Validate(byte[] result, HttpContent content)
    {
      using (content)
      {

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

    foreach (var bytes in new[] { [], Attributes.RandomBytes() })
    {
      using var content = new ByteArrayContent(bytes);

      content.ToBytesAsync().Should().NotBeNull().And.NotBeSameAs(content.ToBytesAsync());
      content.ToBytesAsync().ToArray().Should().Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }

    return;

    static void Validate(byte[] result, HttpContent content)
    {
      using (content)
      {

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

    foreach (var text in new[] { string.Empty, Attributes.RandomString() })
    {
      using var content = new StringContent(text);

      content.ToText().Should().NotBeNull().And.NotBeSameAs(content.ToText()).And.Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }

    return;

    static void Validate(string result, HttpContent content)
    {
      using (content)
      {

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

    foreach (var text in new[] { string.Empty, Attributes.RandomString() })
    {
      using var content = new StringContent(text);

      AssertionExtensions.Should(() => content.ToTextAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      content.ToTextAsync().Should().NotBeNull().And.NotBeSameAs(content.ToTextAsync());
      content.ToTextAsync().Await().Should().Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }

    return;

    static void Validate(string result, HttpContent content)
    {
      using (content)
      {

      }
    }
  }
}