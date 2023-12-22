using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="UdpClient"/>
public static class UdpClientExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this UdpClient udp) => udp?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static UdpClient WithTimeout(this UdpClient udp, TimeSpan? timeout)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    udp.Client.WithTimeout(timeout);

    return udp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <param name="udp"></param>
  /// <param name="endpoint"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte[]> ToEnumerable(this UdpClient udp, IPEndPoint endpoint = null, bool close = false) => udp is not null ? new UdpClientEnumerable(udp, endpoint, close) : throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this UdpClient udp, bool close = false) => udp is not null ? new UdpClientAsyncEnumerable(udp, close) : throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this UdpClient udp) => udp?.ReceiveAsync().Result.Buffer ?? throw new ArgumentNullException(nameof(udp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this UdpClient udp)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));

    var result = await udp.ReceiveAsync().ConfigureAwait(false);

    foreach (var element in result.Buffer)
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this UdpClient udp, Encoding encoding = null) => udp.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this UdpClient udp, Encoding encoding = null) => (await udp.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <param name="udp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<UdpClient> WriteBytesAsync(this UdpClient udp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (udp is null) throw new ArgumentNullException(nameof(udp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    #if NET7_0_OR_GREATER
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
  /// <param name="udp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static UdpClient WriteText(this UdpClient udp, string text, Encoding encoding = null) => udp.WriteBytes(text.ToBytes(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="udp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<UdpClient> WriteTextAsync(this UdpClient udp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await udp.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);

  private sealed class UdpClientEnumerable : IEnumerable<byte[]>
  {
    private readonly UdpClient client;
    private IPEndPoint endpoint;
    private readonly bool close;

    public UdpClientEnumerable(UdpClient client, IPEndPoint endpoint, bool close)
    {
      this.client = client ?? throw new ArgumentNullException(nameof(client));
      this.endpoint = endpoint;
      this.close = close;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private readonly UdpClientEnumerable parent;

      public Enumerator(UdpClientEnumerable parent) => this.parent = parent ?? throw new ArgumentNullException(nameof(parent));

      public byte[] Current { get; private set; } = [];

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

      public void Dispose()
      {
        if (parent.close)
        {
          parent.client.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class UdpClientAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private readonly UdpClient client;
    private readonly bool close;

    public UdpClientAsyncEnumerable(UdpClient client, bool close)
    {
      this.client = client ?? throw new ArgumentNullException(nameof(client));
      this.close = close;
    }

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

      public async ValueTask DisposeAsync()
      {
        if (parent.close)
        {
          parent.client.Dispose();
        }

        await Task.Yield();
      }

      public byte[] Current { get; private set; } = [];

      public async ValueTask<bool> MoveNextAsync()
      {
        #if NET7_0_OR_GREATER
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