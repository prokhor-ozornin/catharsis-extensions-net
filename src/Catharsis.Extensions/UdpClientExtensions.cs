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
  /// <param name="client"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(UdpClient)"/>
  public static bool IsUnset(this UdpClient client) => client is null || client.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="UdpClient"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="client">UDP client instance for evaluation.</param>
  /// <returns>If the specified <paramref name="client"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(UdpClient)"/>
  public static bool IsEmpty(this UdpClient client) => client?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="timeout"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  public static UdpClient WithTimeout(this UdpClient client, TimeSpan? timeout)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));

    client.Client.WithTimeout(timeout);

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static UdpClient TryFinallyDisconnect(this UdpClient client, Action<UdpClient> action)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (action is null) throw new ArgumentNullException(nameof(action));

    client.Client.TryFinallyDisconnect(_ => action(client));

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="bytes"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync(UdpClient, IEnumerable{byte}, CancellationToken)"/>
  public static UdpClient WriteBytes(this UdpClient client, IEnumerable<byte> bytes)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    var datagram = bytes.AsArray();

    client.Send(datagram, datagram.Length);

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(UdpClient, IEnumerable{byte})"/>
  public static async Task<UdpClient> WriteBytesAsync(this UdpClient client, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    #if NET8_0_OR_GREATER
      await client.SendAsync(bytes.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);
#else
      var datagram = bytes.AsArray();
      await client.SendAsync(datagram, datagram.Length).ConfigureAwait(false);
#endif

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync(UdpClient, string, Encoding, CancellationToken)"/>
  public static UdpClient WriteText(this UdpClient client, string text, Encoding encoding = null) => client.WriteBytes(text.ToBytes(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(UdpClient, string, Encoding)"/>
  public static async Task<UdpClient> WriteTextAsync(this UdpClient client, string text, Encoding encoding = null, CancellationToken cancellation = default) => await client.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="endpoint"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(UdpClient, bool)"/>
  public static IEnumerable<byte[]> ToEnumerable(this UdpClient client, IPEndPoint endpoint = null, bool close = false) => client is not null ? new UdpClientEnumerable(client, endpoint, close) : throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(UdpClient, IPEndPoint, bool)"/>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this UdpClient client, bool close = false) => client is not null ? new UdpClientAsyncEnumerable(client, close) : throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(UdpClient)"/>
  public static IEnumerable<byte> ToBytes(this UdpClient client) => client?.ReceiveAsync().Result.Buffer ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(UdpClient)"/>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this UdpClient client)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));

    var result = await client.ReceiveAsync().ConfigureAwait(false);

    foreach (var element in result.Buffer)
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(UdpClient, Encoding)"/>
  public static string ToText(this UdpClient client, Encoding encoding = null) => client.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(UdpClient, Encoding)"/>
  public static async Task<string> ToTextAsync(this UdpClient client, Encoding encoding = null) => (await client.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

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
        #if NET8_0_OR_GREATER
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