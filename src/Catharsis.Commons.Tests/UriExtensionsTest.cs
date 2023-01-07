using System.Text;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="UriExtensions"/>.</para>
/// </summary>
public sealed class UriExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailable(Uri, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_IsAvailable_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.IsAvailable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.IsAvailableAsync(Uri, TimeSpan?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.IsAvailableAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => LocalHost.IsAvailableAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Empty(UriBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void UriBuilder_Empty_Method()
  {
    static void Validate(UriBuilder builder)
    {
      builder.Empty().Should().NotBeNull().And.BeSameAs(builder);
      builder.Fragment.Should().BeEmpty();
      builder.Host.Should().BeEmpty();
      builder.Password.Should().BeEmpty();
      builder.Path.Should().Be("/");
      builder.Port.Should().Be(-1);
      builder.Query.Should().BeEmpty();
      builder.Scheme.Should().BeEmpty();
      builder.UserName.Should().BeEmpty();

      AssertionExtensions.Should(() => builder.Uri).ThrowExactly<UriFormatException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.Empty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new UriBuilder());
      Validate(new UriBuilder("https://user:password@192.168.0.1/path?query#id"));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.GetQuery(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_GetQuery_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).GetQuery()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriExtensions.WithQuery(UriBuilder, IReadOnlyDictionary{string, object})"/></description></item>
  ///     <item><description><see cref="UriExtensions.WithQuery(UriBuilder, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void UriBuilder_WithQuery_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WithQuery(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => new UriBuilder().WithQuery((IReadOnlyDictionary<string, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("parameters");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.WithQuery(null, Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => new UriBuilder().WithQuery(((string Name, object Value)[]) null)).ThrowExactly<ArgumentNullException>().WithParameterName("parameters");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Host(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_Host_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.Host(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Lines(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_Lines_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.LinesAsync(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_LinesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.LinesAsync(null).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.Print{T}(T, Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.Print<object>(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => UriExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.PrintAsync{T}(T, Uri, Encoding, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.PrintAsync<object>(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
    AssertionExtensions.Should(() => UriExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => new object().PrintAsync(LocalHost, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.TryFinallyDelete(Uri, Action{Uri}, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_TryFinallyDelete_Method()
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
  public void Uri_ToEnumerable_Methods()
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
  public void Uri_ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("uri");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriExtensions.ToAsyncEnumerable(null, 1).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("uri");
      AssertionExtensions.Should(() => LocalHost.ToAsyncEnumerable(0).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToUriBuilder(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToUriBuilder_Method()
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
  public void Uri_ToStream_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToStream(null, null, ("name", "value"))).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToStreamAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToMailMessage(Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToMailMessage_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToMailMessage(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytes(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToBytes_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToText(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToText_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToBytesAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToBytesAsync(null).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.ToTextAsync(Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytes(Uri, IEnumerable{byte}, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => LocalHost.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteText(Uri, string, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_WriteText_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => LocalHost.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteBytesAsync(Uri, IEnumerable{byte}, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => LocalHost.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => LocalHost.WriteBytesAsync(Enumerable.Empty<byte>(), null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteTextAsync(Uri, string, Encoding, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => UriExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => LocalHost.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => LocalHost.WriteTextAsync(string.Empty, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteTo(IEnumerable{byte}, Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => UriExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteTo(string, Uri, Encoding, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => UriExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteToAsync(IEnumerable{byte}, Uri, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => UriExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UriExtensions.WriteToAsync(string, Uri, Encoding, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => UriExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(LocalHost, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }
}