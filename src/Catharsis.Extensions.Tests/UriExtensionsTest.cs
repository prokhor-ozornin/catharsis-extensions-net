using System.Text;
using Catharsis.Commons;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="UriExtensions"/>.</para>
/// </summary>
public sealed class UriExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Clone(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<Uri>().And.NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.OriginalString.Should().Be(original.OriginalString);
      clone.AbsoluteUri.Should().Be(original.AbsoluteUri);
      clone.AbsolutePath.Should().Be(original.AbsolutePath);
      clone.Scheme.Should().Be(original.Scheme);
      clone.UserInfo.Should().Be(original.UserInfo);
      clone.Authority.Should().Be(original.Authority);
      clone.Host.Should().Be(original.Host);
      clone.Port.Should().Be(original.Port);
      clone.LocalPath.Should().Be(original.LocalPath);
      clone.PathAndQuery.Should().Be(original.PathAndQuery);
      clone.Fragment.Should().Be(original.Fragment);
      clone.Segments.Should().Equal(original.Segments);
      clone.IsAbsoluteUri.Should().Be(original.IsAbsoluteUri);
      clone.IsDefaultPort.Should().Be(original.IsDefaultPort);
      clone.IsFile.Should().Be(original.IsFile);
      clone.IsLoopback.Should().Be(original.IsLoopback);
      clone.IsUnc.Should().Be(original.IsUnc);
      clone.UserEscaped.Should().Be(original.UserEscaped);
      clone.HostNameType.Should().Be(original.HostNameType);
      clone.IdnHost.Should().Be(original.IdnHost);
      clone.DnsSafeHost.Should().Be(original.DnsSafeHost);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailable(Uri, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.IsAvailable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, Uri uri) => uri.IsAvailable().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailableAsync(Uri, TimeSpan?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.IsAvailableAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().IsAvailableAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, Uri uri)
    {
      var task = uri.IsAvailableAsync();
      task.Should().BeAssignableTo<Task<bool>>();
      task.Await().Should().Be(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.GetQuery(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetQuery_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).GetQuery()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri, params (string Key, string Value)[] result) => uri.GetQuery().ToValueTuple().Should().Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.GetHost(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHost_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.GetHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Uri uri) => uri.GetHost().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Lines(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string[] result, Uri uri, Encoding encoding = null) => uri.Lines(encoding).Should().BeOfType<string[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.LinesAsync(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string[] result, Uri uri, Encoding encoding = null) => uri.LinesAsync(encoding).ToArray().Should().BeOfType<string[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.TryFinallyDelete(Uri, Action{Uri}, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDelete_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.TryFinallyDelete(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
      AssertionExtensions.Should(() => Attributes.LocalHost().TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriExtensions.ToEnumerable(Uri, TimeSpan?, ValueTuple{string, object}[])"/></description></item>
  ///     <item><description><see cref="UriExtensions.ToEnumerable(Uri, int, TimeSpan?, ValueTuple{string, object}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToEnumerable(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

      static void Validate(Uri uri)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToEnumerable(null, 1)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().ToEnumerable(0)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate(Uri uri)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriExtensions.ToAsyncEnumerable(Uri, TimeSpan?, ValueTuple{string, object}[])"/></description></item>
  ///     <item><description><see cref="UriExtensions.ToAsyncEnumerable(Uri, int, TimeSpan?, ValueTuple{string, object}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

      static void Validate(Uri uri)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null, 1).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().ToAsyncEnumerable(0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate(Uri uri)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToUriBuilder(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToUriBuilder_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToUriBuilder(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      Validate("https://user:password@localhost:8080/path?name=value#id".ToUri());
    }

    return;

    static void Validate(Uri uri)
    {
      var builder = uri.ToUriBuilder();

      builder.Should().BeOfType<UriBuilder>();
      builder.Uri.Should().Be(uri);
      builder.Fragment.Should().Be(uri.Fragment);
      builder.Host.Should().Be(uri.Host);
      builder.Path.Should().Be(uri.LocalPath);
      builder.Port.Should().Be(uri.Port);
      builder.Query.Should().Be(uri.Query);
      builder.Scheme.Should().Be(uri.Scheme);
      uri.UserInfo.Should().Be($"{builder.UserName}:{builder.Password}");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToStream(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToStream(null, null, ("name", "value"))).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToStreamAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToMailMessage(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToMailMessage_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToMailMessage(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytes(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, Uri uri) => uri.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytesAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToBytesAsync(null).ToArrayAsync()).ThrowAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, Uri uri) => uri.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToText(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Uri uri, Encoding encoding = null) => uri.ToText(encoding).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToTextAsync(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Uri uri, Encoding encoding = null)
    {
      var task = uri.ToTextAsync(encoding);
      task.Should().BeAssignableTo<Task<string>>();
      task.Await().Should().BeOfType<string>().And.Be(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlReader(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlReaderAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReaderAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDictionaryReader(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDictionaryReaderAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReaderAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDocument(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDocumentAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocumentAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXmlDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXDocument(Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXDocumentAsync(Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().ToXDocumentAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytes(Uri, IEnumerable{byte}, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytesAsync(Uri, IEnumerable{byte}, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteBytesAsync(Enumerable.Empty<byte>(), null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteText(Uri, string, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri, string text, Encoding encoding = null)
    {
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteTextAsync(Uri, string, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Attributes.LocalHost().WriteTextAsync(string.Empty, null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri, string text, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsDataContract{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsDataContractAsync{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContractAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContractAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsXml{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsXmlAsync{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXmlAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXmlAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Uri uri)
    {
    }
  }
}