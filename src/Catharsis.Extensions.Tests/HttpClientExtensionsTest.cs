using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="HttpClientExtensions"/>.</para>
/// </summary>
public sealed class HttpClientExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="HttpClientExtensions.WithHeaders(HttpClient, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="HttpClientExtensions.WithHeaders(HttpClient, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="HttpClientExtensions.WithHeaders(HttpClient, IReadOnlyDictionary{string,object})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void WithHeaders_Methods()
  {
    var headerUserAgent = (Name: "User-Agent", Value: "Mozilla/Firefox");
    var headerConnection = (Name: "Connection", Value: "Close");

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Http.WithHeaders((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.WithHeaders().Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.WithHeaders(headerUserAgent).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);

        http.WithHeaders(headerUserAgent, headerConnection).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().HaveCount(2);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().HaveCount(2).And.AllBeEquivalentTo(headerUserAgent.Value);
        http.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Http.WithHeaders(((string Name, object Value)[]) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.WithHeaders().Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.WithHeaders(headerUserAgent).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);

        http.WithHeaders(headerUserAgent, headerConnection).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().HaveCount(2);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().HaveCount(2).And.AllBeEquivalentTo(headerUserAgent.Value);
        http.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Http.WithHeaders((IReadOnlyDictionary<string, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Should().BeEmpty();

        var headers = new Dictionary<string, object>();
        http.WithHeaders(headers).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().BeEmpty();

        headers = new Dictionary<string, object> {{headerUserAgent.Name, headerUserAgent.Value}, { headerConnection.Name, headerConnection.Value}};
        http.WithHeaders(headers).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().HaveCount(2);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);
        http.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WithTimeout(HttpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((HttpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("http");

    using var http = new HttpClient();

    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.Zero)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

    var timeout = http.Timeout;
    timeout.Should().BeGreaterThan(TimeSpan.Zero);
      
    http.WithTimeout(null).Should().NotBeNull().And.BeSameAs(http);
    http.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.FromTicks(1);
    http.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(http);
    http.Timeout.Should().Be(timespan);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteHead(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteHead_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteHead(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteHead(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteHeadAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteHeadAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteHeadAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteHeadAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteHeadAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteGet(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteGet_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteGet(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteGet(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteGetAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteGetAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteGetAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteGetAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteGetAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePost(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePost_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePost(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePostAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePostAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePostAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePostAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePostAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePut(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePut_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePut(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePut(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePutAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePutAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePutAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePutAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePutAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePatch(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePatch_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePatch(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePatch(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePatchAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePatchAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecutePatchAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePatchAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePatchAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteDelete(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteDelete_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteDelete(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteDeleteAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteDeleteAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ExecuteDeleteAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteDeleteAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteDeleteAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToStream(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ToStream(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToStreamAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ToStreamAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ToStreamAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToBytes(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ToBytes(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToBytesAsync(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ToBytesAsync(null, LocalHost).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToBytesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToText(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToText(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Http.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToTextAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.ToTextAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ToTextAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteBytes(HttpClient, IEnumerable{byte}, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.WriteBytes(null, Enumerable.Empty<byte>(), LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.WriteBytes(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Http.WriteBytes(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteBytesAsync(HttpClient, IEnumerable{byte}, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>(), LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(Enumerable.Empty<byte>(), LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteText(HttpClient, string, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.WriteText(null, string.Empty, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.WriteText(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => Http.WriteText(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteTextAsync(HttpClient, string, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => HttpClientExtensions.WriteTextAsync(null, string.Empty, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(string.Empty, LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }
}