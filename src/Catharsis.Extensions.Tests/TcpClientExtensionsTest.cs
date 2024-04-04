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
  ///   <para>Performs testing of <see cref="TcpClientExtensions.IsUnset(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    throw new NotImplementedException();

    return;

    static void Validate(bool result, TcpClient client)
    {
      using (client)
      {
        client.IsUnset().Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.IsEmpty(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();

    return;

    static void Validate(bool result, TcpClient client)
    {
      using (client)
      {
        client.IsEmpty().Should().Be(result);
      }
    }
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

    tcp.WithTimeout(null).Should().BeOfType<TcpClient>().And.BeSameAs(Attributes.Tcp());
    tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(receiveTimeout);
    tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(sendTimeout);

    new[] { TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue }.ForEach(timespan =>
    {
      tcp.WithTimeout(timespan).Should().BeOfType<TcpClient>().And.BeSameAs(Attributes.Tcp());
      tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be((int) timespan.TotalMilliseconds);
      tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be((int) timespan.TotalMilliseconds);
    });

    return;

    static void Validate(TcpClient client, TimeSpan? timeout)
    {
      using (client)
      {

      }
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
      using (client)
      {

      }
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

      static void Validate(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

      static void Validate(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    throw new NotImplementedException();
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

      static void Validate(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

      static void Validate(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    throw new NotImplementedException();
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

    static void Validate(byte[] result, TcpClient client)
    {
      using (client)
      {
        client.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
      }
    }
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

    static void Validate(byte[] result, TcpClient client)
    {
      using (client)
      {
        client.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
      }
    }
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

    static void Validate(string result, TcpClient client, Encoding encoding = null)
    {
      using (client)
      {
        client.ToText(encoding).Should().BeOfType<string>().And.Be(result);
      }
    }
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

    static void Validate(string result, TcpClient client, Encoding encoding = null)
    {
      using (client)
      {
        var task = client.ToTextAsync(encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>().And.Be(result);
      }
    }
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
      using (client)
      {

      }
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
      using (client)
      {

      }
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
      using (client)
      {

      }
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
      using (client)
      {

      }
    }
  }
}