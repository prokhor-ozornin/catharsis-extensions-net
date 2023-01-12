﻿using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="NetworkExtensions"/>.</para>
/// </summary>
public sealed class NetworkExtensionsTest : UnitTest
{
  private HttpClient Http { get; } = new();
  private TcpClient Tcp { get; } = new();
  private UdpClient Udp { get; } = new();

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailable(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    IPAddress.Loopback.IsAvailable().Should().BeTrue();
    IPAddress.Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();

    IPAddress.IPv6Loopback.IsAvailable().Should().BeTrue();
    IPAddress.IPv6Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailableAsync(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("address").Await();

    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    IPAddress.Loopback.IsAvailableAsync().Await().Should().BeTrue();
    IPAddress.Loopback.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    IPAddress.IPv6Loopback.IsAvailableAsync().Await().Should().BeTrue();
    IPAddress.IPv6Loopback.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailable(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    new IPHostEntry().IsAvailable().Should().BeFalse();
    new IPHostEntry { HostName = string.Empty, AddressList = Array.Empty<IPAddress>() }.IsAvailable().Should().BeFalse();

    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();

    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailableAsync(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("host").Await();

    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    new IPHostEntry().IsAvailableAsync().Await().Should().BeFalse();
    new IPHostEntry { HostName = string.Empty, AddressList = Array.Empty<IPAddress>() }.IsAvailableAsync().Await().Should().BeFalse();
    
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailableAsync().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailableAsync().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    new IPHostEntry().IsEmpty().Should().BeTrue();
    new IPHostEntry { HostName = string.Empty, AddressList = Array.Empty<IPAddress>() }.IsEmpty().Should().BeTrue();
    new IPHostEntry { HostName = "localhost" }.IsEmpty().Should().BeFalse();
    new IPHostEntry { AddressList = new[] { IPAddress.Loopback } }.IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void Cookie_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((Cookie) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("cookie");

    new Cookie().IsEmpty().Should().BeTrue();
    new Cookie("name", null).IsEmpty().Should().BeTrue();
    new Cookie("name", string.Empty).IsEmpty().Should().BeTrue();
    new Cookie("name", " \t\r\n ").IsEmpty().Should().BeTrue();
    new Cookie("name", "value").IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Min(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_Min_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Min(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => IPAddress.Loopback.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
    AssertionExtensions.Should(() => IPAddress.Any.Min(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Min(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.None.Min(IPAddress.IPv6None)).ThrowExactly<SocketException>();
    
    var first = IPAddress.Loopback;
    var second = IPAddress.Loopback;
    first.Min(second).Should().BeSameAs(first);

    first = IPAddress.Parse("192.168.0.1");
    second = IPAddress.Parse("192.168.0.1");
    first.Min(second).Should().BeSameAs(first);

    first = IPAddress.Parse("10.0.0.1");
    second = IPAddress.Parse("10.0.0.2");
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Max(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_Max_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Max(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => IPAddress.Loopback.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
    AssertionExtensions.Should(() => IPAddress.Any.Max(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Max(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.None.Max(IPAddress.IPv6None)).ThrowExactly<SocketException>();

    var first = IPAddress.Loopback;
    var second = IPAddress.Loopback;
    first.Max(second).Should().BeSameAs(first);

    first = IPAddress.Parse("192.168.0.1");
    second = IPAddress.Parse("192.168.0.1");
    first.Max(second).Should().BeSameAs(first);

    first = IPAddress.Parse("10.0.0.1");
    second = IPAddress.Parse("10.0.0.2");
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(Http, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(Http, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(Http, IReadOnlyDictionary{string,object})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HttpClient_WithHeaders_Methods()
  {
    var headerUserAgent = (Name: "User-Agent", Value: "Mozilla/Firefox");
    var headerConnection = (Name: "Connection", Value: "Close");

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.WithHeaders(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
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
      AssertionExtensions.Should(() => NetworkExtensions.WithHeaders(null, Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
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
      AssertionExtensions.Should(() => NetworkExtensions.WithHeaders(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
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
  ///   <para>Performs testing of <see cref="NetworkExtensions.WithTimeout(HttpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WithTimeout_Method()
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
  ///   <para>Performs testing of <see cref="NetworkExtensions.WithTimeout(TcpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    using var tcp = new TcpClient();

    var receiveTimeout = tcp.ReceiveTimeout;
    var sendTimeout = tcp.SendTimeout;
    receiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(0);
    sendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(0);

    tcp.WithTimeout(null).Should().NotBeNull().And.BeSameAs(tcp);
    tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(receiveTimeout);
    tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(sendTimeout);

    foreach (var timespan in new[] {TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue})
    {
      tcp.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(tcp);
      tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be((int) timespan.TotalMilliseconds);
      tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be((int) timespan.TotalMilliseconds);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WithTimeout(UdpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WithTimeout_Method()
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
  ///   <para>Performs testing of <see cref="NetworkExtensions.WithTimeout(SmtpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SmtpClient_WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((SmtpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("smtp");

    using var smtp = new SmtpClient();

    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

    var timeout = smtp.Timeout;
    timeout.Should().Be(100000);

    smtp.WithTimeout(null).Should().NotBeNull().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.Zero;
    smtp.WithTimeout(timespan).Should().NotBeNull().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WithTimeout(Socket, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Socket_WithTimeout_Method()
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
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteHead(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteHead_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteHead(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteHead(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteHeadAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteHeadAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteHeadAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteHeadAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteHeadAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteGet(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteGet_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteGet(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteGet(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteGetAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteGetAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteGetAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteGetAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteGetAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePost(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePost_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePost(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePostAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePostAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePostAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePostAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePostAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePut(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePut_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePut(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePut(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePutAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePutAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePutAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePutAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePutAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePatch(HttpClient, Uri, HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePatch_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePatch(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecutePatch(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecutePatchAsync(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecutePatchAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecutePatchAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecutePatchAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecutePatchAsync(LocalHost, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteDelete(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteDelete_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteDelete(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ExecuteDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ExecuteDeleteAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ExecuteDeleteAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ExecuteDeleteAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ExecuteDeleteAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ExecuteDeleteAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(TcpClient, Action{TcpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Tcp.TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(UdpClient, Action{UdpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(Socket, Action{Socket})"/> method.</para>
  /// </summary>
  [Fact]
  public void Socket_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((Socket) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("socket");
    AssertionExtensions.Should(() => new Socket(SocketType.Stream, ProtocolType.IP).TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    //using (var socket = new Socket(SocketType.Unknown, ProtocolType.Icmp))
    //{
    //  socket.Connect(new DnsEndPoint())
    //}
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    foreach (var host in new[] { new IPHostEntry(), new IPHostEntry { AddressList = Array.Empty<IPAddress>() } })
    {
      host.ToEnumerable().Should().NotBeNull().And.BeSameAs(host.ToEnumerable()).And.Equal(host.AddressList ?? Array.Empty<IPAddress>());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(TcpClient, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(UdpClient, IPEndPoint, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(TcpClient, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(UdpClient, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToIpHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_ToIpHost_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None })
    {
      var host = address.ToIpHost();
      host.Should().NotBeNull().And.NotBeSameAs(address.ToIpHost());
      host.AddressList.Should().Equal(address);
      host.Aliases.Should().BeEmpty();
      host.HostName.Should().BeNull();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStreamAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStreamAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ToStreamAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStreamAsync(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToStreamAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStreamAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();
    AssertionExtensions.Should(() => new StringContent(string.Empty).ToStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None})
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.HaveCount(4).And.Equal(address.GetAddressBytes());
    }

    foreach (var address in new[] { IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None })
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.HaveCount(16).And.Equal(address.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddress_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((PhysicalAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] {PhysicalAddress.None, new PhysicalAddress(RandomBytes)})
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.Equal(address.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToBytes_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToBytes(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytesAsync(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToBytesAsync(null, LocalHost).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToBytesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    foreach (var bytes in new[] { Array.Empty<byte>(), RandomBytes })
    {
      using var content = new ByteArrayContent(bytes);

      content.ToBytes().Should().NotBeNull().And.NotBeSameAs(content.ToBytes()).And.Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytesAsync(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();

    foreach (var bytes in new[] { Array.Empty<byte>(), RandomBytes })
    {
      using var content = new ByteArrayContent(bytes);

      content.ToBytesAsync().Should().NotBeNull().And.NotBeSameAs(content.ToBytesAsync());
      content.ToBytesAsync().ToArray().Should().Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytesAsync(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytesAsync(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.ToText(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Http.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToTextAsync(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToTextAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.ToTextAsync(LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToText_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("content");

    foreach (var text in new[] { string.Empty, RandomString })
    {
      using var content = new StringContent(text);

      content.ToText().Should().NotBeNull().And.NotBeSameAs(content.ToText()).And.Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToTextAsync(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("content").Await();

    foreach (var text in new[] { string.Empty, RandomString })
    {
      using var content = new StringContent(text);

      AssertionExtensions.Should(() => content.ToTextAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      content.ToTextAsync().Should().NotBeNull().And.NotBeSameAs(content.ToTextAsync());
      content.ToTextAsync().Await().Should().Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToTextAsync(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToTextAsync(UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();

    throw new NotImplementedException();
  }




  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(HttpClient, IEnumerable{byte}, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteBytes(null, Enumerable.Empty<byte>(), LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.WriteBytes(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Http.WriteBytes(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(TcpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Tcp.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(UdpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytesAsync(HttpClient, IEnumerable{byte}, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>(), LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.WriteBytesAsync(Enumerable.Empty<byte>(), LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytesAsync(TcpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteBytesAsync(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => Tcp.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Tcp.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytesAsync(UdpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteBytesAsync(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => Udp.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Udp.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(HttpClient, string, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteText(null, string.Empty, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Http.WriteText(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => Http.WriteText(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(TcpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    AssertionExtensions.Should(() => Tcp.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(UdpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    AssertionExtensions.Should(() => Udp.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTextAsync(HttpClient, string, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteTextAsync(null, string.Empty, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Http.WriteTextAsync(string.Empty, LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTextAsync(TcpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => Tcp.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Tcp.WriteTextAsync(string.Empty, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTextAsync(UdpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => Udp.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Udp.WriteTextAsync(string.Empty, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Http, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Http, null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Tcp)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Tcp)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Udp)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Udp)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Http, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(null, LocalHost)).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Http, null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Tcp)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Tcp)).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Udp)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Udp)).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Http, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Http, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Http, LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(IEnumerable{byte}, TcpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Tcp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Tcp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Tcp, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(IEnumerable{byte}, UdpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Udp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Udp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Udp, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(string, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Http, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(null, LocalHost)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Http, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Http, LocalHost, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(string, TcpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Tcp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Tcp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Tcp, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteToAsync(string, UdpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Udp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Udp)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Udp, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    Http.Dispose();
    Tcp.Dispose();
    Udp.Dispose();
  }
}