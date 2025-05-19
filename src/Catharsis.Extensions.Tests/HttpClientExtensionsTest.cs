using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="HttpClientExtensions"/>.</para>
/// </summary>
public sealed class HttpClientExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WithTimeout(HttpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((HttpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("client");

    using var client = new HttpClient();

    AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.Zero)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

    var timeout = client.Timeout;
    timeout.Should().BeGreaterThan(TimeSpan.Zero);

    client.WithTimeout(null).Should().BeOfType<HttpClient>().And.BeSameAs(client);
    client.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.FromTicks(1);
    client.WithTimeout(timespan).Should().BeOfType<HttpClient>().And.BeSameAs(client);
    client.Timeout.Should().Be(timespan);

    return;

    static void Test(HttpClient client, TimeSpan? timeout)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="HttpClientExtensions.WithHeaders(HttpClient, IEnumerable{ValueTuple{string, object}})"/></description></item>
  ///     <item><description><see cref="HttpClientExtensions.WithHeaders(HttpClient, ValueTuple{string, object}[])"/></description></item>
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
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().WithHeaders((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Should().BeEmpty();

        client.WithHeaders().Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().BeEmpty();

        client.WithHeaders(headerUserAgent).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);

        client.WithHeaders(headerUserAgent, headerConnection).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().HaveCount(2);
        client.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().HaveCount(2).And.AllBeEquivalentTo(headerUserAgent.Value);
        client.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }

      static void Test(HttpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().WithHeaders(((string Name, object Value)[]) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Should().BeEmpty();

        client.WithHeaders().Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().BeEmpty();

        client.WithHeaders(headerUserAgent).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);

        client.WithHeaders(headerUserAgent, headerConnection).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().HaveCount(2);
        client.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().HaveCount(2).And.AllBeEquivalentTo(headerUserAgent.Value);
        client.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }

      static void Test(HttpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WithHeaders(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().WithHeaders((IReadOnlyDictionary<string, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("headers");

      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Should().BeEmpty();

        var headers = new Dictionary<string, object>();
        client.WithHeaders(headers).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().BeEmpty();

        headers = new Dictionary<string, object> {{headerUserAgent.Name, headerUserAgent.Value}, { headerConnection.Name, headerConnection.Value}};
        client.WithHeaders(headers).Should().BeOfType<HttpClient>().And.BeSameAs(client);
        client.DefaultRequestHeaders.Should().HaveCount(2);
        client.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);
        client.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }

      static void Test(HttpClient client)
      {
        using (client)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteHead(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteHead_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteHead(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecuteHead(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteHeadAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteHeadAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteHeadAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteHeadAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteHeadAsync("localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteGet(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteGet_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteGet(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecuteGet(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteGetAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteGetAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteGetAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteGetAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteGetAsync("localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePost(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePost_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePost(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecutePost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePostAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePostAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePostAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePostAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePostAsync("localhost".ToUri(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePut(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePut_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePut(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecutePut(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePutAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePutAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePutAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePutAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePutAsync("localhost".ToUri(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteDelete(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteDelete_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteDelete(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecuteDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecuteDeleteAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteDeleteAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecuteDeleteAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteDeleteAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecuteDeleteAsync("localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePatch(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePatch_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePatch(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ExecutePatch(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ExecutePatchAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecutePatchAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ExecutePatchAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePatchAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ExecutePatchAsync("localhost".ToUri(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri, HttpContent content = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteBytes(HttpClient, IEnumerable{byte}, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WriteBytes(null, [], "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().WriteBytes(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => this.Http().WriteBytes([], null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, byte[] bytes, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteBytesAsync(HttpClient, IEnumerable{byte}, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WriteBytesAsync(null, [], "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().WriteBytesAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => this.Http().WriteBytesAsync([], null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().WriteBytesAsync([], "localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, byte[] bytes, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteText(HttpClient, string, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WriteText(null, string.Empty, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().WriteText(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => this.Http().WriteText(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, string text, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.WriteTextAsync(HttpClient, string, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.WriteTextAsync(null, string.Empty, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().WriteTextAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => this.Http().WriteTextAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().WriteTextAsync(string.Empty, "localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, string text, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToBytes(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToBytes(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToBytesAsync(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToBytesAsync(null, "localhost".ToUri()).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ToBytesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToText(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToText(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToTextAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToTextAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ToTextAsync("localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToStream(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToStream(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => this.Http().ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="HttpClientExtensions.ToStreamAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => HttpClientExtensions.ToStreamAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => this.Http().ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => this.Http().ToStreamAsync("localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();

      throw new NotImplementedException();
    }

    return;

    static void Test(HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }
}