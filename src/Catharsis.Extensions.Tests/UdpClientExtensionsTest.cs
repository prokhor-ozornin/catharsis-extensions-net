using System.Net;
using System.Net.Sockets;
using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="UdpClientExtensions"/>.</para>
/// </summary>
public sealed class UdpClientExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.IsUnset(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, UdpClient client) => client.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.IsEmpty(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, UdpClient client) => client.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WithTimeout(UdpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

      using var client = new UdpClient();

      AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
      AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

      var receiveTimeout = client.Client.ReceiveTimeout;
      var sendTimeout = client.Client.SendTimeout;
      receiveTimeout.Should().Be(client.Client.ReceiveTimeout).And.Be(0);
      sendTimeout.Should().Be(client.Client.SendTimeout).And.Be(0);

      client.WithTimeout(null).Should().BeOfType<UdpClient>().And.BeSameAs(Attributes.Udp());
      client.Client.ReceiveTimeout.Should().Be(receiveTimeout);
      client.Client.SendTimeout.Should().Be(sendTimeout);

      var timespan = TimeSpan.FromMilliseconds(-1);
      client.WithTimeout(timespan).Should().BeOfType<UdpClient>().And.BeSameAs(Attributes.Udp());
      client.Client.ReceiveTimeout.Should().Be(0);
      client.Client.SendTimeout.Should().Be(0);

      timespan = TimeSpan.Zero;
      client.WithTimeout(timespan).Should().BeOfType<UdpClient>().And.BeSameAs(Attributes.Udp());
      client.Client.ReceiveTimeout.Should().Be((int) timespan.TotalMilliseconds);
      client.Client.SendTimeout.Should().Be((int) timespan.TotalMilliseconds);
    }

    return;

    static void Validate(UdpClient client, TimeSpan? timespan, TimeSpan? timeout)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.TryFinallyDisconnect(UdpClient, Action{UdpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDisconnect_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
      AssertionExtensions.Should(() => Attributes.Udp().TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToEnumerable(UdpClient, IPEndPoint, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToAsyncEnumerable(UdpClient, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToBytes(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, UdpClient client)
    {
      using (client)
      {
        client.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToBytesAsync(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, UdpClient client)
    {
      using (client)
      {
        client.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToText(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, UdpClient client, Encoding encoding = null)
    {
      using (client)
      {
        client.ToText(encoding).Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToTextAsync(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, UdpClient client, Encoding encoding = null)
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
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteBytes(UdpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
      AssertionExtensions.Should(() => Attributes.Udp().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client, byte[] bytes)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteBytesAsync(UdpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).WriteBytesAsync(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
      AssertionExtensions.Should(() => Attributes.Udp().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Attributes.Udp().WriteBytesAsync(Enumerable.Empty<byte>(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client, byte[] bytes)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteText(UdpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
      AssertionExtensions.Should(() => Attributes.Udp().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client, string text, Encoding encoding = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteTextAsync(UdpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
      AssertionExtensions.Should(() => Attributes.Udp().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Attributes.Udp().WriteTextAsync(string.Empty, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(UdpClient client, string text, Encoding encoding = null)
    {
      using (client)
      {

      }
    }
  }
}