using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="NetworkExtensions"/>.</para>
/// </summary>
public sealed class NetworkExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailable(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).IsAvailable()).ThrowExactlyAsync<ArgumentNullException>().Await();

    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable().Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.Zero).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.FromMilliseconds(-1)).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentOutOfRangeException>();

    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable().Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.Zero).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.FromMilliseconds(-1)).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentOutOfRangeException>();

    IPAddress.Loopback.IsAvailable().Await().Should().BeTrue();
    IPAddress.Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    IPAddress.IPv6Loopback.IsAvailable().Await().Should().BeTrue();
    IPAddress.IPv6Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsAvailable(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailable()).ThrowExactlyAsync<ArgumentNullException>().Await();

    AssertionExtensions.Should(() => IPAddress.Any.ToHost().IsAvailable().Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.Any.ToHost().IsAvailable(TimeSpan.Zero).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.Any.ToHost().IsAvailable(TimeSpan.FromMilliseconds(-1)).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentOutOfRangeException>();

    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToHost().IsAvailable().Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToHost().IsAvailable(TimeSpan.Zero).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentException>();
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToHost().IsAvailable(TimeSpan.FromMilliseconds(-1)).Await()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentOutOfRangeException>();

    new IPHostEntry().IsAvailable().Await().Should().BeFalse();
    new IPHostEntry { HostName = string.Empty, AddressList = Array.Empty<IPAddress>() }.IsAvailable().Await().Should().BeFalse();
    
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPHostEntry_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((TcpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void Cookie_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((Cookie) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => NetworkExtensions.Min(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Min(null)).ThrowExactly<ArgumentNullException>();
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
    AssertionExtensions.Should(() => NetworkExtensions.Max(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Max(null)).ThrowExactly<ArgumentNullException>();
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
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(HttpClient, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(HttpClient, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.WithHeaders(HttpClient, IDictionary{string,object})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HttpClient_WithHeaders_Methods()
  {
    var headerUserAgent = (Name: "User-Agent", Value: "Mozilla/Firefox");
    var headerConnection = (Name: "Connection", Value: "Close");

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.WithHeaders(null)).ThrowExactly<ArgumentNullException>();

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
      AssertionExtensions.Should(() => NetworkExtensions.WithHeaders(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new HttpClient().WithHeaders((IDictionary<string, object>) null)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((HttpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>();

    using var http = new HttpClient();

    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.Zero)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => http.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

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
    AssertionExtensions.Should(() => ((TcpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((UdpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>();

    using var udp = new UdpClient();

    AssertionExtensions.Should(() => udp.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => udp.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

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
    AssertionExtensions.Should(() => ((SmtpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>();

    using var smtp = new SmtpClient();

    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

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
    AssertionExtensions.Should(() => ((SmtpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>();

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
  ///   <para>Performs testing of <see cref="NetworkExtensions.Head(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Head_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Head(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Head(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Get(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Get_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Get(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Get(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Post(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Post_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Post(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Post(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Put(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Put_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Put(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Put(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Patch(HttpClient, Uri, HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Patch_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Patch(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Patch(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Delete(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Delete_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Delete(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().Delete(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(TcpClient, Action{TcpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new TcpClient().TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(UdpClient, Action{UdpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new UdpClient().TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.TryFinallyDisconnect(Socket, Action{Socket})"/> method.</para>
  /// </summary>
  [Fact]
  public void Socket_TryFinallyDisconnect_Method()
  {
    AssertionExtensions.Should(() => ((Socket) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new Socket(SocketType.Stream, ProtocolType.IP).TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((IPHostEntry) null).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    foreach (var host in new[] { new IPHostEntry(), new IPHostEntry { AddressList = Array.Empty<IPAddress>() } })
    {
      host.ToEnumerable().Should().NotBeNull().And.BeSameAs(host.ToEnumerable()).And.Equal(host.AddressList ?? Array.Empty<IPAddress>());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(UdpClient, IPEndPoint)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_ToHost_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToHost(null)).ThrowExactly<ArgumentNullException>();

    foreach (var address in new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None })
    {
      var host = address.ToHost();
      host.Should().NotBeNull().And.NotBeSameAs(address.ToHost());
      host.AddressList.Should().Equal(address);
      host.Aliases.Should().BeEmpty();
      host.HostName.Should().BeNull();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().ToStream(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((PhysicalAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>();

    foreach (var address in new[] {PhysicalAddress.None, new PhysicalAddress(RandomBytes)})
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.Equal(address.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.ToBytes(null, "https://localhost".ToUri()).ToArray()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().ToBytes(null).ToArray()).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToBytes().ToArray()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var bytes in new[] {Array.Empty<byte>(), RandomBytes})
    {
      using var content = new ByteArrayContent(bytes);

      AssertionExtensions.Should(() => content.ToBytes(Cancellation).ToArray()).ThrowExactlyAsync<TaskCanceledException>().Await();

      content.ToBytes().Should().NotBeNull().And.NotBeSameAs(content.ToBytes());
      content.ToBytes().ToArray().Await().Should().Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(TcpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>();

      var file = Assembly.GetExecutingAssembly().Location;
      file.ToUri().ToBytes().ToList().Await().Should().HaveCount((int) file.ToFile().Length);

      "https://ya.ru".ToUri().ToBytes().ToList().Await().Should().NotBeNullOrEmpty();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToBytes(UdpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>();

      var file = Assembly.GetExecutingAssembly().Location;
      file.ToUri().ToBytes().ToList().Await().Should().HaveCount((int) file.ToFile().Length);

      "https://ya.ru".ToUri().ToBytes().ToList().Await().Should().NotBeNullOrEmpty();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.ToText(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().ToText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToText_Method()
  {
    AssertionExtensions.Should(() => ((HttpContent) null).ToText()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var text in new[] { string.Empty, RandomString })
    {
      using var content = new StringContent(text);

      AssertionExtensions.Should(() => content.ToText(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      content.ToText().Should().NotBeNull().And.NotBeSameAs(content.ToText());
      content.ToText().Await().Should().Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(TcpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).ToText()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToText(UdpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).ToText()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(HttpClient, IEnumerable{byte}, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteBytes(null, Enumerable.Empty<byte>(), "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().WriteBytes(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().WriteBytes(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(TcpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new TcpClient().WriteBytes(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteBytes(UdpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteBytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new UdpClient().WriteBytes(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(HttpClient, string, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.WriteText(null, string.Empty, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().WriteText(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().WriteText(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(TcpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null).WriteText(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new TcpClient().WriteText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteText(UdpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_WriteText_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null).WriteText(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new UdpClient().WriteText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(new HttpClient(), "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(new HttpClient(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, TcpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(new TcpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(new TcpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(IEnumerable{byte}, UdpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(new UdpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(new UdpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(new HttpClient(), "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => string.Empty.WriteTo(null, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => string.Empty.WriteTo(new HttpClient(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, TcpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(new TcpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => string.Empty.WriteTo(new TcpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.WriteTo(string, UdpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(new UdpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => string.Empty.WriteTo(new UdpClient())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }
}