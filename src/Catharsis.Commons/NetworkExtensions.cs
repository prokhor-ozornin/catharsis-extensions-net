using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="IPAddress"/>
/// <seealso cref="IPHostEntry"/>
/// <seealso cref="PhysicalAddress"/>
/// <seealso cref="HttpClient"/>
/// <seealso cref="SmtpClient"/>
/// <seealso cref="TcpClient"/>
/// <seealso cref="UdpClient"/>
/// <seealso cref="Socket"/>
/// <seealso cref="Cookie"/>
public static class NetworkExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static bool IsAvailable(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = timeout != null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static bool IsAvailable(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsEmpty() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address == null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = timeout != null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static async Task<bool> IsAvailableAsync(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = await (timeout != null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static async Task<bool> IsAvailableAsync(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsEmpty() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address == null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = await (timeout != null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  public static bool IsEmpty(this IPHostEntry host) => host is not null ? host.HostName.IsEmpty() && (host.AddressList == null || host.AddressList.IsEmpty()) : throw new ArgumentNullException(nameof(host));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static bool IsEmpty(this TcpClient tcp) => tcp.ToEnumerable().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  public static bool IsEmpty(this UdpClient udp) => udp.ToEnumerable().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="cookie"></param>
  /// <returns></returns>
  public static bool IsEmpty(this Cookie cookie) => cookie is not null ? cookie.Name.IsEmpty() || cookie.Value.IsEmpty() : throw new ArgumentNullException(nameof(cookie));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IPAddress Min(this IPAddress left, IPAddress right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Address <= right.Address ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IPAddress Max(this IPAddress left, IPAddress right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Address >= right.Address ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static HttpClient WithHeaders(this HttpClient http, IEnumerable<(string Name, object Value)> headers)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (headers is null) throw new ArgumentNullException(nameof(headers));

    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Name, header.Value?.ToInvariantString()));

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static HttpClient WithHeaders(this HttpClient http, params (string Name, object Value)[] headers) => http.WithHeaders(headers as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static HttpClient WithHeaders(this HttpClient http, IReadOnlyDictionary<string, object> headers)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (headers is null) throw new ArgumentNullException(nameof(headers));

    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Key, header.Value?.ToInvariantString()));

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static HttpClient WithTimeout(this HttpClient http, TimeSpan? timeout)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));

    if (timeout != null)
    {
      http.Timeout = timeout.Value;
    }

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static TcpClient WithTimeout(this TcpClient tcp, TimeSpan? timeout)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    if (timeout != null)
    {
      tcp.ReceiveTimeout = (int) timeout.Value.TotalMilliseconds;
      tcp.SendTimeout = (int) timeout.Value.TotalMilliseconds;
    }

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static UdpClient WithTimeout(this UdpClient udp, TimeSpan? timeout)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    udp.Client.WithTimeout(timeout);

    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="smtp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static SmtpClient WithTimeout(this SmtpClient smtp, TimeSpan? timeout)
  {
    if (smtp is null) throw new ArgumentNullException(nameof(smtp));

    if (timeout != null)
    {
      smtp.Timeout = (int) timeout.Value.TotalMilliseconds;
    }

    return smtp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="socket"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static Socket WithTimeout(this Socket socket, TimeSpan? timeout)
  {
    if (socket is null) throw new ArgumentNullException(nameof(socket));

    if (timeout != null)
    {
      socket.ReceiveTimeout = (int) timeout.Value.TotalMilliseconds;
      socket.SendTimeout = (int) timeout.Value.TotalMilliseconds;
    }

    return socket;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent ExecuteHead(this HttpClient http, Uri uri) => http.ExecuteHeadAsync(uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent ExecuteGet(this HttpClient http, Uri uri) => http.ExecuteGetAsync(uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  public static HttpContent ExecutePost(this HttpClient http, Uri uri, HttpContent content = null) => http.ExecutePostAsync(uri, content).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  public static HttpContent ExecutePut(this HttpClient http, Uri uri, HttpContent content = null) => http.ExecutePutAsync(uri, content).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  public static HttpContent ExecutePatch(this HttpClient http, Uri uri, HttpContent content = null) => http.ExecutePatchAsync(uri, content).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent ExecuteDelete(this HttpClient http, Uri uri) => http.ExecuteDeleteAsync(uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecuteHeadAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri), cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecuteGetAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.GetAsync(uri, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecutePostAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.PostAsync(uri, content, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
   
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecutePutAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.PutAsync(uri, content, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecutePatchAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.PatchAsync(uri, content, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> ExecuteDeleteAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var response = await http.DeleteAsync(uri, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static TcpClient TryFinallyDisconnect(this TcpClient tcp, Action<TcpClient> action)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (action is null) throw new ArgumentNullException(nameof(action));

    tcp.Client.TryFinallyDisconnect(_ => action(tcp));

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static UdpClient TryFinallyDisconnect(this UdpClient udp, Action<UdpClient> action)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));
    if (action is null) throw new ArgumentNullException(nameof(action));

    udp.Client.TryFinallyDisconnect(_ => action(udp));

    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="socket"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static Socket TryFinallyDisconnect(this Socket socket, Action<Socket> action) => socket.TryFinally(action, socket => socket.Disconnect(true));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  public static IEnumerable<IPAddress> ToEnumerable(this IPHostEntry host) => host is not null ? host.AddressList ?? Enumerable.Empty<IPAddress>() : throw new ArgumentNullException(nameof(host));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToEnumerable(this TcpClient tcp) => tcp is not null ? tcp.GetStream().ToEnumerable() : throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this TcpClient tcp, int count) => tcp is not null ? tcp.GetStream().ToEnumerable(count) : throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="endpoint"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this UdpClient udp, IPEndPoint endpoint = null) => udp is not null ? new UdpClientEnumerable(udp, endpoint) : throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this TcpClient tcp) => tcp is not null ? tcp.GetStream().ToAsyncEnumerable() : throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this TcpClient tcp, int count) => tcp is not null ? tcp.GetStream().ToAsyncEnumerable(count) : throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this UdpClient udp) => udp is not null ? new UdpClientAsyncEnumerable(udp) : throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static IPHostEntry ToIpHost(this IPAddress address) => address is not null ? new IPHostEntry() { AddressList = new[] { address }, Aliases = Array.Empty<string>() } : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static Stream ToStream(this HttpClient http, Uri uri) => http.ToStreamAsync(uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public static Stream ToStream(this HttpContent content)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    #if NET6_0
      return content.ReadAsStream();
    #else
      return content.ReadAsStreamAsync().Result;
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Stream> ToStreamAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

#if NET6_0
      return await http.GetStreamAsync(uri, cancellation).ConfigureAwait(false);
#else
    return await http.GetStreamAsync(uri).ConfigureAwait(false);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Stream> ToStreamAsync(this HttpContent content, CancellationToken cancellation = default)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

#if NET6_0
    return await content.ReadAsStreamAsync(cancellation).ConfigureAwait(false);
#else
    return await content.ReadAsStreamAsync().ConfigureAwait(false);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this IPAddress address) => address is not null ? address.GetAddressBytes() : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this PhysicalAddress address) => address is not null ? address.GetAddressBytes() : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var stream = http.ToStream(uri);

    return stream.ToBytes();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this HttpContent content)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    using var stream = content.ToStream();

    return stream.ToBytes();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this TcpClient tcp) => tcp is not null ? tcp.GetStream().ToBytes() : throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this UdpClient udp) => udp is not null ? udp.ReceiveAsync().Result.Buffer : throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this HttpClient http, Uri uri, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await using var stream = await http.ToStreamAsync(uri, cancellation);

    var result = stream.ToBytesAsync(cancellation).ConfigureAwait(false);

    await foreach (var value in result)
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this HttpContent content, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

#if NET6_0
    var result = content.ReadAsByteArrayAsync(cancellation).ConfigureAwait(false);
#else
      var result = content.ReadAsByteArrayAsync().ConfigureAwait(false);
#endif

    foreach (var value in await result)
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this TcpClient tcp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    await foreach (var element in tcp.GetStream().ToBytesAsync(cancellation).ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this UdpClient udp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));

