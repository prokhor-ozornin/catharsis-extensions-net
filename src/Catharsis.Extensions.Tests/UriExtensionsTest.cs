using System.Text;
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
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailable(Uri, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.IsAvailable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailableAsync(Uri, TimeSpan?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.IsAvailableAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => LocalHost.IsAvailableAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.GetQuery(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetQuery_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).GetQuery()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.GetHost(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHost_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.GetHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Lines(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.LinesAsync(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.TryFinallyDelete(Uri, Action{Uri}, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDelete_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.TryFinallyDelete(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    AssertionExtensions.Should(() => LocalHost.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriExtensions.ToEnumerable(Uri, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="UriExtensions.ToEnumerable(Uri, int, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToEnumerable(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToEnumerable(null, 1)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => LocalHost.ToEnumerable(0)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriExtensions.ToAsyncEnumerable(Uri, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="UriExtensions.ToAsyncEnumerable(Uri, int, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null, 1).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => LocalHost.ToAsyncEnumerable(0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToUriBuilder(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToUriBuilder_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToUriBuilder(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    var uri = "https://user:password@localhost:8080/path?name=value#id".ToUri();

    var builder = uri.ToUriBuilder();
    builder.Should().NotBeNull().And.NotBeSameAs(uri.ToUriBuilder());
    builder.Uri.Should().Be(uri);
    builder.Fragment.Should().Be(uri.Fragment);
    builder.Host.Should().Be(uri.Host);
    builder.Path.Should().Be(uri.LocalPath);
    builder.Port.Should().Be(uri.Port);
    builder.Query.Should().Be(uri.Query);
    builder.Scheme.Should().Be(uri.Scheme);
    $"{builder.UserName}:{builder.Password}".Should().Be(uri.UserInfo);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToStream(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToStream(null, null, ("name", "value"))).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToStreamAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToMailMessage(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToMailMessage_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToMailMessage(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytes(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytesAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToBytesAsync(null).ToArrayAsync()).ThrowAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToText(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToTextAsync(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlReader(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlReaderAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReaderAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDictionaryReader(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDictionaryReaderAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReaderAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDocument(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXmlDocumentAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXDocument(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToXDocumentAsync(Uri, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => LocalHost.ToXDocumentAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytes(Uri, IEnumerable{byte}, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => LocalHost.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytesAsync(Uri, IEnumerable{byte}, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => LocalHost.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => LocalHost.WriteBytesAsync(Enumerable.Empty<byte>(), null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteText(Uri, string, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => LocalHost.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteTextAsync(Uri, string, Encoding, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => LocalHost.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => LocalHost.WriteTextAsync(string.Empty, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsDataContract{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsDataContractAsync{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContractAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContractAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsXml{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.DeserializeAsXmlAsync{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXmlAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXmlAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }
}