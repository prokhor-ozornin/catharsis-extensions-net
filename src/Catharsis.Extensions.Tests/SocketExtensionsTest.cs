using System.Net.Sockets;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SocketExtensions"/>.</para>
/// </summary>
public sealed class SocketExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SocketExtensions.WithTimeout(Socket, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((Socket) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("socket");

    using var socket = new Socket(SocketType.Stream, ProtocolType.IP);

    var receiveTimeout = socket.ReceiveTimeout;
    var sendTimeout = socket.SendTimeout;
    receiveTimeout.Should().Be(0);
    sendTimeout.Should().Be(0);

    socket.WithTimeout(null).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be(receiveTimeout);
    socket.SendTimeout.Should().Be(sendTimeout);

    var timespan = TimeSpan.FromMilliseconds(-1);
    socket.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be(0);
    socket.SendTimeout.Should().Be(0);

    timespan = TimeSpan.Zero;
    socket.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be((int) timespan.TotalMilliseconds);
    socket.SendTimeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SocketExtensions.TryFinallyDisconnect(Socket, Action{Socket})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((Socket) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("socket");
    AssertionExtensions.Should(() => new Socket(SocketType.Stream, ProtocolType.IP).TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    //using (var socket = new Socket(SocketType.Unknown, ProtocolType.Icmp))
    //{
    //  socket.Connect(new DnsEndPoint())
    //}
    throw new NotImplementedException();
  }
}