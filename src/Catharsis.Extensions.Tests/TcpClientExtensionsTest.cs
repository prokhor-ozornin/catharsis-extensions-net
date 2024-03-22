using System.Net.Sockets;
using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TcpClientExtensions"/>.</para>
/// </summary>
public sealed class TcpClientExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.IsEmpty(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, bool result) => client.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WithTimeout(TcpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    using var tcp = new TcpClient();

    var receiveTimeout = tcp.ReceiveTimeout;
    var sendTimeout = tcp.SendTimeout;
    receiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(0);
    sendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(0);

    tcp.WithTimeout(null).Should().NotBeNull().And.BeSameAs(Attributes.Tcp());
    tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(receiveTimeout);
    tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(sendTimeout);

    foreach (var timespan in new[] {TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue})
    {
      tcp.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(Attributes.Tcp());
      tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be((int) timespan.TotalMilliseconds);
      tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be((int) timespan.TotalMilliseconds);
    }

    return;

    static void Validate(TcpClient client, TimeSpan? timeout)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.TryFinallyDisconnect(TcpClient, Action{TcpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Attributes.Tcp().TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TcpClientExtensions.ToEnumerable(TcpClient, bool)"/></description></item>
  ///     <item><description><see cref="TcpClientExtensions.ToEnumerable(TcpClient, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    }

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TcpClientExtensions.ToAsyncEnumerable(TcpClient, bool)"/></description></item>
  ///     <item><description><see cref="TcpClientExtensions.ToAsyncEnumerable(TcpClient, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    }

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToBytes(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, byte[] result) => client.ToBytes().Should().NotBeNull().And.NotBeSameAs(client.ToBytes()).And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToBytesAsync(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, byte[] result) => client.ToBytesAsync().ToArray().Should().NotBeNull().And.NotBeSameAs(client.ToBytesAsync().ToArray()).And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToText(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, string result, Encoding encoding = null) => client.ToText(encoding).Should().NotBeNull().And.NotBeSameAs(client.ToText(encoding)).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToTextAsync(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, string result, Encoding encoding = null) => client.ToTextAsync(encoding).Await().Should().NotBeNull().And.NotBeSameAs(client.ToTextAsync(encoding).Await()).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteBytes(TcpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Attributes.Tcp().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, byte[] bytes)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteBytesAsync(TcpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteBytesAsync(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => Attributes.Tcp().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Attributes.Tcp().WriteBytesAsync(Enumerable.Empty<byte>(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, byte[] bytes)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteText(TcpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Attributes.Tcp().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, string text, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteTextAsync(TcpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => Attributes.Tcp().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Attributes.Tcp().WriteTextAsync(string.Empty, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();

    return;

    static void Validate(TcpClient client, string text, Encoding encoding = null)
    {
    }
  }
}