#if NET6_0
    var result = await udp.ReceiveAsync(cancellation).ConfigureAwait(false);
#else
    var result = await udp.ReceiveAsync().ConfigureAwait(false);
#endif

    foreach (var element in result.Buffer)
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static string ToText(this HttpClient http, Uri uri) => http.ToTextAsync(uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public static string ToText(this HttpContent content) => content.ToTextAsync().Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string ToText(this TcpClient tcp, Encoding encoding = null) => tcp.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string ToText(this UdpClient udp, Encoding encoding = null) => udp.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

#if NET6_0
      return await http.GetStringAsync(uri, cancellation).ConfigureAwait(false);
#else
      return await http.GetStringAsync(uri).ConfigureAwait(false);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this HttpContent content, CancellationToken cancellation = default)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

#if NET6_0
      return await content.ReadAsStringAsync(cancellation).ConfigureAwait(false);
#else
      return await content.ReadAsStringAsync().ConfigureAwait(false);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this TcpClient tcp, Encoding encoding = null, CancellationToken cancellation = default) => (await tcp.ToBytesAsync(cancellation).ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this UdpClient udp, Encoding encoding = null, CancellationToken cancellation = default) => (await udp.ToBytesAsync(cancellation).ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="bytes"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent WriteBytes(this HttpClient http, IEnumerable<byte> bytes, Uri uri) => http.WriteBytesAsync(bytes, uri).Result;
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static TcpClient WriteBytes(this TcpClient tcp, IEnumerable<byte> bytes)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    tcp.GetStream().WriteBytes(bytes);

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static UdpClient WriteBytes(this UdpClient udp, IEnumerable<byte> bytes)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    var datagram = bytes.AsArray();

    udp.Send(datagram, datagram.Length);
    
    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="bytes"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> WriteBytesAsync(this HttpClient http, IEnumerable<byte> bytes, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var content = new ByteArrayContent(bytes.AsArray());
    
    return await http.ExecutePostAsync(uri, content, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> WriteBytesAsync(this TcpClient tcp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    await tcp.GetStream().WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);
    
    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<UdpClient> WriteBytesAsync(this UdpClient udp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

#if NET6_0
      await udp.SendAsync(bytes.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);
#else
    var datagram = bytes.AsArray();
    await udp.SendAsync(datagram, datagram.Length).ConfigureAwait(false);
#endif

    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="text"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent WriteText(this HttpClient http, string text, Uri uri) => http.WriteTextAsync(text, uri).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static TcpClient WriteText(this TcpClient tcp, string text, Encoding encoding = null) => tcp.WriteBytes(text.ToBytes(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static UdpClient WriteText(this UdpClient udp, string text, Encoding encoding = null) => udp.WriteBytes(text.ToBytes(encoding));
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="text"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> WriteTextAsync(this HttpClient http, string text, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var content = new StringContent(text);

    return await ExecutePostAsync(http, uri, content, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> WriteTextAsync(this TcpClient tcp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await tcp.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<UdpClient> WriteTextAsync(this UdpClient udp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await udp.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent WriteTo(this IEnumerable<byte> bytes, HttpClient http, Uri uri) => http.WriteBytes(bytes, uri);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, TcpClient tcp)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    tcp.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="udp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, UdpClient udp)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    udp.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static HttpContent WriteTo(this string text, HttpClient http, Uri uri) => http.WriteText(text, uri);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, TcpClient tcp, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    tcp.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, UdpClient udp, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    udp.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> WriteToAsync(this IEnumerable<byte> bytes, HttpClient http, Uri uri, CancellationToken cancellation = default) => await http.WriteBytesAsync(bytes, uri, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="tcp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, TcpClient tcp, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    await tcp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="udp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, UdpClient udp, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    await udp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> WriteToAsync(this string text, HttpClient http, Uri uri, CancellationToken cancellation = default) => await http.WriteTextAsync(text, uri, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, TcpClient tcp, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    await tcp.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, UdpClient udp, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    await udp.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  private sealed class UdpClientEnumerable : IEnumerable<byte[]>
  {
    private readonly UdpClient client;
    private IPEndPoint endpoint;

    public UdpClientEnumerable(UdpClient client, IPEndPoint endpoint)
    {
      this.client = client ?? throw new ArgumentNullException(nameof(client));
      this.endpoint = endpoint;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private readonly UdpClientEnumerable parent;

      public Enumerator(UdpClientEnumerable parent) => this.parent = parent ?? throw new ArgumentNullException(nameof(parent));

      public byte[] Current { get; private set; } = Array.Empty<byte>();

      public bool MoveNext()
      {
        var buffer = parent.client.Receive(ref parent.endpoint);

        if (buffer.Length > 0)
        {
          Current = buffer;
        }

        return buffer.Length > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class UdpClientAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private readonly UdpClient client;

    public UdpClientAsyncEnumerable(UdpClient client) => this.client = client ?? throw new ArgumentNullException(nameof(client));

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this, cancellation);

    private sealed class Enumerator : IAsyncEnumerator<byte[]>
    {
      private readonly UdpClientAsyncEnumerable parent;
      private readonly CancellationToken cancellation;

      public Enumerator(UdpClientAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        this.cancellation = cancellation;
      }

      public ValueTask DisposeAsync() => default;

      public byte[] Current { get; private set; } = Array.Empty<byte>();

      public async ValueTask<bool> MoveNextAsync()
      {
#if NET6_0
        var buffer = (await parent.client.ReceiveAsync(cancellation).ConfigureAwait(false)).Buffer;
#else
        var buffer = (await parent.client.ReceiveAsync().ConfigureAwait(false)).Buffer;
#endif

        if (buffer.Length > 0)
        {
          Current = buffer;
        }

        return buffer.Length > 0;
      }
    }
  }
}