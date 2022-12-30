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
  public static HttpClient WithHeaders(this HttpClient http, IEnumerable<(string Name, object Value)> headers)
  {
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
  public static HttpClient WithHeaders(this HttpClient http, IDictionary<string, object> headers)
  {
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
  public static async Task<HttpContent> Head(this HttpClient http, Uri uri, CancellationToken cancellation = default)
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
  public static async Task<HttpContent> Get(this HttpClient http, Uri uri, CancellationToken cancellation = default)
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
  public static async Task<HttpContent> Post(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
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
  public static async Task<HttpContent> Put(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
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
  public static async Task<HttpContent> Patch(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
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
  public static async Task<HttpContent> Delete(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    var response = await http.DeleteAsync(uri, cancellation);
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
  public static IEnumerable<byte[]> ToEnumerable(this UdpClient udp, IPEndPoint endpoint = null) => new UdpClientEnumerable(udp, endpoint);

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
  /// <param name="address"></param>
  /// <returns></returns>
  public static IPHostEntry ToHost(this IPAddress address) => new() { AddressList = new[] { address }, Aliases = Array.Empty<string>() };

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
  /// <param name="content"></param>
  /// <returns></returns>
  public static async Task<Stream> ToStream(this HttpContent content) => await content.ReadAsStreamAsync();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this IPAddress address) => address.GetAddressBytes();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this PhysicalAddress address) => address.GetAddressBytes();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToBytes(this HttpClient http, Uri uri, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    #if NET6_0
      var result = http.GetByteArrayAsync(uri, cancellation);
    #else
      var result = http.GetByteArrayAsync(uri);
    #endif

    foreach (var value in await result)
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
  public static async IAsyncEnumerable<byte> ToBytes(this HttpContent content, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    #if NET6_0
      var result = content.ReadAsByteArrayAsync(cancellation);
    #else
      var result = content.ReadAsByteArrayAsync();
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
  public static async IAsyncEnumerable<byte> ToBytes(this TcpClient tcp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    await foreach (var element in tcp.GetStream().ToBytes(cancellation))
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
  public static async IAsyncEnumerable<byte> ToBytes(this UdpClient udp, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
#if NET6_0
    var result = await udp.ReceiveAsync(cancellation);
#else
    var result = await udp.ReceiveAsync();
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this HttpClient http, Uri uri, CancellationToken cancellation = default)
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
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this HttpContent content, CancellationToken cancellation = default)
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
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this TcpClient tcp, Encoding encoding = null, CancellationToken cancellation = default) => (await tcp.ToBytes(cancellation).ToArray()).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this UdpClient udp, Encoding encoding = null, CancellationToken cancellation = default) => (await udp.ToBytes(cancellation).ToArray()).ToText(encoding);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="bytes"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> WriteBytes(this HttpClient http, IEnumerable<byte> bytes, Uri uri, CancellationToken cancellation = default)
  {
    using var content = new ByteArrayContent(bytes.AsArray());
    
    return await http.PostAsync(uri, content, cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> WriteBytes(this TcpClient tcp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await tcp.GetStream().WriteBytes(bytes, cancellation);
    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<UdpClient> WriteBytes(this UdpClient udp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    #if NET6_0
      await udp.SendAsync(bytes.ToReadOnlyMemory(), cancellation);
    #else
    var array = bytes.AsArray();
    await udp.SendAsync(array, array.Length);
    #endif

    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="text"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpContent> WriteText(this HttpClient http, string text, Uri uri, CancellationToken cancellation = default)
  {
    using var content = new StringContent(text);
    return await http.Post(uri, content, cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TcpClient> WriteText(this TcpClient tcp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await tcp.WriteBytes(text.ToBytes(encoding), cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<UdpClient> WriteText(this UdpClient udp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await udp.WriteBytes(text.ToBytes(encoding), cancellation);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> WriteTo(this IEnumerable<byte> bytes, HttpClient http, Uri uri, CancellationToken cancellation = default) => await http.WriteBytes(bytes, uri, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="tcp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, TcpClient tcp, CancellationToken cancellation = default)
  {
    await tcp.WriteBytes(bytes, cancellation);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="udp"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, UdpClient udp, CancellationToken cancellation = default)
  {
    await udp.WriteBytes(bytes, cancellation);
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
  public static async Task<HttpContent> WriteTo(this string text, HttpClient http, Uri uri, CancellationToken cancellation = default) => await http.WriteText(text, uri, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, TcpClient tcp, Encoding encoding = null, CancellationToken cancellation = default)
  {
    await tcp.WriteText(text, encoding, cancellation);
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
  public static async Task<string> WriteTo(this string text, UdpClient udp, Encoding encoding = null, CancellationToken cancellation = default)
  {
    await udp.WriteText(text, encoding, cancellation);
    return text;
  }

  private sealed class UdpClientEnumerable : IEnumerable<byte[]>
  {
    private readonly UdpClient client;
    private IPEndPoint endpoint;

    public UdpClientEnumerable(UdpClient client,IPEndPoint endpoint)
    {
      this.client = client;
      this.endpoint = endpoint;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private readonly UdpClientEnumerable parent;

      public Enumerator(UdpClientEnumerable parent) => this.parent = parent;

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

    public UdpClientAsyncEnumerable(UdpClient client) => this.client = client;

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this, cancellation);

    private sealed class Enumerator : IAsyncEnumerator<byte[]>
    {
      private readonly UdpClientAsyncEnumerable parent;
      private readonly CancellationToken cancellation;

      public Enumerator(UdpClientAsyncEnumerable parent, CancellationToken cancellation)
      {
        this.parent = parent;
        this.cancellation = cancellation;
      }

      public ValueTask DisposeAsync() => default;

      public byte[] Current { get; private set; } = Array.Empty<byte>();

      public async ValueTask<bool> MoveNextAsync()
      {
#if NET6_0
        var buffer = (await parent.client.ReceiveAsync(cancellation)).Buffer;
#else
        var buffer = (await parent.client.ReceiveAsync()).Buffer;
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