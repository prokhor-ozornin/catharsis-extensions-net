using System.Net;
using System.Net.Sockets;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="UdpClientExtensions"/>.</para>
/// </summary>
public sealed class UdpClientExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.IsEmpty(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WithTimeout(UdpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    using var udp = new UdpClient();

    AssertionExtensions.Should(() => udp.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => udp.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

    var receiveTimeout = udp.Client.ReceiveTimeout;
    var sendTimeout = udp.Client.SendTimeout;
    receiveTimeout.Should().Be(udp.Client.ReceiveTimeout).And.Be(0);
    sendTimeout.Should().Be(udp.Client.SendTimeout).And.Be(0);

    udp.WithTimeout(null).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be(receiveTimeout);
    udp.Client.SendTimeout.Should().Be(sendTimeout);

    var timespan = TimeSpan.FromMilliseconds(-1);
    udp.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be(0);
    udp.Client.SendTimeout.Should().Be(0);

    timespan = TimeSpan.Zero;
    udp.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be((int) timespan.TotalMilliseconds);
    udp.Client.SendTimeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.TryFinallyDisconnect(UdpClient, Action{UdpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToEnumerable(UdpClient, IPEndPoint, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToAsyncEnumerable(UdpClient, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToBytes(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToBytesAsync(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToText(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.ToTextAsync(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteBytes(UdpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteBytesAsync(UdpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteBytesAsync(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => Udp.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Udp.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteText(UdpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="UdpClientExtensions.WriteTextAsync(UdpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => Udp.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Udp.WriteTextAsync(string.Empty, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }
}