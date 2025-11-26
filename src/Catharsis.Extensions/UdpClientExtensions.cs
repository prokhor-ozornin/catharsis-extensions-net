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
  /// <param name="client"></param>
  extension(UdpClient client)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => client is null || client.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="UdpClient"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="client"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => client?.ToEnumerable().IsEmpty ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    public byte[] Bytes => client.ToBytes().ToArray();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text => client.ToText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    public UdpClient WithTimeout(TimeSpan? timeout)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));

      client.Client.WithTimeout(timeout);

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public UdpClient TryFinallyDisconnect(Action<UdpClient> action)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (action is null) throw new ArgumentNullException(nameof(action));

      client.Client.TryFinallyDisconnect(_ => action(client));

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(UdpClient, IEnumerable{byte}, CancellationToken)"/>
    public UdpClient WriteBytes(IEnumerable<byte> bytes)
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
    /// <param name="bytes"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(UdpClient, IEnumerable{byte})"/>
    public async Task<UdpClient> WriteBytesAsync(IEnumerable<byte> bytes, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      cancellation.ThrowIfCancellationRequested();

      #if NET10_0_OR_GREATER
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
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(UdpClient, string, Encoding, CancellationToken)"/>
    public UdpClient WriteText(string text, Encoding encoding = null) => client.WriteBytes(text.ToBytes(encoding));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(UdpClient, string, Encoding)"/>
    public async Task<UdpClient> WriteTextAsync(string text, Encoding encoding = null, CancellationToken cancellation = default) => await client.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(UdpClient, bool)"/>
    public IEnumerable<byte[]> ToEnumerable(IPEndPoint endpoint = null, bool close = false) => client is not null ? new UdpClientEnumerable(client, endpoint, close) : throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(UdpClient, IPEndPoint, bool)"/>
    public IAsyncEnumerable<byte[]> ToAsyncEnumerable(bool close = false) => client is not null ? new UdpClientAsyncEnumerable(client, close) : throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(UdpClient)"/>
    public IEnumerable<byte> ToBytes() => client?.ReceiveAsync().Result.Buffer ?? throw new ArgumentNullException(nameof(client));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(UdpClient)"/>
    public async IAsyncEnumerable<byte> ToBytesAsync()
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
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(UdpClient, Encoding)"/>
    public string ToText(Encoding encoding = null) => client.ToBytes().AsArray().ToText(encoding);
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(UdpClient, Encoding)"/>
    public async Task<string> ToTextAsync(Encoding encoding = null) => (await client.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);
  }
  
  private sealed class UdpClientEnumerable : IEnumerable<byte[]>
  {
    private IPEndPoint endpoint;
    private UdpClient Client { get; }
    private bool Close { get; }

    public UdpClientEnumerable(UdpClient client, IPEndPoint endpoint, bool close)
    {
      Client = client ?? throw new ArgumentNullException(nameof(client));
      this.endpoint = endpoint;
      Close = close;
    }

    public IEnumerator<byte[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<byte[]>
    {
      private UdpClientEnumerable Parent { get; }

      public Enumerator(UdpClientEnumerable parent) => Parent = parent ?? throw new ArgumentNullException(nameof(parent));

      public byte[] Current { get; private set; } = [];

      public bool MoveNext()
      {
        var buffer = Parent.Client.Receive(ref Parent.endpoint);

        if (buffer.Length > 0)
        {
          Current = buffer;
        }

        return buffer.Length > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose()
      {
        if (Parent.Close)
        {
          Parent.Client.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class UdpClientAsyncEnumerable : IAsyncEnumerable<byte[]>
  {
    private UdpClient Client { get; }
    private bool Close { get; }

    public UdpClientAsyncEnumerable(UdpClient client, bool close)
    {
      Client = client ?? throw new ArgumentNullException(nameof(client));
      Close = close;
    }

    public IAsyncEnumerator<byte[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this, cancellation);

    private sealed class Enumerator : IAsyncEnumerator<byte[]>
    {
      private UdpClientAsyncEnumerable Parent { get; }
      private CancellationToken Cancellation { get; }

      public Enumerator(UdpClientAsyncEnumerable parent, CancellationToken cancellation)
      {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Cancellation = cancellation;
      }

      public async ValueTask DisposeAsync()
      {
        if (Parent.Close)
        {
          Parent.Client.Dispose();
        }

        await Task.Yield();
      }

      public byte[] Current { get; private set; } = [];

      public async ValueTask<bool> MoveNextAsync()
      {
        #if NET10_0_OR_GREATER
          var buffer = (await Parent.Client.ReceiveAsync(Cancellation).ConfigureAwait(false)).Buffer;
        #else
          var buffer = (await Parent.Client.ReceiveAsync().ConfigureAwait(false)).Buffer;
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