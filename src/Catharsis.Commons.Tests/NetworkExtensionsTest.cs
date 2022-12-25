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
    AssertionExtensions.Should(() => ((IPAddress) null!).IsAvailable()).ThrowExactlyAsync<ArgumentNullException>().Await();

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
    //AssertionExtensions.Should(() => ((IPHostEntry) null!).IsAvailable()).ThrowExactlyAsync<ArgumentNullException>().Await();

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
    //AssertionExtensions.Should(() => ((IPHostEntry) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((TcpClient) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.IsEmpty(Cookie)"/> method.</para>
  /// </summary>
  [Fact]
  public void Cookie_IsEmpty_Method()
  {
    //AssertionExtensions.Should(() => ((Cookie) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
    //AssertionExtensions.Should(() => NetworkExtensions.Min(null!, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => IPAddress.Loopback.Min(null!)).ThrowExactly<ArgumentNullException>();
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
    //AssertionExtensions.Should(() => NetworkExtensions.Max(null!, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => IPAddress.Loopback.Max(null!)).ThrowExactly<ArgumentNullException>();
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
  ///     <item><description><see cref="NetworkExtensions.Headers(HttpClient, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.Headers(HttpClient, IDictionary{string, object?})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HttpClient_Headers_Methods()
  {
    var headerUserAgent = (Name: "User-Agent", Value: "Mozilla/Firefox");
    var headerConnection = (Name: "Connection", Value: "Close");

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => NetworkExtensions.Headers(null!)).ThrowExactly<ArgumentNullException>();

      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.Headers().Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().BeEmpty();

        http.Headers(headerUserAgent).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);

        http.Headers(headerUserAgent, headerConnection).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().HaveCount(2);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().HaveCount(2).And.AllBeEquivalentTo(headerUserAgent.Value);
        http.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => NetworkExtensions.Headers(null!, new Dictionary<string, object?>())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => new HttpClient().Headers((IDictionary<string, object?>) null!)).ThrowExactly<ArgumentNullException>();

      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Should().BeEmpty();

        var headers = new Dictionary<string, object?>();
        http.Headers(headers).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().BeEmpty();

        headers = new Dictionary<string, object?> {{headerUserAgent.Name, headerUserAgent.Value}, { headerConnection.Name, headerConnection.Value}};
        http.Headers(headers).Should().NotBeNull().And.BeSameAs(http);
        http.DefaultRequestHeaders.Should().HaveCount(2);
        http.DefaultRequestHeaders.GetValues(headerUserAgent.Name).Should().Equal(headerUserAgent.Value);
        http.DefaultRequestHeaders.GetValues(headerConnection.Name).Should().Equal(headerConnection.Value);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Timeout(HttpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_Timeout_Method()
  {
    //AssertionExtensions.Should(() => ((HttpClient) null!).Timeout(null)).ThrowExactly<ArgumentNullException>();

    using var http = new HttpClient();

    AssertionExtensions.Should(() => http.Timeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => http.Timeout(TimeSpan.Zero)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => http.Timeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

    var timeout = http.Timeout;
    timeout.Should().BeGreaterThan(TimeSpan.Zero);
      
    http.Timeout(null).Should().NotBeNull().And.BeSameAs(http);
    http.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.FromTicks(1);
    http.Timeout(timespan).Should().NotBeNull().And.BeSameAs(http);
    http.Timeout.Should().Be(timespan);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Timeout(TcpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_Timeout_Method()
  {
    //AssertionExtensions.Should(() => ((TcpClient) null!).Timeout(null)).ThrowExactly<ArgumentNullException>();

    using var tcp = new TcpClient();

    var receiveTimeout = tcp.ReceiveTimeout;
    var sendTimeout = tcp.SendTimeout;
    receiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(0);
    sendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(0);

    tcp.Timeout(null).Should().NotBeNull().And.BeSameAs(tcp);
    tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be(receiveTimeout);
    tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be(sendTimeout);

    foreach (var timespan in new[] {TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue})
    {
      tcp.Timeout(timespan).Should().NotBeNull().And.BeSameAs(tcp);
      tcp.ReceiveTimeout.Should().Be(tcp.Client.ReceiveTimeout).And.Be((int) timespan.TotalMilliseconds);
      tcp.SendTimeout.Should().Be(tcp.Client.SendTimeout).And.Be((int) timespan.TotalMilliseconds);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Timeout(UdpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_Timeout_Method()
  {
    //AssertionExtensions.Should(() => ((UdpClient) null!).Timeout(null)).ThrowExactly<ArgumentNullException>();

    using var udp = new UdpClient();

    AssertionExtensions.Should(() => udp.Timeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => udp.Timeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

    var receiveTimeout = udp.Client.ReceiveTimeout;
    var sendTimeout = udp.Client.SendTimeout;
    receiveTimeout.Should().Be(udp.Client.ReceiveTimeout).And.Be(0);
    sendTimeout.Should().Be(udp.Client.SendTimeout).And.Be(0);

    udp.Timeout(null).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be(receiveTimeout);
    udp.Client.SendTimeout.Should().Be(sendTimeout);

    var timespan = TimeSpan.FromMilliseconds(-1);
    udp.Timeout(timespan).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be(0);
    udp.Client.SendTimeout.Should().Be(0);

    timespan = TimeSpan.Zero;
    udp.Timeout(timespan).Should().NotBeNull().And.BeSameAs(udp);
    udp.Client.ReceiveTimeout.Should().Be((int) timespan.TotalMilliseconds);
    udp.Client.SendTimeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Timeout(SmtpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SmtpClient_Timeout_Method()
  {
    //AssertionExtensions.Should(() => ((SmtpClient) null!).Timeout(null)).ThrowExactly<ArgumentNullException>();

    using var smtp = new SmtpClient();

    AssertionExtensions.Should(() => smtp.Timeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => smtp.Timeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>();

    var timeout = smtp.Timeout;
    timeout.Should().Be(100000);

    smtp.Timeout(null).Should().NotBeNull().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.Zero;
    smtp.Timeout(timespan).Should().NotBeNull().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Timeout(SmtpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Socket_Timeout_Method()
  {
    //AssertionExtensions.Should(() => ((SmtpClient) null!).Timeout(null)).ThrowExactly<ArgumentNullException>();

    using var socket = new Socket(SocketType.Stream, ProtocolType.IP);

    var receiveTimeout = socket.ReceiveTimeout;
    var sendTimeout = socket.SendTimeout;
    receiveTimeout.Should().Be(0);
    sendTimeout.Should().Be(0);

    socket.Timeout(null).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be(receiveTimeout);
    socket.SendTimeout.Should().Be(sendTimeout);

    var timespan = TimeSpan.FromMilliseconds(-1);
    socket.Timeout(timespan).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be(0);
    socket.SendTimeout.Should().Be(0);

    timespan = TimeSpan.Zero;
    socket.Timeout(timespan).Should().NotBeNull().And.BeSameAs(socket);
    socket.ReceiveTimeout.Should().Be((int) timespan.TotalMilliseconds);
    socket.SendTimeout.Should().Be((int) timespan.TotalMilliseconds);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestHead(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestHead_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestHead(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestHead(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestGet(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestGet_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestGet(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestGet(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestPost(HttpClient, Uri, HttpContent?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestPost_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestPost(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestPost(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestPut(HttpClient, Uri, HttpContent?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestPut_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestPut(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestPut(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestPatch(HttpClient, Uri, HttpContent?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestPatch_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestPatch(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestPatch(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.RequestDelete(HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_RequestDelete_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.RequestDelete(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().RequestDelete(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Bytes(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_Bytes_Method()
  {
    //AssertionExtensions.Should(() => ((IPAddress) null!).Bytes()).ThrowExactly<ArgumentNullException>();

    foreach (var address in new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None})
    {
      address.Bytes().Should().NotBeNull().And.NotBeSameAs(address.Bytes()).And.HaveCount(4).And.Equal(address.GetAddressBytes());
    }

    foreach (var address in new[] { IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None })
    {
      address.Bytes().Should().NotBeNull().And.NotBeSameAs(address.Bytes()).And.HaveCount(16).And.Equal(address.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Bytes(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddress_Bytes_Method()
  {
    //AssertionExtensions.Should(() => ((PhysicalAddress) null!).Bytes()).ThrowExactly<ArgumentNullException>();

    foreach (var address in new[] {PhysicalAddress.None, new PhysicalAddress(RandomBytes)})
    {
      address.Bytes().Should().NotBeNull().And.NotBeSameAs(address.Bytes()).And.Equal(address.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NetworkExtensions.Bytes(HttpClient, Uri, CancellationToken)"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.Bytes(HttpClient, Uri, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HttpClient_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.Bytes(null!, "https://localhost".ToUri())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new HttpClient().Bytes(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.Bytes(null!, "https://localhost".ToUri(), Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().Bytes(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().Bytes("https://localhost".ToUri(), null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Bytes(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_Bytes_Method()
  {
    //AssertionExtensions.Should(() => ((HttpContent) null!).Bytes()).ThrowExactly<ArgumentNullException>();

    foreach (var bytes in new[] {Array.Empty<byte>(), RandomBytes})
    {
      using var content = new ByteArrayContent(bytes);

      AssertionExtensions.Should(() => content.Bytes(Cancellation).ToArray()).ThrowExactlyAsync<TaskCanceledException>().Await();

      content.Bytes().Should().NotBeNull().And.NotBeSameAs(content.Bytes());
      content.Bytes().ToArray().Await().Should().Equal(content.ReadAsByteArrayAsync().Await()).And.Equal(bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NetworkExtensions.Bytes(TcpClient, CancellationToken)"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.Bytes(TcpClient, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TcpClient_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null!).Bytes()).ThrowExactly<ArgumentNullException>();

      var file = Assembly.GetExecutingAssembly().Location;
      file.ToUri().Bytes().ToList().Await().Should().HaveCount((int) file.ToFile().Length);

      "https://ya.ru".ToUri().Bytes().ToList().Await().Should().NotBeNullOrEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new TcpClient().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NetworkExtensions.Bytes(UdpClient, CancellationToken)"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.Bytes(UdpClient, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void UdpClient_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null!).Bytes()).ThrowExactly<ArgumentNullException>();

      var file = Assembly.GetExecutingAssembly().Location;
      file.ToUri().Bytes().ToList().Await().Should().HaveCount((int) file.ToFile().Length);

      "https://ya.ru".ToUri().Bytes().ToList().Await().Should().NotBeNullOrEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((UdpClient) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new UdpClient().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NetworkExtensions.Text(HttpClient, Uri, CancellationToken)"/></description></item>
  ///     <item><description><see cref="NetworkExtensions.Text(HttpClient, Uri, string, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HttpClient_Text_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.Text(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NetworkExtensions.Text(null!, "https://localhost".ToUri(), string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().Text(null!, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new HttpClient().Text("https://localhost".ToUri(), null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Text(HttpContent, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_Text_Method()
  {
    //AssertionExtensions.Should(() => ((HttpContent) null!).Text()).ThrowExactlyAsync<ArgumentNullException>().Await();

    foreach (var text in new[] { string.Empty, RandomString })
    {
      using var content = new StringContent(text);

      AssertionExtensions.Should(() => content.Text(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      content.Text().Should().NotBeNull().And.NotBeSameAs(content.Text());
      content.Text().Await().Should().Be(content.ReadAsStringAsync().Await()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.Text(TcpClient, string, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_Text_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.Text(null!, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ((TcpClient) null!).Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.UseTemporarily(TcpClient, Action{TcpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_UseTemporarily_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null!).UseTemporarily(_ => {})).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new TcpClient().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.UseTemporarily(UdpClient, Action{UdpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_UseTemporarily_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new UdpClient().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.UseTemporarily(Socket, Action{Socket})"/> method.</para>
  /// </summary>
  [Fact]
  public void Socket_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((Socket) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new Socket(SocketType.Stream, ProtocolType.IP).UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

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
    //AssertionExtensions.Should(() => ((IPHostEntry) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    foreach (var host in new[] {new IPHostEntry(), new IPHostEntry {AddressList = Array.Empty<IPAddress>()}})
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
    AssertionExtensions.Should(() => ((TcpClient) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToEnumerable(UdpClient, IPEndPoint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void TcpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((TcpClient) null!).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToAsyncEnumerable(UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void UdpClient_ToAsyncEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((UdpClient) null!).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpClient_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => new HttpClient().ToStream(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToStream(HttpContent)"/> method.</para>
  /// </summary>
  [Fact]
  public void HttpContent_ToStream_Method()
  {
    AssertionExtensions.Should(() => NetworkExtensions.ToStream(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NetworkExtensions.ToHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void IPAddress_ToHost_Method()
  {
    //AssertionExtensions.Should(() => NetworkExtensions.ToHost(null!)).ThrowExactly<ArgumentNullException>();

    foreach (var address in new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None})
    {
      var host = address.ToHost();
      host.Should().NotBeNull().And.NotBeSameAs(address.ToHost());
      host.AddressList.Should().Equal(address);
      host.Aliases.Should().BeEmpty();
      host.HostName.Should().BeNull();
    }
  }
}