using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

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
  public static async Task<bool> IsAvailable(this IPAddress address, TimeSpan? timeout = null)
  {
    using var ping = new Ping();

    var reply = await (timeout != null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address));

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static async Task<bool> IsAvailable(this IPHostEntry host, TimeSpan? timeout = null)
  {
    var address = host.HostName.IsEmpty() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address == null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = await (timeout != null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address));

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  public static bool IsEmpty(this IPHostEntry host) => host.HostName.IsEmpty() && (host.AddressList == null || host.AddressList.IsEmpty());

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
  public static bool IsEmpty(this Cookie cookie) => cookie.Name.IsEmpty() || cookie.Value.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IPAddress Min(this IPAddress left, IPAddress right) => left.Address <= right.Address ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IPAddress Max(this IPAddress left, IPAddress right) => left.Address >= right.Address ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static HttpClient Headers(this HttpClient http, params (string Name, object? Value)[] headers)
  {
    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Name, header.Value?.ToStringInvariant()));
    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static HttpClient Headers(this HttpClient http, IDictionary<string, object?> headers)
  {
    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Key, header.Value?.ToStringInvariant()));
    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static HttpClient Timeout(this HttpClient http, TimeSpan? timeout)
  {
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
  public static TcpClient Timeout(this TcpClient tcp, TimeSpan? timeout)
  {
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
  public static UdpClient Timeout(this UdpClient udp, TimeSpan? timeout)
  {
    udp.Client.Timeout(timeout);
    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="smtp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  public static SmtpClient Timeout(this SmtpClient smtp, TimeSpan? timeout)
  {
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
  public static Socket Timeout(this Socket socket, TimeSpan? timeout)
  {
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> RequestHead(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri), cancellation);
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
  public static async Task<HttpContent> RequestGet(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    var response = await http.GetAsync(uri, cancellation);
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
  public static async Task<HttpContent> RequestPost(this HttpClient http, Uri uri, HttpContent? content = null, CancellationToken cancellation = default)
  {
    var response = await http.PostAsync(uri, content, cancellation);
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
  public static async Task<HttpContent> RequestPut(this HttpClient http, Uri uri, HttpContent? content = null, CancellationToken cancellation = default)
  {
    var response = await http.PutAsync(uri, content, cancellation);
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
  public static async Task<HttpContent> RequestPatch(this HttpClient http, Uri uri, HttpContent? content = null, CancellationToken cancellation = default)
  {
    var response = await http.PatchAsync(uri, content, cancellation);
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
  public static async Task<HttpContent> RequestDelete(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    var response = await http.DeleteAsync(uri, cancellation);
    response.EnsureSuccessStatusCode();
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] Bytes(this IPAddress address) => address.GetAddressBytes();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] Bytes(this PhysicalAddress address) => address.GetAddressBytes();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> Bytes(this HttpClient http, Uri uri, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    #if NET6_0
    var bytes = http.GetByteArrayAsync(uri, cancellation);
    #else
      var bytes = http.GetByteArrayAsync(uri);
    #endif

    foreach (var value in await bytes)
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpClient> Bytes(this HttpClient http, Uri uri, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await http.PostAsync(uri, new ByteArrayContent(bytes.AsArray()), cancellation);

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> Bytes(this HttpContent content, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
#if NET6_0
    var bytes = await content.ReadAsByteArrayAsync(cancellation);
#else
    var bytes = await content.ReadAsByteArrayAsync();
#endif
    foreach (var value in bytes)
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
  public static async IAsyncEnumerable<byte> Bytes(this TcpClient tcp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    await foreach (var item in tcp.GetStream().Bytes(cancellation))
    {
      yield return item;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> Bytes(this TcpClient tcp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await tcp.GetStream().Bytes(bytes, cancellation);

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> Bytes(this UdpClient udp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
#if NET6_0
    var result = await udp.ReceiveAsync(cancellation);
#else
    var result = await udp.ReceiveAsync();
#endif

    foreach (var item in result.Buffer)
    {
      yield return item;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<UdpClient> Bytes(this UdpClient udp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
#if NET6_0
    await udp.SendAsync(bytes.ToReadOnlyMemory(), cancellation);
#else
    await udp.SendAsync(bytes.AsArray(), bytes.Count());
#endif
    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> Text(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
#if NET6_0
    return await http.GetStringAsync(uri, cancellation);
#else
      return await http.GetStringAsync(uri);
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpClient> Text(this HttpClient http, Uri uri, string text, CancellationToken cancellation = default)
  {
    await http.RequestPost(uri, new StringContent(text), cancellation);
    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> Text(this HttpContent content, CancellationToken cancellation = default)
  {
#if NET6_0
    return await content.ReadAsStringAsync(cancellation);
#else
    return await content.ReadAsStringAsync();
#endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> Text(this TcpClient tcp, string text, CancellationToken cancellation = default)
  {
    await tcp.GetStream().Text(text, cancellation);

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static TcpClient UseTemporarily(this TcpClient tcp, Action<TcpClient> action)
  {
    tcp.Client.UseTemporarily(_ => action(tcp));
    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static UdpClient UseTemporarily(this UdpClient udp, Action<UdpClient> action)
  {
    udp.Client.UseTemporarily(_ => action(udp));
    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="socket"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static Socket UseTemporarily(this Socket socket, Action<Socket> action) => socket.UseFinally(action, socket => socket.Disconnect(true));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  public static IEnumerable<IPAddress> ToEnumerable(this IPHostEntry host) => host.AddressList ?? Enumerable.Empty<IPAddress>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToEnumerable(this TcpClient tcp) => tcp.GetStream().ToEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this TcpClient tcp, int count) => tcp.GetStream().ToEnumerable(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="endpoint"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this UdpClient udp, IPEndPoint? endpoint = null) => new UdpClientEnumerable(udp, endpoint);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this TcpClient tcp) => tcp.GetStream().ToAsyncEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this TcpClient tcp, int count) => tcp.GetStream().ToAsyncEnumerable(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this UdpClient udp) => new UdpClientAsyncEnumerable(udp);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Stream> ToStream(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    #if NET6_0
      return await http.GetStreamAsync(uri, cancellation);
    #else
      return await http.GetStreamAsync(uri);
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static IPHostEntry ToHost(this IPAddress address) => new() { AddressList = new[] { address }, Aliases = Array.Empty<string>() };

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public static async Task<Stream> ToStream(this HttpContent content) => await content.ReadAsStreamAsync();

  private sealed class UdpClientEnumerable : IEnumerable<byte[]>
  {
    private readonly UdpClient client;
    private IPEndPoint? endpoint;

    public UdpClientEnumerable(UdpClient client,IPEndPoint? endpoint)
    {
      this.client = client;
      this.endpoint = endpoint;
    }

    public IEnumerator<byte[]> GetEnumerator() => new UdpClientEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class UdpClientEnumerator : IEnumerator<byte[]>
    {
      private UdpClientEnumerable Parent { get; }

      public UdpClientEnumerator(UdpClientEnumerable parent)
      {
        Parent = parent;
        Current = Array.Empty<byte>();
      }

      public byte[] Current { get; private set; }

      public bool MoveNext()
      {
        Current = Parent.client.Receive(ref Parent.endpoint);

        return Current.Length > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class UdpClientAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private readonly UdpClient client;

    public UdpClientAsyncEnumerable(UdpClient client) => this.client = client;

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new UdpClientAsyncEnumerator(this, cancellation);

    private sealed class UdpClientAsyncEnumerator : IAsyncEnumerator<byte[]>
    {
      private readonly UdpClientAsyncEnumerable parent;
      private readonly CancellationToken cancellation;

      public UdpClientAsyncEnumerator(UdpClientAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent;
        this.cancellation = cancellation;

        Current = Array.Empty<byte>();
      }

      public ValueTask DisposeAsync() => default;

      public byte[] Current { get; private set; }

      public async ValueTask<bool> MoveNextAsync()
      {
#if NET6_0
        var result = parent.client.ReceiveAsync(cancellation);
#else
        var result = parent.client.ReceiveAsync();
#endif
        Current = (await result).Buffer;
        return Current.Length > 0;
      }
    }
  }
}